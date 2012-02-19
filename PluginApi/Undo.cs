using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace PluginApi
{
	public class Undo : IDisposable
	{
		[NotNull] private List<Action> _undo = new List<Action>();

		private Undo([NotNull] Action undo)
		{
			_undo.Add(undo);
		}

		public static Undo Step([NotNull] Action undo)
		{
			return new Undo(undo);
		}

		public void Add(Action item)
		{
			_undo.Add(item);
		}

		public void Dispose()
		{
			_undo.Reverse();
			_undo.Each(op=>op());
			_undo.Clear();
		}

		public void Commit()
		{
			_undo.Clear();
		}
	}
}