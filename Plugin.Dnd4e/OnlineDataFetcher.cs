using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using WotcOnlineDataRepository;

namespace Plugin.Dnd4e
{
	public class OnlineDataFetcher
	{
		private Task<IDnd4ERepository> _repository;

		public OnlineDataFetcher(Task<IDnd4ERepository> repository)
		{
			_repository = repository;
		}

		public Task<IDnd4ERepository> Repository
		{
			set { _repository = value; }
		}

		public void Update([NotNull] CharacterDnd4E pc)
		{
			var powersToUpdate = pc.Powers.ToList(); // make a copy, in case underlying data changes while we're talking to the servers.
			_WhenLoggedIn(repo => _AddOnlineDataToPowers(repo, powersToUpdate));
		}

		private void _WhenLoggedIn([NotNull] Action<IDnd4ERepository> whatToDo)
		{
			_repository.ContinueWith(repo => whatToDo(repo.Result));
		}

		private static void _AddOnlineDataToPowers(IDnd4ERepository repo, [NotNull] List<Power> powersToUpdate)
		{
			var powerDetails = repo.PowerDetails(powersToUpdate.Where(power => power.PowerId.HasValue).Select(power => power.PowerId.Value));
			powerDetails.ContinueWith(allPowers => powersToUpdate.Each(power =>
			{
				WotcOnlineDataRepository.Power powerData;
				if (allPowers.Result.TryGetValue(power.Name, out powerData))
					power.OnlineData = powerData;
			}));
		}
	}
}
