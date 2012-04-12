using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Controls;
using JetBrains.Annotations;
using PluginApi.Model;
using SenseOfWonder.Model.Serialization;
using SenseOfWonder.Views;

namespace SenseOfWonder.Model
{
	public class WonderCharacter : JsonBackedCharacter<SenseOfWonder, CharacterData>
	{
		protected WonderCharacter([NotNull] SenseOfWonder system, [NotNull] FileInfo characterData) : base(system)
		{
			File = characterData;
		}

		public static WonderCharacter CreateWithoutBackingDataStore([NotNull] SenseOfWonder system,
			[NotNull] FileInfo characterData)
		{
			return new WonderCharacter(system, characterData);
		}

		public static WonderCharacter Create([NotNull] SenseOfWonder system, [NotNull] IDataFile characterData)
		{
			var result = new WonderCharacter(system, characterData.Location);
			return (WonderCharacter) result.FinishCreate(characterData);
		}

		public static WonderCharacter Load([NotNull] SenseOfWonder system, [NotNull] IDataFile characterData)
		{
			var result = new WonderCharacter(system, characterData.Location);
			return (WonderCharacter) result.FinishLoad(characterData);
		}

		protected override void AddInitialCards(ObservableCollection<Control> cards)
		{
			base.AddInitialCards(cards);
			cards.Add(new CharacterSummaryCard
			{
				DataContext = this
			});
		}

		public override void UpdateFrom(CharacterData characterData)
		{
			Name = characterData.Name ?? string.Empty;
			Gender = characterData.Gender ?? string.Empty;
		}

		public override CharacterData PersistableData
		{
			get
			{
				return new CharacterData
				{
					Name = Name,
					Gender = Gender
				};
			}
		}
	}

	public class WonderCharacterDesignData : WonderCharacter
	{
		public WonderCharacterDesignData() : base(new SenseOfWonder(), new FileInfo("anything.wonder"))
		{
			Name = "Wonderful Character";
			Gender = "Female";
		}
	}
}
