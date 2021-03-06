using System.Collections.Generic;
using System.IO;
using System.Linq;
using EventBasedProgramming.Binding;
using JetBrains.Annotations;
using PluginApi.Model;
using SenseOfWonder.Model.Serialization;

namespace SenseOfWonder.Model
{
	public class WonderRulesCharacter : JsonBackedCharacter<RulesEditingSystem, RulesData>
	{
		protected WonderRulesCharacter([NotNull] RulesEditingSystem system, [NotNull] FileInfo characterData)
			: base(system)
		{
			File = characterData;
			CardData = new List<CardData>();
			CreateCardCommand = new SimpleCommand(() => !string.IsNullOrWhiteSpace(Name), CreateCardAndView);
			_name.WhenChanged(CreateCardCommand.NotifyThatCanExecuteChanged);
			EqualityFields.Add(CardData);
		}

		[NotNull]
		public List<CardData> CardData { get; private set; }

		[NotNull]
		public SimpleCommand CreateCardCommand { get; private set; }

		public static WonderRulesCharacter CreateWithoutBackingDataStore([NotNull] RulesEditingSystem system,
			[NotNull] FileInfo characterData)
		{
			return new WonderRulesCharacter(system, characterData);
		}

		public static WonderRulesCharacter Create([NotNull] RulesEditingSystem system, [NotNull] IDataFile characterData)
		{
			var result = new WonderRulesCharacter(system, characterData.Location);
			return (WonderRulesCharacter) result.FinishCreate(characterData);
		}

		public static WonderRulesCharacter Load([NotNull] RulesEditingSystem system, [NotNull] IDataFile characterData)
		{
			var result = new WonderRulesCharacter(system, characterData.Location);
			return (WonderRulesCharacter) result.FinishLoad(characterData);
		}

		public override RulesData PersistableData
		{
			get
			{
				return new RulesData
				{
					Cards = CardData
				};
			}
		}

		public override void UpdateFrom(RulesData characterData)
		{
			CardData.Clear();
			CardData.AddRange(characterData.Cards);
			characterData.Cards.Each(_PropagateChanges);
			Cards.Clear();
			CardData.Select(_WrapCardInView).Each(Cards.Add);
		}

		public void CreateCardAndView()
		{
			Cards.Add(_WrapCardInView(CreateUnboundCard()));
			FirePropertyChanged(() => PersistableData);
		}

		public CardData CreateUnboundCard()
		{
			var newCard = new CardData
			{
				Name = Name
			};
			_PropagateChanges(newCard);
			CardData.Add(newCard);
			return newCard;
		}

		private void _PropagateChanges(CardData newCard)
		{
			this.Propagate(() => PersistableData).From(newCard, c => c.Name);
		}

		private static WonderCardView _WrapCardInView(CardData c)
		{
			return new WonderCardView
			{
				DataContext = c
			};
		}

		public override string ToString()
		{
			return string.Format("Rules with Cards: [{0}]", string.Join(", ", CardData.Select(c => c.Name)));
		}
	}

	public class WonderCardsDesignData : WonderRulesCharacter
	{
		public WonderCardsDesignData()
			: base(new RulesEditingSystem(), new FileInfo("anything.wonder"))
		{
			Name = "Agrippan Disk";
		}
	}
}
