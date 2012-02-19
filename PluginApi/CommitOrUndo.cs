using System;
using JetBrains.Annotations;

namespace PluginApi
{
	public class CommitOrUndo : IDisposable
	{
		[CanBeNull] private Action _undo;

		public CommitOrUndo([NotNull] Action undo)
		{
			_undo = undo;
		}

		public void Dispose()
		{
			if (_undo == null)
				return;
			_undo();
			_undo = null;
		}

		public void Commit()
		{
			_undo = null;
		}
	}
}