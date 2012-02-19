using System;
using System.IO;
using System.Windows.Controls;
using JetBrains.Annotations;
using Plugin.Dnd4e.Templates;
using Plugin.Dnd4e.Templates.Anders;
using PluginApi.Model;

namespace Plugin.Dnd4e
{
	public class GameSystem4E : GameSystem<CharacterDnd4E>
	{
		[NotNull] private IDataFile _configFile;

		[NotNull] private readonly CharacterTransformer<CharacterDnd4E, Control> _compiler = new CharacterTransformer
			<CharacterDnd4E, Control>(
			path => new CharacterFile(path).ToCharacter(),
			GenerateCardsWithFactory<Control>.Using(new CardFactoryAnders()));

		public GameSystem4E() : base("4th Edition D&D", "dnd4e") {}

		protected override CharacterDnd4E Parse(IDataFile characterData)
		{
			var data = new CharacterData4E(characterData);
			throw new NotImplementedException();
			_LocateConfigFile();
			_UpdateCards();
		}

		private void _LocateConfigFile()
		{
			string charFile = Character.File.Location.FullName;
			string fileNameWithoutExtension = charFile.Substring(0, charFile.Length - Path.GetExtension(charFile).Length);
			_configFile = new CachedFile(new FileInfo(fileNameWithoutExtension + ".conf"), false);
		}

		private void _UpdateCards()
		{
			Cards.Clear();
//			_compiler.Compile(new[] {_charFileLocation}).Each(card => Cards.Add(card));
		}
	}
}
