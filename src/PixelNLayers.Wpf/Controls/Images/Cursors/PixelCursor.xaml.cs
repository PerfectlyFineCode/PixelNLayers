using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using PixelNLayers.Shared.Extensions;
using PixelNLayers.Shared.Images.Wrapper;

namespace PixelNLayers.Wpf.Controls.Images.Cursors;

/// <summary>
///     Interaction logic for PixelCursor.xaml
/// </summary>
public partial class PixelCursor : Border
{
	public static readonly DependencyProperty PositionXProperty = DependencyProperty.Register(
		nameof(PositionX), typeof(int), typeof(PixelCursor),
		new FrameworkPropertyMetadata(default(int),
			FrameworkPropertyMetadataOptions.AffectsRender,
			PositionChangedCallback));

	public static readonly DependencyProperty PositionYProperty = DependencyProperty.Register(
		nameof(PositionY), typeof(int), typeof(PixelCursor),
		new FrameworkPropertyMetadata(default(int),
			FrameworkPropertyMetadataOptions.AffectsRender,
			PositionChangedCallback));

	public static readonly DependencyProperty SourceProperty = DependencyProperty.Register(
		nameof(Source), typeof(EditableImage), typeof(PixelCursor), new PropertyMetadata(default(EditableImage)));

	public static readonly DependencyProperty CursorBrushProperty = DependencyProperty.Register(
		"CursorBrush", typeof(SolidColorBrush), typeof(PixelCursor),
		new FrameworkPropertyMetadata(default(SolidColorBrush),
			FrameworkPropertyMetadataOptions.AffectsRender));

	public PixelCursor()
	{
		InitializeComponent();
		Loaded += PixelCursor_Loaded;
	}

	public SolidColorBrush CursorBrush
	{
		get => (SolidColorBrush)GetValue(CursorBrushProperty);
		set => SetValue(CursorBrushProperty, value);
	}

	public EditableImage Source
	{
		get => (EditableImage)GetValue(SourceProperty);
		set => SetValue(SourceProperty, value);
	}

	public int PositionY
	{
		get => (int)GetValue(PositionYProperty);
		set => SetValue(PositionYProperty, value);
	}

	public int PositionX
	{
		get => (int)GetValue(PositionXProperty);
		set => SetValue(PositionXProperty, value);
	}

	private void PixelCursor_Loaded(object sender, RoutedEventArgs e)
	{
		Source.Invalidated += Value_Invalidated;
	}

	private void Value_Invalidated(object? sender, EventArgs e)
	{
		Debug.WriteLine("invalidated");
		SetCursorColor();
	}

	private static void PositionChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{

		if (d is not PixelCursor cursor || e.NewValue is not int value) return;
		switch (e.Property.Name)
		{
			case nameof(PositionX):
				cursor.SetValue(Canvas.LeftProperty, value * 1.333333);
				cursor.SetCursorColor();
				break;
			case nameof(PositionY):
				cursor.SetValue(Canvas.TopProperty, value * 1.333333);
				cursor.SetCursorColor();
				break;
		}
	}

	private void SetCursorColor()
	{
		if (Source == DependencyProperty.UnsetValue) return;
		var color = Source[PositionX, PositionY];
		CursorBrush = new SolidColorBrush(color.Reverse());
	}
}