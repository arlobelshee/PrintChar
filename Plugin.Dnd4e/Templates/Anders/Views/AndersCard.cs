using System.Windows;
using System.Windows.Media;

namespace PrintChar.Templates.Anders.Views
{
	public partial class AndersCard
	{
		public static readonly DependencyProperty CardColorProperty = DependencyProperty.Register(
			"CardColor",
			typeof (Brush),
			typeof (AndersCard),
			new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender)
			);

		public static readonly DependencyProperty ActionTypeIconProperty = DependencyProperty.Register(
			"ActionTypeIcon",
			typeof (ImageSource),
			typeof (AndersCard),
			new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender)
			);

		public static readonly DependencyProperty TargetTypeIconProperty = DependencyProperty.Register(
			"TargetTypeIcon",
			typeof (ImageSource),
			typeof (AndersCard),
			new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender)
			);

		public static readonly DependencyProperty TitleTextProperty = DependencyProperty.Register(
			"TitleText",
			typeof (string),
			typeof (AndersCard),
			new FrameworkPropertyMetadata("<Please set the TitleText property>", FrameworkPropertyMetadataOptions.AffectsRender)
			);

		public static readonly DependencyProperty SubtitleTextProperty = DependencyProperty.Register(
			"SubtitleText",
			typeof (string),
			typeof (AndersCard),
			new FrameworkPropertyMetadata("<Please set the SubtitleText property>", FrameworkPropertyMetadataOptions.AffectsRender)
			);

		public Brush CardColor
		{
			get { return (Brush) GetValue(CardColorProperty); }
			set { SetValue(CardColorProperty, value); }
		}

		public string TitleText
		{
			get { return (string) GetValue(TitleTextProperty); }
			set { SetValue(TitleTextProperty, value); }
		}

		public string SubtitleText
		{
			get { return (string) GetValue(SubtitleTextProperty); }
			set { SetValue(SubtitleTextProperty, value); }
		}

		public ImageSource ActionTypeIcon
		{
			get { return (ImageSource) GetValue(ActionTypeIconProperty); }
			set { SetValue(ActionTypeIconProperty, value); }
		}

		public ImageSource TargetTypeIcon
		{
			get { return (ImageSource) GetValue(TargetTypeIconProperty); }
			set { SetValue(TargetTypeIconProperty, value); }
		}
	}
}
