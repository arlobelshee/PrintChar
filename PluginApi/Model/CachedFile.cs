using System;
using System.IO;
using System.Threading;
using JetBrains.Annotations;

namespace PluginApi.Model
{
	public class CachedFile : IDataFile
	{
		private static readonly TimeSpan _ONLY_ONCE = TimeSpan.FromMilliseconds(-1);
		private static readonly TimeSpan _SAVE_DELAY = TimeSpan.FromMilliseconds(500);
		private readonly FileInfo _backingFile;
		private readonly object _fileLock = new object();
		private string _cachedValue = string.Empty;
		private Timer _currentSaver;
		private _DataState _dataState;

		public CachedFile([NotNull] FileInfo backingFile, bool requireToExistAlready = true)
		{
			_backingFile = backingFile;
			if (!_backingFile.Exists)
			{
				if (requireToExistAlready)
					throw new FileNotFoundException("Data file not found.", _backingFile.FullName);
				_backingFile.CreateText().Close();
			}
			_dataState = _DataState.FileIsCanonical;
		}

		public void Dispose()
		{
			if (_currentSaver != null)
				_currentSaver.Dispose();
			_EnsureBackingFileIsUpToDate();
		}

		public FileInfo Location
		{
			get { return _backingFile; }
		}

		public string Contents
		{
			get { return _EnsureMemoryCacheIsUpToDate(); }
			set
			{
				lock (_fileLock)
				{
					_cachedValue = value;
					_dataState = _DataState.CacheIsCanonical;
				}
				_PrepareADelayedSave();
			}
		}

		public IDataFile FromSameDirectory(string targetFileName)
		{
			return new CachedFile(new FileInfo(Path.Combine(Location.DirectoryName, targetFileName)), false);
		}

		public void EnsureCacheIsCurrent()
		{
			_EnsureMemoryCacheIsUpToDate();
		}

		private void _PrepareADelayedSave()
		{
			if (_currentSaver == null)
				_currentSaver = new Timer(arg => _EnsureBackingFileIsUpToDate(), null, _SAVE_DELAY, _ONLY_ONCE);
			else
				_currentSaver.Change(_SAVE_DELAY, _ONLY_ONCE);
		}

		private void _EnsureBackingFileIsUpToDate()
		{
			lock (_fileLock)
			{
				if (_dataState != _DataState.CacheIsCanonical)
					return;
				if (_backingFile.Exists)
					_backingFile.Delete();
				using (var writer = _backingFile.CreateText())
				{
					writer.Write(_cachedValue);
				}
			}
		}

		private string _EnsureMemoryCacheIsUpToDate()
		{
			lock (_fileLock)
			{
				if (_dataState != _DataState.FileIsCanonical)
					return _cachedValue;
				using (var reader = _backingFile.OpenText())
				{
					_cachedValue = reader.ReadToEnd();
				}
				_dataState = _DataState.Synchronized;
				return _cachedValue;
			}
		}

		private enum _DataState
		{
			FileIsCanonical,
			Synchronized,
			CacheIsCanonical
		}
	}
}
