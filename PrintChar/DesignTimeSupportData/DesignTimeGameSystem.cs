using System;
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
	}
}