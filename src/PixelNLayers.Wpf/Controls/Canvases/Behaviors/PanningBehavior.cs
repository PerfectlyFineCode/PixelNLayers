using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Xaml.Behaviors;
using PixelNLayers.Wpf.Controls.Grids;

namespace PixelNLayers.Wpf.Controls.Canvases.Behaviors;

#nullable disable
internal class PanningBehavior : Behavior<DraggableCanvas>
{
	private MovableGrid _grid;
	private MatrixTransform _transform;
	private Point InitialPoint;
	private Point InitialPosition;
	private bool IsDragging { get; set; }

	private void SetGridViewport(Point position)
	{
		var viewPort = _grid.GridViewport;
		viewPort.X = position.X;
		viewPort.Y = position.Y;
		_grid.GridViewport = viewPort;
	}

	/// <inheritdoc />
	protected override void OnAttached()
	{
		AssociatedObject.Loaded += AssociatedObject_Loaded;
	}

	private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
	{
		if (AssociatedObject.ParentControl == DependencyProperty.UnsetValue)
			throw new NullReferenceException("ParentControl has not been set!");

		_grid = AssociatedObject.ParentControl;

		AssociatedObject.ParentControl.PreviewMouseDown += ParentControl_MouseDown;
		AssociatedObject.ParentControl.PreviewMouseUp += ParentControl_MouseUp;
		AssociatedObject.ParentControl.PreviewMouseMove += ParentControl_MouseMove;

		if (AssociatedObject.RenderTransform is not TransformGroup group) return;
		_transform = group.Children[2] as MatrixTransform;
	}

	private void ParentControl_MouseMove(object sender, MouseEventArgs e)
	{
		if (!IsDragging) return;
		var pos = e.GetPosition(_grid) - InitialPoint;

		double dampening = Math.Max(0.0001, _transform.Matrix.M11);

		var p = InitialPosition + (Vector)new Point(pos.X / dampening, pos.Y / dampening);
		AssociatedObject.Position = p;
		SetGridViewport(p);
	}

	private void ParentControl_MouseUp(object sender, MouseButtonEventArgs e)
	{
		if (e.ChangedButton != MouseButton.Left) return;
		IsDragging = false;
		_grid.ReleaseMouseCapture();
	}

	private void ParentControl_MouseDown(object sender, MouseButtonEventArgs e)
	{
		if (e.ChangedButton != MouseButton.Left || !Keyboard.IsKeyDown(Key.Space)) return;
		InitialPoint = e.GetPosition(_grid);
		InitialPosition = AssociatedObject.Position;
		IsDragging = _grid.CaptureMouse();
	}
}