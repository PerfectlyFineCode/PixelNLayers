using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using PixelNLayers.Shared.Images.Wrapper;

namespace PixelNLayers.Wpf.Controls.Editor;

/// <summary>
///     Interaction logic for PixelEditor.xaml
/// </summary>
public partial class PixelEditor : UserControl
{
    public static readonly DependencyProperty DrawableSourceProperty = DependencyProperty.Register(
        nameof(DrawableSource), typeof(EditableImage), typeof(PixelEditor),
        new PropertyMetadata(default(EditableImage)));

    public static readonly DependencyProperty CurrentColorProperty = DependencyProperty.Register(
        nameof(CurrentColor), typeof(Color), typeof(PixelEditor), new PropertyMetadata(default(Color)));

    public PixelEditor()
    {
        InitializeComponent();
        Loaded += PixelEditor_Loaded;
    }

    public Color CurrentColor
    {
        get => (Color)GetValue(CurrentColorProperty);
        set => SetValue(CurrentColorProperty, value);
    }

    public EditableImage DrawableSource
    {
        get => (EditableImage)GetValue(DrawableSourceProperty);
        set => SetValue(DrawableSourceProperty, value);
    }

    private void PixelEditor_Loaded(object sender, RoutedEventArgs e)
    {
    }
}