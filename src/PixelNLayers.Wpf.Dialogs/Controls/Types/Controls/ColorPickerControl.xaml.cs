#nullable disable

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using PixelNLayers.Wpf.Dialogs.Host;
using PixelNLayers.Wpf.Dialogs.Interfaces;

namespace PixelNLayers.Wpf.Dialogs.Controls.Types.Controls;

/// <summary>
///     Interaction logic for ColorPickerControl.xaml
/// </summary>
internal partial class ColorPickerControl : UserControl, IDialogControl
{
	public static readonly DependencyProperty CurrentColorProperty = DependencyProperty.Register(
		nameof(CurrentColor), typeof(Color), typeof(ColorPickerControl),
		new PropertyMetadata(default(Color)));

	public static readonly DependencyProperty ColorAProperty = DependencyProperty.Register(
		nameof(ColorA), typeof(byte), typeof(ColorPickerControl),
		new PropertyMetadata(default(byte), ColorPropertyChanged));

	public static readonly DependencyProperty ColorRProperty = DependencyProperty.Register(
		nameof(ColorR), typeof(byte), typeof(ColorPickerControl),
		new PropertyMetadata(default(byte), ColorPropertyChanged));

	public static readonly DependencyProperty ColorGProperty = DependencyProperty.Register(
		nameof(ColorG), typeof(byte), typeof(ColorPickerControl),
		new PropertyMetadata(default(byte), ColorPropertyChanged));

	public static readonly DependencyProperty ColorBProperty = DependencyProperty.Register(
		nameof(ColorB), typeof(byte), typeof(ColorPickerControl),
		new PropertyMetadata(default(byte), ColorPropertyChanged));

	public ColorPickerControl()
	{
		InitializeComponent();
		((IDialogControl)this).Activate(this);
	}

	public byte ColorA
	{
		get => (byte)GetValue(ColorAProperty);
		set => SetValue(ColorAProperty, value);
	}

	public byte ColorR
	{
		get => (byte)GetValue(ColorRProperty);
		set => SetValue(ColorRProperty, value);
	}

	public byte ColorG
	{
		get => (byte)GetValue(ColorGProperty);
		set => SetValue(ColorGProperty, value);
	}

	public byte ColorB
	{
		get => (byte)GetValue(ColorBProperty);
		set => SetValue(ColorBProperty, value);
	}

	public Color CurrentColor
	{
		get => (Color)GetValue(CurrentColorProperty);
		set => SetValue(CurrentColorProperty, value);
	}

	public object Result => CurrentColor;

	public PixelDialogHost Host { get; set; }

	private static void ColorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is not ColorPickerControl control) return;
		control.CurrentColor = Color.FromArgb(control.ColorA, control.ColorR, control.ColorG, control.ColorB);
	}
}