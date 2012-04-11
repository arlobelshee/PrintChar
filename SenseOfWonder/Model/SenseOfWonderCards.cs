using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using PluginApi.Model;
using SenseOfWonder.Views;

namespace SenseOfWonder.Model
{
	[Export(typeof (GameSystem))]
	public class SenseOfWonderCards : GameSystem
	{
		public SenseOfWonderCards() : base("Sense of Wonder Cards", "wondercards")
		{
			IsReadOnly = true;
		}

		public override Character Parse(IDataFile characterData)
		{
			return WonderCardHolder.Load(this, characterData);
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
