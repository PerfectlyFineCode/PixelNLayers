using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Xaml.Behaviors;
using PixelNLayers.Wpf.Controls.Grids;

namespace PixelNLayers.Wpf.Controls.Canvases.Behaviors;

#nullable disable
internal class ZoomBehavior : Behavior<DraggableCanvas>
{
	private const double ScaleDown = 1 / 1.1;
	private const double ScaleUp = 1.1;

	public static readonly DependencyProperty MinThresholdProperty = DependencyProperty.Register(
		nameof(MinThreshold), typeof(double), typeof(ZoomBehavior),
		new PropertyMetadata(0.2));

	public static readonly DependencyProperty MaxThresholdProperty = DependencyProperty.Register(
		nameof(MaxThreshold), typeof(double), typeof(ZoomBehavior),
		new PropertyMetadata(100.0));

	private MovableGrid _grid;
	private MatrixTransform _transform;

	public double MaxThreshold
	{
		get => (double)GetValue(MaxThresholdProperty);
		set => SetValue(MaxThresholdProperty, value);
	}

	public double MinThreshold
	{
		get => (double)GetValue(MinThresholdProperty);
		set => SetValue(MinThresholdProperty, value);
	}

	protected override void OnAttached()
	{
		AssociatedObject.Loaded += AssociatedObject_Loaded;
	}

	private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
	{
		if (AssociatedObject.ParentControl == DependencyProperty.UnsetValue)
			throw new NullReferenceException("ParentControl has not been set!");

		_grid = AssociatedObject.ParentControl;

		_grid.MouseWheel += _grid_MouseWheel;

		if (AssociatedObject.RenderTransform is not TransformGroup group) return;
		_transform = group.Children[2] as MatrixTransform;
	}

	// Scale up / down towards cursor position
	private void _grid_MouseWheel(object sender, MouseWheelEventArgs e)
	{
		double scale = e.Delta > 0 ? ScaleUp : ScaleDown;
		//double value = AssociatedObject.Zoom + scale;
		//AssociatedObject.Zoom = Math.Clamp(value, MinThreshold, MaxThreshold);

		var pos = e.GetPosition(_grid);

		var mat = _transform.Matrix;

		mat.ScaleAt(scale, scale, pos.X, pos.Y);

		double calc = mat.M11 / mat.M22;
		Debug.WriteLine(calc);
		if (calc == 0) return;
		mat.M11 = Math.Clamp(mat.M11, MinThreshold, MaxThreshold);
		mat.M22 = Math.Clamp(mat.M22, MinThreshold, MaxThreshold);

		_transform.Matrix = mat;

	}
}