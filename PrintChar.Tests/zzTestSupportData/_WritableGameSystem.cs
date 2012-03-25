using PluginApi.Model;

namespace PrintChar.Tests.zzTestSupportData
{
	internal class _WritableGameSystem : GameSystem
	{
		internal const string Extension = "write";

		public _WritableGameSystem()
			: base("Writable Characters", Extension) {}

		public _WritableGameSystem(string extensionOverride)
			: base("Writable Characters", extensionOverride) {}

		public override Character Parse(IDataFile characterData)
		{
			return new _SillyCharacter(characterData, this);
		}
	}
}
