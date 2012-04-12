using PluginApi.Model;
using SenseOfWonder.Model.Impl;

namespace SenseOfWonder.Model
{
	public abstract class JsonBackedCharacter<TGameSystem, TPersistableData> : Character<TGameSystem>,
		IPersistTo<TPersistableData>
		where TGameSystem : GameSystem where TPersistableData : class, new()
	{
		protected JsonBackedCharacter(TGameSystem system) : base(system) {}

		public abstract TPersistableData PersistableData { get; }
		public abstract void UpdateFrom(TPersistableData characterData);

		protected Character FinishCreate(IDataFile characterData)
		{
			var serializer = new CharSerializer<TPersistableData>(this, characterData);
			PropertyChanged += serializer.UpdateFile;
			return this;
		}

		protected Character FinishLoad(IDataFile characterData)
		{
			var serializer = new CharSerializer<TPersistableData>(this, characterData);
			serializer.LoadFromFile();
			PropertyChanged += serializer.UpdateFile;
			return this;
		}
	}
}
