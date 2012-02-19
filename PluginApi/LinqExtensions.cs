using System.Collections.Generic;
using JetBrains.Annotations;

namespace System.Linq
{
	public static class LinqExtensions
	{
		public static void Each<T>(this IEnumerable<T> args, Action<T> op)
		{
			foreach (T arg in args)
			{
				op(arg);
			}
		}

		public static TResult IfValid<TArg, TResult>([CanBeNull] this TArg mayBeNull, Func<TArg, TResult> op)
			where TArg : class
		{
			if (null == mayBeNull)
			{
				if (typeof (TResult) == typeof (string))
					return (TResult) (object) string.Empty;
				return default(TResult);
			}
			return op(mayBeNull);
		}

		public static TResult IfValidV<TArg, TResult>([CanBeNull] this TArg? mayBeNull, Func<TArg, TResult> op)
			where TArg : struct
		{
			if (!mayBeNull.HasValue)
			{
				if (typeof (TResult) == typeof (string))
					return (TResult) (object) string.Empty;
				return default(TResult);
			}
			return op(mayBeNull.Value);
		}
	}
}
