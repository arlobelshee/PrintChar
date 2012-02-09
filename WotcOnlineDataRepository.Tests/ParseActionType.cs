using System;
using NUnit.Framework;

namespace WotcOnlineDataRepository.Tests
{
	[TestFixture]
	public class ParseActionType
	{
		[Test]
		public void StandardAction()
		{
			Assert.That(ActionType.For("STandArd AcTion"), Is.EqualTo(ActionType.Standard()));
		}

		[Test]
		public void FreeAction()
		{
			Assert.That(ActionType.For("FrEE AcTion"), Is.EqualTo(ActionType.Free()));
		}

		[Test]
		public void MinorAction()
		{
			Assert.That(ActionType.For("mInOR AcTion"), Is.EqualTo(ActionType.Minor()));
		}

		[Test]
		public void ImmediateInterrupt()
		{
			Assert.That(ActionType.For("ImMediatE iNTerruPT"), Is.EqualTo(ActionType.Interrupt()));
		}

		[Test]
		public void ImmediateReaction()
		{
			Assert.That(ActionType.For("immEdiaTe reActiOn"), Is.EqualTo(ActionType.Reaction()));
		}

		[Test]
		public void NoAction()
		{
			Assert.That(ActionType.For("nO AcTion"), Is.EqualTo(ActionType.NoAction()));
		}

		[Test]
		public void MoveAction()
		{
			Assert.That(ActionType.For("mOVe AcTion"), Is.EqualTo(ActionType.Move()));
		}

		[Test]
		public void OpportunityAction()
		{
			Assert.That(ActionType.For("OppOrtunity AcTion"), Is.EqualTo(ActionType.Opportunity()));
		}

		[Test]
		public void UnparsableActionTypeThrowsException()
		{
			Assert.Throws(typeof (NotImplementedException), () => ActionType.For("Frogger action"));
			Assert.Throws(typeof (NotImplementedException), () => ActionType.For("melee 3"));
		}
	}
}
