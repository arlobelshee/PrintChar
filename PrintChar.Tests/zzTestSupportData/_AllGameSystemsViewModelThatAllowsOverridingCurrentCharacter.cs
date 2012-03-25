using PluginApi.Model;

namespace PrintChar.Tests.zzTestSupportData
{
	internal class _AllGameSystemsViewModelThatAllowsOverridingCurrentCharacter : AllGameSystemsViewModel
	{
		public _AllGameSystemsViewModelThatAllowsOverridingCurrentCharacter(params GameSystem[] gameSystems) : base(gameSystems) {}

		public Character CurrentCharacter
		{
			set { Character = value; }
		}
	}
}