using System;
using System.Threading.Tasks;
using NUnit.Framework;
using WotcOnlineDataRepository;

namespace PrintChar.Tests
{
	[TestFixture]
	public class FetchOnlineDataForPowers
	{
		[SetUp]
		public void SetUp()
		{
			_testSubject = new OnlineDataFetcher(ServiceFactory.MakeLocalOnlyFakeServiceForTesting());
			_pc = new Character();
		}

		[Test]
		public void AppendOnlineDataToPowerWhenFound()
		{
			_pc.Powers.Add(_MakeLocalPower(TestPowers.PowerSimple));
			var dataArrived = _BindOnlineDataArrivalEventToATask(_pc);
			_testSubject.Update(_pc);
			Assert.That(dataArrived.Wait(TimeSpan.FromMilliseconds(1800)));
			Assert.That(_pc.Powers[0].OnlineData.Name, Is.EqualTo(TestPowers.PowerSimple.Name));
		}

		[Test]
		public void DataFetcherIgnoresPowersWhoseNameDoesNotMatchAnyOfThoseDownloaded()
		{
			_pc.Powers.Add(new Power {PowerId = TestPowers.PowerSimple, Name = "Not the correct value"});
			var dataArrived = _BindOnlineDataArrivalEventToATask(_pc);
			_testSubject.Update(_pc);
			Assert.That(dataArrived.Wait(TimeSpan.FromMilliseconds(180)), Is.False);
			Assert.That(_pc.Powers[0].OnlineData, Is.Null);
		}

		[Test]
		public void DataFetcherIgnoresPowersWithoutIDs()
		{
			_pc.Powers.Add(new Power {PowerId = null});
			var dataArrived = _BindOnlineDataArrivalEventToATask(_pc);
			_testSubject.Update(_pc);
			Assert.That(dataArrived.Wait(TimeSpan.FromMilliseconds(180)), Is.False);
			Assert.That(_pc.Powers[0].OnlineData, Is.Null);
		}

		private static Task<WotcOnlineDataRepository.Power> _BindOnlineDataArrivalEventToATask(Character pc)
		{
			var taskSource = new TaskCompletionSource<WotcOnlineDataRepository.Power>();
			pc.Powers[0].OnlineDataArrived += () => taskSource.SetResult(pc.Powers[0].OnlineData);
			var dataArrived = taskSource.Task;
			return dataArrived;
		}

		private static Power _MakeLocalPower(TestPowers whichPower)
		{
			return new Power {PowerId = (int) whichPower, Name = whichPower.Name};
		}

		private Character _pc;
		private OnlineDataFetcher _testSubject;
	}
}
