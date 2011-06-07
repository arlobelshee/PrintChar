using System;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace WotcOnlineDataRepository
{
	internal static class AsyncHelpers
	{
		[NotNull]
		public static Task<TResult> ToTask<TResult>([NotNull] this IAsyncResult beginResult, [NotNull] Func<IAsyncResult, TResult> completion)
		{
			return Task<TResult>.Factory.FromAsync(beginResult, completion);
		}
	}
}
