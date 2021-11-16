﻿using System.Windows;
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

	public Color? this[int x, int y]
	{
		get => GetPixel(x, y);
		set => SetPixel(x, y, value);
	}

	private Color? GetPixel(int x, int y)
	{
		try
		{
			unsafe
			{
				// Reserve the back buffer for updates.
				Image.Lock();

				// Get a pointer to the back buffer.
				var pBackBuffer = Image.BackBuffer;

				// Find the address of the pixel to draw.
				pBackBuffer += y * Image.BackBufferStride;
				pBackBuffer += x * 4;

				var data = (byte*)pBackBuffer.ToPointer();

				byte b = data[0];
				byte g = data[1];
				byte a = data[3];
				byte r = data[2];

				return Color.FromArgb(a, r, g, b);
			}

		}
		catch
		{
			return null;
		}
		finally
		{
			// Release the back buffer and make it available for display.
			Image.Unlock();
		}
	}

	public static implicit operator ImageSource(EditableImage image)
	{
		return image.Image;
	}

	private void SetPixel(int x, int y, Color? color)
	{
		if (color is not { } _color) return;
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
				int color_data = _color.B;    // A
				color_data |= _color.G << 8;  // R
				color_data |= _color.R << 16; // G
				color_data |= _color.A << 24; // B


				// Assign the color data to the pixel.
				*(int*)pBackBuffer = color_data;
			}

			// Specify the area of the bitmap that changed.
			Image.AddDirtyRect(new Int32Rect(x, y, 1, 1));
		}
		finally
		{
			// Release the back buffer and make it available for display.
			Image.Unlock();
		}
	}
}