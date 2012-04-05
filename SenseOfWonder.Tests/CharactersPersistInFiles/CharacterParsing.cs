using FluentAssertions;
using NUnit.Framework;
using PluginApi.Model;

namespace SenseOfWonder.Tests.CharactersPersistInFiles
{
	[TestFixture]
	public class CharacterParsing
	{
		[Test]
		public void ShouldCreateEmptyCharacterFromEmptyFile()
		{
			var testSubject = new SenseOfWonderSystem();
			var emptyFile = Data.EmptyAt("arbitrary.wonder");
			var defaultCharacter = new WonderCharacter(testSubject, emptyFile);

			testSubject.Parse(emptyFile).Should().Be(defaultCharacter);
		}
	}
}
