using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using PluginApi.Display;
using PluginApi.Model;

namespace PrintChar.DesignTimeSupportData
{
	public class DesignTimeGameSystem : GameSystem
	{
		public DesignTimeGameSystem() : base("Design Time Game", "designdatachar")
		{
		}

		public override Character Parse(IDataFile characterData)
		{
			throw new NotImplementedException();
		}

		public override ObservableCollection<TabItem> EditorPages
		{
			get
			{
				return new ObservableCollection<TabItem>
				{
					new TabItem
					{
						Header = "First tab",Content = new Button() { Content = "A big button that would really be a control for editing the character in some way." }
					},
					new TabItem
					{
						Header = "Second tab",Content = new Button() { Content = "A second button" }
					}
				};
			}
		}
	}
}