using System.IO;
using EventBasedProgramming.TestSupport;
using FluentAssertions;
using JetBrains.Annotations;
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
		public void OpenFileButtonShouldAlwaysBeEnabledAndBoundCorrectly()
		{
			var testSubject = _With(_AnyGames());
			Assert.That(testSubject.OpenCharCommand,
				Command.DelegatesTo(() => Always.Enabled(), () => testSubject.SwitchCharacter()));
		}

		[Test]
		public void CharacterOpenerShouldUseAllGameSystems()
		{
			_With(_readOnlySystem, _writableSystem).CharacterOpener.ShouldMatch(new
			{
				GameSystems = new GameSystem[] { _readOnlySystem, _writableSystem },
				RequireFileToExist = true
			});
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
				Character result = _openExistingCharacter.LoadCharacter(_testSubject.Character, tempFile);
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
				Character result = _openExistingCharacter.LoadCharacter(_testSubject.Character, tempFile);
				result.GameSystem.Should().BeSameAs(_readOnlySystem);
			}
		}

		private static _AllGameSystemsViewModelThatAllowsOverridingCurrentCharacter _With(params GameSystem[] gameSystems)
		{
			return new _AllGameSystemsViewModelThatAllowsOverridingCurrentCharacter(gameSystems);
		}

		[NotNull]
		private static GameSystem[] _AnyGames()
		{
			return new GameSystem[] {};
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
		private GameSystem[] _gameSystemsInUse;

		[SetUp]
		public void Setup()
		{
			_writableSystem = new _WritableGameSystem();
			_readOnlySystem = new _ReadOnlyGameSystem();
			_gameSystemsInUse = new GameSystem[] {_writableSystem, _readOnlySystem};
			_openExistingCharacter = new CharacterFileInteraction(_gameSystemsInUse, true,
				(gameSystem, fileName) => gameSystem.LoadCharacter(fileName));
			_testSubject = new _AllGameSystemsViewModelThatAllowsOverridingCurrentCharacter(_writableSystem, _readOnlySystem);
			_dataFile = Data.EmptyAt(new FileInfo(@"R:\arbitrary\path\ee.dnd4e"));
			_arbitraryCharacter = new _SillyCharacter(_dataFile, new _WritableGameSystem());
		}
	}
}
