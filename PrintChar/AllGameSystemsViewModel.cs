using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using EventBasedProgramming.Binding;
using JetBrains.Annotations;
using Microsoft.Win32;
using PluginApi.Model;
using PrintChar.DesignTimeSupportData;

namespace PrintChar
{
	public class AllGameSystemsViewModel : IFirePropertyChanged
	{
		[NotNull] private readonly GameSystem[] _allGameSystems;
		[NotNull] private readonly TrackingNullableProperty<Character> _currentCharacter;

		public AllGameSystemsViewModel() : this(Plugins.GameSystems()) {}

		public AllGameSystemsViewModel([NotNull] params GameSystem[] gameSystems)
		{
			_allGameSystems = gameSystems;
			_currentCharacter = new TrackingNullableProperty<Character>(this,
				() => Character, () => IsValid, () => CharFileName);
			OpenCharCommand = new SimpleCommand(() => true, SwitchCharacter);
			CreateCharCommand = new SimpleCommand(_HasAtLeastOneWritableGameSystem, CreateNewCharacter);
		}

		private bool _HasAtLeastOneWritableGameSystem()
		{
			return _allGameSystems.FirstOrDefault(gs => gs.IsReadOnly == false) != null;
		}

		public SimpleCommand OpenCharCommand { get; private set; }
		public SimpleCommand CreateCharCommand { get; private set; }

		public void SwitchCharacter()
		{
			LoadCharacter(_Open(CreateOpenDialog()), (gameSystem, fileName) => gameSystem.LoadCharacter(fileName));
		}

		public void CreateNewCharacter()
		{
			LoadCharacter(_Open(CreateCreateDialog()), (gameSystem, fileName) => gameSystem.LoadCharacter(fileName));
		}

		public void LoadCharacter([CanBeNull] string fileName, Func<GameSystem, string, Character> loader)
		{
			if (string.IsNullOrEmpty(fileName) || (Character != null && Character.File.Location.FullName == fileName))
				return;

			string extensionWithoutPeriod = Path.GetExtension(fileName).Substring(1);
			Character = loader(_allGameSystems.First(g => g.FileExtension == extensionWithoutPeriod), fileName);
		}

		[NotNull]
		public OpenFileDialog CreateOpenDialog()
		{
			return _CreateDialogForSystems(_allGameSystems, true);
		}

		[NotNull]
		public OpenFileDialog CreateCreateDialog()
		{
			return _CreateDialogForSystems(_allGameSystems.Where(g => !g.IsReadOnly), false);
		}

		[NotNull]
		private OpenFileDialog _CreateDialogForSystems([NotNull] IEnumerable<GameSystem> gameSystems, bool requireFileToExist)
		{
			return new OpenFileDialog
			{
				Filter = string.Join("|", gameSystems
					.Select(_FormatGameSystemFileInfo)),
				DefaultExt = (Character == null ? gameSystems.First() : Character.GameSystem).FileExtension,
				CheckFileExists = requireFileToExist,
				Multiselect = false,
				InitialDirectory = Character == null ? null : Character.File.Location.DirectoryName
			};
		}

		[NotNull]
		private static string _FormatGameSystemFileInfo([NotNull] GameSystem g)
		{
			return string.Format("{0} file ({1})|{1}", g.Name, g.FilePattern);
		}

		[CanBeNull]
		public Character Character
		{
			get { return _currentCharacter.Value; }
			protected set { _currentCharacter.Value = value; }
		}

		public bool IsValid
		{
			get { return _currentCharacter.Value != null; }
		}

		[NotNull]
		public string CharFileName
		{
			get { return _currentCharacter.Value == null ? string.Empty : _currentCharacter.Value.File.Location.FullName; }
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public void FirePropertyChanged(Expression<Func<object>> propertyThatChanged)
		{
			PropertyChanged.Raise(this, propertyThatChanged);
		}

		[CanBeNull]
		private string _Open([NotNull] OpenFileDialog dialog)
		{
			return dialog.ShowDialog() == true ? dialog.FileName : null;
		}
	}

	public class DesignTimeGameSystems : AllGameSystemsViewModel
	{
		private readonly DesignTimeGameSystem _gameSystem;

		public DesignTimeGameSystems() : this(new DesignTimeGameSystem())
		{
			Character = new TheDesigner(_gameSystem);
		}

		private DesignTimeGameSystems(DesignTimeGameSystem gameSystem) : base(gameSystem)
		{
			_gameSystem = gameSystem;
		}
	}
}
