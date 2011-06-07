using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using HtmlAgilityPack;
using WotcOnlineDataRepository.WotcServices.Content;
using WotcOnlineDataRepository.WotcServices.Workspace;

namespace WotcOnlineDataRepository
{
	internal class RawDnd4ERepository : IRawDnd4ERepository
	{
		private readonly IContentVaultService _content;
		private readonly ID20WorkspaceService _workspace;
		private readonly CompendiumClient _compendium;

		public RawDnd4ERepository(IContentVaultService content, ID20WorkspaceService workspace, CompendiumClient compendium)
		{
			_content = content;
			_workspace = workspace;
			_compendium = compendium;
		}

		public Task<List<XmlDetailsData>> AllCharacters()
		{
			return
				_content.BeginGetAvailableContent(new GetAvailableContentRequest((int) ContentId.ContentType.Character), null, null).ToTask(
					result => _content.EndGetAvailableContent(result).GetAvailableContentResult.Select(ctnt => new XmlDetailsData(ctnt.CommittedContent)).ToList());
		}

		public Task<HtmlNode> PowerData(int powerId)
		{
			return _compendium.GetPower(powerId);
		}
	}
}