using System;
using System.ComponentModel;
using System.Threading.Tasks;
using JetBrains.Annotations;
using PluginApi.Display.Helpers;
using WotcOnlineDataRepository;

namespace Plugin.Dnd4e
{
	public class OnlineRepositoryViewModel : INotifyPropertyChanged
	{
		private readonly Action<string, string> _logIn;

		public OnlineRepositoryViewModel()
		{
			Username = string.Empty;
			Password = string.Empty;
			LogInCommand = new SimpleCommand(() => !Repository.IsCompleted, () => _logIn(Username, Password));
			Tuple<Task<IDnd4ERepository>, Action<string, string>> orchestration = ServiceFactory.LogInToRealServiceEventually();
			Repository = orchestration.Item1;
			_logIn = orchestration.Item2;
		}

		public virtual event PropertyChangedEventHandler PropertyChanged;

		[NotNull]
		public string Username { private get; set; }

		[NotNull]
		public string Password { private get; set; }

		[NotNull]
		public SimpleCommand LogInCommand { get; private set; }

		[NotNull]
		public Task<IDnd4ERepository> Repository { get; private set; }
	}
}
