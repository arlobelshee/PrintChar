﻿using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using PluginApi.Model;

namespace SenseOfWonder.Model
{
	[Export(typeof (GameSystem))]
	public class SenseOfWonderCards : GameSystem
	{
		public SenseOfWonderCards() : base("Sense of Wonder Rule System", "wonderrules")
		{
			IsReadOnly = true;
		}

		public override Character Parse(IDataFile characterData)
		{
			return WonderRulesCharacter.Load(this, characterData);
		}

		public override ObservableCollection<TabItem> EditorPages
		{
			get
			{
				return new ObservableCollection<TabItem>
				{
					new TabItem
					{
						Header = "New Cards",
						Content = new CreateNewCard()
					}
				};
			}
		}
	}
}
