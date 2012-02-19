using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using JetBrains.Annotations;
using WotcOnlineDataRepository;

namespace PrintChar.Templates.Anders.ViewModels
{
	public class PowerViewModel : INotifyPropertyChanged
	{
		[NotNull] private static readonly ResourceDictionary _ANDERS_RESOURCES = Template.ResourceDict("Anders",
			"AndersResources.xaml");

		[NotNull] private readonly Power _data;

		public PowerViewModel([NotNull] Power data)
		{
			_data = data;
			_data.OnlineDataArrived += _NotifyAboutNewOnlineData;
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public FlowDocument Description
		{
			get { return _data.OnlineData.ToDocument(_FormatTheDocument, _FormatEachParagraph); }
		}

		public string PowerCategorization
		{
			get
			{
				return _data.OnlineData == null ? string.Empty : string.Format("{0} {1} {2}", _data.OnlineData.Source, _data.OnlineData.Type, _data.OnlineData.Level);
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
			get { return Template.Resource("Anders", string.Format("images/{0}.png", _data.Action.IfValid(action => action.IconCode))); }
		}

		public Uri TargetIcon
		{
			get { return Template.Resource("Anders", string.Format("images/{0}.png", _data.Target.IfValid(target => target.IconCode))); }
		}

		private void _NotifyAboutNewOnlineData()
		{
			PropertyChanged.Raise(this, () => Description);
			PropertyChanged.Raise(this, () => PowerCategorization);
		}

		private static void _FormatTheDocument([NotNull] FlowDocument result)
		{
			result.PagePadding = new Thickness(0);
			result.FontFamily = new FontFamily("Palatino Linotype");
			result.FontSize = 12.0;
		}

		private static void _FormatEachParagraph([NotNull] Paragraph result)
		{
			result.Margin = new Thickness(0);
		}
	}

	public class PowerDesignData : PowerViewModel
	{
		private static readonly IDnd4ERepository _CARD_DATA = ServiceFactory.MakeLocalOnlyFakeServiceForTesting().Result;
		public PowerDesignData() : base(new Power
		{
			Name = "A Really Cool Power",
			Refresh = Power.Usage.Encounter,
			Action = ActionType.Move(),
			Target = TargetType.AnyWeapon,
			OnlineData = TestPowers.PowerWithAugments.LoadFrom(_CARD_DATA)
		}) {}
	}
}
