using System.IO;
using FluentAssertions;
using NUnit.Framework;
using PluginApi;
using PluginApi.Model;
using PrintChar.Tests.zzTestSupportData;

namespace PrintChar.Tests.CharacterManagement
{
	[TestFixture]
	public class OpenExistingCharacters
	{
		[Test]
		public void OpenFileButtonShouldAlwaysBeEnabled()
		{
			_testSubject.OpenCharCommand.CanExecute(null).Should().BeTrue();
		}

		[Test]
		public void ShouldUseLabelAndExtensionCorrectlyToInitializeOpenDialog()
		{
			_testSubject.CreateOpenDialog().ShouldHave().SharedProperties().EqualTo(new
			{
				Filter = "Trivial file (*.simple)|*.simple|Not Done file (*.nope)|*.nope",
				DefaultExt = "simple",
				CheckFileExists = true,
				Multiselect = false,
				InitialDirectory = string.Empty
			});
		}

		[Test]
		public void DefaultExtensionShouldBeSameAsCurrentCharacter()
		{
			_testSubject.CurrentCharacter = new _SillyCharacter(_dataFile, new _ReadOnlyGameSystem());
			_testSubject.CreateOpenDialog().ShouldHave().SharedProperties().EqualTo(new
			{
				DefaultExt = "nope",
			});
		}

		[Test]
		public void IfHaveAlreadyOpenedCharacterShouldSetOpenDialogToStartInThatLocation()
		{
			_testSubject.CurrentCharacter = _arbitraryCharacter;
			_testSubject.CreateOpenDialog().InitialDirectory.Should().Be(_arbitraryCharacter.File.Location.DirectoryName);
		}

		[Test]
		public void CancellingTheOpenDialogShouldResultInNoChangeInOpenCharacter()
		{
			_testSubject.CurrentCharacter = _arbitraryCharacter;
			_testSubject.LoadCharacter(null);
			_testSubject.Character.Should().BeSameAs(_arbitraryCharacter);
		}

		[Test]
		public void OpeningANewCharacterShouldUpdateTheCharacterFile()
		{
			string tempFile = MakeTempFile(_simpleSystem.FileExtension);
			using (Undo.Step(() => File.Delete(tempFile)))
			{
				_testSubject.LoadCharacter(tempFile);
				_testSubject.Character.File.Location.FullName.Should().Be(tempFile);
				_testSubject.Character.GameSystem.Should().BeSameAs(_simpleSystem);
			}
		}

		[Test]
		public void NewCharactersShouldBeOpenedWithCorrectGameSystem()
		{
			string tempFile = MakeTempFile(_unfinishedSystem.FileExtension);
			using (Undo.Step(() => File.Delete(tempFile)))
			{
				_testSubject.LoadCharacter(tempFile);
				_testSubject.Character.GameSystem.Should().BeSameAs(_unfinishedSystem);
			}
		}

		private static string MakeTempFile(string extension)
		{
			string tempFile = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName() + "." + extension);
			File.WriteAllText(tempFile, string.Empty);
			return tempFile;
		}

		private _AllGameSystemsViewModel _testSubject;
		private _SillyCharacter _arbitraryCharacter;
		private IDataFile _dataFile;
		private _WritableGameSystem _simpleSystem;
		private _ReadOnlyGameSystem _unfinishedSystem;

		[SetUp]
		public void Setup()
		{
			_simpleSystem = new _WritableGameSystem();
			_unfinishedSystem = new _ReadOnlyGameSystem();
			_testSubject = new _AllGameSystemsViewModel(_simpleSystem, _unfinishedSystem);
			_dataFile = Data.EmptyAt(new FileInfo(@"R:\arbitrary\path\ee.dnd4e"));
			_arbitraryCharacter = new _SillyCharacter(_dataFile, new _WritableGameSystem());
		}

		internal class _AllGameSystemsViewModel : AllGameSystemsViewModel
		{
			public _AllGameSystemsViewModel(params GameSystem[] gameSystems) : base(gameSystems) {}

			public Character CurrentCharacter
			{
				set { Character = value; }
			}
		}
	}
}
