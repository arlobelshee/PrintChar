using System.IO;
using System.Threading.Tasks;
using JetBrains.Annotations;
using PluginApi.Model;

namespace Plugin.Dnd4e
{
	public class CharacterData4E
	{
		public CharacterData4E(IDataFile primaryFile)
		{
			_charData = Task<CharacterFile>.Factory.StartNew(() => new CharacterFile(primaryFile));
			_configData = Task<IDataFile>.Factory.StartNew(() => _LoadConfigData(primaryFile));
		}

		private static IDataFile _LoadConfigData(IDataFile primaryFile)
		{
			var data = primaryFile.FromSameDirectory(Path.GetFileNameWithoutExtension(primaryFile.Location.Name) + ".conf");
			data.EnsureCacheIsCurrent();
			return data;
		}

		private readonly Task<CharacterFile> _charData;
		private readonly Task<IDataFile> _configData;

		[NotNull]
		public CharacterFile CharData
		{
			get { return _charData.Result; }
		}

		[NotNull]
		public IDataFile ConfigData
		{
			get { return _configData.Result; }
		}

		[NotNull]
		public CharacterDnd4E ToCharacter([NotNull] GameSystem4E system)
		{
			return _charData.Result.ToCharacter(system);
		}
	}
}