﻿using BarRaider.SdTools;
using BarRaider.SdTools.Payloads;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace local.basictwitchsoundplayer.suimachine
{
	[PluginActionId("local.basictwitchsoundplayer.suimachine.dialaction")]
	public class DialsAction : EncoderBase
	{
		private class PluginSettings
		{
			public static PluginSettings CreateDefaultSettings()
			{
				PluginSettings instance = new PluginSettings();
				instance.OutputFileName = String.Empty;
				instance.InputString = String.Empty;
				return instance;
			}

			[FilenameProperty]
			[JsonProperty(PropertyName = "outputFileName")]
			public string OutputFileName { get; set; }

			[JsonProperty(PropertyName = "inputString")]
			public string InputString { get; set; }
		}

		#region Private Members

		private PluginSettings settings;

		#endregion
		public DialsAction(SDConnection connection, InitialPayload payload) : base(connection, payload)
		{
			if (payload.Settings == null || payload.Settings.Count == 0)
			{
				this.settings = PluginSettings.CreateDefaultSettings();
				SaveSettings();
			}
			else
			{
				this.settings = payload.Settings.ToObject<PluginSettings>();
			}
		}

		public override void Dispose()
		{
			Logger.Instance.LogMessage(TracingLevel.INFO, $"Destructor called");
		}

		public override void DialRotate(DialRotatePayload payload)
		{
			Logger.Instance.LogMessage(TracingLevel.INFO, $"Dial Rotated: {payload.Ticks}");
		}

		public override void DialDown(DialPayload payload)
		{
			Logger.Instance.LogMessage(TracingLevel.INFO, "Dial Pressed");
		}

		public override void DialUp(DialPayload payload)
		{
			Logger.Instance.LogMessage(TracingLevel.INFO, "Dial Released");
		}

		public override void TouchPress(TouchpadPressPayload payload)
		{
			Logger.Instance.LogMessage(TracingLevel.INFO, "TouchScreen Pressed");
		}

		public override void OnTick() { }

		public override void ReceivedSettings(ReceivedSettingsPayload payload)
		{
			Tools.AutoPopulateSettings(settings, payload.Settings);
			SaveSettings();
		}

		public override void ReceivedGlobalSettings(ReceivedGlobalSettingsPayload payload) { }

		#region Private Methods

		private Task SaveSettings()
		{
			return Connection.SetSettingsAsync(JObject.FromObject(settings));
		}

		#endregion
	}
}