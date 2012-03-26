using EventBasedProgramming.TestSupport;
using JetBrains.Annotations;
using NUnit.Framework;
using PluginApi.Model;
using PrintChar.Tests.zzTestSupportData;
using FluentAssertions;
using System.Linq;

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
				GameSystems = new GameSystem[] {_readOnlySystem, _writableSystem},
				RequireFileToExist = true
			});
		}

		[Test]
		public void CharacterOpenerShouldDispatchToGameSystemLoadCharacter()
		{
			var trace = new _TracingGameSystem();
			_With(_AnyGames()).CharacterOpener.Loader(trace, ArbitraryFileName);
			trace.ShouldHaveLoaded(ArbitraryFileName);
			trace.ShouldHaveCreatedNothing();
		}

		[NotNull]
		private static _AllGameSystemsViewModelThatAllowsOverridingCurrentCharacter _With(params GameSystem[] gameSystems)
		{
			return new _AllGameSystemsViewModelThatAllowsOverridingCurrentCharacter(gameSystems);
		}

		[NotNull]
		private static GameSystem[] _AnyGames()
		{
			return new GameSystem[] {};
		}

		[SetUp]
		public void Setup()
		{
			_writableSystem = new _WritableGameSystem();
			_readOnlySystem = new _ReadOnlyGameSystem();
		}

		private _WritableGameSystem _writableSystem;
		private _ReadOnlyGameSystem _readOnlySystem;
		private const string ArbitraryFileName = @"C:\arbitry\path\and\file.whatever";
	}
}
