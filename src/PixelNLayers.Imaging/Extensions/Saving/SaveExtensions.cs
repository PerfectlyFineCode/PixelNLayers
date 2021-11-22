using System.IO;
using System.Windows.Media.Imaging;
using PixelNLayers.Shared.Images.Wrapper;

namespace PixelNLayers.Imaging.Extensions.Saving;

public static class SaveExtensions
{
	public static bool Save(this EditableImage image, string filePath)
	{
		try
		{
			using var fs = new FileStream(filePath, FileMode.Create);
			BitmapEncoder encoder = new PngBitmapEncoder();
			encoder.Frames.Add(BitmapFrame.Create(image));
			encoder.Save(fs);
			return true;
		}
		catch
		{
			return false;
		}
	}
}