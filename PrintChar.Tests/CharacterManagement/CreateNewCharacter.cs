using EventBasedProgramming.TestSupport;
using FluentAssertions;
using Microsoft.Win32;
using NUnit.Framework;
using PluginApi.Model;
using PrintChar.Tests.zzTestSupportData;

namespace PrintChar.Tests.CharacterManagement
{
	[TestFixture]
	public class CreateNewCharacter
	{
		[Test]
		public void CreateShouldBeDisabledWithOnlyReadOnlySystemsPresent()
		{
			_With(_readOnlyGameSystem).CreateCharCommand.CanExecute(null).Should().BeFalse();
		}

		[Test]
		public void CreateShouldBeEnabledWithAtLeastOneWritableSystemPresent()
		{
			_With(_readOnlyGameSystem, _writableGameSystem).CreateCharCommand.CanExecute(null).Should().BeTrue();
		}

		[Test]
		public void CreateCharCommandShouldBeBoundToTheCorrectMethod()
		{
			var testSubject = _With(_anyGameSystem);
			Assert.That(testSubject.CreateCharCommand, Command.DelegatesTo(() => testSubject.CreateNewCharacter()));
		}

		[Test]
		public void ShouldUseLabelAndExtensionCorrectlyToInitializeOpenDialog()
		{
			_AllGameSystemsViewModelThatAllowsOverridingCurrentCharacter testSubject = _With(_readOnlyGameSystem, _writableGameSystem);
			testSubject.CharacterCreator.CreateDialog(testSubject.Character).ShouldMatch(new
			{
				Filter = "Writable Characters file (*.write)|*.write",
				DefaultExt = _WritableGameSystem.Extension,
				CheckFileExists = false,
				Multiselect = false,
				InitialDirectory = string.Empty
			});
		}

		[Test]
		public void DefaultExtensionShouldBeSameAsCurrentCharacter()
		{
			var nonDefaultSystem = new _WritableGameSystem("unusualextension");
			var testSubject = _With(_readOnlyGameSystem, _writableGameSystem, nonDefaultSystem);
			testSubject.CurrentCharacter = new _SillyCharacter(Data.Anything(), nonDefaultSystem);

			testSubject.CharacterCreator.CreateDialog(testSubject.Character).ShouldMatch(new
			{
				DefaultExt = nonDefaultSystem.FileExtension,
			});
		}

		private static _AllGameSystemsViewModelThatAllowsOverridingCurrentCharacter _With(params GameSystem[] gameSystems)
		{
			return new _AllGameSystemsViewModelThatAllowsOverridingCurrentCharacter(gameSystems);
		}

		[SetUp]
		public void Setup()
		{
			_readOnlyGameSystem = new _ReadOnlyGameSystem();
			_anyGameSystem = new _ReadOnlyGameSystem();
			_writableGameSystem = new _WritableGameSystem();
		}

		private GameSystem _readOnlyGameSystem;
		private GameSystem _anyGameSystem;
		private GameSystem _writableGameSystem;
	}
}
