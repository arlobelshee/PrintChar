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
			_testSubject.Name.Should().Be("Trivial");
		}

		[Test]
		public void ShouldDescribeFileFilters()
		{
			_testSubject.FileExtension.Should().Be("simple");
			_testSubject.FilePattern.Should().Be("*.simple");
		}

		[Test]
		public void ShouldUseLabelAndExtensionCorrectlyToInitializeOpenDialog()
		{
			_testSubject.CreateOpenDialog().ShouldHave().SharedProperties().EqualTo(new
			{
				Filter = "Trivial file (*.simple)|*.simple",
				DefaultExt = "simple",
				CheckFileExists = true,
				Multiselect = false,
				InitialDirectory = string.Empty
			});
		}

		[Test]
		public void IfHaveAlreadyOpenedCharacterShouldSetOpenDialogToStartInThatLocation()
		{
			_testSubject.Character = _arbitraryCharacter;
			_testSubject.CreateOpenDialog().InitialDirectory.Should().Be(_arbitraryCharacter.File.Location.DirectoryName);
		}

		[Test]
		public void CancellingTheOpenDialogShouldResultInNoChangeInOpenCharacter()
		{
			_testSubject.Character = _arbitraryCharacter;
			_testSubject.LoadCharacter(null);
			_testSubject.Character.Should().BeSameAs(_arbitraryCharacter);
		}

		[Test]
		public void OpeningANewCharacterShouldUpdateTheCharacterFile()
		{
			string tempFile = Path.GetTempFileName();
			using (Undo.Step(() => File.Delete(tempFile)))
			{
				_testSubject.Character = _arbitraryCharacter;
				_testSubject.LoadCharacter(tempFile);
				_testSubject.Character.File.Location.FullName.Should().Be(tempFile);
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

		private GameSystem<SillyCharacter> _testSubject;
		private SillyCharacter _arbitraryCharacter;

		[SetUp]
		public void Setup()
		{
			_testSubject = new _SimplisticGameSystem();
			_arbitraryCharacter = new SillyCharacter(Data.EmptyAt(new FileInfo(@"R:\arbitrary\path\ee.dnd4e")));
		}

		public class SillyCharacter : Character
		{
			public SillyCharacter(IDataFile data)
			{
				File = data;
			}
		}

		internal class _SimplisticGameSystem : GameSystem<SillyCharacter>
		{
			public _SimplisticGameSystem()
				: base("Trivial", "simple") {}

			protected override SillyCharacter Parse(IDataFile characterData)
			{
				return new SillyCharacter(characterData);
			}
		}
	}
}
