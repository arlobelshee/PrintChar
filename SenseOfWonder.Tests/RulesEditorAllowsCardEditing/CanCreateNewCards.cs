using System.Linq;
using EventBasedProgramming.TestSupport;
using FluentAssertions;
using NUnit.Framework;
using SenseOfWonder.Model;
using SenseOfWonder.Model.Serialization;
using SenseOfWonder.Tests.zzTestSupportData;
using FluentAssertions.EventMonitoring;

namespace SenseOfWonder.Tests.RulesEditorAllowsCardEditing
{
	[TestFixture]
	public class CanCreateNewCards
	{
		[Test]
		public void ShouldBeAbleToCreateNewCardOnlyWhenNameIsProvided()
		{
			WonderRulesCharacter testSubject = _TestData.EmptyRulesetCharacter();
			testSubject.Name = string.Empty;
			testSubject.CreateCardCommand.CanExecute(null).Should().BeFalse();
			testSubject.Name = "No longer empty";
			testSubject.CreateCardCommand.CanExecute(null).Should().BeTrue();
		}

		[Test]
		public void CreateNewCardCommandShouldBeBoundToCorrectMethod()
		{
			WonderRulesCharacter testSubject = _TestData.EmptyRulesetCharacter();
			Assert.That(testSubject.CreateCardCommand,
				Command.DelegatesTo(() => testSubject.CreateCard()));
		}

		[Test, RequiresSTA, Explicit]
		public void CreatingNewCardShouldCreateBothDataAndTheControlAndSendProperNotification()
		{
			// This test checks a bunch of things at once because CreateCard actually creates controls, so
			// is very expensive to call.
			WonderRulesCharacter testSubject = _TestData.EmptyRulesetCharacter();
			const string cardName = "The new card";
			testSubject.Name = cardName;

			testSubject.MonitorEvents();
			testSubject.CreateCard();

			testSubject.CardData.Select(c => c.Name).Should().Equal(new object[] {cardName});
			testSubject.Cards.Select(c => c.DataContext.As<CardData>().Name).Should().Equal(new object[] {cardName});

			testSubject.ShouldRaisePropertyChangeFor(s => s.CardData);
		}
	}
}
