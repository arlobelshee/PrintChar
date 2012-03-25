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
		public void DefaultExtensionShouldBeSameAsCurrentCharacter()
		{
			_testSubject.CurrentCharacter = new _SillyCharacter(_dataFile, new _ReadOnlyGameSystem());
			_testSubject.CharacterOpener.CreateDialog(_testSubject.Character).ShouldMatch(new
			{
				DefaultExt = _ReadOnlyGameSystem.Extension,
			});
		}

		[Test]
		public void IfHaveAlreadyOpenedCharacterShouldSetOpenDialogToStartInThatLocation()
		{
			_testSubject.CurrentCharacter = _arbitraryCharacter;
			_testSubject.CharacterOpener.CreateDialog(_testSubject.Character).InitialDirectory.Should().Be(_arbitraryCharacter.File.Location.DirectoryName);
		}

		[Test]
		public void CancellingTheOpenDialogShouldResultInNoChangeInOpenCharacter()
		{
			_testSubject.CurrentCharacter = _arbitraryCharacter;
			_openExistingCharacter.LoadCharacter(_testSubject.Character, null);
			_testSubject.Character.Should().BeSameAs(_arbitraryCharacter);
		}

		[Test]
		public void OpeningANewCharacterShouldUpdateTheCharacterFile()
		{
			string tempFile = _MakeTempFile(_writableSystem.FileExtension);
			using (Undo.Step(() => File.Delete(tempFile)))
			{
				var result = _openExistingCharacter.LoadCharacter(_testSubject.Character, tempFile);
				result.File.Location.FullName.Should().Be(tempFile);
				result.GameSystem.Should().BeSameAs(_writableSystem);
			}
		}

		[Test]
		public void NewCharactersShouldBeOpenedWithCorrectGameSystem()
		{
			string tempFile = _MakeTempFile(_readOnlySystem.FileExtension);
			using (Undo.Step(() => File.Delete(tempFile)))
			{
				var result = _openExistingCharacter.LoadCharacter(_testSubject.Character, tempFile);
				result.GameSystem.Should().BeSameAs(_readOnlySystem);
			}
		}

		private static string _MakeTempFile(string extension)
		{
			string tempFile = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName() + "." + extension);
			File.WriteAllText(tempFile, string.Empty);
			return tempFile;
		}

		private _AllGameSystemsViewModelThatAllowsOverridingCurrentCharacter _testSubject;
		private _SillyCharacter _arbitraryCharacter;
		private IDataFile _dataFile;
		private _WritableGameSystem _writableSystem;
		private _ReadOnlyGameSystem _readOnlySystem;
		private CharacterFileInteraction _openExistingCharacter;

		[SetUp]
		public void Setup()
		{
			_writableSystem = new _WritableGameSystem();
			_readOnlySystem = new _ReadOnlyGameSystem();
			_openExistingCharacter = new CharacterFileInteraction(new GameSystem[] { _writableSystem, _readOnlySystem }, true, (gameSystem, fileName) => gameSystem.LoadCharacter(fileName));
			_testSubject = new _AllGameSystemsViewModelThatAllowsOverridingCurrentCharacter(_writableSystem, _readOnlySystem);
			_dataFile = Data.EmptyAt(new FileInfo(@"R:\arbitrary\path\ee.dnd4e"));
			_arbitraryCharacter = new _SillyCharacter(_dataFile, new _WritableGameSystem());
		}
	}
}
