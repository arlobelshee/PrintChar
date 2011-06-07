using System.Xml.Linq;
using JetBrains.Annotations;
using WotcOnlineDataRepository.WotcServices.Content;

namespace WotcOnlineDataRepository
{
	public class XmlDetailsData
	{
		internal XmlDetailsData([NotNull] ContentDetails content)
		{
			ContentIdentifier = new ContentId(content.Identifier);
			Content = XDocument.Parse(content.Details).Root;
		}

		[NotNull]
		public ContentId ContentIdentifier { get; private set; }

		[NotNull]
		public XElement Content { get; private set; }

		public override string ToString()
		{
			return string.Format("ID: {0} Content: {1}", ContentIdentifier, Content);
		}
	}
}
