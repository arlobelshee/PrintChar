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
			File = Data.EmptyAt(@"C:\some\path\to\doug.designdatafile");
		}

		public override GameSystem GameSystem
		{
			get { return _gameSystem; }
		}
	}
}
