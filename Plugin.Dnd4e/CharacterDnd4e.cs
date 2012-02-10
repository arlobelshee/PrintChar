using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using JetBrains.Annotations;
using PluginApi.Model;
using PrintChar;

namespace Plugin.Dnd4e
{
	public class CharacterDnd4E : Character
	{
		[NotNull] private readonly TrackingNonNullProperty<string> _race;
		[NotNull] private readonly TrackingNonNullProperty<string> _charClass;
		[NotNull] private readonly ObservableCollection<Power> _powers = new ObservableCollection<Power>();
		[NotNull] private readonly EqualityFingerprint _equalityFields;

		public CharacterDnd4E()
		{
			_gender.AddDependantProperty(() => SummaryLine);
			_race = new TrackingNonNullProperty<string>(string.Empty, this, () => Race, () => SummaryLine);
			_charClass = new TrackingNonNullProperty<string>(string.Empty, this, () => CharClass, () => SummaryLine);
			_equalityFields = new EqualityFingerprint(_gender, _name, _race, _charClass);
		}

		public override event PropertyChangedEventHandler PropertyChanged;

		[NotNull]
		public string SummaryLine
		{
			get { return string.Format("{0} {1} {2}", _gender, _race, _charClass); }
		}

		[NotNull]
		public ObservableCollection<Power> Powers
		{
			get { return _powers; }
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

		public bool Equals(CharacterDnd4E other)
		{
			if (ReferenceEquals(null, other))
				return false;
			if (ReferenceEquals(this, other))
				return true;
			return _powers.SequenceEqual(other._powers) && Equals(other._equalityFields, _equalityFields);
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as CharacterDnd4E);
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
	}

	public class CharacterDnd4EDesignData : CharacterDnd4E
	{
		public CharacterDnd4EDesignData()
		{
			Name = "Sam Ple O'data";
			Gender = "Male";
			Race = "Gnome";
			CharClass = "Illusionist";
		}
	}
}
