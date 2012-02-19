using System;
using System.Threading.Tasks;
using NUnit.Framework;
using WotcOnlineDataRepository;

namespace Plugin.Dnd4e.Tests
{
	[TestFixture]
	public class FetchOnlineDataForPowers
	{
		[SetUp]
		public void SetUp()
		{
			_testSubject = new OnlineDataFetcher(ServiceFactory.MakeLocalOnlyFakeServiceForTesting());
			_pc = new CharacterDnd4E();
		}

		[Test]
		public void ShouldFindOnlineDataForPowers()
		{
			_pc.Powers.Add(_MakeLocalPower(TestPowers.PowerSimple));
			var dataArrived = _BindOnlineDataArrivalEventToATask(_pc);
			_testSubject.Update(_pc);
			Assert.That(dataArrived.Wait(TimeSpan.FromMilliseconds(180)));
			Assert.That(dataArrived.Result.Name, Is.EqualTo(TestPowers.PowerSimple.Name));
		}

		[Test]
		public void ShouldSafelyIgnorePowersNotFoundOnServer()
		{
			_pc.Powers.Add(new Power {PowerId = TestPowers.PowerSimple, Name = "Not the correct value"});
			var dataArrived = _BindOnlineDataArrivalEventToATask(_pc);
			_testSubject.Update(_pc);
			Assert.That(dataArrived.Wait(TimeSpan.FromMilliseconds(180)), Is.False);
			Assert.That(_pc.Powers[0].OnlineData, Is.Null);
		}

		[Test]
		public void ShouldIgnorePowersWithoutIDs()
		{
			_pc.Powers.Add(new Power {PowerId = null});
			var dataArrived = _BindOnlineDataArrivalEventToATask(_pc);
			_testSubject.Update(_pc);
			Assert.That(dataArrived.Wait(TimeSpan.FromMilliseconds(180)), Is.False);
			Assert.That(_pc.Powers[0].OnlineData, Is.Null);
		}

		private static Task<WotcOnlineDataRepository.Power> _BindOnlineDataArrivalEventToATask(CharacterDnd4E pc)
		{
			var taskSource = new TaskCompletionSource<WotcOnlineDataRepository.Power>();
			pc.Powers[0].OnlineDataArrived += () => taskSource.SetResult(pc.Powers[0].OnlineData);
			return taskSource.Task;
		}

		private static Power _MakeLocalPower(TestPowers whichPower)
		{
			return new Power {PowerId = (int) whichPower, Name = whichPower.Name};
		}

		private CharacterDnd4E _pc;
		private OnlineDataFetcher _testSubject;
	}
}
