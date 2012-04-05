using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using SenseOfWonder.Model;
using SenseOfWonder.Tests.zzTestSupportData;
using SenseOfWonder.Views;

namespace SenseOfWonder.Tests.CharactersHaveCards
{
	[TestFixture, Explicit]
	public class ExposeCards
	{
		[Test]
		public void FirstCardShouldBeCharacterSummaryCard()
		{
			WonderCharacter robert = _TestData.Character("Robert", string.Empty);
			var firstCard = robert.Cards.FirstOrDefault();
			firstCard.Should()
				.BeOfType<CharacterSummaryCard>()
				.And.ShouldMatch(new {DataContext=robert});
		}
	}
}
