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

		public IDataFile FromSameDirectory(string targetFileName)
		{
			return new Data
			{
				Location = new FileInfo(Path.Combine(Location.DirectoryName, targetFileName)),
				Contents = string.Empty
			};
		}

		[NotNull]
		public static IDataFile EmptyAt([NotNull] string location)
		{
			return EmptyAt(new FileInfo(location));
		}

		[NotNull]
		public static IDataFile EmptyAt([NotNull] FileInfo location)
		{
			return new Data
			{
				Contents = string.Empty,
				Location = location
			};
		}

		public static IDataFile Anything()
		{
			return EmptyAt(@"L:\does\not\exist\file.extension");
		}

		public static IDataFile AnyXmlFile()
		{
			return XmlNamed("anything.xml");
		}

		public static IDataFile XmlNamed(string fileName)
		{
			return new Data
			{
				Contents = "<xml />",
				Location = new FileInfo(string.Format(@"X:\another\path\to\{0}", fileName))
			};
		}
	}
}
