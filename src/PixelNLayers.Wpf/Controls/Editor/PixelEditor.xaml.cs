using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PixelNLayers.Wpf.Controls.Editor;

/// <summary>
///     Interaction logic for PixelEditor.xaml
/// </summary>
public partial class PixelEditor : UserControl
{
	public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register(
		nameof(ImageSource), typeof(ImageSource), typeof(PixelEditor),
		new PropertyMetadata(default(ImageSource)));

	public PixelEditor()
	{
		InitializeComponent();
	}

	public ImageSource ImageSource
	{
		get => (ImageSource)GetValue(ImageSourceProperty);
		set => SetValue(ImageSourceProperty, value);
	}
}