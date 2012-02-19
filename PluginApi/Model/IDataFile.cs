using System;
using System.IO;

namespace PluginApi.Model
{
	public interface IDataFile : IDisposable
	{
		FileInfo Location { get; }
		string Contents { get; set; }
	}
}