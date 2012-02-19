using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace WotcOnlineDataRepository
{
	internal class HttpClient
	{
		private CookieContainer _currentCookies = new CookieContainer();
		private static readonly Encoding _ENCODING = Encoding.UTF8;

		[NotNull]
		public Task<Response> Post([NotNull] Uri address, [NotNull] NameValueCollection postData)
		{
			return Post(address, postData, new Dictionary<string, string>(), CancellationToken.None);
		}

		[NotNull]
		public Task<Response> Post([NotNull] Uri address, [NotNull] NameValueCollection postData, [NotNull] Dictionary<string, string> headers)
		{
			return Post(address, postData, headers, CancellationToken.None);
		}

		[NotNull]
		public Task<Response> Post([NotNull] Uri address, [NotNull] NameValueCollection postData, [NotNull] Dictionary<string, string> headers,
			CancellationToken cancellation)
		{
			return _Request(headers, cancellation, (completions, client) =>
			{
				client.UploadValuesCompleted += (obj, args) => _HandleResponse(client, completions, args.Cancelled, args.Error, ()=>_ENCODING.GetString(args.Result));
				client.UploadValuesAsync(address, "POST", postData);
			});
		}

		[NotNull]
		public Task<Response> Get([NotNull] Uri address)
		{
			return Get(address, CancellationToken.None);
		}

		[NotNull]
		public Task<Response> Get([NotNull] Uri address, CancellationToken cancellation)
		{
			return _Request(new Dictionary<string, string>(), cancellation, (completions, client) =>
			{
				client.DownloadStringCompleted += (obj, args) => _HandleResponse(client, completions, args.Cancelled, args.Error, ()=>args.Result);
				client.DownloadStringAsync(address);
			});
		}

		private void _HandleResponse([NotNull] CookieAwareWebClient client, [NotNull] TaskCompletionSource<Response> completions, bool cancelled, Exception exception,
			Func<string> body)
		{
			if (_RequestFailed(completions, cancelled, exception))
				return;
			completions.SetResult(new Response(this, body(), client.Cookies));
		}

		private Task<Response> _Request(Dictionary<string, string> headers, CancellationToken cancellation,
			Action<TaskCompletionSource<Response>, CookieAwareWebClient> performRequest)
		{
			var completions = new TaskCompletionSource<Response>();
			var client = new CookieAwareWebClient(_currentCookies);
			foreach (var header in headers)
			{
				client.Headers[header.Key] = header.Value;
			}
			client.Encoding = _ENCODING;
			cancellation.Register(client.CancelAsync);
			performRequest(completions, client);
			return completions.Task;
		}

		private static bool _RequestFailed(TaskCompletionSource<Response> completions, bool cancelled, Exception exception)
		{
			if (cancelled)
			{
				completions.SetCanceled();
				return true;
			}
			if (exception != null)
			{
				completions.SetException(exception);
				return true;
			}
			return false;
		}

		internal class Response
		{
			public string Body { get; private set; }
			private readonly CookieContainer _cookies;
			private readonly HttpClient _owner;

			public Response(HttpClient owner, string body, CookieContainer cookies)
			{
				Body = body;
				_owner = owner;
				_cookies = cookies;
			}

			public void UseTheseCookiesForFutureRequests()
			{
				_owner._currentCookies = _cookies;
			}

			public bool HasCookie([NotNull] Uri server, [NotNull] string cookieName)
			{
				return _cookies.GetCookies(server).Cast<Cookie>().Any(cookie => cookie.Name == cookieName);
			}

			public void CopyCookiesBetweenServers(Uri from, Uri to)
			{
				var dest = _cookies.GetCookies(to);
				_cookies.GetCookies(from).Cast<Cookie>().Each(dest.Add);
			}
		}
	}
}
