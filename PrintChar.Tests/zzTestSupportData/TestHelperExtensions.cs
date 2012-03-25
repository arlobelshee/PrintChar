using FluentAssertions;
using Microsoft.Win32;

namespace PrintChar.Tests.zzTestSupportData
{
	internal static class _TestHelperExtensions
	{
		public static void ShouldMatch(this OpenFileDialog actual, object expected)
		{
			actual.ShouldHave().SharedProperties().EqualTo(expected);
		}
	}
}
