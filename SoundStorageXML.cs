using BasicTwitchSoundPlayer.Extensions;
using BasicTwitchSoundPlayer.Structs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace BasicTwitchSoundPlayer.SoundStorage
{
	class SoundStorageXML
	{
		private static readonly string varSounds = "Sounds";
		private static readonly string varRequirement = "Requirement";
		private static readonly string varDescription = "SoundDescription";
		private static readonly string varDateAdded = "DateAdded";


		public static List<Structs.SoundEntry> LoadSoundBase(string XmlPath)
		{
			XmlDocument doc = new XmlDocument();
			XmlNode ROOTNODE = null;
			List<SoundEntry> entries = new List<SoundEntry>();

			if (!File.Exists(XmlPath))
			{
				ROOTNODE = doc.Sui_GetNode("Entries");
				SaveSoundBase(XmlPath, entries);
			}
			else
			{
				doc.Load(XmlPath);
				foreach (XmlNode sound in doc["Entries"])
				{
					var newSoundEntry = doc.GetQuickSoundEntry(sound);
					if (newSoundEntry.GetIsProperEntry())
						entries.Add(newSoundEntry);
				}
			}
			return entries;
		}

		public static void SaveSoundBase(string XmlPath, List<Structs.SoundEntry> entries)
		{
			XmlDocument doc = new XmlDocument();
			XmlNode ROOTNODE = doc.Sui_GetNode("Entries");

			var parentDir = Directory.GetParent(XmlPath);
			if (!parentDir.Exists)
				parentDir.Create();

			foreach (var entry in entries)
			{
				if (entry.GetIsProperEntry())
				{
					var ChildNode = ROOTNODE.Sui_GetNode(doc, entry.GetCommand());
					ChildNode.Sui_SetAttributeValue(doc, varSounds, string.Join(";", entry.GetAllFiles()));
					ChildNode.Sui_SetAttributeValue(doc, varRequirement, entry.GetRequirement().ToString());
					ChildNode.Sui_SetAttributeValue(doc, varDescription, entry.GetDescription());
					ChildNode.Sui_SetAttributeValue(doc, varDateAdded, entry.GetDateAdded().ToString());
				}
			}

			doc.Save(XmlPath);
		}
	}

	class VSSStorageXML
	{
		private static readonly string varDescription = "Description";
		private static readonly string varSoundPath = "SoundPath";

		public static VSS.VSS_Entry_Group LoadVSSBase(string XmlPath)
		{
			XmlDocument doc = new XmlDocument();
			VSS.VSS_Entry_Group RootGroupNode = null;
			XmlNode ROOTNODE = null;

			if (!File.Exists(XmlPath))
			{
				RootGroupNode = new VSS.VSS_Entry_Group("Root Node", Keys.V);
				ROOTNODE = GetXMLNodeForVSS(doc, RootGroupNode);
				SaveVSSBase(XmlPath, RootGroupNode);
			}
			else
			{
				doc.Load(XmlPath);
				ROOTNODE = doc.FirstChild;
				RootGroupNode = (VSS.VSS_Entry_Group)GetVSSFromXMLNode(ROOTNODE);
			}
			return RootGroupNode;
		}


		public static void SaveVSSBase(string XmlPath, VSS.VSS_Entry rootVSSNode)
		{
			XmlDocument doc = new XmlDocument();
			XmlNode ROOTNODE = GetXMLNodeForVSS(doc, rootVSSNode);

			var parentDir = Directory.GetParent(XmlPath);
			if (!parentDir.Exists)
				parentDir.Create();

			doc.Save(XmlPath);
		}

		private static XmlNode GetXMLNodeForVSS(XmlDocument doc, VSS.VSS_Entry entry, VSS.VSS_Entry parent = null)
		{
			if (parent == null)
			{
				var node = doc.CreateElement(entry.Hotkey.ToString());
				var desc = doc.CreateAttribute(varDescription);
				desc.InnerText = entry.Description;
				node.Attributes.Append(desc);
				foreach (var child in entry.Nodes)
				{

					if (child.GetType() == typeof(VSS.VSS_Entry_Group))
						node.AppendChild(GetXMLNodeForVSS(doc, (VSS.VSS_Entry_Group)child, entry));
					else
						node.AppendChild(GetXMLNodeForVSS(doc, (VSS.VSS_Entry_Sound)child, entry));
				}
				doc.AppendChild(node);
				return node;
			}
			else
			{
				if (entry.GetType() == typeof(VSS.VSS_Entry_Group))
				{
					var node = doc.CreateElement(entry.Hotkey.ToString());
					var desc = doc.CreateAttribute(varDescription);
					desc.InnerText = entry.Description;
					node.Attributes.Append(desc);

					var tmpCast = (VSS.VSS_Entry_Group)entry;
					foreach (var child in tmpCast.Nodes)
					{
						if (child.GetType() == typeof(VSS.VSS_Entry_Group))
							node.AppendChild(GetXMLNodeForVSS(doc, (VSS.VSS_Entry_Group)child, entry));
						else
							node.AppendChild(GetXMLNodeForVSS(doc, (VSS.VSS_Entry_Sound)child, entry));
					}
					return node;
				}
				else
				{
					var node = doc.CreateElement(entry.Hotkey.ToString());
					var desc = doc.CreateAttribute(varDescription);
					desc.InnerText = entry.Description;
					var sound = doc.CreateAttribute(varSoundPath);
					sound.InnerText = ((VSS.VSS_Entry_Sound)entry).Filepath;
					node.Attributes.Append(desc);
					node.Attributes.Append(sound);
					return node;
				}
			}
		}

		private static VSS.VSS_Entry GetVSSFromXMLNode(XmlNode node)
		{
			if (node.Attributes[varSoundPath] == null)
			{
				if (Enum.TryParse(node.Name, out Keys key))
				{
					string Description = node.Attributes[varDescription].InnerText;
					VSS.VSS_Entry_Group groupVSSNode = new VSS.VSS_Entry_Group(Description, key);

					foreach (var child in node.ChildNodes)
					{
						var vssChild = GetVSSFromXMLNode((XmlNode)child);
						if (vssChild != null)
						{
							groupVSSNode.Nodes.Add(vssChild);
						}
					}
					return groupVSSNode;
				}
			}
			else
			{
				if (Enum.TryParse(node.Name, out Keys key))
				{
					string Description = node.Attributes[varDescription].InnerText;
					string FilePath = node.Attributes[varSoundPath].InnerText;
					return new VSS.VSS_Entry_Sound(Description, key, FilePath);
				}
			}
			return null;
		}
	}

	static class SoundEntriesExtansions
	{
		private static readonly string varSounds = "Sounds";
		private static readonly string varRequirement = "Requirement";
		private static readonly string varDescription = "SoundDescription";
		private static readonly string varDateAdded = "DateAdded";

		public static SoundEntry GetQuickSoundEntry(this XmlDocument xmlDoc, XmlNode node)
		{
			try
			{
				string tmpCommand = node.Name;
				string[] tmpSounds = node.Sui_GetAttributeValue(xmlDoc, varSounds, "").Split(';');
				TwitchRightsEnum tmpRequirement = node.Sui_GetAttributeValue(xmlDoc, varRequirement, TwitchRightsEnum.Disabled.ToString()).ToTwitchRights();
				string tmpDescription = node.Sui_GetAttributeValue(xmlDoc, varDescription, "");
				DateTime dateAdded = node.Sui_GetAttributeValue(xmlDoc, varDateAdded, DateTime.UtcNow.ToString()).ToDateTimeSafe();
				return new SoundEntry(tmpCommand, tmpRequirement, tmpSounds, tmpDescription, dateAdded);
			}
			catch
			{
				return new SoundEntry();
			}
		}
	}
}
