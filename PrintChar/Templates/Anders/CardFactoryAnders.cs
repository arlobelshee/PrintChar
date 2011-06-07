using System.Windows.Controls;
using PrintChar.Templates.Anders.ViewModels;
using PrintChar.Templates.Anders.Views;

namespace PrintChar.Templates.Anders
{
	public class CardFactoryAnders : IFactory<Control>
	{
		public Control StatsFor(Character data)
		{
			return new CharStatsCard {DataContext = data};
		}

		public Control CardFor(Power data)
		{
			return new PowerCard {DataContext = new PowerViewModel(data)};
		}
	}
}
