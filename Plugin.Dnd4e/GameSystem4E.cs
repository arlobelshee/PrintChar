using JetBrains.Annotations;
using PluginApi.Model;

namespace Plugin.Dnd4e
{
	public class GameSystem4E : GameSystem
	{
		[NotNull] private OnlineRepositoryViewModel _login = new OnlineRepositoryViewModel();

		public GameSystem4E() : base("4th Edition D&D", "dnd4e") {}

		protected override Character Parse(IDataFile characterData)
		{
			var data = new CharacterData4E(characterData);
			CharacterDnd4E viewModel = data.ToCharacter();
			return viewModel;
		}
	}
}
