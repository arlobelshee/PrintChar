using System;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using EventBasedProgramming.Binding;
using EventBasedProgramming.Binding.Impl;
using EventBasedProgramming.TestSupport;
using JetBrains.Annotations;
using PluginApi.Model;
using PrintChar.DesignTimeSupportData;

namespace PrintChar
{
	public class AllGameSystemsViewModel : IFirePropertyChanged
	{
		[NotNull] private readonly GameSystem[] _allGameSystems;
		[NotNull] private readonly TrackingNullableProperty<Character> _currentCharacter;
		private readonly CharacterFileInteraction _characterOpener;
		private readonly CharacterFileInteraction _createNewCharacter;

		public AllGameSystemsViewModel() : this(Plugins.GameSystems()) {}

		public AllGameSystemsViewModel([NotNull] params GameSystem[] gameSystems)
		{
			_allGameSystems = gameSystems;
			_characterOpener = new CharacterFileInteraction(_allGameSystems, true,
				(gameSystem, fileName) => gameSystem.LoadCharacter(fileName));
			_createNewCharacter = new CharacterFileInteraction(_allGameSystems.Where(g => !g.IsReadOnly), false,
				(gameSystem, fileName) => gameSystem.CreateCharacter(fileName));
			_currentCharacter = new TrackingNullableProperty<Character>(this,
				() => Character, () => IsValid, () => CharFileName);
			OpenCharCommand = new SimpleCommand(Always.Enabled, SwitchCharacter);
			CreateCharCommand = new SimpleCommand(_HasAtLeastOneWritableGameSystem, CreateNewCharacter);
		}

		[NotNull]
		public SimpleCommand OpenCharCommand { get; private set; }

		[NotNull]
		public SimpleCommand CreateCharCommand { get; private set; }

		public void SwitchCharacter()
		{
			Character = _characterOpener.LoadCharacter(Character);
		}

		public void CreateNewCharacter()
		{
			Character = _createNewCharacter.LoadCharacter(Character);
		}

		[NotNull]
		public CharacterFileInteraction CharacterOpener
		{
			get { return _characterOpener; }
		}

		[NotNull]
		public CharacterFileInteraction CharacterCreator
		{
			get { return _createNewCharacter; }
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

		private bool _HasAtLeastOneWritableGameSystem()
		{
			return _allGameSystems.FirstOrDefault(gs => gs.IsReadOnly == false) != null;
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
