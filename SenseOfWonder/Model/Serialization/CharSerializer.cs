using System.ComponentModel;
using PluginApi.Model;
using ServiceStack;

namespace SenseOfWonder.Model.Serialization
{
	public class CharSerializer<TPersistableData> where TPersistableData : class, new()
	{
		private readonly IPersistTo<TPersistableData> _who;
		private readonly IDataFile _backingStore;

		public CharSerializer(IPersistTo<TPersistableData> who, IDataFile backingStore)
		{
			_who = who;
			_backingStore = backingStore;
		}

		public void UpdateFile(object sender, PropertyChangedEventArgs e)
		{
			UpdateFile();
		}

		public void UpdateFile()
		{
			_backingStore.Contents = _who.PersistableData.ToJson();
		}

		public void LoadFromFile()
		{
			_who.UpdateFrom((_backingStore.Contents.FromJson<TPersistableData>() ?? new TPersistableData()));
		}
	}
}
