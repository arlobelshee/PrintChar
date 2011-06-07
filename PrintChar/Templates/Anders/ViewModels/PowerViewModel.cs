using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using JetBrains.Annotations;
using WotcOnlineDataRepository;
using System.Linq;

namespace PrintChar.Templates.Anders.ViewModels
{
	public class PowerViewModel : INotifyPropertyChanged
	{
		[NotNull] private static readonly ResourceDictionary _ANDERS_RESOURCES = Template.ResourceDict("Anders", "AndersResources.xaml");
		[NotNull] private readonly Power _data;

		public PowerViewModel([NotNull] Power data)
		{
			_data = data;
			_data.OnlineDataArrived += () => PropertyChanged.Raise(this, () => Description);
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public string Description
		{
			get
			{
				var onlineData = _data.OnlineData;
				return onlineData == null ? string.Empty : string.Join("\r\n", onlineData.Description);
			}
		}

		public string Name
		{
			get { return _data.Name; }
			set
			{
				if (_data.Name == value)
					return;
				_data.Name = value;
				PropertyChanged.Raise(this, () => Name);
			}
		}

		public bool Hidden
		{
			get { return _data.Hidden; }
			set { _data.Hidden = value; }
		}

		public virtual Brush CardColor
		{
			get { return (Brush) _ANDERS_RESOURCES[string.Format("Power_Refresh_{0}", _data.Refresh)]; }
		}

		public string ActionName
		{
			get { return _data.Action.ToString(); }
		}

		public Uri ActionIcon
		{
			get { return Template.Resource("Anders", string.Format("images/{0}.png", _data.Action.IfValid(action=>action.IconCode))); }
		}

		public Uri TargetIcon
		{
			get { return Template.Resource("Anders", string.Format("images/{0}.png", _data.Target.IfValid(target=>target.IconCode))); }
		}
	}

	public class PowerDesignData : PowerViewModel
	{
		public PowerDesignData()
			: base(
				new Power
				{
					Name = "A Really Cool Power",
					Refresh = Power.Usage.Encounter,
					Action = ActionType.Move(),
					Target = TargetType.AnyWeapon,
					OnlineData =
						new WotcOnlineDataRepository.Power
						{
							Description =
								{
									new Descriptor("Sample label", "These are all the details for this label"),
									new Descriptor("Augment 664", string.Empty),
									new Descriptor("Para 2", "A whole bunch more details. A whole bunch more details. A whole bunch more details. A whole bunch more details. A whole bunch more details. A whole bunch more details. A whole bunch more details. A whole bunch more details. A whole bunch more details. A whole bunch more details. A whole bunch more details.")
								}
						}
				}) {}
	}
}
