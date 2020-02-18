@echo off
::Microsoft sucks with its CMD/Batch garbage syntaxes

cls
set release_dir=%cd%
set dd=%date:~0,2%
set mm=%date:~3,2%
set yyyy=%date:~6,4%
set release_date=%yyyy%_%mm%_%dd%

7za a -tzip "Release_dir/btsp-release-%release_date%.zip" "BasicTwitchSoundPlayer.exe" "Gma.System.MouseKeyHook.dll" "Google.Apis.Auth.dll" "Google.Apis.Auth.PlatformServices.dll" "Google.Apis.Core.dll" "Google.Apis.dll" "Google.Apis.PlatformServices.dll" "Google.Apis.Sheets.v4.dll" "log4net.dll" "Meebey.SmartIrc4net.dll" "NAudio.dll" "NAudio.Vorbis.dll" "Newtonsoft.Json.dll" "NVorbis.dll" "Twitch_Reward_Images/"
pause
