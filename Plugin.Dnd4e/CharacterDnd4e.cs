using System.Collections.ObjectModel;
using JetBrains.Annotations;
using PluginApi.Model;
using Plugin.Dnd4e;
using PrintChar;

namespace Plugin.Dnd4e
{
	public class CharacterDnd4E : Character
	{
		[NotNull] private readonly TrackingNonNullProperty<string> _race;
		[NotNull] private readonly TrackingNonNullProperty<string> _charClass;
		[NotNull] private readonly ObservableCollection<Power> _powers = new ObservableCollection<Power>();

		public CharacterDnd4E()
		{
			_gender.AddDependantProperty(() => SummaryLine);
			_race = new TrackingNonNullProperty<string>(string.Empty, this, () => Race, () => SummaryLine);
			_charClass = new TrackingNonNullProperty<string>(string.Empty, this, () => CharClass, () => SummaryLine);
			EqualityFields.Add(_race, _charClass);
			EqualityFields.AddSequences(_powers);
		}

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
