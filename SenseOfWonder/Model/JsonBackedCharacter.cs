using PluginApi.Model;
using SenseOfWonder.Model.Impl;

namespace SenseOfWonder.Model
{
	public class JsonBackedCharacter<TGameSystem> : Character<TGameSystem> where TGameSystem : GameSystem
	{
		public JsonBackedCharacter(TGameSystem system) : base(system) {}

		public WonderCharData PersistableData
		{
			get
			{
				return new WonderCharData
				{
					Name = Name,
					Gender = Gender
				};
			}
		}

		public void UpdateFrom(WonderCharData wonderCharData)
		{
			Name = wonderCharData.Name ?? string.Empty;
			Gender = wonderCharData.Gender ?? string.Empty;
		}
	}
}
