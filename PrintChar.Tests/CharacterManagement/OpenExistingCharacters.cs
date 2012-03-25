using EventBasedProgramming.TestSupport;
using JetBrains.Annotations;
using NUnit.Framework;
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
			_AllGameSystemsViewModelThatAllowsOverridingCurrentCharacter testSubject = _With(_AnyGames());
			Assert.That(testSubject.OpenCharCommand,
				Command.DelegatesTo(() => Always.Enabled(), () => testSubject.SwitchCharacter()));
		}

		[Test]
		public void CharacterOpenerShouldUseAllGameSystems()
		{
			_With(_readOnlySystem, _writableSystem).CharacterOpener.ShouldMatch(new
			{
				GameSystems = new GameSystem[] {_readOnlySystem, _writableSystem},
				RequireFileToExist = true
			});
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

		private _WritableGameSystem _writableSystem;
		private _ReadOnlyGameSystem _readOnlySystem;

		[SetUp]
		public void Setup()
		{
			_writableSystem = new _WritableGameSystem();
			_readOnlySystem = new _ReadOnlyGameSystem();
		}
	}
}
