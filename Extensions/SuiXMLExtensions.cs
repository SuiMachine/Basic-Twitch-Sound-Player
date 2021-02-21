using System.Xml;

namespace BasicTwitchSoundPlayer.Extensions
{
	public static class SuiXMLExtensions
	{
		public static XmlNode Sui_GetNode(this XmlDocument doc, string NodeName)
		{
			if (doc[NodeName] != null)
				return doc[NodeName];
			else
			{
				XmlNode newNode = doc.CreateElement(NodeName);
				doc.AppendChild(newNode);
				return newNode;
			}
		}

		public static XmlNode Sui_GetNode(this XmlNode parentNode, XmlDocument doc, string NodeName)
		{
			if (parentNode[NodeName] != null)
				return parentNode[NodeName];
			else
			{
				XmlNode newNode = doc.CreateElement(NodeName);
				parentNode.AppendChild(newNode);
				return newNode;
			}
		}

		#region AttributeValues
		public static string Sui_GetAttributeValue(this XmlNode node, XmlDocument doc, string AttributeName, string DEFAULT_VALUE)
		{
			if (node.Attributes[AttributeName] != null)
			{
				if (node.Attributes[AttributeName].InnerText != null)
					return node.Attributes[AttributeName].InnerText;
				else
				{
					node.Attributes[AttributeName].InnerText = DEFAULT_VALUE;
					return DEFAULT_VALUE;
				}
			}
			else
			{
				var newAttrib = doc.CreateAttribute(AttributeName);
				newAttrib.InnerText = DEFAULT_VALUE;
				node.Attributes.Append(newAttrib);
				return DEFAULT_VALUE;
			}
		}

		public static void Sui_SetAttributeValue(this XmlNode node, XmlDocument doc, string AttributeName, string VALUE)
		{
			if (node.Attributes[AttributeName] != null)
			{
				node.Attributes[AttributeName].InnerText = VALUE;
			}
			else
			{
				var newAttrib = doc.CreateAttribute(AttributeName);
				newAttrib.InnerText = VALUE;
				node.Attributes.Append(newAttrib);
			}
		}
		#endregion

		#region InnerText
		public static string Sui_GetInnerText(this XmlNode Node, string DEFAULT_VALUE)
		{
			if (Node.InnerText != null)
				return Node.InnerText;
			else
			{
				Node.InnerText = DEFAULT_VALUE;
				return DEFAULT_VALUE;
			}

		}

		public static void Sui_SetInnerText(this XmlNode Node, string VALUE)
		{
			Node.InnerText = VALUE;
		}
		#endregion
	}
}
