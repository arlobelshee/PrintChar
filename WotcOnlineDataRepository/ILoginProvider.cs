using System;
using JetBrains.Annotations;

namespace WotcOnlineDataRepository
{
	public interface ILoginProvider<out TResult>
	{
		[NotNull]
		IAsyncResult BeginLogin();

		[NotNull]
		TResult EndLogin([NotNull] IAsyncResult asyncResult);
	}
}
