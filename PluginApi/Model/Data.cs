using System.IO;
using JetBrains.Annotations;

namespace PluginApi.Model
{
	public class Data : IDataFile
	{
		public void Dispose() {}

		[NotNull]
		public FileInfo Location { get; private set; }

		[NotNull]
		public string Contents { get; set; }

		public static IDataFile EmptyAt(FileInfo location)
		{
			return new Data
			{
				Contents = string.Empty,
				Location = location
			};
		}
	}
}
