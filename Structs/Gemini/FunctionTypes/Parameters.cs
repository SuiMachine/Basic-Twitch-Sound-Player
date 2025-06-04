using Newtonsoft.Json;
using SuiBotAI.Components.Other.Gemini.FunctionTypes;
using System;
using System.Collections.Generic;

namespace BasicTwitchSoundPlayer.Structs.Gemini.FunctionTypes
{
	[Serializable]
	public class TimeOutParameters : GeminiProperty
	{
		public Parameter_Number duration_in_seconds;
		public Parameter_String text_response;

		public TimeOutParameters()
		{
			this.duration_in_seconds = new Parameter_Number();
			this.text_response = new Parameter_String();
		}

		public override List<string> GetRequiredFieldsNames() => new List<string>() { nameof(duration_in_seconds), nameof(text_response) };
	}

	[Serializable]
	public class BanParameters : GeminiProperty
	{
		public Parameter_String response;

		public BanParameters()
		{
			this.response = new Parameter_String();
		}

		public override List<string> GetRequiredFieldsNames() => new List<string>() { };
	}
}
