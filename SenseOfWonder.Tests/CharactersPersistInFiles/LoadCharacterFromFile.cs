using FluentAssertions;
using NUnit.Framework;
using PluginApi.Model;
using SenseOfWonder.Model;

namespace SenseOfWonder.Tests.CharactersPersistInFiles
{
	[TestFixture]
	public class LoadCharacterFromFile
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
