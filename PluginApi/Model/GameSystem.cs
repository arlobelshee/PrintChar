namespace PluginApi.Model
{
	public class GameSystem
	{
		public GameSystem(string systemLabel, string fileExtension)
		{
			Name = systemLabel;
			FileExtension = string.Format(".{0}", fileExtension);
			FilePattern = string.Format("*.{0}", fileExtension);
		}

		public string Name { get; private set; }
		public string FileExtension { get; private set; }
		public string FilePattern { get; private set; }
	}
}
