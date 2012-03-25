using JetBrains.Annotations;
using PluginApi.Model;

namespace PrintChar.Tests.zzTestSupportData
{
	internal class _ReadOnlyGameSystem : GameSystem
	{
		internal const string Extension = "read";

		public _ReadOnlyGameSystem()
			: base("Read Only Characters", Extension)
		{
			IsReadOnly = true;
		}

		[NotNull]
		public override Character Parse([NotNull] IDataFile characterData)
		{
			return new _SillyCharacter(characterData, this);
		}
	}
}
