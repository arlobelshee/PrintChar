using System.Collections.Generic;
using HtmlAgilityPack;
using JetBrains.Annotations;

namespace WotcOnlineDataRepository
{
	public static class NodeExtensions
	{
		[NotNull]
		public static string FirstText([NotNull] this HtmlNode node)
		{
			if (!node.HasChildNodes)
				return string.Empty;
			return node.SelectSingleNode("child::text()").InnerText ?? string.Empty;
		}

		[NotNull]
		public static ICollection<HtmlNode> SelectNodesNotBroken([NotNull] this HtmlNode node, [NotNull] string xpath)
		{
			return (ICollection<HtmlNode>) node.SelectNodes(xpath) ?? new List<HtmlNode>();
		}
	}
}
