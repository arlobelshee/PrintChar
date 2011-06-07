using System.Linq;
using JetBrains.Annotations;
using NUnit.Framework;
using PrintChar.Templates;

namespace PrintChar.Tests
{
	[TestFixture]
	public class DefaultGeneratorMakesCorrectCards
	{
		[SetUp]
		public void SetUp()
		{
			_testSubject = new GenerateCardsWithFactory<string>(new _NameUsingFakeFactory());
			_bobby = new Character
			{
				Name = "Bobby"
			};
			_bobby.Powers.Add(new Power
			{
				Name = "Big Fist"
			});
			_bobby.Powers.Add(new Power
			{
				Name = "Popgun"
			});
		}

		[Test]
		public void FirstCardShouldBeStatsCard()
		{
			Assert.That(_testSubject.Generate(_bobby).First(), Is.EqualTo("Bobby's stats"));
		}

		[Test]
		public void ShouldContainOneCardPerPower()
		{
			Assert.That(new[] {"The Big Fist power", "The Popgun power"}, Is.SubsetOf(_testSubject.Generate(_bobby)));
		}

		[Test]
		public void ShouldSkipHiddenPowerCards()
		{
			_bobby.Powers[0].Hidden = true;
			Assert.That(_testSubject.Generate(_bobby).ToArray(), Has.None.EqualTo("The Big Fist power"));
		}

		private class _NameUsingFakeFactory : IFactory<string>
		{
			public string StatsFor(Character data)
			{
				return string.Format("{0}'s stats", data.Name);
			}

			public string CardFor(Power data)
			{
				return string.Format("The {0} power", data.Name);
			}
		}

		[NotNull] private Character _bobby;
		[NotNull] private GenerateCardsWithFactory<string> _testSubject;
	}
}
