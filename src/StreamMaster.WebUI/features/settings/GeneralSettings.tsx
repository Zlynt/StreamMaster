import { GetMessage } from '@lib/common/intl';
import { useSettingsContext } from '@lib/context/SettingsProvider';
import React from 'react';
import { BaseSettings } from './BaseSettings';
import { GetCheckBoxLine } from './components/GetCheckBoxLine';
import { GetInputNumberLine } from './components/GetInputNumberLine';
import { GetInputTextLine } from './components/GetInputTextLine';
import { GetPasswordLine } from './components/GetPasswordLine';

export function GeneralSettings(): React.ReactElement {
  const { currentSetting } = useSettingsContext();
  return (
    <BaseSettings title="GENERAL">
      <>
        {GetInputTextLine({ field: 'DeviceID' })}
        {/* {GetCheckBoxLine({ field: 'CleanURLs' })} */}
        {GetInputTextLine({ field: 'FFMPegExecutable' })}
        {GetInputTextLine({ field: 'FFProbeExecutable' })}
        {GetInputNumberLine({ field: 'DefaultPort', max: 65535, min: 1, showComma: false })}
        {GetInputNumberLine({ field: 'DefaultSSLPort', max: 65535, min: 1, showComma: false })}
        {GetInputNumberLine({ field: 'ReadTimeOutMs', max: 65535, min: 0 })}
        {GetInputNumberLine({ field: 'StreamStartTimeoutMs', max: 65535, min: 0 })}
        {GetCheckBoxLine({ field: 'EnableSSL' })}
        {currentSetting?.EnableSSL === true && (
          <>
            {GetInputTextLine({ field: 'SSLCertPath', warning: GetMessage('changesServiceRestart') })}
            {GetPasswordLine({
              field: 'SSLCertPassword',
              warning: GetMessage('changesServiceRestart')
            })}
          </>
        )}
        {/* {getCheckBoxLine({ currentSetting, field: 'EnablePrometheus', onChange })} */}
        {GetInputNumberLine({ field: 'MaxLogFiles' })}
        {GetInputNumberLine({ field: 'MaxLogFileSizeMB' })}
      </>
    </BaseSettings>
  );
}
