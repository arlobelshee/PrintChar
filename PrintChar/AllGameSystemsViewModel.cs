using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
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

		public AllGameSystemsViewModel() : this(new GameSystem4E()) {}

		public AllGameSystemsViewModel(params GameSystem[] gameSystems)
		{
			_allGameSystems = gameSystems;
		}

		public void SwitchCharacter()
		{
			LoadCharacter(_Open(CreateOpenDialog()));
		}

		public void LoadCharacter([CanBeNull] string fileName)
		{
			if (string.IsNullOrEmpty(fileName) || (Character != null && Character.File.Location.FullName == fileName))
				return;

			var extensionWithoutPeriod = Path.GetExtension(fileName).Substring(1);
			Character = _allGameSystems.First(g=>g.FileExtension == extensionWithoutPeriod).LoadCharacter(fileName);
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

	public class AllGameSystemsViewModelDesignData : AllGameSystemsViewModel
	{
		public AllGameSystemsViewModelDesignData() : base(new GameSystem4E())
		{
			LoadCharacter(@"..\..\..\Plugin.Dnd4e.Tests\SampleData\Shivra.dnd4e");
		}
	}
}
