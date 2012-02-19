using System.Windows.Controls;
using JetBrains.Annotations;
using Plugin.Dnd4e.Templates;
using Plugin.Dnd4e.Templates.Anders;
using PluginApi.Model;

namespace Plugin.Dnd4e
{
	public class GameSystem4E : GameSystem<CharacterDnd4E>
	{
		[NotNull] private IDataFile _configFile;

		[NotNull] private readonly CharacterTransformer<CharacterDnd4E, Control> _compiler = new CharacterTransformer
			<CharacterDnd4E, Control>(
			data => new CharacterData4E(data).ToCharacter(),
			GenerateCardsWithFactory<Control>.Using(new CardFactoryAnders()));

		[NotNull]
		private OnlineRepositoryViewModel _login = new OnlineRepositoryViewModel();

		public GameSystem4E() : base("4th Edition D&D", "dnd4e")
		{
		}

		protected override CharacterDnd4E Parse(IDataFile characterData)
		{
			var data = new CharacterData4E(characterData);
			CharacterDnd4E viewModel = data.ToCharacter();
			return viewModel;
		}

		private void _UpdateCards()
		{
			Cards.Clear();
//			_compiler.Compile(new[] {_charFileLocation}).Each(card => Cards.Add(card));
		}
	}
}
