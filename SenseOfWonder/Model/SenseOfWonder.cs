using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using PluginApi.Model;
using SenseOfWonder.Views;

namespace SenseOfWonder.Model
{
	[Export(typeof (GameSystem))]
	public class SenseOfWonder : GameSystem
	{
		public SenseOfWonder() : base("Sense of Wonder", "wonder") {}

		public override Character Parse(IDataFile characterData)
		{
			return WonderCharacter.Load(this, characterData);
		}

		public override Character CreateIn(IDataFile characterData)
		{
			return WonderCharacter.Create(this, characterData);
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
						Content = new EditCharacter()
					}
				};
			}
		}
	}
}
