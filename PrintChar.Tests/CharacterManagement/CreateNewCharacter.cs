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

		[Test, Ignore("Work in progress. Test body is wrong.")]
		public void CreateCharCommandShouldBeBoundToTheCorrectMethod()
		{
			_With(_anyGameSystem).CreateCharCommand.CanExecute(null).Should().BeFalse();
		}

		private static AllGameSystemsViewModel _With(params GameSystem[] gameSystems)
		{
			return new AllGameSystemsViewModel(gameSystems);
		}
	}
}
