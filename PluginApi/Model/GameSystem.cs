using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Controls;
using JetBrains.Annotations;
using Microsoft.Win32;

namespace PluginApi.Model
{
	public abstract class GameSystem<TCharacter> where TCharacter : Character
	{
		[CanBeNull]
		public TCharacter Character { get; set; }

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

		public void SwitchCharacter()
		{
			LoadCharacter(_Open(CreateOpenDialog()));
		}

		public void LoadCharacter([CanBeNull] string fileName)
		{
			if (string.IsNullOrEmpty(fileName) || (Character != null && Character.File.Location.FullName == fileName))
				return;
			Character = Parse(new CachedFile(new FileInfo(fileName)));
		}

		protected abstract TCharacter Parse(CachedFile characterData);

		[CanBeNull]
		private string _Open([NotNull] OpenFileDialog dialog)
		{
			return dialog.ShowDialog() == true ? dialog.FileName : null;
		}

		[NotNull]
		public OpenFileDialog CreateOpenDialog()
		{
			return new OpenFileDialog
			{
				Filter = string.Format("{0} file ({1})|{1}", Name, FilePattern),
				DefaultExt = FileExtension,
				CheckFileExists = true,
				Multiselect = false,
				InitialDirectory = Character == null ? null : Character.File.Location.DirectoryName
			};
		}
	}
}
