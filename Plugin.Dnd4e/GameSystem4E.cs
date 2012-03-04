using JetBrains.Annotations;
using PluginApi.Model;

namespace Plugin.Dnd4e
{
	public class GameSystem4E : GameSystem
	{
		public GameSystem4E() : base("4th Edition D&D", "dnd4e")
		{
			Login = new OnlineRepositoryViewModel();
		}

		[NotNull]
		public OnlineRepositoryViewModel Login { get; private set; }

		protected override Character Parse(IDataFile characterData)
		{
			var data = new CharacterData4E(characterData);
			CharacterDnd4E viewModel = data.ToCharacter(this);
			return viewModel;
		}
	}
}
