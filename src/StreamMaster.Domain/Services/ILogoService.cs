﻿using StreamMaster.Domain.Configuration;
using StreamMaster.Domain.XmltvXml;

namespace StreamMaster.Domain.Services
{
    /// <summary>
    /// Provides methods for managing and caching logo files.
    /// </summary>
    public interface ILogoService
    {
        string GetLogoUrl(SMChannel smChannel);

        string GetLogoUrl(SMChannel smChannel, string baseUrl);

        string GetLogoUrl(XmltvChannel xmltvChannel);

        Task<(FileStream? fileStream, string? FileName, string? ContentType)> GetTVLogoAsync(string Source, CancellationToken cancellationToken);

        Task<(FileStream? fileStream, string? FileName, string? ContentType)> GetCustomLogoAsync(string Source, CancellationToken cancellationToken);

        Task<(FileStream? fileStream, string? FileName, string? ContentType)> GetProgramLogoAsync(string fileName, CancellationToken cancellationToken);

        Task<(FileStream? fileStream, string? FileName, string? ContentType)> GetLogoAsync(string fileName, CancellationToken cancellationToken);

        Task<(FileStream? fileStream, string? FileName, string? ContentType)> GetLogoForChannelAsync(int SMChannelId, CancellationToken cancellationToken);

        /// <summary>
        /// Adds a new logo based on the specified artwork URI and title.
        /// </summary>
        /// <param name="URL">The URI of the artwork.</param>
        /// <param name="title">The title associated with the logo.</param>
        void AddLogoToCache(string source, string title, SMFileTypes sMFileType);

        void AddLogoToCache(
          string source,
          string value,
          string title,
          SMFileTypes sMFileType,
          bool HashSource = false);

        /// <summary>
        /// Builds the logo cache using the current streams asynchronously.
        /// </summary>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The result contains a <see cref="DataResponse{Boolean}"/> indicating the success of the operation.</returns>
        Task<DataResponse<bool>> CacheSMStreamLogosAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Caches the logos for EPG Channels.
        /// </summary>
        Task<DataResponse<bool>> CacheEPGChannelLogosAsync(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Caches the logos for SM Channels.
        /// </summary>
        Task<DataResponse<bool>> CacheSMChannelLogosAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Clears all logos from the cache.
        /// </summary>
        void ClearLogos();

        ///// <summary>
        ///// Clears all TV logos from the cache.
        ///// </summary>
        //void ClearTvLogos();

        /// <summary>
        /// Retrieves the logo corresponding to the specified source.
        /// </summary>
        /// <param name="source">The source URL of the logo.</param>
        /// <returns>The <see cref="LogoFileDto"/> if found; otherwise, null.</returns>
        CustomLogoDto? GetLogoBySource(string source);

        /// <summary>
        /// Retrieves a list of logos of the specified file type.
        /// </summary>
        /// <returns>A list of <see cref="LogoFileDto"/> objects.</returns>
        List<CustomLogoDto> GetLogos();

        /// <summary>
        /// Retrieves a valid image path for the specified URL and file type.
        /// </summary>
        /// <param name="URL">The URL of the image.</param>
        /// <param name="fileType">The type of the file. If null, the file type is determined automatically.</param>
        /// <returns>The valid <see cref="ImagePath"/>, or null if not found.</returns>
        ImagePath? GetValidImagePath(string baseURL, SMFileTypes fileType, bool? checkExists = true);

        /// <summary>
        /// Reads the directory containing TV logos asynchronously.
        /// </summary>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The result contains a boolean indicating the success of the operation.</returns>
        Task<bool> ScanForTvLogosAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Removes logos associated with the specified M3U file ID.
        /// </summary>
        /// <param name="id">The ID of the M3U file.</param>
        void RemoveLogosByM3UFileId(int id);

        string AddCustomLogo(string Name, string Url);

        void RemoveCustomLogo(string Url);
    }
}