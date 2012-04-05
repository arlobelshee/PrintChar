using System.ComponentModel;
using PluginApi.Model;
using ServiceStack.Text;

namespace SenseOfWonder.Model.Impl
{
	public class CharSerializer
	{
		private readonly WonderCharacter _who;
		private readonly IDataFile _backingStore;

		public CharSerializer(WonderCharacter who, IDataFile backingStore)
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
			_backingStore.Contents = WonderCharData.From(_who).ToJson();
		}

		public void LoadFromFile() {}
	}
}
