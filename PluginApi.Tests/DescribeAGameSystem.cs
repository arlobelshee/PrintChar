using System.IO;
using FluentAssertions;
using NUnit.Framework;
using PluginApi.Model;

namespace PluginApi.Tests
{
	[TestFixture]
	public class DescribeAGameSystem
	{
		[Test]
		public void ShouldHaveAName()
		{
			_testSubject.Name.Should().Be("4th Edition D&D");
		}

		[Test]
		public void ShouldDescribeFileFilters()
		{
			_testSubject.FileExtension.Should().Be("dnd4e");
			_testSubject.FilePattern.Should().Be("*.dnd4e");
		}

		[Test]
		public void ShouldUseLabelAndExtensionCorrectlyToInitializeOpenDialog()
		{
			_testSubject.CreateOpenDialog().ShouldHave().SharedProperties().EqualTo(new
			{
				Filter = "4th Edition D&D file (*.dnd4e)|*.dnd4e",
				DefaultExt = "dnd4e",
				CheckFileExists = true,
				Multiselect = false,
				InitialDirectory = string.Empty
			});
		}

		[Test]
		public void IfHaveAlreadyOpenedCharacterShouldSetOpenDialogToStartInThatLocation()
		{
			_testSubject.CharacterFile = _arbitraryFile;
			_testSubject.CreateOpenDialog().InitialDirectory.Should().Be(_arbitraryFile.Location.DirectoryName);
		}

		[Test]
		public void CancellingTheOpenDialogShouldResultInNoChangeInOpenCharacter()
		{
			_testSubject.CharacterFile = _arbitraryFile;
			_testSubject.LoadCharacter(null);
			_testSubject.CharacterFile.Should().BeSameAs(_arbitraryFile);
		}

		[Test]
		public void OpeningANewCharacterShouldUpdateTheCharacterFile()
		{
			string tempFile = Path.GetTempFileName();
			using (new CommitOrUndo(() => File.Delete(tempFile)))
			{
				_testSubject.CharacterFile = _arbitraryFile;
				_testSubject.LoadCharacter(tempFile);
				_testSubject.CharacterFile.Location.FullName.Should().Be(tempFile);
			}
		}

		[Test]
		public void ShouldAllowCreatingNewCharactersByDefault()
		{
			_testSubject.IsReadOnly.Should().BeFalse();
		}

		[Test]
		public void ShouldAllowGameSystemsWhichCannotCreateCharacters()
		{
			_testSubject.IsReadOnly = true;
			_testSubject.IsReadOnly.Should().BeTrue();
		}

		private GameSystem _testSubject;
		private CachedFile _arbitraryFile;

		[SetUp]
		public void Setup()
		{
			_testSubject = new _SimplisticGameSystem();
			_arbitraryFile = new CachedFile(new FileInfo(Path.GetTempPath() + @"ee.dnd4e"), false);
		}

		internal class _SimplisticGameSystem : GameSystem
		{
			public _SimplisticGameSystem()
				: base("4th Edition D&D", "dnd4e") {}
		}
	}
}
