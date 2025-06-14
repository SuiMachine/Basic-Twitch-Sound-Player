using System;
using System.IO;
using System.Xml.Serialization;

namespace SSC
{
	public static class XML_Utils
	{
		public static T Load<T>(string filePath, T defaultVal)
		{
			string DirectoryPath = Directory.GetParent(filePath).FullName;

			if (!Directory.Exists(DirectoryPath))
			{
				Directory.CreateDirectory(DirectoryPath);
				return defaultVal;
			}

			if (!File.Exists(filePath))
				return defaultVal;

			FileStream fs = null;
			T result = defaultVal;
			try
			{
				XmlSerializer serializer = new XmlSerializer(typeof(T));
				fs = new FileStream(filePath, FileMode.Open);
				result = (T)serializer.Deserialize(fs);

			}
			catch (Exception ex)
			{
				MainForm.Instance.ThreadSafeAddPreviewText($"Error: {ex.Message}", LineType.TwitchSocketCommand);
				result = defaultVal;
			}
			finally
			{
				fs.Close();
			}
			return result;
		}

		public static void Save<T>(string filePath, T objectToSave)
		{
			string DirectoryPath = Directory.GetParent(filePath).FullName;

			if (!Directory.Exists(DirectoryPath))
				Directory.CreateDirectory(DirectoryPath);

			XmlSerializer serializer = new XmlSerializer(typeof(T));
			StreamWriter fw = new StreamWriter(filePath);
			serializer.Serialize(fw, objectToSave);
			fw.Close();
		}
	}

}
