using FluentAssertions;
using NUnit.Framework;
using PluginApi.Model;

namespace Plugin.Dnd4e.Tests
{
	[TestFixture]
	public class FindAllCharacterDataAsyncronously
	{
		[Test]
		public void ShouldDiscoverConfigFileForCharacter()
		{
			_testSubject.CharData.Source.Should().Be(_mainCharFile.Location);
		}

		[Test]
		public void ShouldChooseConfigFileWithCorrectName()
		{
			_testSubject.ConfigData.Location.DirectoryName.Should().Be(_mainCharFile.Location.DirectoryName);
			_testSubject.ConfigData.Location.Name.Should().Be("charname.conf");
		}

		private IDataFile _mainCharFile;
		private CharacterData4E _testSubject;

		[SetUp]
		public void Setup()
		{
			_mainCharFile = Data.XmlNamed("charname.dnd4e");
			_testSubject = new CharacterData4E(_mainCharFile);
		}
	}
}
