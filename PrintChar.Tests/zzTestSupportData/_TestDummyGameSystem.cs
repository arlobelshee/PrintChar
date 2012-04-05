using System.Collections.ObjectModel;
using System.Windows.Controls;
using JetBrains.Annotations;
using PluginApi.Model;

namespace PrintChar.Tests.zzTestSupportData
{
	internal class _TestDummyGameSystem : GameSystem
	{
		protected _TestDummyGameSystem(string systemLabel, string fileExtension) : base(systemLabel, fileExtension) {}

		[NotNull]
		public override ObservableCollection<TabItem> EditorPages
		{
			get { return new ObservableCollection<TabItem>(); }
		}

		[NotNull]
		public override Character Parse([NotNull] IDataFile characterData)
		{
			return new _SillyCharacter(characterData, this);
		}
	}
}