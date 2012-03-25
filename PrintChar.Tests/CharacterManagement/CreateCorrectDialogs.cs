using System;
using System.Collections.Generic;
using Microsoft.Win32;
using NUnit.Framework;
using PluginApi.Model;
using PrintChar.Tests.zzTestSupportData;

namespace PrintChar.Tests.CharacterManagement
{
	[TestFixture]
	public class CreateCorrectDialogs
	{
		[Test]
		public void SeveralPropertiesShouldBeBasedDirectlyOnConstructorArgs()
		{
			var dialog = TestSubject(_Games(_readOnly, _writeable, _otherWritable), true).CreateDialog(null);
			dialog.ShouldMatch(new
			{
				Filter =
					"Read Only Characters file (*.read)|*.read|Writable Characters file (*.write)|*.write|Writable Characters file (*.nondefault)|*.nondefault",
				CheckFileExists = true,
				Multiselect = false,
			});
		}

		[Test]
		public void CheckFileExistsShouldBeOverridableViaConstructor()
		{
			var dialog = TestSubject(_AnyGames(), false).CreateDialog(null);
			dialog.ShouldMatch(new
			{
				CheckFileExists = false,
			});
		}

		[Test]
		public void WithNoCurrentCharacterDialogShouldDefaultToFirstSystemInListAndInitialDirectoryShouldBeEmpty()
		{
			var dialog = TestSubject(_Games(_readOnly, _writeable), true).CreateDialog(null);
			dialog.ShouldMatch(new
			{
				DefaultExt = _ReadOnlyGameSystem.Extension,
				InitialDirectory = string.Empty
			});
		}

		[Test]
		public void CurrentCharacterShouldOverrideDefaultSystemAndInitialDirectory()
		{
			var currentCharacterFileLocation = Data.Anything();
			var currentCharacter = new _SillyCharacter(currentCharacterFileLocation, _writeable);

			var dialog = TestSubject(_Games(_readOnly, _writeable), true).CreateDialog(currentCharacter);
			dialog.ShouldMatch(new
			{
				DefaultExt = _writeable.FileExtension,
				InitialDirectory = currentCharacterFileLocation.Location.DirectoryName
			});
		}

		private CharacterFileInteraction TestSubject(IEnumerable<GameSystem> gameSystems, bool requireFileToExist)
		{
			return new CharacterFileInteraction(gameSystems, requireFileToExist, _ShouldNeverBeCalled);
		}

		private static IEnumerable<GameSystem> _Games(params GameSystem[] gameSystems)
		{
			return gameSystems;
		}

		private IEnumerable<GameSystem> _AnyGames()
		{
			return _Games(_readOnly);
		}

		private static Character _ShouldNeverBeCalled(GameSystem gameSystem, string s)
		{
			Assert.Fail("This should never be called.");
			throw new NotImplementedException();
		}

		[SetUp]
		public void SetUp()
		{
			_readOnly = new _ReadOnlyGameSystem();
			_writeable = new _WritableGameSystem();
			_otherWritable = new _WritableGameSystem("nondefault");
		}

		private _ReadOnlyGameSystem _readOnly;
		private _WritableGameSystem _writeable;
		private _WritableGameSystem _otherWritable;
	}
}
