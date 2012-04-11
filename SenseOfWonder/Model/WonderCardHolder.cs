using System.Collections.Generic;
using System.IO;
using System.Linq;
using EventBasedProgramming.Binding;
using EventBasedProgramming.TestSupport;
using JetBrains.Annotations;
using PluginApi.Model;
using SenseOfWonder.Model.Impl;

namespace SenseOfWonder.Model
{
	public class WonderCardHolder : Character<SenseOfWonderCards>
	{
		protected WonderCardHolder([NotNull] SenseOfWonderCards system, [NotNull] FileInfo characterData)
			: base(system)
		{
			File = characterData;
			CreateCardCommand = new SimpleCommand(Always.Enabled, _CreateCard);
		}

		[NotNull]
		public List<WonderCard> CardData { get; private set; }

		[NotNull]
		public SimpleCommand CreateCardCommand { get; private set; }

		public static WonderCardHolder Create([NotNull] SenseOfWonderCards system, [NotNull] IDataFile characterData)
		{
			var result = new WonderCardHolder(system, characterData.Location);
			var serializer = new CardSerializer(result, characterData);
			result.PropertyChanged += serializer.UpdateFile;
			return result;
		}

		public static WonderCardHolder Load([NotNull] SenseOfWonderCards system, [NotNull] IDataFile characterData)
		{
			var result = new WonderCardHolder(system, characterData.Location);
			var serializer = new CardSerializer(result, characterData);
			serializer.LoadFromFile();
			result.PropertyChanged += serializer.UpdateFile;
			return result;
		}

		public void UpdateFrom(IEnumerable<WonderCard> cards)
		{
			CardData = cards.ToList();
			Cards.Clear();
			CardData.Select(WrapCardInView).Each(Cards.Add);
		}

		private void _CreateCard()
		{
			var newCard = new WonderCard()
			{
				Name = Name
			};
			CardData.Add(newCard);
			Cards.Add(WrapCardInView(newCard));
		}

		private WonderCardView WrapCardInView(WonderCard c)
		{
			return new WonderCardView
			{
				DataContext = c
			};
		}
	}

	public class WonderCardsDesignData : WonderCardHolder
	{
		public WonderCardsDesignData()
			: base(new SenseOfWonderCards(), new FileInfo("anything.wonder"))
		{
			Name = "Agrippan Disk";
		}
	}
}
