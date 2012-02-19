using System;
using System.IO;
using JetBrains.Annotations;

namespace PluginApi.Model
{
	public class Data : IDataFile
	{
		public void Dispose() {}

		[NotNull]
		public FileInfo Location { get; private set; }

		private string _contents;

		[NotNull]
		public string Contents
		{
			get
			{
				CacheIsCurrent = true;
				return _contents;
			}
			set
			{
				CacheIsCurrent = true;
				_contents = value;
			}
		}

		public bool CacheIsCurrent { get; private set; }

		public Data([NotNull] FileInfo location, [CanBeNull] string contents = null)
		{
			if (location == null)
				throw new ArgumentNullException("location");
			Location = location;
			Contents = contents ?? string.Empty;
			CacheIsCurrent = false;
		}

		public void EnsureCacheIsCurrent()
		{
			CacheIsCurrent = true;
		}

		[NotNull]
		public static IDataFile EmptyAt([NotNull] string location)
		{
			return EmptyAt(new FileInfo(location));
		}

		[NotNull]
		public static IDataFile EmptyAt([NotNull] FileInfo location)
		{
			return new Data(location);
		}

		[NotNull]
		public IDataFile FromSameDirectory(string targetFileName)
		{
			return EmptyAt(Path.Combine(Location.DirectoryName, targetFileName));
		}

		[NotNull]
		public static IDataFile Anything()
		{
			return EmptyAt(@"L:\does\not\exist\file.extension");
		}

		[NotNull]
		public static IDataFile AnyXmlFile()
		{
			return XmlNamed("anything.xml");
		}

		public static IDataFile XmlNamed(string fileName)
		{
			return new Data(new FileInfo(string.Format(@"X:\another\path\to\{0}", fileName)), "<xml />");
		}
	}
}
