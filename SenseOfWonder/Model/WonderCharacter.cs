using JetBrains.Annotations;
using PluginApi.Model;
using SenseOfWonder.Model.Impl;

namespace SenseOfWonder.Model
{
	public class WonderCharacter : Character<SenseOfWonderSystem>
	{
		public WonderCharacter([NotNull] SenseOfWonderSystem system, IDataFile characterData) : base(system)
		{
			File = characterData;
			var serializer = new CharSerializer(this, characterData);
			serializer.LoadFromFile();
			PropertyChanged += serializer.UpdateFile;
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
