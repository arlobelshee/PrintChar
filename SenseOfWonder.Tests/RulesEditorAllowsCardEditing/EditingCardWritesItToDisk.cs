using System;
using System.Linq;
using EventBasedProgramming.Binding;
using FluentAssertions;
using FluentAssertions.Assertions;
using FluentAssertions.EventMonitoring;
using NUnit.Framework;
using SenseOfWonder.Model;
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
			WonderRulesCharacter testSubject = _TestData.EmptyRulesetCharacter();
			testSubject.Name = "new card name";
			testSubject.CreateUnboundCard();
			PropagateChangesTo(testSubject.CardData.First().Should(), () => testSubject.PersistableData);
		}

		private void PropagateChangesTo(ObjectAssertions changeNotifier, Func<object> propertyThatShouldChange)
		{
			changeNotifier.BeAssignableTo<IFirePropertyChanged>();
			//changeNotifier.As<IFirePropertyChanged>().PropertyChanged += sf
		}
	}
}
