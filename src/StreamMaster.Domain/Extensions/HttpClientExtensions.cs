﻿using System.Net;

namespace StreamMaster.Domain.Extensions;

public static class HttpClientExtensions
{
    public static async Task<HttpResponseMessage?> GetWithRedirectAsync(
        this HttpClient httpClient,
        string requestUri,
        int maxRetries = 5,
        CancellationToken cancellationToken = default)
    {
        int retryCount = 0;
        HttpResponseMessage? response = null;
        string redirectUrl = requestUri;

        while (retryCount <= maxRetries)
        {
            httpClient.DefaultRequestHeaders.Range = null;

            response = await httpClient.GetAsync(redirectUrl, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);

            if (response.StatusCode == HttpStatusCode.Redirect)
            {
                if (response.Headers.Location != null)
                {
                    redirectUrl = response.Headers.Location.AbsoluteUri;
                    retryCount++;
                }
                else
                {
                    break;
                }
            }
            else
            {
                break;
            }
        }

        return response;
    }
}