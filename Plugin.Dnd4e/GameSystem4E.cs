using System;
using PluginApi.Model;

namespace Plugin.Dnd4e
{
	public class GameSystem4E : GameSystem
	{
		public GameSystem4E() : base("4th Edition D&D", "dnd4e") { }

		protected override Character Parse(CachedFile characterData)
		{
			throw new NotImplementedException();
		}
	}
}