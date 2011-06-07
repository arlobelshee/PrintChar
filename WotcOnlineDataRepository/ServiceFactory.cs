using System;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using WotcOnlineDataRepository.WotcServices.Content;
using WotcOnlineDataRepository.WotcServices.Workspace;

namespace WotcOnlineDataRepository
{
	public static class ServiceFactory
	{
		public static Task<IDnd4ERepository> LogInToRealService([NotNull] string username, [NotNull] string password)
		{
			var contentVault = _LogIn(new ContentVaultConnector(username, password));
			var workspace = _LogIn(new WorkspaceConnector(username, password));
			var compendium = _LogInToCompendium(username, password);
			return contentVault.ContinueWith(vault => _CreateRepository(vault, workspace, compendium));
		}

		public static Tuple<Task<IDnd4ERepository>, Action<string, string>> LogInToRealServiceEventually()
		{
			var orchestration = new TaskCompletionSource<Tuple<string, string>>();
			var logIn = orchestration.Task.ContinueWith(credentials => LogInToRealService(credentials.Result.Item1, credentials.Result.Item2).Result);
			return new Tuple<Task<IDnd4ERepository>, Action<string, string>>(logIn,
				(username, password) => orchestration.SetResult(new Tuple<string, string>(username, password)));
		}

		public static Task<IDnd4ERepository> SimulateLoginFailureForTesting(string username, string password)
		{
			return new TaskFactory<IDnd4ERepository>().StartNew(() => { throw new LoginFailureException(username); });
		}

		public static Task<IDnd4ERepository> MakeLocalOnlyFakeServiceForTesting()
		{
			return new TaskFactory<IDnd4ERepository>().StartNew(() => new Fake4ERepository());
		}

		public static Task<IDnd4ERepository> MakeServiceThatWrapsTestDataSource(IRawDnd4ERepository dataSource)
		{
			return new TaskFactory<IDnd4ERepository>().StartNew(() => new Dnd4ERepository(dataSource));
		}

		[NotNull]
		private static Task<T> _LogIn<T>([NotNull] ILoginProvider<T> loginProvider)
		{
			return loginProvider.BeginLogin().ToTask(loginProvider.EndLogin);
		}

		private static Task<CompendiumClient> _LogInToCompendium(string username, string password)
		{
			var compendiumClient = new CompendiumClient();
			var compendium = compendiumClient.Login(username, password).ContinueWith(task => compendiumClient);
			return compendium;
		}

		private static IDnd4ERepository _CreateRepository([NotNull] Task<IContentVaultService> vault, [NotNull] Task<ID20WorkspaceService> workspace,
			[NotNull] Task<CompendiumClient> compendium)
		{
			try
			{
				return new Dnd4ERepository(vault.Result, workspace.Result, compendium.Result);
			}
			catch (AggregateException allExceptions)
			{
				allExceptions.Flatten().Handle(ex => ex is LoginFailureException);
				throw allExceptions.InnerExceptions.First(ex => ex is LoginFailureException);
			}
		}
	}
}
