using FluentAssertions;
using PluginApi.Model;

namespace SenseOfWonder.Tests.zzTestSupportData
{
	internal static class _TestHelperExtensions
	{
		public static void ShouldMatch<T>(this T actual, object expected)
		{
			actual.ShouldHave().SharedProperties().EqualTo(expected);
		}
	}
}
