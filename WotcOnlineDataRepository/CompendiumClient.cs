using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using HtmlAgilityPack;
using JetBrains.Annotations;

namespace WotcOnlineDataRepository
{
	public class CompendiumClient : ICompendiumClient
	{
		[NotNull] private readonly HttpClient _theWeb;

		public CompendiumClient()
		{
			_theWeb = new HttpClient();
		}

		[NotNull]
		public Task Login([NotNull] string username, [NotNull] string password)
		{
			const string ddiHomePage = "http://www.wizards.com/default.asp?x=dnd/welcome";
			const string ddiLoginPage = "https://www.wizards.com/global/dnd_login.asp";
			var loginFormFields = new NameValueCollection {{"url", ddiHomePage}, {"email", username}, {"password", password}, {"submit.x", "31"}, {"submit.y", "15"},};

			var headers = new Dictionary<string, string>();
			headers["Referer"] = ddiHomePage;
			var loginResponse = _theWeb.Post(new Uri(ddiLoginPage, UriKind.Absolute), loginFormFields, headers);
			return loginResponse.ContinueWith(task => _FinishLogin(task.Result, username));
		}

		private static void _FinishLogin([NotNull] HttpClient.Response response, [NotNull] string username)
		{
			var secureServer = new Uri("https://wizards.com/");
			var regularServer = new Uri("http://wizards.com/");
			if (!response.HasCookie(secureServer, "iPlanetDirectoryPro"))
				throw new LoginFailureException(username);
			response.CopyCookiesBetweenServers(secureServer, regularServer);
			response.UseTheseCookiesForFutureRequests();
		}

		public Task<HtmlNode> GetPower(int powerId)
		{
			var request = _theWeb.Get(new Uri(string.Format("http://www.wizards.com/dndinsider/compendium/power.aspx?id={0}", powerId)));
			return request.ContinueWith(task => task.Result.Body.ToDetailsNode());
		}
	}
}
