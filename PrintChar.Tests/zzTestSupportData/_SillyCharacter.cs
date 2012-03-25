using PluginApi.Model;

namespace PrintChar.Tests.zzTestSupportData
{
	internal class _SillyCharacter : Character<GameSystem>
	{
		public _SillyCharacter(IDataFile data, GameSystem system) : base(system)
		{
			File = data;
		}
	}
}