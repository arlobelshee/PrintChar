using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using JetBrains.Annotations;
using PluginApi.Model;

namespace PrintChar
{
	public class Plugins
	{
		[CanBeNull] private static Plugins _instance;
		[NotNull] private readonly CompositionContainer _plugins;

		public Plugins()
		{
			var catalog = new AggregateCatalog();
			catalog.Catalogs.Add(new AssemblyCatalog(typeof (Plugins).Assembly));
			if (Directory.Exists("plugins"))
				catalog.Catalogs.Add(new DirectoryCatalog("plugins"));
			_plugins = new CompositionContainer(catalog);
		}

		public static GameSystem[] GameSystems()
		{
			return _Instance._GameSystems();
		}

		private GameSystem[] _GameSystems()
		{
			return _plugins.GetExports<GameSystem>().Select(g => g.Value).ToArray();
		}

		private static Plugins _Instance
		{
			get { return _instance ?? (_instance = new Plugins()); }
		}
	}
}
