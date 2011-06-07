using System;
using System.ServiceModel.Channels;
using JetBrains.Annotations;
using WotcOnlineDataRepository.WotcServices.Content;

namespace WotcOnlineDataRepository
{
	internal class ContentVaultConnector : ILoginProvider<IContentVaultService>
	{
		[NotNull] private readonly string _password;
		[NotNull] private readonly ContentVaultServiceClient _service;
		[NotNull] private readonly string _username;

		public ContentVaultConnector([NotNull] string username, [NotNull] string password)
		{
			_username = username;
			_password = password;
			_service = new ContentVaultServiceClient();
		}

		public IAsyncResult BeginLogin()
		{
			var credentials = new LoginRequest(_username, SimpleEncryption.Encrypt(_password, _username));
// ReSharper disable AssignNullToNotNullAttribute
			return _service.BeginLogin(credentials, null, null);
// ReSharper restore AssignNullToNotNullAttribute
		}

		public IContentVaultService EndLogin(IAsyncResult asyncResult)
		{
			if (!_service.EndLogin(asyncResult).LoginResult)
				throw new LoginFailureException(_username);
			return _service;
		}
	}
}
