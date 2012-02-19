using System;
using System.IO;
using JetBrains.Annotations;

namespace PluginApi.Model
{
	public interface IDataFile : IDisposable
	{
		[NotNull]
		FileInfo Location { get; }

		[NotNull]
		string Contents { get; set; }

		[NotNull]
		IDataFile FromSameDirectory([NotNull] string targetFileName);

		void EnsureCacheIsCurrent();
	}
}
