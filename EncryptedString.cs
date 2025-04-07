using System;
using System.IO;
using System.Security.Cryptography;
using System.Xml.Serialization;

namespace BasicTwitchSoundPlayer
{
	[Serializable]
	public class EncryptedString
	{
		private static string GetEncryptionLocationIV() => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BasicTwitchSoundPlayer", "EncryptionIV.bin");
		private static string GetEncryptionLocationKey() => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BasicTwitchSoundPlayer", "EncryptionKey.bin");

		private static Aes m_AES;
		private static ICryptoTransform m_Encryptor;
		private static ICryptoTransform m_Decryptor;


		[XmlAttribute] public string EncryptedStr;
		[NonSerialized] private string DecodedString = null;

		public static implicit operator EncryptedString(string stringToEncode)
		{
			PrepareEncryption();

			return new EncryptedString()
			{
				EncryptedStr = Encrypt(stringToEncode),
				DecodedString = stringToEncode,
			};
		}

		public static implicit operator string(EncryptedString str)
		{
			PrepareEncryption();
			if(str.DecodedString == null)
			{
				str.DecodedString = Decrypt(str.EncryptedStr);
			}
			return str.DecodedString;
		}

		private static string Encrypt(string plainText)
		{
			if (plainText == null || plainText.Length <= 0)
				return "";

			byte[] encrypted;

			using (MemoryStream msEncrypt = new MemoryStream())
			{
				using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, m_Encryptor, CryptoStreamMode.Write))
				{
					using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
					{
						swEncrypt.Write(plainText);
					}
				}

				encrypted = msEncrypt.ToArray();
			}

			return Convert.ToBase64String(encrypted);
		}


		static string Decrypt(string cipherText)
		{
			if (cipherText == null || cipherText.Length <= 0)
				return "";

			var cipherBytes = Convert.FromBase64String(cipherText);


			string plaintext = null;

			using (MemoryStream msDecrypt = new MemoryStream(cipherBytes))
			{
				using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, m_Decryptor, CryptoStreamMode.Read))
				{
					using (StreamReader srDecrypt = new StreamReader(csDecrypt))
					{
						plaintext = srDecrypt.ReadToEnd();
					}
				}
			}

			return plaintext;
		}

		private static void PrepareEncryption()
		{
			if (m_AES == null)
			{
				if (!File.Exists(GetEncryptionLocationIV()) || !File.Exists(GetEncryptionLocationKey()))
				{
					m_AES = Aes.Create();
					m_AES.GenerateIV();
					m_AES.GenerateKey();

					Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BasicTwitchSoundPlayer"));
					File.WriteAllBytes(GetEncryptionLocationIV(), m_AES.IV);
					File.WriteAllBytes(GetEncryptionLocationKey(), m_AES.Key);

					m_Encryptor = m_AES.CreateEncryptor(m_AES.Key, m_AES.IV);
					m_Decryptor = m_AES.CreateDecryptor(m_AES.Key, m_AES.IV);
				}
				else
				{
					m_AES = Aes.Create();
					m_AES.IV = File.ReadAllBytes(GetEncryptionLocationIV());
					m_AES.Key = File.ReadAllBytes(GetEncryptionLocationKey());

					m_Encryptor = m_AES.CreateEncryptor(m_AES.Key,m_AES.IV);
					m_Decryptor = m_AES.CreateDecryptor(m_AES.Key, m_AES.IV);
				}
			}
		}

		public override string ToString()
		{
			if (DecodedString == null)
			{
				PrepareEncryption();
				DecodedString = Decrypt(EncryptedStr);
			}
			return DecodedString;
		}
	}
}
