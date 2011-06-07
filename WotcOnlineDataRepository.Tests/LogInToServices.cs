using System;
using System.Linq;
using System.Net;
using JetBrains.Annotations;
using NUnit.Framework;

namespace WotcOnlineDataRepository.Tests
{
	[TestFixture, Explicit]
	public class LogInToServices
	{
		/// <summary>
		/// 	This test will only pass if the account you're testing with has some characters.
		/// </summary>
		[Test]
		public void GetMyCharacters()
		{
			var testSubject = _LogInCorrectly();
			Assert.That(testSubject.Raw.AllCharacters().Result, Is.Not.Empty);
		}

		[Test]
		public void LoadDataForOnePowerFromTheCompendium()
		{
			var testSubject = _LogInCorrectly();
			var localData = ServiceFactory.MakeLocalOnlyFakeServiceForTesting().Result;
			Assert.That(testSubject.Raw.PowerData(TestPowers.Monk).Result.WriteTo(), Is.EqualTo(localData.Raw.PowerData(TestPowers.Monk).Result.WriteTo()));
		}

		[Test]
		public void CompendiumClientShouldReportInternalServerErrorsWhenProvidingRawData()
		{
			var testSubject = _LogInCorrectly();
			var powerData = testSubject.Raw.PowerData(TestPowers.Subpower);
			var aggregate = Assert.Throws<AggregateException>(() => { var foo = powerData.Result; });
			Assert.That(aggregate.Flatten().InnerExceptions.Select(ex => ex.GetType()).ToArray(), Is.EqualTo(new []{typeof (WebException)}));
		}

		[Test]
		public void CompendiumClientShouldTolerateInternalServerErrorWhenRequestingAPowerTheServerDoesntKnow()
		{
			var testSubject = _LogInCorrectly();
			var powerData = testSubject.PowerDetails(new[]{(int)TestPowers.Subpower});
			Assert.That(powerData.Result, Is.Empty);
		}

		[Test]
		public void BadPasswordShouldFailToLogIn()
		{
			var realService = ServiceFactory.LogInToRealService(TestCredentials.USERNAME, TestCredentials.WRONG_PASSWORD);
			var outerException = Assert.Throws<AggregateException>(() => { var foo = realService.Result; });
			Assert.That(outerException.InnerExceptions.Select(ex => ex.GetType()), Is.EqualTo(new[] {typeof (LoginFailureException)}));
		}

		[NotNull]
		private static IDnd4ERepository _LogInCorrectly()
		{
			return ServiceFactory.LogInToRealService(TestCredentials.USERNAME, TestCredentials.CORRECT_PASSWORD).Result;
		}
	}
}
