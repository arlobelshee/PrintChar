using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using Plugin.Dnd4e;
using PluginApi.Display.Helpers;
using PluginApi.Model;

namespace PrintChar
{
	public class AllGameSystemsViewModel : IFirePropertyChanged
	{
		private List<GameSystem> _allGameSystems = new List<GameSystem>
		{
			new GameSystem4E()
		};
		public event PropertyChangedEventHandler PropertyChanged;

		public void FirePropertyChanged(Expression<Func<object>> propertyThatChanged)
		{
			PropertyChanged.Raise(this, propertyThatChanged);
		}
	}
}
