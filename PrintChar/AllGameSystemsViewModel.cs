using System;
using System.ComponentModel;
using System.Linq.Expressions;
using JetBrains.Annotations;
using Microsoft.Win32;
using Plugin.Dnd4e;
using PluginApi.Display.Helpers;
using PluginApi.Model;

namespace PrintChar
{
	public class AllGameSystemsViewModel : IFirePropertyChanged
	{
		[NotNull] private readonly GameSystem[] _allGameSystems;
		[NotNull] private readonly GameSystem _currentGameSystem;

		public AllGameSystemsViewModel() : this(new GameSystem4E()) {}

		public AllGameSystemsViewModel(params GameSystem[] gameSystems)
		{
			_allGameSystems = gameSystems;
			_currentGameSystem = _allGameSystems[0];
		}

		public void SwitchCharacter()
		{
			LoadCharacter(_Open(CreateOpenDialog()));
		}

		public void LoadCharacter(string fileName)
		{
			if (string.IsNullOrEmpty(fileName) || (Character != null && Character.File.Location.FullName == fileName))
				return;
			Character = _currentGameSystem.LoadCharacter(fileName);
		}

		[NotNull]
		public OpenFileDialog CreateOpenDialog()
		{
			return new OpenFileDialog
			{
				Filter = string.Format("{0} file ({1})|{1}", _currentGameSystem.Name, _currentGameSystem.FilePattern),
				DefaultExt = _currentGameSystem.FileExtension,
				CheckFileExists = true,
				Multiselect = false,
				InitialDirectory = Character == null ? null : Character.File.Location.DirectoryName
			};
		}

		public Character Character { get; protected set; }

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
}
