using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Controls;
using JetBrains.Annotations;
using PluginApi.Model;
using SenseOfWonder.Model.Impl;
using SenseOfWonder.Views;

namespace SenseOfWonder.Model
{
	public class WonderCharacter : Character<SenseOfWonderSystem>
	{
		protected WonderCharacter([NotNull] SenseOfWonderSystem system, [NotNull] FileInfo characterData) : base(system)
		{
			File = characterData;
		}

		public static WonderCharacter CreateWithoutBackingDataStore([NotNull] SenseOfWonderSystem system, [NotNull] FileInfo characterData)
		{
			return new WonderCharacter(system, characterData);
		}

		public static WonderCharacter Create([NotNull] SenseOfWonderSystem system, [NotNull] IDataFile characterData)
		{
			var result = new WonderCharacter(system, characterData.Location);
			var serializer = new CharSerializer(result, characterData);
			result.PropertyChanged += serializer.UpdateFile;
			return result;
		}

		public static WonderCharacter Load([NotNull] SenseOfWonderSystem system, [NotNull] IDataFile characterData)
		{
			var result = new WonderCharacter(system, characterData.Location);
			var serializer = new CharSerializer(result, characterData);
			serializer.LoadFromFile();
			result.PropertyChanged += serializer.UpdateFile;
			return result;
		}

		protected override void AddInitialCards(ObservableCollection<Control> cards)
		{
			base.AddInitialCards(cards);
			cards.Add(new CharacterSummaryCard(){DataContext = this});
		}

		public WonderCharData PersistableData
		{
			get
			{
				return new WonderCharData
				{
					Name = Name,
					Gender = Gender
				};
			}
		}

		public void UpdateFrom(WonderCharData wonderCharData)
		{
			Name = wonderCharData.Name ?? string.Empty;
			Gender = wonderCharData.Gender ?? string.Empty;
		}
	}

	public class WonderCharacterDesignData : WonderCharacter
	{
		public WonderCharacterDesignData() : base(new SenseOfWonderSystem(), new FileInfo("anything.wonder"))
		{
			Name = "Wonderful Character";
			Gender = "Female";
		}
	}
}
