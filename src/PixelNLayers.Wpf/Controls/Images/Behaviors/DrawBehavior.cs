using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Xaml.Behaviors;
using PixelNLayers.Shared.Images.Recorder;
using PixelNLayers.Shared.Images.Wrapper;

namespace PixelNLayers.Wpf.Controls.Images.Behaviors;

#nullable disable
internal class DrawBehavior : Behavior<PixelImage>
{
	public static readonly DependencyProperty SelectedColorProperty = DependencyProperty.Register(
		nameof(SelectedColor), typeof(Color), typeof(DrawBehavior), new PropertyMetadata(default(Color)));

	private EditableImage _image;

	public Color SelectedColor
	{
		get => (Color)GetValue(SelectedColorProperty);
		set => SetValue(SelectedColorProperty, value);
	}

	private bool IsEditing { get; set; }

	private int PixelX { get; set; }
	private int PixelY { get; set; }

	private bool IsDrawing { get; set; }

	protected override void OnAttached()
	{
		AssociatedObject.MouseDown += AssociatedObject_MouseDown;
		AssociatedObject.MouseMove += AssociatedObject_MouseMove;
		AssociatedObject.MouseUp += AssociatedObject_MouseUp;
		AssociatedObject.MouseEnter += AssociatedObject_MouseEnter;
		AssociatedObject.MouseLeave += AssociatedObject_MouseLeave;
		AssociatedObject.Loaded += AssociatedObject_Loaded;
	}

	private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
	{
		_image = AssociatedObject.EditableImageSource;
	}

	private void AssociatedObject_MouseLeave(object sender, MouseEventArgs e)
	{
		if (IsEditing)
			IsEditing = false;

		e.Handled = true;
	}

	private void AssociatedObject_MouseEnter(object sender, MouseEventArgs e)
	{
		if (!IsEditing)
			IsEditing = true;

		e.Handled = true;
	}

	private void AssociatedObject_MouseUp(object sender, MouseButtonEventArgs e)
	{
		if (e.ChangedButton != MouseButton.Left) return;
		IsDrawing = false;
		SetPixelAtPosition();
		Undo.EndRecord(_image);
		AssociatedObject.ReleaseMouseCapture();
		e.Handled = true;
	}

	private void AssociatedObject_MouseMove(object sender, MouseEventArgs e)
	{
		var pos = e.GetPosition(AssociatedObject);

		PixelX = (int)(pos.X / AssociatedObject.ActualWidth * _image.Image.PixelWidth);
		PixelY = (int)(pos.Y / AssociatedObject.ActualHeight * _image.Image.PixelHeight);

		if (IsDrawing)
			SetPixelAtPosition();
	}

	private void SetPixelAtPosition()
	{
		int x = PixelX;
		int y = PixelY;
		if (x < 0 || y < 0 || x >= _image.Image.PixelWidth || y >= _image.Image.PixelHeight) return;
		_image[PixelX, PixelY] = SelectedColor;
	}

	private void AssociatedObject_MouseDown(object sender, MouseButtonEventArgs e)
	{
		if (e.ChangedButton != MouseButton.Left) return;
		if (IsDrawing) return;
		if (!AssociatedObject.CaptureMouse()) return;
		Undo.BeginRecord(_image, "Started drawing ..");
		IsDrawing = true;
	}
}