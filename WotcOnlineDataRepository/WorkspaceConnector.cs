using System;
using JetBrains.Annotations;
using WotcOnlineDataRepository.WotcServices.Workspace;

namespace WotcOnlineDataRepository
{
	internal class WorkspaceConnector : ILoginProvider<ID20WorkspaceService>
	{
		[NotNull] private readonly string _password;
		[NotNull] private readonly D20WorkspaceServiceClient _service;
		[NotNull] private readonly string _username;

		public WorkspaceConnector([NotNull] string username, [NotNull] string password)
		{
			_username = username;
			_password = password;
			_service = new D20WorkspaceServiceClient();
		}

		public IAsyncResult BeginLogin()
		{
			var credentials = new LoginRequest(_username, SimpleEncryption.Encrypt(_password, _username));
// ReSharper disable AssignNullToNotNullAttribute
			return _service.BeginLogin(credentials, null, null);
// ReSharper restore AssignNullToNotNullAttribute
		}

		public ID20WorkspaceService EndLogin(IAsyncResult asyncResult)
		{
			if (!_service.EndLogin(asyncResult).LoginResult)
				throw new LoginFailureException(_username);
			return _service;
		}
	}
}