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
		public SenseOfWonderSystem() : base("Sense of Wonder", "wonder")
		{
		}

		public override Character Parse(IDataFile characterData)
		{
			return new WonderCharacter(this, characterData);
		}

		public override Character CreateIn(IDataFile characterData)
		{
			return new WonderCharacter(this, characterData);
		}

		public override ObservableCollection<Control> EditorPages
		{
			get
			{
				return new ObservableCollection<Control>
				{
					new Editor()
				};
			}
		}
	}
}
