using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using PixelNLayers.Wpf.Controls.Grids;

namespace PixelNLayers.Wpf.Controls.Canvases;

/// <summary>
///     Interaction logic for DraggableCanvas.xaml
/// </summary>
public partial class DraggableCanvas : Canvas
{
	public static readonly DependencyProperty ParentControlProperty = DependencyProperty.Register(
		nameof(ParentControl), typeof(MovableGrid), typeof(DraggableCanvas),
		new PropertyMetadata(default(MovableGrid)));

	public static readonly DependencyProperty ZoomProperty = DependencyProperty.Register(
		nameof(Zoom), typeof(double), typeof(DraggableCanvas),
		new PropertyMetadata(10.0));

	public static readonly DependencyProperty ImageProperty = DependencyProperty.Register(
		nameof(Image), typeof(WriteableBitmap), typeof(DraggableCanvas),
		new PropertyMetadata(default(WriteableBitmap)));

	public static readonly DependencyProperty PositionProperty = DependencyProperty.Register(
		nameof(Position), typeof(Point), typeof(DraggableCanvas), new PropertyMetadata(default(Point)));

	public DraggableCanvas()
	{
		InitializeComponent();
	}

	public double Zoom
	{
		get => (double)GetValue(ZoomProperty);
		set => SetValue(ZoomProperty, value);
	}

	public MovableGrid ParentControl
	{
		get => (MovableGrid)GetValue(ParentControlProperty);
		set => SetValue(ParentControlProperty, value);
	}

	public Point Position
	{
		get => (Point)GetValue(PositionProperty);
		set => SetValue(PositionProperty, value);
	}

	public WriteableBitmap Image
	{
		get => (WriteableBitmap)GetValue(ImageProperty);
		set => SetValue(ImageProperty, value);
	}
}