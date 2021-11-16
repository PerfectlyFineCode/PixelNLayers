using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using PixelNLayers.Shared.Images.Wrapper;

namespace PixelNLayers.Wpf.Shared.ViewModels;

public class PixelEditorViewModel : ObservableObject
{
	private EditableImage _image;

	public PixelEditorViewModel()
	{
		var img = new EditableImage(64, 64);

		for (var i = 0; i < img.Image.PixelHeight; i++)
		{
			for (var j = 0; j < img.Image.PixelWidth; j++)
			{
				img[i, j] = j % 2 == 0 && i % 2 == 0 ? Colors.Red : Colors.Blue;
			}
		}

		_image = img;
		Save(Image);
	}

	public EditableImage Image
	{
		get => _image;
		set
		{
			SetProperty(ref _image, value);
			OnPropertyChanged(nameof(ImageSource));
		}
	}

	public ImageSource ImageSource => _image;

	private void Save(ImageSource source)
	{
		using var fs = new FileStream(@"C:\aaa.png", FileMode.Create);
		BitmapEncoder encoder = new PngBitmapEncoder();
		encoder.Frames.Add(BitmapFrame.Create((BitmapSource)source));
		encoder.Save(fs);
	}
}