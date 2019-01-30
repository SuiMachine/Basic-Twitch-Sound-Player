Basic Twitch Sound Player
=====================
Basic Sound player for Twitch written in C#.

Known issues / incomplete features
-------
  * VSS may play out 2nd sound incorrectly, if the sound frequency or format is different than previously played sound.

Credits
-------
  * [SuicideMachine](http://twitch.tv/suicidemachine)
  
External libraries used
-------
  * [SmartIrc4Net](https://github.com/meebey/SmartIrc4net) for IRC connection.
  * [NAudio](https://github.com/naudio/NAudio) for reliable audio playback.
  * [NAudio.Vorbis](https://github.com/naudio/Vorbis) for Vorbis files playback.
  * [Newtonsoft.Json](https://www.newtonsoft.com/json) for deserializing Json.  
  * [globalmousekeyhook](https://github.com/gmamaladze/globalmousekeyhook) for hooking input
  * [Google Spreadsheet .NET](https://developers.google.com/sheets/api/quickstart/dotnet) for exporting sound database to Google Spreadsheets

