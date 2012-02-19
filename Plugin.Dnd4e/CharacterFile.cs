using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using JetBrains.Annotations;
using Plugin.Dnd4e;
using PluginApi.Model;
using WotcOnlineDataRepository;

namespace Plugin.Dnd4e
{
	public class CharacterFile
	{
		private static readonly Dictionary<string, Power.Usage> _USAGE_PARSER = new Dictionary<string, Power.Usage>
		{
			{"At-Will", Power.Usage.AtWill},
			{"Encounter", Power.Usage.Encounter},
			{"Encounter (Special)", Power.Usage.Encounter},
			{"Daily", Power.Usage.Daily},
			{"Healing Surge", Power.Usage.HealingSurge},
		};

		[NotNull] private readonly XmlDocument _dataFile;

		public CharacterFile([NotNull] FileInfo fileName)
		{
			_dataFile = new XmlDocument();
			_dataFile.Load(fileName.OpenText());
		}

		[NotNull]
		public string Name
		{
			get { return _Detail("name"); }
		}

		[NotNull]
		public string Gender
		{
			get { return _Rule("Gender"); }
		}

		[NotNull]
		public string Race
		{
			get { return _Rule("Race"); }
		}

		[NotNull]
		public string CharClass
		{
			get { return _Rule("Class"); }
		}

		[NotNull]
		private string _Detail([NotNull] string detailNodeName)
		{
			var searchPath = string.Format("/D20Character/CharacterSheet/Details/{0}", detailNodeName);
			return _dataFile.SelectSingleNode(searchPath).IfValid(elt=>elt.InnerText).Trim();
		}

		[NotNull]
		private string _Rule([NotNull] string ruleType)
		{
			var searchPath = string.Format("/D20Character/CharacterSheet/RulesElementTally/RulesElement[@type='{0}']", ruleType);
			var rule = (XmlElement) _dataFile.SelectSingleNode(searchPath);
			return rule.IfValid(elt => elt.GetAttribute("name"));
		}

		[NotNull]
		private XmlElement _NamedRule([NotNull] string ruleType, [NotNull] string ruleName)
		{
			var searchPath = string.Format("/D20Character/CharacterSheet/RulesElementTally/RulesElement[@type='{0}' and @name=\"{1}\"]", ruleType, ruleName);
			var result = (XmlElement) _dataFile.SelectSingleNode(searchPath);
			if (result == null)
				throw new ParseError(string.Format("Failed to find the {0} rule named {1}", ruleType, ruleName));
			return result;
		}

		[NotNull]
		public CharacterDnd4E ToCharacter()
		{
			return _AddPowers(new CharacterDnd4E {Name = Name, Gender = Gender, CharClass = CharClass, Race = Race,});
		}

		[NotNull]
		private static string _ChildNodeSpecific([NotNull] XmlElement data, string nodeTypeName)
		{
			return data.SelectSingleNode(string.Format("specific[@name=\'{0}\']", nodeTypeName)).InnerText.Trim();
		}

		[NotNull]
		private Power _ParseOnePower([NotNull] XmlElement data)
		{
			var usage = _ChildNodeSpecific(data, "Power Usage");
			var action = _ChildNodeSpecific(data, "Action Type");
			var name = data.GetAttribute("name");
			var url = _NamedRule("Power", name).GetAttribute("url");
			var power = new Power {Name = name, Refresh = _USAGE_PARSER[usage], Action = ActionType.For(action)};
			if (!string.IsNullOrEmpty(url))
			{
				var queryParams = new Uri(url, UriKind.Absolute).QueryParams();
				power.PowerId = int.Parse(queryParams["id"]);
			}
			return power;
		}

		[NotNull]
		private CharacterDnd4E _AddPowers([NotNull] CharacterDnd4E character)
		{
			_dataFile.SelectNodes("//PowerStats/Power").Cast<XmlElement>().Select(_ParseOnePower).Each(pow => character.Powers.Add(pow));
			return character;
		}
	}
}
