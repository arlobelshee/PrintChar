using System.Linq;
using FluentAssertions;
using PluginApi.Model;

namespace PrintChar.Tests.zzTestSupportData
{
	internal static class _TestHelperExtensions
	{
		public static void ShouldMatch<T>(this T actual, object expected)
		{
			actual.ShouldHave().SharedProperties().EqualTo(expected);
		}

		public static void ShouldBeFor(this Character character, string expectedSourceFile)
		{
			character.File.FullName.Should().Be(expectedSourceFile);
		}
	}
}
