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
			return new WonderCharacter(this);
		}

		public override Character CreateIn(IDataFile characterData)
		{
			return new WonderCharacter(this);
		}

		public override Control EditorDisplay
		{
			get { return new Editor(); }
		}
	}
}
