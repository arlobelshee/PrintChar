using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using HtmlAgilityPack;
using JetBrains.Annotations;
using WotcOnlineDataRepository.WotcServices.Content;
using WotcOnlineDataRepository.WotcServices.Workspace;

namespace WotcOnlineDataRepository
{
	internal class Dnd4ERepository : IDnd4ERepository
	{
		public Dnd4ERepository(IContentVaultService content, ID20WorkspaceService workspace, CompendiumClient compendium)
		{
			Raw = new RawDnd4ERepository(content, workspace, compendium);
		}

		internal Dnd4ERepository(IRawDnd4ERepository dataSource)
		{
			Raw = dataSource;
		}

		public IRawDnd4ERepository Raw { get; private set; }

		public Task<Dictionary<string, Power>> PowerDetails(IEnumerable<int> powerIds)
		{
			var tasks = powerIds.Select(id => Raw.PowerData(id)).ToArray();
			return Task<Dictionary<string, Power>>.Factory.ContinueWhenAll(tasks, Power.ParseToPowersDict);
		}
	}
}
