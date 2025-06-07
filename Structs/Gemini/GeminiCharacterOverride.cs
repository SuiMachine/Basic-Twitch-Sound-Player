using BasicTwitchSoundPlayer.Extensions;
using SuiBotAI.Components.Other.Gemini;
using System;
using System.Text;
using System.Xml.Serialization;
using static SuiBotAI.Components.Other.Gemini.GeminiSafetySettingsCategory;

namespace BasicTwitchSoundPlayer.Structs.Gemini
{
	[Serializable]
	public class GeminiCharacterOverride
	{
		[NonSerialized][XmlIgnore] public string Path;
		[NonSerialized][XmlIgnore] public string Username;
		public string SystemInstruction { get; set; }
		public AISafetySettingsValues HARM_CATEGORY_HARASSMENT = AISafetySettingsValues.BLOCK_ONLY_HIGH;
		public AISafetySettingsValues HARM_CATEGORY_HATE_SPEECH = AISafetySettingsValues.BLOCK_ONLY_HIGH;
		public AISafetySettingsValues HARM_CATEGORY_SEXUALLY_EXPLICIT = AISafetySettingsValues.BLOCK_ONLY_HIGH;
		public AISafetySettingsValues HARM_CATEGORY_DANGEROUS_CONTENT = AISafetySettingsValues.BLOCK_ONLY_HIGH;
		public AISafetySettingsValues HARM_CATEGORY_CIVIC_INTEGRITY = AISafetySettingsValues.BLOCK_LOW_AND_ABOVE;

		public static string GetOverridePath(string username)
		{
			return System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "BasicTwitchSoundPlayer", "AI_Overrides", username + ".xml");
		}

		public static GeminiCharacterOverride GetOverride(string path)
		{
			var content = XML_Utils.Load<GeminiCharacterOverride>(path, null);
			if (content == null)
				return null;

			content.Path = path;
			content.Username = System.IO.Path.GetFileNameWithoutExtension(path);
			return content;
		}

		public void Save()
		{
			if (string.IsNullOrEmpty(Path))
				throw new Exception("Can't save! No path was set!");

			XML_Utils.Save(Path, this);
		}

		public GeminiSafetySettingsCategory[] GetSafetyOverrides()
		{
			return new GeminiSafetySettingsCategory[]
			{
				new GeminiSafetySettingsCategory("HARM_CATEGORY_HARASSMENT", HARM_CATEGORY_HARASSMENT),
				new GeminiSafetySettingsCategory("HARM_CATEGORY_HATE_SPEECH", HARM_CATEGORY_HATE_SPEECH),
				new GeminiSafetySettingsCategory("HARM_CATEGORY_SEXUALLY_EXPLICIT", HARM_CATEGORY_SEXUALLY_EXPLICIT),
				new GeminiSafetySettingsCategory("HARM_CATEGORY_DANGEROUS_CONTENT", HARM_CATEGORY_DANGEROUS_CONTENT),
				new GeminiSafetySettingsCategory("HARM_CATEGORY_CIVIC_INTEGRITY", HARM_CATEGORY_CIVIC_INTEGRITY),            
			};
		}

		public GeminiMessage GetFullInstruction(AIConfig aiConfig)
		{
			var sb = new StringBuilder();
			sb.Append(aiConfig.Instruction_Character);
			sb.AppendLine();
			sb.AppendLine(SystemInstruction);
			sb.AppendStreamInstructionPostfix(true, true);
			return new GeminiMessage()
			{
				parts = new GeminiResponseMessagePart[]
				{
					new GeminiResponseMessagePart()
					{
						text = sb.ToString(),
					}
				},
				role = Role.user,
			};
		}
	}
}
