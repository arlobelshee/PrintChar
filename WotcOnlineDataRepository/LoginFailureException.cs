using System;

namespace WotcOnlineDataRepository
{
	public class LoginFailureException : Exception
	{
		public readonly string Username;

		public LoginFailureException(string username) : base(string.Format("Login failed for username '{0}'. Please check your password and try again.", username))
		{
			Username = username;
		}
	}
}