using NUnit.Framework;
using Plugin.Dnd4e.Templates.Anders;
using Plugin.Dnd4e.Templates.Anders.ViewModels;

namespace Plugin.Dnd4e.Tests
{
	[TestFixture, Explicit, RequiresSTA]
	public class AndersFactoryCreatesValidCards
	{
		[SetUp]
		public void SetUp()
		{
			_testSubject = new CardFactoryAnders();
		}

		[Test]
		public void CreateACharacterStatsCard()
		{
			var data = new CharacterDnd4E();
			var card = _testSubject.StatsFor(data);
			Assert.That(card.DataContext, Is.SameAs(data));
		}

		[Test]
		public void CreateAPowerCard()
		{
			var data = new Power {Name = "The correct name"};
			var card = _testSubject.CardFor(data);
			var dataContext = card.DataContext as PowerViewModel;
			Assert.That(dataContext, Is.Not.Null);
			Assert.That(dataContext.Name, Is.EqualTo(data.Name));
		}

		private CardFactoryAnders _testSubject;
	}
}
