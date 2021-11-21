using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using PixelNLayers.Shared.Images.Wrapper;

namespace PixelNLayers.Wpf.Controls.Images.LineGrids;

/// <summary>
///     Interaction logic for LineGrid.xaml
/// </summary>
public partial class LineGrid : ItemsControl
{
	public static readonly DependencyProperty ShowGridProperty = DependencyProperty.Register(
		nameof(ShowGrid), typeof(bool), typeof(LineGrid), new PropertyMetadata(default(bool)));

	public static readonly DependencyProperty LinesProperty = DependencyProperty.Register(
		nameof(Lines), typeof(Line[]), typeof(LineGrid),
		new PropertyMetadata(Array.Empty<Line>()));

	public static readonly DependencyProperty PixelImageSourceProperty = DependencyProperty.Register(
		nameof(PixelImageSource), typeof(EditableImage), typeof(LineGrid),
		new PropertyMetadata(default(EditableImage)));

	public LineGrid()
	{
		InitializeComponent();
	}

	public EditableImage PixelImageSource
	{
		get => (EditableImage)GetValue(PixelImageSourceProperty);
		set => SetValue(PixelImageSourceProperty, value);
	}

	public Line[] Lines
	{
		get => (Line[])GetValue(LinesProperty);
		set => SetValue(LinesProperty, value);
	}

	public bool ShowGrid
	{
		get => (bool)GetValue(ShowGridProperty);
		set => SetValue(ShowGridProperty, value);
	}
}