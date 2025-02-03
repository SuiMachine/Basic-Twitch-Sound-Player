using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace BasicTwitchSoundPlayer.Structs.Gemini
{
	[Serializable]
	public class GeminiContent
	{
		[Serializable]
		public class GenerationConfig
		{
			[JsonIgnore]
			public int TokenCount = 0;
			public float temperature = 1f;
			public float topK = 40;
			public float topP = 0.95f;
			public int maxOutputTokens = 8192;
			public string responseMimeType = "text/plain";
		}

		[JsonIgnore]
		[NonSerialized]
		[XmlIgnore] public string StoragePath;
		public List<GeminiMessage> contents;
		[XmlIgnore] public SafetySettingsCategory[] safetySettings;
		[XmlIgnore] public GeminiMessage systemInstruction;
		public GenerationConfig generationConfig;
	}

	public class SafetySettingsCategory
	{
		public string category;
		[JsonConverter(typeof(StringEnumConverter))]
		public AISafetySettingsValues threshold;

		public SafetySettingsCategory()
		{
			category = "";
			threshold = AISafetySettingsValues.BLOCK_ONLY_HIGH;
		}

		public SafetySettingsCategory(string category, AISafetySettingsValues threshold)
		{
			this.category = category;
			this.threshold = threshold;
		}
	}

	public enum AISafetySettingsValues
	{
		BLOCK_NONE,
		BLOCK_ONLY_HIGH, //Block few
		BLOCK_MEDIUM_AND_ABOVE, //Block some
		BLOCK_LOW_AND_ABOVE //Block Most
	}

	[Serializable]
	public class GeminiResponse
	{
		[Serializable]
		public class GeminiResponseCandidates
		{
			public GeminiMessage content;
			public string finishReason = "";
			public double avgLogprobs = 0;
		}

		[Serializable]
		public class UsageMetadata
		{
			public int promptTokenCount = 0;
			public int candidatesTokenCount = 0;
			public int totalTokenCount = 0;
		}

		public GeminiResponseCandidates[] candidates;
		public UsageMetadata usageMetadata;
		public string modelVersion;
	}
}
