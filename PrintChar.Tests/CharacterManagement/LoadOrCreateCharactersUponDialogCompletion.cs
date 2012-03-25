using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using PluginApi.Model;
using PrintChar.Tests.zzTestSupportData;

namespace PrintChar.Tests.CharacterManagement
{
	[TestFixture]
	public class LoadOrCreateCharactersUponDialogCompletion
	{
		[Test]
		public void NothingShouldHappenIfTheUserCancelsTheDialog()
		{
			_testSubject.LoadCharacter(_previousCharacter, null)
				.Should().BeSameAs(_previousCharacter);
			_loadedFiles.Should().BeEmpty();
		}

		[Test]
		public void NothingShouldHappenIfTheUserReloadsTheSameCharacter()
		{
			_testSubject.LoadCharacter(_previousCharacter, _previousCharacter.File.Location.FullName)
				.Should().BeSameAs(_previousCharacter);
			_loadedFiles.Should().BeEmpty();
		}

		[Test]
		public void CharactersShouldLoadWithCorrectGameSystem()
		{
			string fileName = "charfile." + _gameSystemThatKnowsLoadedFileType.FileExtension;
			_testSubject.LoadCharacter(_previousCharacter, fileName)
				.GameSystem.Should().BeSameAs(_gameSystemThatKnowsLoadedFileType);
			_loadedFiles.Should().Equal(new object[] {fileName});
		}

		private CharacterFileInteraction _TestSubject(IEnumerable<GameSystem> gameSystems, bool requireFileToExist)
		{
			return new CharacterFileInteraction(gameSystems, requireFileToExist, _LoaderMethod);
		}

		private Character _LoaderMethod(GameSystem system, string fileName)
		{
			system.Should().BeSameAs(_gameSystemThatKnowsLoadedFileType);
			_loadedFiles.Add(fileName);
			return new _SillyCharacter(Data.Anything(), system);
		}

		private Character _AnyCharacter()
		{
			return new _SillyCharacter(Data.Anything(), _readOnly);
		}

		private static IEnumerable<GameSystem> _Games(params GameSystem[] gameSystems)
		{
			return gameSystems;
		}

		[SetUp]
		public void SetUp()
		{
			_readOnly = new _ReadOnlyGameSystem();
			_gameSystemThatKnowsLoadedFileType = new _WritableGameSystem("nondefault");
			_loadedFiles = new List<string>();
			_testSubject = _TestSubject(_Games(_readOnly, _gameSystemThatKnowsLoadedFileType), false);
			_previousCharacter = _AnyCharacter();
		}

		private _ReadOnlyGameSystem _readOnly;
		private _WritableGameSystem _gameSystemThatKnowsLoadedFileType;
		private List<string> _loadedFiles;
		private CharacterFileInteraction _testSubject;
		private Character _previousCharacter;
	}
}
