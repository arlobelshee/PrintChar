using PluginApi.Model;

namespace PrintChar.Tests.zzTestSupportData
{
	internal class _WritableGameSystem : GameSystem
	{
		public _WritableGameSystem()
			: base("Trivial", "simple") {}

		public override Character Parse(IDataFile characterData)
		{
			return new _SillyCharacter(characterData, this);
		}
	}
}