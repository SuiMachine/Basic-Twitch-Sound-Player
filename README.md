Basic Twitch Sound Player
=====================
Basic Sound player for Twitch written in C#.

Known issues / incomplete features
-------
  * Sound Database Editor doesn't actually store anything.
  * Add new entry dialog doesn't do anything for now.
  * Opening Color Settings doesn't do anything.
  * Obtaining New Login Data starts a browser and redirect the result the listener, but listener doesn't do anything for now.
  * Player officially only supports wav files for now.

Credits
-------
  * [SuicideMachine](http://twitch.tv/suicidemachine)
  
External libraries used
-------
  * [SmartIrc4Net](https://github.com/meebey/SmartIrc4net) for IRC connection.
  * [NAudio](https://github.com/naudio/NAudio) for reliable audio playback.
  * [Newtonsoft.Json](https://www.newtonsoft.com/json) for deserializing Json.  
