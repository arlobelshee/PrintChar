using System.Collections.Generic;
using JetBrains.Annotations;

namespace System.Linq
{
	public static class LinqExtensions
	{
		public static void Each<T>([NotNull] this IEnumerable<T> items, [NotNull] Action<T> operation)
		{
			foreach (var item in items)
			{
				operation(item);
			}
		}

		public static TResult IfValid<TArg, TResult>([CanBeNull] this TArg mayBeNull, Func<TArg, TResult> op) where TArg : class
		{
			if (null == mayBeNull)
			{
				if (typeof (TResult) == typeof (string))
					return (TResult) (object) string.Empty;
				return default(TResult);
			}
			return op(mayBeNull);
		}
	}
}
