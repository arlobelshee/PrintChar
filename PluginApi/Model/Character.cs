using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq.Expressions;
using System.Windows.Controls;
using EventBasedProgramming.Binding;
using JetBrains.Annotations;

namespace PluginApi.Model
{
	public abstract class Character : IFirePropertyChanged, IEquatable<Character>
	{
		[NotNull] protected readonly TrackingNonNullProperty<string> _gender;
		[NotNull] protected readonly TrackingNonNullProperty<string> _name;
		[NotNull] protected readonly EqualityFingerprint EqualityFields;

		[NotNull] private readonly Lazy<ObservableCollection<Control>> _cards;

		public virtual event PropertyChangedEventHandler PropertyChanged;

		protected Character()
		{
			_cards = new Lazy<ObservableCollection<Control>>(MakeCards);
			_gender = new TrackingNonNullProperty<string>(string.Empty, this, () => Gender);
			_name = new TrackingNonNullProperty<string>(string.Empty, this, () => Name);
			EqualityFields = new EqualityFingerprint(_gender, _name);
		}

		[NotNull]
		public abstract GameSystem GameSystem { get; }

		[NotNull]
		public string Name
		{
			get { return _name.Value; }
			set { _name.Value = value; }
		}

		[NotNull]
		public string Gender
		{
			get { return _gender.Value; }
			set { _gender.Value = value; }
		}

		[NotNull]
		public ObservableCollection<Control> Cards
		{
			get { return _cards.Value; }
		}

		[NotNull]
		public FileInfo File { get; protected set; }

		public void FirePropertyChanged(Expression<Func<object>> propertyThatChanged)
		{
			PropertyChanged.Raise(this, propertyThatChanged);
		}

		public bool Equals(Character other)
		{
			if (ReferenceEquals(null, other))
				return false;
			if (ReferenceEquals(this, other))
				return true;
			return Equals(other.EqualityFields, EqualityFields);
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as Character);
		}

		public override int GetHashCode()
		{
			return EqualityFields.GetHashCode();
		}

		protected virtual void AddInitialCards(ObservableCollection<Control> cards) {}

		private ObservableCollection<Control> MakeCards()
		{
			var cards = new ObservableCollection<Control>();
			AddInitialCards(cards);
			return cards;
		}
	}

	public class Character<TGameSystem> : Character where TGameSystem : GameSystem
	{
		public Character([NotNull] TGameSystem system)
		{
			Require.NotNull(system);
			System = system;
		}

		[NotNull]
		public TGameSystem System { get; private set; }

		public override GameSystem GameSystem
		{
			get { return System; }
		}
	}
}
