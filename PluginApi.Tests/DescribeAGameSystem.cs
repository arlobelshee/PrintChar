using NUnit.Framework;
using PluginApi.Model;
using FluentAssertions;

namespace PluginApi.Tests
{
	[TestFixture]
	public class DescribeAGameSystem
	{
		[Test]
		public void ShouldHaveAName()
		{
			var testSubject = new GameSystem("4th Edition D&D", "dnd4e");
			testSubject.Name.Should().Be("4th Edition D&D");
		}

		[Test]
		public void ShouldDescribeFileFilters()
		{
			var testSubject = new GameSystem("4th Edition D&D", "dnd4e");
			testSubject.FileExtension.Should().Be(".dnd4e");
			testSubject.FilePattern.Should().Be("*.dnd4e");
		}
	}
}
