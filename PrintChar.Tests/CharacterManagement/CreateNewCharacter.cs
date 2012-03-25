using EventBasedProgramming.TestSupport;
using FluentAssertions;
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

		private static AllGameSystemsViewModel _With(params GameSystem[] gameSystems)
		{
			return new AllGameSystemsViewModel(gameSystems);
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
