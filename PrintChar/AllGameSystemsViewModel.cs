using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using JetBrains.Annotations;
using Microsoft.Win32;
using PluginApi.Display.Helpers;
using PluginApi.Model;

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
			_currentCharacter = new TrackingNullableProperty<Character>(this, () => Character, () => IsValid);
			OpenCharCommand = new SimpleCommand(() => true, SwitchCharacter);
			CreateCharCommand = new SimpleCommand(_HasAtLesatOneWritableGameSystem, SwitchCharacter);
		}

		private bool _HasAtLesatOneWritableGameSystem()
		{
			return _allGameSystems.FirstOrDefault(gs => gs.IsReadOnly == false) != null;
		}

		public SimpleCommand OpenCharCommand { get; private set; }
		public SimpleCommand CreateCharCommand { get; private set; }

		public void SwitchCharacter()
		{
			LoadCharacter(_Open(CreateOpenDialog()));
		}

		public void LoadCharacter([CanBeNull] string fileName)
		{
			if (string.IsNullOrEmpty(fileName) || (Character != null && Character.File.Location.FullName == fileName))
				return;

			string extensionWithoutPeriod = Path.GetExtension(fileName).Substring(1);
			Character = _allGameSystems.First(g => g.FileExtension == extensionWithoutPeriod).LoadCharacter(fileName);
		}

		[NotNull]
		public OpenFileDialog CreateOpenDialog()
		{
			return new OpenFileDialog
			{
				Filter = string.Join("|",
					_allGameSystems.Select(g => string.Format("{0} file ({1})|{1}", g.Name, g.FilePattern))),
				DefaultExt = (Character == null ? _allGameSystems[0] : Character.GameSystem).FileExtension,
				CheckFileExists = true,
				Multiselect = false,
				InitialDirectory = Character == null ? null : Character.File.Location.DirectoryName
			};
		}

		[CanBeNull]
		public Character Character
		{
			get { return _currentCharacter.Value; }
			protected set { _currentCharacter.Value = value; }
		}

		public bool IsValid
		{
			get { return _currentCharacter.Value == null; }
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

	public class ViewModelWithShivraSelected : AllGameSystemsViewModel {}
}
