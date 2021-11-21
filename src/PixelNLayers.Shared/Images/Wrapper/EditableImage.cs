using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using PixelNLayers.Shared.Images.Recorder.Interfaces;
#if DEBUG
using System.Diagnostics;
#endif

namespace PixelNLayers.Shared.Images.Wrapper;

#nullable disable
public class EditableImage : INotifyPropertyChanged, IRecordable
{
	private readonly Stack<IReadOnlyList<PixelData>> HistoryData = new();
	private readonly Stack<IReadOnlyList<PixelData>> UndoHistoryData = new();

	private WriteableBitmap _image;
	private bool _state;

	private List<PixelData> _temporaryPixelDatas;

	public EditableImage(int width, int height)
	{
		Image = new WriteableBitmap(width, height, 72, 72, PixelFormats.Bgra32, null);
	}

	public WriteableBitmap Image
	{
		get => _image;
		set
		{
			_image = value;
			RaisePropertyChanged();
		}
	}

	public Color this[int x, int y]
	{
		get => GetPixel(x, y);
		set
		{
			var old = GetPixel(x, y);
			if (old == value) return;
			if (!SetPixel(x, y, value)) return;
			if (!_state) return;
			AddToHistory(x, y, value, old);

		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	void IRecordable.StartRecord()
	{
		_temporaryPixelDatas = new List<PixelData>();
		_state = true;
	}

	bool IRecordable.StopRecord()
	{
		if (!_state) return false;
		Debug.WriteLine(_temporaryPixelDatas.Count);
		if (_temporaryPixelDatas.Count <= 0) return false;
		HistoryData.Push(_temporaryPixelDatas);
		_state = false;
		OnInvalidated();
		return true;

	}

	/// <inheritdoc />
	void IRecordable.GoBack()
	{
		if (HistoryData.Count <= 0) return;
		IReadOnlyList<PixelData> task = HistoryData.Pop();
		if (task == null) return;
		DoBack(task);
		UndoHistoryData.Push(task);
		Debug.WriteLine("backward");
	}

	/// <inheritdoc />
	void IRecordable.GoForward()
	{
		if (UndoHistoryData.Count <= 0) return;
		IReadOnlyList<PixelData> task = UndoHistoryData.Pop();
		if (task == null) return;
		DoForward(task);
		HistoryData.Push(task);
	}

	public event EventHandler Invalidated;

	private void DoBack(IReadOnlyList<PixelData> data)
	{
		foreach (var pixelData in data)
		{
			this[pixelData.X, pixelData.Y] = pixelData.PreviousColor;
		}
	}

	private void DoForward(IReadOnlyList<PixelData> data)
	{
		foreach (var pixelData in data)
		{
			this[pixelData.X, pixelData.Y] = pixelData.Color;
		}
	}

	private void AddToHistory(int x, int y, Color color, Color previousColor)
	{
		_temporaryPixelDatas.Add(new PixelData(x, y, color, previousColor));
	}

	public static implicit operator WriteableBitmap(EditableImage source)
	{
		return source.Image;
	}

	private Color GetPixel(int x, int y)
	{
		if (x < 0 || y < 0 || x >= _image.PixelWidth || y >= _image.PixelHeight)
		{
			#if DEBUG
			Debug.WriteLine($"X: {x}, Y: {y}");
			#endif
			return Colors.Transparent;
		}

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
			return Colors.Transparent;
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

	private bool SetPixel(int x, int y, Color? color)
	{
		if (x < 0 || y < 0 || x >= _image.PixelWidth || y >= _image.PixelHeight)
		{
			#if DEBUG
			Debug.WriteLine($"X: {x}, Y: {y}");
			#endif
			return false;
		}

		if (color is not { } _color) return false;
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

				int PixelColorData = _color.B;    // B
				PixelColorData |= _color.G << 8;  // G
				PixelColorData |= _color.R << 16; // R
				PixelColorData |= _color.A << 24; // A

				// Assign the color data to the pixel.
				*(int*)pBackBuffer = PixelColorData;
			}

			// Specify the area of the bitmap that changed.
			Image.AddDirtyRect(new Int32Rect(x, y, 1, 1));
			return true;
		}
		catch
		{
			return false;
		}
		finally
		{
			// Release the back buffer and make it available for display.
			Image.Unlock();
		}
	}

	public void RaisePropertyChanged([CallerMemberName] string propertyName = "")
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}

	protected virtual void OnInvalidated()
	{
		Invalidated?.Invoke(this, EventArgs.Empty);
	}
}

public readonly struct PixelData
{
	public int X { get; }
	public int Y { get; }
	public Color Color { get; }
	public Color PreviousColor { get; }

	public PixelData(int x, int y, Color color, Color previousColor)
	{
		X = x;
		Y = y;
		Color = color;
		PreviousColor = previousColor;
	}
}