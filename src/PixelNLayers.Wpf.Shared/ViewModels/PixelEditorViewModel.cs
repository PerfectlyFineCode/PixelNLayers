using Microsoft.Toolkit.Mvvm.ComponentModel;
using PixelNLayers.Shared.Images.Wrapper;

namespace PixelNLayers.Wpf.Shared.ViewModels;

#nullable disable
public class PixelEditorViewModel : ObservableObject
{
	private EditableImage _image;

	public PixelEditorViewModel()
	{
		Image = new EditableImage(64, 64);
	}

	public EditableImage Image
	{
		get => _image;
		set => SetProperty(ref _image, value);
	}
}