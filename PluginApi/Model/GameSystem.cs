using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Controls;
using JetBrains.Annotations;
using Microsoft.Win32;

namespace PluginApi.Model
{
	public abstract class GameSystem
	{
		[CanBeNull]
		public Character Character { get; protected set; }

		protected GameSystem(string systemLabel, string fileExtension)
		{
			Name = systemLabel;
			FileExtension = string.Format("{0}", fileExtension);
			FilePattern = string.Format("*.{0}", fileExtension);
			Cards = new ObservableCollection<Control>();
		}

		[NotNull]
		public string Name { get; private set; }

		[NotNull]
		public string FileExtension { get; private set; }

		[NotNull]
		public string FilePattern { get; private set; }

		public bool IsReadOnly { get; set; }

		[NotNull]
		public Control EditorDisplay { get; private set; }

		[NotNull]
		public ObservableCollection<Control> Cards { get; private set; }

		public Character LoadCharacter([CanBeNull] string fileName)
		{
			if (string.IsNullOrEmpty(fileName) || (Character != null && Character.File.Location.FullName == fileName))
				return null;
			Character = Parse(new CachedFile(new FileInfo(fileName)));
			return Character;
		}

		protected abstract Character Parse(IDataFile characterData);
	}
}
