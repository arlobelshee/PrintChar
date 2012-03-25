using NUnit.Framework;
using PluginApi.Model;
using PrintChar.Tests.zzTestSupportData;

namespace PrintChar.Tests.CharacterManagement
{
	[TestFixture]
	public class CreateNewCharacter
	{
		private GameSystem _readOnlyGameSystem;

		[SetUp]
		public void Setup()
		{
			_readOnlyGameSystem = new _ReadOnlyGameSystem();
		}

		[Test]
		public void WithOnlyReadOnlySystemsLoadedCreateShouldBeDisabled()
		{
			_With(_readOnlyGameSystem);
		}

		private static AllGameSystemsViewModel _With(params GameSystem[] gameSystems)
		{
			return new AllGameSystemsViewModel(gameSystems);
		}
	}
}
