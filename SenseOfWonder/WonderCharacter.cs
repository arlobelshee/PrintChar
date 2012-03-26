using JetBrains.Annotations;
using PluginApi.Model;

namespace SenseOfWonder
{
	public class WonderCharacter : Character<SenseOfWonderSystem>
	{
		public WonderCharacter([NotNull] SenseOfWonderSystem system, IDataFile characterData) : base(system)
		{
			File = characterData;
		}
	}

	public class WonderCharacterDesignData : WonderCharacter
	{
		public WonderCharacterDesignData() : base(new SenseOfWonderSystem(), Data.Anything())
		{
			Name = "Wonderful Character";
			Gender = "Female";
		}
	}
}
