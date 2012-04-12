using EventBasedProgramming.TestSupport;
using NUnit.Framework;
using SenseOfWonder.Model;
using SenseOfWonder.Tests.zzTestSupportData;
using FluentAssertions;

namespace SenseOfWonder.Tests.RulesEditorAllowsCardEditing
{
	[TestFixture]
	public class UiIsHookedUp
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
	}
}
