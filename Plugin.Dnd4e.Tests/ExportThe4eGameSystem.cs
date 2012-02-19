using NUnit.Framework;
using FluentAssertions;

namespace Plugin.Dnd4e.Tests
{
	[TestFixture]
	public class ExportThe4eGameSystem
	{
		[Test]
		public void ShouldHaveCorrectNameAndExtension()
		{
			var testSubject = new GameSystem4E();
			testSubject.FileExtension.Should().Be("dnd4e");
			testSubject.Name.Should().Be("4th Edition D&D");
		}
	}
}
