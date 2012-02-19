using System.Windows.Controls;
using JetBrains.Annotations;
using Microsoft.Win32;

namespace PluginApi.Model
{
	public class GameSystem
	{
		[CanBeNull]
		public CachedFile CharacterFile { get; set; }

		public GameSystem(string systemLabel, string fileExtension)
		{
			Name = systemLabel;
			FileExtension = string.Format("{0}", fileExtension);
			FilePattern = string.Format("*.{0}", fileExtension);
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

		public void SwitchCharacter()
		{
			LoadCharacter(_Open(CreateOpenDialog()));
		}

		public void LoadCharacter([CanBeNull] string fileName)
		{
		}

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
				InitialDirectory = CharacterFile == null ? null : CharacterFile.Location.DirectoryName
			};
		}
	}
}
