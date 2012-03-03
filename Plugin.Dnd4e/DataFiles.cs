using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using JetBrains.Annotations;
using Microsoft.Win32;
using Plugin.Dnd4e.Templates;
using Plugin.Dnd4e.Templates.Anders;
using PluginApi.Display.Helpers;
using PluginApi.Model;

namespace Plugin.Dnd4e
{
	public class DataFiles : OnlineRepositoryViewModel, IDisposable
	{
		private readonly ObservableCollection<Control> _allCards;

		[NotNull] private readonly CharacterTransformer<CharacterDnd4E, Control> _compiler = new CharacterTransformer
			<CharacterDnd4E, Control>(
			path => new CharacterFile(path).ToCharacter(),
			GenerateCardsWithFactory<Control>.Using(new CardFactoryAnders()));

		private FileInfo _charFileLocation;
		private CachedFile _configFile;

		public DataFiles()
		{
			OpenCharCommand = new SimpleCommand(() => true, _AskUserToSelectNewCharacter);
			_allCards = new ObservableCollection<Control>();
			_compiler.Add(new OnlineDataFetcher(Repository).Update);
			GameSystem = new GameSystem4E();
		}

		public GameSystem4E GameSystem { get; private set; }

		public void Dispose()
		{
			if (_configFile != null)
				_configFile.Dispose();
		}

		public override event PropertyChangedEventHandler PropertyChanged;

		[NotNull]
		public string Path
		{
			get { return _charFileLocation == null ? string.Empty : _charFileLocation.FullName; }
			set
			{
				if (!value.EndsWith(".dnd4e") || (_charFileLocation != null && _charFileLocation.FullName == value))
					return;
				_charFileLocation = new FileInfo(value);
				_LocateConfigFile();
				_UpdateCards();
				PropertyChanged.Raise(this, () => Path);
				PropertyChanged.Raise(this, () => ConfigData);
				PropertyChanged.Raise(this, () => CharFileName);
				PropertyChanged.Raise(this, () => IsValid);
			}
		}

		public ObservableCollection<Control> Cards
		{
			get { return _allCards; }
		}

		public string CharFileName
		{
			get { return _charFileLocation == null ? string.Empty : _charFileLocation.Name; }
		}

		public string ConfigData
		{
			get { return _configFile == null ? string.Empty : _configFile.Contents; }
			set
			{
				if (_configFile.Contents == value)
					return;
				_configFile.Contents = value;
				PropertyChanged.Raise(this, () => ConfigData);
			}
		}

		public Visibility IsValid
		{
			get { return _charFileLocation != null ? Visibility.Visible : Visibility.Collapsed; }
		}

		public SimpleCommand OpenCharCommand { get; private set; }

		private void _UpdateCards()
		{
			_allCards.Clear();
			_compiler.Compile(new[] {new CachedFile(_charFileLocation),}).Each(card => _allCards.Add(card));
		}

		private void _AskUserToSelectNewCharacter()
		{
			var dialog = new OpenFileDialog
			{
				Filter = "Character Builder file (*.dnd4e)|*.dnd4e",
				DefaultExt = "dnd4e",
				CheckFileExists = true,
				Multiselect = false
			};
			if (_charFileLocation != null)
				dialog.InitialDirectory = _charFileLocation.DirectoryName;
			if (dialog.ShowDialog() == true)
				Path = dialog.FileName;
		}

		private void _LocateConfigFile()
		{
			string charFile = _charFileLocation.FullName;
			string fileNameWithoutExtension = charFile.Substring(0,
				charFile.Length - System.IO.Path.GetExtension(charFile).Length);
			_configFile = new CachedFile(new FileInfo(fileNameWithoutExtension + ".conf"), false);
		}
	}

	public class DataFilesDesignData : DataFiles
	{
		public DataFilesDesignData()
		{
			Path = new FileInfo(@"..\..\..\Plugin.Dnd4e.Tests\SampleData\Shivra.dnd4e").FullName;
		}
	}
}
