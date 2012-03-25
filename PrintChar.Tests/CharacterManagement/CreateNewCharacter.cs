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
		private GameSystem _readOnlyGameSystem;
		private GameSystem _anyGameSystem;
		private GameSystem _writableGameSystem;
		private AllGameSystemsViewModel testSubject;

		[SetUp]
		public void Setup()
		{
			_readOnlyGameSystem = new _ReadOnlyGameSystem();
			_anyGameSystem = new _ReadOnlyGameSystem();
			_writableGameSystem = new _WritableGameSystem();
		}

		[Test]
		public void WithOnlyReadOnlySystemsCreateShouldBeDisabled()
		{
			_With(_readOnlyGameSystem).CreateCharCommand.CanExecute(null).Should().BeFalse();
		}

		[Test]
		public void WithSomeWritableSystemsCreateShouldBeEnabled()
		{
			_With(_readOnlyGameSystem, _writableGameSystem).CreateCharCommand.CanExecute(null).Should().BeTrue();
		}

		[Test]
		public void CreateCharCommandShouldBeBoundToTheCorrectMethod()
		{
			testSubject = _With(_anyGameSystem);
			Assert.That(testSubject.CreateCharCommand, Command.DelegatesTo(() => testSubject.CreateNewCharacter()));
		}

		private static AllGameSystemsViewModel _With(params GameSystem[] gameSystems)
		{
			return new AllGameSystemsViewModel(gameSystems);
		}
	}
}
