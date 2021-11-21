using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using Microsoft.Xaml.Behaviors;

namespace PixelNLayers.Wpf.Controls.Images.LineGrids.Behavior;

internal class LineGridBehavior : Behavior<LineGrid>
{
	private const double Unit = 1.333333333;

	/// <inheritdoc />
	protected override void OnAttached()
	{
		AssociatedObject.Loaded += AssociatedObject_Loaded;
	}

	private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
	{
		Calculate();
	}

	private void Calculate()
	{
		if (AssociatedObject.PixelImageSource is not { } _image) return;
		int height = _image.Image.PixelHeight;
		int width = _image.Image.PixelWidth;

		var lines = new Line[2];
		lines[0] = new Line
		{
			X1 = 0,
			X2 = width * Unit,
			Y1 = height / 2.0 * Unit,
			Y2 = height / 2.0 * Unit,
			Stroke = Brushes.White,
			StrokeThickness = 0.1,
			Opacity = 0.8
		};

		lines[1] = new Line
		{
			X1 = width / 2.0 * Unit,
			X2 = width / 2.0 * Unit,
			Y1 = -height / 2.0 * Unit,
			Y2 = height / 2.0 * Unit,
			Stroke = Brushes.White,
			StrokeThickness = 0.1,
			Opacity = 0.8
		};

		AssociatedObject.Lines = lines;
	}
}