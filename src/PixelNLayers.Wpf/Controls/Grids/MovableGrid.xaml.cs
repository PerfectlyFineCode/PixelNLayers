using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace PixelNLayers.Wpf.Controls.Grids;

/// <summary>
///     Interaction logic for MovableGrid.xaml
/// </summary>
public partial class MovableGrid : UserControl
{
	public static readonly DependencyProperty ImageProperty = DependencyProperty.Register(
		nameof(Image), typeof(WriteableBitmap), typeof(MovableGrid),
		new PropertyMetadata(default(WriteableBitmap)));

	public static readonly DependencyProperty GridViewportProperty = DependencyProperty.Register(
		nameof(GridViewport), typeof(Rect), typeof(MovableGrid),
		new PropertyMetadata(new Rect(0, 0, 20, 20)));

	public MovableGrid()
	{
		InitializeComponent();
	}

	public Rect GridViewport
	{
		get => (Rect)GetValue(GridViewportProperty);
		set => SetValue(GridViewportProperty, value);
	}

	public WriteableBitmap Image
	{
		get => (WriteableBitmap)GetValue(ImageProperty);
		set => SetValue(ImageProperty, value);
	}
}