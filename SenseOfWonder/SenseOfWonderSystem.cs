using System.ComponentModel.Composition;
using PluginApi.Model;

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
	}
}
