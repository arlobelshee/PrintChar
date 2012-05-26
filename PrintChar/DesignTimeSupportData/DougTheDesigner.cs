using System.IO;
using System.Windows.Controls;
using PluginApi.Model;

namespace PrintChar.DesignTimeSupportData
{
	public class DougTheDesigner : Character
	{
		private readonly DesignTimeGameSystem _gameSystem;

		public DougTheDesigner(DesignTimeGameSystem gameSystem)
		{
			_gameSystem = gameSystem;
			Name = "Doug Designer";
			Gender = "Robotic";
			File = new FileInfo(@"C:\some\path\to\doug.designdatafile");
			_AddCard("First Card");
			_AddCard("Second Card");
			_AddCard("Third Card");
			_AddCard("Fourth Card");
		}

		private void _AddCard(string name)
		{
			Cards.Add(new Button
			{
				Content = name
			});
		}

		public override GameSystem GameSystem
		{
			get { return _gameSystem; }
		}
	}
}
