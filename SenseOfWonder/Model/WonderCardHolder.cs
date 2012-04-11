using System.Collections.Generic;
using System.IO;
using System.Linq;
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
		}

		public IEnumerable<WonderCard> CardData { get; private set; }

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
			CardData.Select(c => new WonderCardView
			{
				DataContext = c
			}).Each(Cards.Add);
		}
	}
}
