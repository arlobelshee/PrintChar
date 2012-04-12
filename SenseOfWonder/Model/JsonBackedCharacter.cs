using PluginApi.Model;
using SenseOfWonder.Model.Impl;

namespace SenseOfWonder.Model
{
	public abstract class JsonBackedCharacter<TGameSystem> : Character<TGameSystem>, IPersistTo<WonderCharData>
		where TGameSystem : GameSystem
	{
		protected JsonBackedCharacter(TGameSystem system) : base(system) {}

		public abstract WonderCharData PersistableData { get; }
		public abstract void UpdateFrom(WonderCharData wonderCharData);
	}
}
