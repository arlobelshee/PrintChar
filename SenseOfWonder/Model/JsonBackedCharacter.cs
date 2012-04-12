using PluginApi.Model;
using SenseOfWonder.Model.Impl;

namespace SenseOfWonder.Model
{
	public abstract class JsonBackedCharacter<TGameSystem, TPersistableData> : Character<TGameSystem>, IPersistTo<TPersistableData>
		where TGameSystem : GameSystem
	{
		protected JsonBackedCharacter(TGameSystem system) : base(system) {}

		public abstract TPersistableData PersistableData { get; }
		public abstract void UpdateFrom(TPersistableData wonderCharData);
	}
}
