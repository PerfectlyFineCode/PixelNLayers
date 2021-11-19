using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Xaml.Behaviors;
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

        if (IsDrawing)
            IsDrawing = false;

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
        IsDrawing = false;
        SetPixelAtPosition();
        e.Handled = true;
    }

    private void AssociatedObject_MouseMove(object sender, MouseEventArgs e)
    {
        var pos = e.GetPosition(AssociatedObject);
        PixelX = (int)(pos.X / AssociatedObject.ActualWidth * _image.Image.PixelWidth);
        PixelY = (int)(pos.Y / AssociatedObject.ActualHeight * _image.Image.PixelHeight);

        if (IsDrawing)
            SetPixelAtPosition();

        e.Handled = true;
    }

    private void SetPixelAtPosition()
    {
        _image[PixelX, PixelY] = SelectedColor;
    }

    private void AssociatedObject_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (IsDrawing || !IsEditing) return;
        IsDrawing = true;
        e.Handled = true;
    }
}