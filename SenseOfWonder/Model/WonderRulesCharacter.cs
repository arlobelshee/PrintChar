using System.Collections.Generic;
using System.IO;
using System.Linq;
using EventBasedProgramming.Binding;
using EventBasedProgramming.TestSupport;
using JetBrains.Annotations;
using PluginApi.Model;

namespace SenseOfWonder.Model
{
	public class WonderRulesCharacter : JsonBackedCharacter<SenseOfWonderCards, WonderRulesData>
	{
		protected WonderRulesCharacter([NotNull] SenseOfWonderCards system, [NotNull] FileInfo characterData)
			: base(system)
		{
			File = characterData;
			CreateCardCommand = new SimpleCommand(Always.Enabled, _CreateCard);
		}

		[NotNull]
		public List<WonderCard> CardData { get; private set; }

		[NotNull]
		public SimpleCommand CreateCardCommand { get; private set; }

		public static WonderRulesCharacter Create([NotNull] SenseOfWonderCards system, [NotNull] IDataFile characterData)
		{
			var result = new WonderRulesCharacter(system, characterData.Location);
			return (WonderRulesCharacter) result.FinishCreate(characterData);
		}

		public static WonderRulesCharacter Load([NotNull] SenseOfWonderCards system, [NotNull] IDataFile characterData)
		{
			var result = new WonderRulesCharacter(system, characterData.Location);
			return (WonderRulesCharacter) result.FinishLoad(characterData);
		}

		public override WonderRulesData PersistableData
		{
			get
			{
				return new WonderRulesData
				{
					Cards = CardData
				};
			}
		}

		public override void UpdateFrom(WonderRulesData rules)
		{
			CardData = rules.Cards.ToList();
			Cards.Clear();
			CardData.Select(_WrapCardInView).Each(Cards.Add);
		}

		private void _CreateCard()
		{
			var newCard = new WonderCard()
			{
				Name = Name
			};
			CardData.Add(newCard);
			Cards.Add(_WrapCardInView(newCard));
		}

		private static WonderCardView _WrapCardInView(WonderCard c)
		{
			return new WonderCardView
			{
				DataContext = c
			};
		}
	}

	public class WonderCardsDesignData : WonderRulesCharacter
	{
		public WonderCardsDesignData()
			: base(new SenseOfWonderCards(), new FileInfo("anything.wonder"))
		{
			Name = "Agrippan Disk";
		}
	}
}
