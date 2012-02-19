using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace PluginApi.Model
{
	public static class UriExtensions
	{
		[NotNull]
		public static Dictionary<string, string> QueryParams([NotNull] this Uri uri)
		{
			return uri.Query.Trim('?').Split('&').Select(pair => pair.Split('=')).ToDictionary(parts => parts[0],
				parts => parts[1]);
		}
	}
}
