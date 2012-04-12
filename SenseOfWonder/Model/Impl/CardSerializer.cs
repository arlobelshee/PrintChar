using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using PluginApi.Model;
using ServiceStack.Text;

namespace SenseOfWonder.Model.Impl
{
	public class CardSerializer
	{
		private readonly WonderCardHolder _who;
		private readonly IDataFile _backingStore;

		public CardSerializer(WonderCardHolder who, IDataFile backingStore)
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
			_backingStore.Contents = WonderRulesData.From(_who).ToJson();
		}

		public void LoadFromFile()
		{
			_who.UpdateFrom(_backingStore.Contents.FromJson<WonderRulesData>() ?? new WonderRulesData());
		}
	}
}
