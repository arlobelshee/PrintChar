using System.Collections.Generic;
using NUnit.Framework;

namespace WotcOnlineDataRepository.Tests
{
	[TestFixture]
	public class ParsePowers
	{
		[SetUp]
		public void SetUp()
		{
			_testSubject = ServiceFactory.MakeServiceThatWrapsTestDataSource(ServiceFactory.MakeLocalOnlyFakeServiceForTesting().Result.Raw).Result;
		}

		[Test]
		public void FindTheName()
		{
			Assert.That(_MonkPower().Name, Is.EqualTo(TestPowers.Monk.Name));
		}

		[Test]
		public void FindTheDescription()
		{
			Assert.That(_ArdentPower().Description,
				Is.EqualTo(new[]
				{
					new Descriptor("Trigger", "An enemy targets you with a melee attack"), new Descriptor("Target", "The triggering enemy"),
					new Descriptor("Attack", "Charisma vs. AC"),
					new Descriptor("Hit", "1[W] + Charisma modifier damage, and you slide the target 1 square to a square adjacent to you."),
					new Descriptor("Effect", "You lose your standard action on your next turn."), new Descriptor("Augment 1", ""),
					new Descriptor("Hit", "As above, and one ally adjacent to you can shift to any unoccupied square adjacent to the target’s new position as a free action."),
					new Descriptor("Augment 2", ""),
					new Descriptor("Hit",
						"1[W] + Charisma modifier damage, and you slide the target a number of squares equal to your Wisdom modifier. You then slide each enemy now adjacent to the target 1 square.")
					, new Descriptor("Effect", "You do not lose your standard action on your next turn."),
				}));
		}

		[Test]
		public void MissingLevelIsParsedToNull()
		{
			var monkPower = _MonkPower();
			Assert.That(monkPower.Source, Is.EqualTo("Monk"));
			Assert.That(monkPower.Type, Is.EqualTo("Feature"));
			Assert.That(monkPower.Level, Is.Null);
		}

		[Test]
		public void FindTheSourceTypeAndLevel()
		{
			var ardentPower = _ArdentPower();
			Assert.That(ardentPower.Source, Is.EqualTo("Ardent"));
			Assert.That(ardentPower.Type, Is.EqualTo("Attack"));
			Assert.That(ardentPower.Level, Is.EqualTo(3));
		}

		[Test]
		public void WhenARequestReturnsMultiplePowersParseThemBoth()
		{
			Assert.That(_GetPower(TestPowers.PowerWithSubpower).Keys, Is.EquivalentTo(new[] {TestPowers.PowerWithSubpower.Name, TestPowers.Subpower.Name}));
		}

		[Test]
		public void JustIgnoreRequestsThatFail()
		{
			Assert.That(_GetPower(TestPowers.Subpower), Is.Empty);
		}

		[Test]
		public void StillGetDataForValidRequestsEvenWhenSomeFail()
		{
			Assert.That(_GetPower(TestPowers.Subpower, TestPowers.Monk).Keys, Is.EquivalentTo(new[] {TestPowers.Monk.Name}));
		}

		[Test]
		public void CleanWorksWithOnlyOnePower()
		{
			const string original = @"<div id='detail'>This text should be removed.<h1>Something</h1><p>First</p><p>Second</p></div>";
			const string cleaned = @"<div id='detail'><div><h1>Something</h1><p>First</p><p>Second</p></div></div>";
			_AssertCleansUpAs(original, cleaned);
		}

		[Test]
		public void CleanWorksWithTwoPowers()
		{
			const string original = @"<div id='detail'><h1>Power 1</h1><p>First</p><p>Second</p><h1>Second Power</h1><p>Third</p></div>";
			const string cleaned = @"<div id='detail'><div><h1>Power 1</h1><p>First</p><p>Second</p></div><div><h1>Second Power</h1><p>Third</p></div></div>";
			_AssertCleansUpAs(original, cleaned);
		}

		[Test]
		public void CleanHandlesFloatingLabelsLikeOnAugmentablePowers()
		{
			const string original = @"<div id='detail'><h1>Something</h1><p>First</p><b>Augment 1</b><br><p>Second</p></div>";
			const string cleaned = @"<div id='detail'><div><h1>Something</h1><p>First</p><p><b>Augment 1</b></p><p>Second</p></div></div>";
			_AssertCleansUpAs(original, cleaned);
		}

		private Power _ArdentPower()
		{
			var whichPower = TestPowers.Ardent;
			return _GetPower(whichPower)[whichPower.Name];
		}

		private static void _AssertCleansUpAs(string original, string cleaned)
		{
			var target = original.ToDetailsNode();
			Power.Clean(target);
			Assert.That(target.WriteTo(), Is.EqualTo(cleaned));
		}

		private Dictionary<string, Power> _GetPower(params int[] whichPowers)
		{
			return _testSubject.PowerDetails(whichPowers).Result;
		}

		private Power _MonkPower()
		{
			var whichPower = TestPowers.Monk;
			return _GetPower(whichPower)[whichPower.Name];
		}

		private IDnd4ERepository _testSubject;
	}
}
