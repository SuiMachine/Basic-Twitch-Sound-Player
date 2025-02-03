using System;

namespace BasicTwitchSoundPlayer.Structs.Gemini
{
	[Serializable]
	public class GeminiCharacterOverride
	{
		public string SystemInstruction { get; set; }
		public AISafetySettingsValues HARM_CATEGORY_HARASSMENT = AISafetySettingsValues.BLOCK_ONLY_HIGH;
		public AISafetySettingsValues HARM_CATEGORY_HATE_SPEECH = AISafetySettingsValues.BLOCK_ONLY_HIGH;
		public AISafetySettingsValues HARM_CATEGORY_SEXUALLY_EXPLICIT = AISafetySettingsValues.BLOCK_ONLY_HIGH;
		public AISafetySettingsValues HARM_CATEGORY_DANGEROUS_CONTENT = AISafetySettingsValues.BLOCK_ONLY_HIGH;
		public AISafetySettingsValues HARM_CATEGORY_CIVIC_INTEGRITY = AISafetySettingsValues.BLOCK_LOW_AND_ABOVE;
	}
}
