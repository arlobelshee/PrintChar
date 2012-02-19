using System.Windows.Controls;
using Plugin.Dnd4e;
using Plugin.Dnd4e.Templates.Anders.ViewModels;
using Plugin.Dnd4e.Templates.Anders.Views;

namespace Plugin.Dnd4e.Templates.Anders
{
	public class CardFactoryAnders : IFactory<Control>
	{
		public Control StatsFor(CharacterDnd4E data)
		{
			return new CharStatsCard {DataContext = data};
		}

		public Control CardFor(Power data)
		{
			return new PowerCard {DataContext = new PowerViewModel(data)};
		}
	}
}
