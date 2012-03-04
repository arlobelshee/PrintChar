using JetBrains.Annotations;
using PluginApi.Model;

namespace PluginApi.Tests
{
	internal class _SimplisticGameSystem : GameSystem
	{
		public _SimplisticGameSystem()
			: base("Trivial", "simple") {}

		[NotNull]
		protected override Character Parse([NotNull] IDataFile characterData)
		{
			return new DescribeAGameSystem.SillyCharacter(characterData, this);
		}
	}
}