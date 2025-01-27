using Newtonsoft.Json;
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

		public List<GeminiMessage> contents;
		[XmlIgnore] public GeminiMessage systemInstruction;
		public GenerationConfig generationConfig;
	}

	public enum AISafetySettingsValues
	{
		BlockNone,
		BlockFew,
		BlockSome,
		BlockMost
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
