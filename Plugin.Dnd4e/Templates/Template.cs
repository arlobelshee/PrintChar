using System;
using System.Windows;

namespace Plugin.Dnd4e.Templates
{
	public static class Template
	{
		public static Uri Resource(string templateName, string resourcePath)
		{
			return new Uri(String.Format("/Plugin.Dnd4e;component/Templates/{0}/{1}", templateName, resourcePath), UriKind.Relative);
		}

		public static ResourceDictionary ResourceDict(string templateName, string pathToResourceXaml)
		{
			return new ResourceDictionary {Source = Resource(templateName, pathToResourceXaml)};
		}
	}
}
