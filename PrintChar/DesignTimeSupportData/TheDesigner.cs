using PluginApi.Model;

namespace PrintChar.DesignTimeSupportData
{
	public class TheDesigner : Character
	{
		private readonly DesignTimeGameSystem _gameSystem;

		public TheDesigner(DesignTimeGameSystem gameSystem)
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