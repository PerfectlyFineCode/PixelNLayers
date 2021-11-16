using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PixelNLayers.Shared.Images.Wrapper;

public class EditableImage
{
	public EditableImage(int width, int height)
	{
		Image = new WriteableBitmap(width, height, 72, 72, PixelFormats.Bgra32, null);
	}

	public WriteableBitmap Image { get; }

	public Color this[int x, int y]
	{
		set => SetPixel(x, y, value);
	}

	private void GetPixel()
	{

	}

	public static implicit operator ImageSource(EditableImage image)
	{
		return image.Image;
	}

	private void SetPixel(int x, int y, Color color)
	{
		try
		{
			// Reserve the back buffer for updates.
			Image.Lock();

			unsafe
			{
				// Get a pointer to the back buffer.
				var pBackBuffer = Image.BackBuffer;

				// Find the address of the pixel to draw.
				pBackBuffer += y * Image.BackBufferStride;
				pBackBuffer += x * 4;

				// Compute the pixel's color.
				int color_data = color.A << 24; // A
				color_data |= color.R << 16;    // R
				color_data |= color.G << 4;     // G
				color_data |= color.B;          // B

				// Assign the color data to the pixel.

				*(int*)pBackBuffer = color_data;
			}

			// Specify the area of the bitmap that changed.
			Image.AddDirtyRect(new Int32Rect(x, y, 1, 1));
		}
		catch (Exception ex)
		{
			Debug.WriteLine(ex);
		}
		finally
		{
			// Release the back buffer and make it available for display.
			Image.Unlock();
		}
	}
}