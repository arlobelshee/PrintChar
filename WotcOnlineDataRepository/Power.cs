using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using HtmlAgilityPack;
using JetBrains.Annotations;

namespace WotcOnlineDataRepository
{
	public class Power
	{
		public Power()
		{
			Description = new List<Descriptor>();
		}

		[NotNull]
		public string Name { get; internal set; }

		[NotNull]
		public string Source { get; internal set; }

		[NotNull]
		public string Type { get; internal set; }

		public int? Level { get; internal set; }

		[NotNull]
		public List<Descriptor> Description { get; set; }

		public static Dictionary<string, Power> ParseToPowersDict([NotNull] IEnumerable<Task<HtmlNode>> rawData)
		{
			var responses = rawData.SelectMany(_ValidResponses);
			var powers = responses.SelectMany(_Parse);
			return powers.ToDictionary(power => power.Name);
		}

		public static void Clean([NotNull] HtmlNode data)
		{
			var originalChildren = data.ChildNodes.ToList();
			HtmlNode currentPowerDiv = null;
			foreach (var child in originalChildren)
			{
				data.RemoveChild(child);
				if (child.NodeType == HtmlNodeType.Element && child.Name == "br")
					continue;
				if (child.NodeType == HtmlNodeType.Element && child.Name == "h1")
				{
					currentPowerDiv = data.OwnerDocument.CreateElement("div");
					data.AppendChild(currentPowerDiv);
				}
				if (child.NodeType == HtmlNodeType.Element && child.Name == "b")
				{
					var newPara = data.OwnerDocument.CreateElement("p");
					newPara.AppendChild(child);
					if (currentPowerDiv != null)
						currentPowerDiv.AppendChild(newPara);
					continue;
				}
				if (child.NodeType == HtmlNodeType.Element && child.Name == "p" && !_IsProbablyStatBlock(child))
				{
					var currentPara = child;
					var originalGrandchildren = child.ChildNodes.ToList();
					foreach (var grandchild in originalGrandchildren)
					{
						child.RemoveChild(grandchild);
						if (grandchild.NodeType == HtmlNodeType.Element && grandchild.Name == "br")
						{
							if (currentPowerDiv != null)
								currentPowerDiv.AppendChild(currentPara);
							currentPara = data.OwnerDocument.CreateElement("p");
							continue;
						}
						currentPara.AppendChild(grandchild);
					}
					if (currentPowerDiv != null)
						currentPowerDiv.AppendChild(currentPara);
					continue;
				}
				if (null != currentPowerDiv)
					currentPowerDiv.AppendChild(child);
			}
		}

		[NotNull]
		private static IEnumerable<Power> _Parse([NotNull] HtmlNode data)
		{
			Clean(data);
			foreach (var powerData in data.ChildNodes)
			{
				var titleBarRegion = powerData.SelectSingleNode("h1");
				_Require(titleBarRegion != null, () => _Missing("name", powerData));
				var levelDescriptors = titleBarRegion.SelectSingleNode("span[@class='level']");
				var allParagraphs = powerData.SelectNodesNotBroken("p");

				var power = new Power {Name = titleBarRegion.FirstText()};
				_AddLevelDescriptors(power, levelDescriptors, powerData);
				_AddDescription(power, allParagraphs);
				yield return power;
			}
		}

		private static void _AddLevelDescriptors([NotNull] Power power, HtmlNode levelDescriptors, [NotNull] HtmlNode data)
		{
			_Require(levelDescriptors != null, () => _Missing("level", data));
			if (!levelDescriptors.HasChildNodes)
				return;
			var parts = levelDescriptors.FirstText().Split(' ');
			power.Source = parts[0];
			power.Type = parts[1];
			if (parts.Length > 2)
				power.Level = Convert.NInt(parts[2]);
		}

		private static void _AddDescription([NotNull] Power power, [NotNull] ICollection<HtmlNode> allParagraphs)
		{
			power.Description = allParagraphs.Select(_ParseDescriptorPara).Where(desc => desc != null).ToList();
		}

		private static Descriptor _ParseDescriptorPara([NotNull] HtmlNode para)
		{
			if (_IsProbablyFlavorText(para))
				return null;
			if (_IsProbablyStatBlock(para))
				return null;
			if (_IsProbablyPublicationReference(para))
				return null;
			var labels = para.SelectNodesNotBroken("b");
			_Require(labels.Count < 2,
				() => { throw new ParseError(string.Format("Found too many bold regions in {0}. I do not know how to interpret them.", para.WriteTo())); });
			var label = labels.Count == 1 ? labels.First().FirstText() : string.Empty;
			var details = para.WriteContentTo();
			if (labels.Count > 0)
			{
				details = details.Substring(details.LastIndexOf(@"</b>") + @"</b>".Length);
			}
			if (details.StartsWith(":"))
				details = details.Substring(1);
			return new Descriptor(label, details);
		}

		private static bool _IsProbablyStatBlock([NotNull] HtmlNode para)
		{
			return para.SelectSingleNode("img[@src='images/bullet.gif']") != null;
		}

		private static bool _IsProbablyFlavorText([NotNull] HtmlNode para)
		{
			var possibleFlavor = para.SelectSingleNode("i | em");
			return possibleFlavor != null && 10.0*possibleFlavor.InnerText.Length/para.InnerText.Length > 8.0;
		}

		private static bool _IsProbablyPublicationReference([NotNull] HtmlNode para)
		{
			return para.SelectSingleNode("a") != null;
		}

		[AssertionMethod]
		private static void _Require([AssertionCondition(AssertionConditionType.IS_TRUE)] bool condition, [NotNull] Func<string> messageMaker)
		{
			if (!condition)
				throw new ParseError(messageMaker());
		}

		[NotNull]
		private static string _Missing([NotNull] string nodeName, [NotNull] HtmlNode data)
		{
			return string.Format("I could not find the {0} when parsing {1}", nodeName, data.WriteTo());
		}

		private static IEnumerable<HtmlNode> _ValidResponses([NotNull] Task<HtmlNode> request)
		{
			if (request.IsFaulted)
				request.Exception.Flatten().Handle(ex => ex is WebException);
			else if (request.IsCompleted)
				yield return request.Result;
		}
	}

	public class ParseError : Exception
	{
		public ParseError(string message) : base(message) {}
	}
}
