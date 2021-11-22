using System.Windows.Media;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using PixelNLayers.Shared.Images.Wrapper;

namespace PixelNLayers.Wpf.Shared.ViewModels;

#nullable disable
public class PixelEditorViewModel : ObservableObject
{
    private Color _currentColor = Colors.Black;
    private EditableImage _image;

    public PixelEditorViewModel()
    {
        Image = new EditableImage(64, 64);
    }

    public Color CurrentColor
    {
        get => _currentColor;
        set => SetProperty(ref _currentColor, value);
    }

    public EditableImage Image
    {
        get => _image;
        set => SetProperty(ref _image, value);
    }
}