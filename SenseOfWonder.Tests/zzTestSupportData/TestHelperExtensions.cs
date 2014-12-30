using FluentAssertions;

namespace SenseOfWonder.Tests.zzTestSupportData
{
	internal static class _TestHelperExtensions
	{
		public static void ShouldMatch<T>(this T actual, object expected)
		{
			actual.ShouldBeEquivalentTo(expected, options => options.ExcludingMissingProperties());
		}
	}
}
