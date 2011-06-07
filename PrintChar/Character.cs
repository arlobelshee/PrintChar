using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using JetBrains.Annotations;

namespace PrintChar
{
	public class Character : INotifyPropertyChanged, IFirePropertyChanged
	{
		[NotNull] private readonly TrackingNonNullProperty<string> _gender;
		[NotNull] private readonly TrackingNonNullProperty<string> _name;
		[NotNull] private readonly TrackingNonNullProperty<string> _race;
		[NotNull] private readonly TrackingNonNullProperty<string> _charClass;
		[NotNull] private readonly ObservableCollection<Power> _powers = new ObservableCollection<Power>();
		[NotNull] private readonly EqualityFingerprint _equalityFields;

		public Character()
		{
			_gender = new TrackingNonNullProperty<string>(string.Empty, this, ()=> Gender, ()=>SummaryLine);
			_name = new TrackingNonNullProperty<string>(string.Empty, this, () => Name);
			_race = new TrackingNonNullProperty<string>(string.Empty, this, () => Race, () => SummaryLine);
			_charClass = new TrackingNonNullProperty<string>(string.Empty, this, () => CharClass, () => SummaryLine);
			_equalityFields = new EqualityFingerprint(_gender, _name, _race, _charClass);
		}

		public event PropertyChangedEventHandler PropertyChanged;

		[NotNull]
		public string SummaryLine
		{
			get { return string.Format("{0} {1} {2}", _gender, _race, _charClass); }
		}

		[NotNull]
		public string Name
		{
			get { return _name.Value; }
			set { _name.Value = value; }
		}

		[NotNull]
		public ObservableCollection<Power> Powers
		{
			get { return _powers; }
		}

		[NotNull]
		public string Gender
		{
			get { return _gender.Value; }
			set { _gender.Value = value; }
		}

		[NotNull]
		public string Race
		{
			get { return _race.Value; }
			set { _race.Value = value; }
		}

		[NotNull]
		public string CharClass
		{
			get { return _charClass.Value; }
			set { _charClass.Value = value; }
		}

		public override string ToString()
		{
			return string.Format("Character(Name: {0}, Gender: {2}, Powers: [{1}])", _name, string.Join(", ", _powers), _gender);
		}

		public bool Equals(Character other)
		{
			if (ReferenceEquals(null, other))
				return false;
			if (ReferenceEquals(this, other))
				return true;
			return _powers.SequenceEqual(other._powers) && Equals(other._equalityFields, _equalityFields);
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as Character);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var result = _powers.GetHashCode();
				result = (result*397) ^ _equalityFields.GetHashCode();
				return result;
			}
		}

		public void FirePropertyChanged(Expression<Func<object>> propertyThatChanged)
		{
			PropertyChanged.Raise(this, propertyThatChanged);
		}
	}

	public class CharacterDesignData : Character
	{
		public CharacterDesignData()
		{
			Name = "Sam Ple O'data";
			Gender = "Male";
			Race = "Gnome";
			CharClass = "Illusionist";
		}
	}
}
