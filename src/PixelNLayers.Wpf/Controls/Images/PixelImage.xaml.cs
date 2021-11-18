using System.Windows;
using System.Windows.Controls;
using PixelNLayers.Shared.Images.Wrapper;

namespace PixelNLayers.Wpf.Controls.Images;

/// <summary>
///     Interaction logic for PixelImage.xaml
/// </summary>
public partial class PixelImage : Image
{
	public static readonly DependencyProperty EditableImageSourceProperty = DependencyProperty.Register(
		nameof(EditableImageSource), typeof(EditableImage), typeof(PixelImage),
		new PropertyMetadata(default(EditableImage)));

	public PixelImage()
	{
		InitializeComponent();
	}

	public EditableImage EditableImageSource
	{
		get => (EditableImage)GetValue(EditableImageSourceProperty);
		set => SetValue(EditableImageSourceProperty, value);
	}
}