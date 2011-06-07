using System.Collections.Generic;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace WotcOnlineDataRepository
{
	public interface IDnd4ERepository
	{
		IRawDnd4ERepository Raw { get; }
		Task<Dictionary<string, Power>> PowerDetails(IEnumerable<int> powerIds);
	}

	public interface IRawDnd4ERepository
	{
		Task<List<XmlDetailsData>> AllCharacters();
		Task<HtmlNode> PowerData(int powerId);
	}
}
