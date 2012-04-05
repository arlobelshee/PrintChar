using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using PluginApi.Model;
using SenseOfWonder.Views;

namespace SenseOfWonder
{
	[Export(typeof (GameSystem))]
	public class SenseOfWonderSystem : GameSystem
	{
		public SenseOfWonderSystem() : base("Sense of Wonder", "wonder") {}

		public override Character Parse(IDataFile characterData)
		{
			return new WonderCharacter(this, characterData);
		}

		public override Character CreateIn(IDataFile characterData)
		{
			return new WonderCharacter(this, characterData);
		}

		public override ObservableCollection<TabItem> EditorPages
		{
			get
			{
				return new ObservableCollection<TabItem>
				{
					new TabItem
					{
						Header = "Basics",
						Content = new Editor()
					}
				};
			}
		}
	}
}
