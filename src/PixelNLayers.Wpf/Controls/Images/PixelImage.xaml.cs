using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using PixelNLayers.Shared.Images.Wrapper;

namespace PixelNLayers.Wpf.Controls.Images;

/// <summary>
///     Interaction logic for PixelImage.xaml
/// </summary>
public partial class PixelImage : UserControl
{
    public static readonly DependencyProperty EditableImageSourceProperty = DependencyProperty.Register(
        nameof(EditableImageSource), typeof(EditableImage), typeof(PixelImage),
        new PropertyMetadata(default(EditableImage)));

    public static readonly DependencyProperty CursorPixelXProperty = DependencyProperty.Register(
        nameof(CursorPixelX), typeof(int), typeof(PixelImage), new PropertyMetadata(default(int)));

    public static readonly DependencyProperty CursorPixelYProperty = DependencyProperty.Register(
        nameof(CursorPixelY), typeof(int), typeof(PixelImage), new PropertyMetadata(default(int)));

    public static readonly DependencyProperty CurrentColorProperty = DependencyProperty.Register(
        nameof(CurrentColor), typeof(Color), typeof(PixelImage),
        new PropertyMetadata(default(Color)));

    public PixelImage()
    {
        InitializeComponent();
    }

    public Color CurrentColor
    {
        get => (Color)GetValue(CurrentColorProperty);
        set => SetValue(CurrentColorProperty, value);
    }

    public int CursorPixelX
    {
        get => (int)GetValue(CursorPixelXProperty);
        set => SetValue(CursorPixelXProperty, value);
    }

    public int CursorPixelY
    {
        get => (int)GetValue(CursorPixelYProperty);
        set => SetValue(CursorPixelYProperty, value);
    }

    public EditableImage EditableImageSource
    {
        get => (EditableImage)GetValue(EditableImageSourceProperty);
        set => SetValue(EditableImageSourceProperty, value);
    }
}