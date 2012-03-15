using JetBrains.Annotations;
using PluginApi.Model;

namespace SenseOfWonder
{
	public class WonderCharacter : Character<SenseOfWonderSystem>
	{
		public WonderCharacter([NotNull] SenseOfWonderSystem system) : base(system) {}
	}
}