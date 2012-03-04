using PluginApi.Model;

namespace PluginApi.Tests
{
	internal class _SimplisticGameSystem : GameSystem
	{
		public _SimplisticGameSystem()
			: base("Trivial", "simple") {}

		protected override Character Parse(IDataFile characterData)
		{
			return new DescribeAGameSystem.SillyCharacter(characterData, this);
		}
	}
}