using FluentAssertions;
using NUnit.Framework;
using SenseOfWonder.Model.Serialization;
using SenseOfWonder.Tests.zzTestSupportData;

namespace SenseOfWonder.Tests.RulesEditorAllowsCardEditing
{
	[TestFixture]
	public class EditingCardWritesItToDisk
	{
		[Test]
		public void CardShouldNotifyObserversWhenNameIsChanged()
		{
			var testSubject = new CardData
			{
				Name = "initial name"
			};
			testSubject.MonitorEvents();
			testSubject.Name = "new value";
			testSubject.ShouldRaisePropertyChangeFor(s => s.Name);
		}

		[Test]
		public void RulesEditorShouldNotifyThatPersistableDataHasChangedWhenAnyCardDataChanges()
		{
			var testSubject = _TestData.EmptyRulesetCharacter();
			testSubject.MonitorEvents();
			testSubject.Name = "new card name";
			testSubject.CreateUnboundCard();
			testSubject.ShouldNotRaisePropertyChangeFor(self => self.PersistableData);
		}
	}
}
