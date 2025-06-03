using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BasicTwitchSoundPlayer.Structs.Gemini.FunctionTypes
{
	[Serializable]
	public class ParametersContainer
	{
		public string type = "object"; //return type
		public GeminiProperty properties = null;
		public List<string> required = new List<string>();

		public ParametersContainer() { }
	}

	[Serializable]
	public abstract class GeminiProperty
	{
		public abstract List<string> GetRequiredFieldsNames();

		public class Parameter_String
		{
			public string type = "string";
		}

		public class Parameter_Number
		{
			public string type = "number";
		}
	}

	[Serializable]
	public class PurgeMessage : GeminiProperty
	{
		public Parameter_String username;

		public PurgeMessage()
		{
			username = new Parameter_String();
		}

		public override List<string> GetRequiredFieldsNames() => new List<string>() { };
	}

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
