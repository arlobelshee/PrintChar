using FluentAssertions;
using NUnit.Framework;
using PluginApi.Model;
using SenseOfWonder.Model;
using SenseOfWonder.Tests.zzTestSupportData;

namespace SenseOfWonder.Tests.CharactersPersistInFiles
{
	[TestFixture]
	public class LoadCharacterFromFile
	{
		[Test]
		public void ShouldCreateEmptyCharacterFromEmptyFile()
		{
			var testSubject = new Model.SenseOfWonder();
			var emptyFile = Data.EmptyAt("arbitrary.wonder");

			testSubject.Parse(emptyFile).Should().Be(_TestData.DefaultCharacter());
		}

		[Test]
		public void FileWithJustANameShouldSetJustTheName()
		{
			var testSubject = new Model.SenseOfWonder();
			var input = Data.EmptyAt("arbitrary.wonder");
			input.Contents = @"{""Name"":""Rogers Hammerstein""}";

			var expected = _TestData.Character("Rogers Hammerstein", string.Empty);

			testSubject.Parse(input).Should().Be(expected);
		}
	}
}
