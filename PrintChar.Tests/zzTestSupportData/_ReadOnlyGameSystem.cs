using JetBrains.Annotations;
using PluginApi.Model;

namespace PrintChar.Tests.zzTestSupportData
{
	internal class _ReadOnlyGameSystem : GameSystem
	{
		public _ReadOnlyGameSystem()
			: base("Not Done", "nope") {}

		[NotNull]
		public override Character Parse([NotNull] IDataFile characterData)
		{
			return new _SillyCharacter(characterData, this);
		}
	}
}
