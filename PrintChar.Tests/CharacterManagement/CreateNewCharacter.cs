using System.Linq;
using EventBasedProgramming.TestSupport;
using FluentAssertions;
using JetBrains.Annotations;
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
			_AllGameSystemsViewModelThatAllowsOverridingCurrentCharacter testSubject = _With(_anyGameSystem);
			Assert.That(testSubject.CreateCharCommand, Command.DelegatesTo(() => testSubject.CreateNewCharacter()));
		}

		[Test]
		public void CreateCharCommandShouldIncludeOnlyWritableSystems()
		{
			_With(_readOnlyGameSystem, _writableGameSystem).CharacterCreator.ShouldMatch(new
				{
					GameSystems = new[] { _writableGameSystem },
					RequireFileToExist = false
				});
		}

		[Test]
		public void CharacterOpenerShouldDispatchToGameSystemLoadCharacter()
		{
			var trace = new _TracingGameSystem();
			_With(_AnyGames()).CharacterCreator.Loader(trace, ArbitraryFileName);
			trace.ShouldHaveCreated(ArbitraryFileName);
			trace.ShouldHaveLoadedNothing();
		}

		[NotNull]
		private static _AllGameSystemsViewModelThatAllowsOverridingCurrentCharacter _With(params GameSystem[] gameSystems)
		{
			return new _AllGameSystemsViewModelThatAllowsOverridingCurrentCharacter(gameSystems);
		}

		[NotNull]
		private static GameSystem[] _AnyGames()
		{
			return new GameSystem[] { };
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
		private const string ArbitraryFileName = @"C:\arbitry\path\and\file.whatever";
	}
}
