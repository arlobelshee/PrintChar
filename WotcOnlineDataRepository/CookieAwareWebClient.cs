using System;
using System.Net;

namespace WotcOnlineDataRepository
{
	internal class CookieAwareWebClient : WebClient
	{
		public CookieAwareWebClient(CookieContainer cookies = null)
		{
			Cookies = cookies ?? new CookieContainer();
		}

		public CookieContainer Cookies { get; private set; }

		protected override WebRequest GetWebRequest(Uri address)
		{
			var request = base.GetWebRequest(address);
			if (request is HttpWebRequest)
			{
				(request as HttpWebRequest).CookieContainer = Cookies;
			}
			return request;
		}
	}
}
