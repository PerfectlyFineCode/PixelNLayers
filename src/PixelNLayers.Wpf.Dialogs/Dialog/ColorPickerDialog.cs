using System.Windows.Media;
using PixelNLayers.Wpf.Dialogs.Controls.Types.Controls;
using PixelNLayers.Wpf.Dialogs.Host;

namespace PixelNLayers.Wpf.Dialogs.Dialog;

public class ColorPickerDialog
{
	private readonly ColorPickerControl _control;

	public ColorPickerDialog()
	{
		_control = new ColorPickerControl();
	}

	public async Task<Color?> ShowDialogAsync(PixelDialogHost parent, Color? initialColor = null)
	{
		if (parent.Children.Contains(_control)) return null;
		if (initialColor is { } color)
		{
			_control.ColorA = color.A;
			_control.ColorR = color.R;
			_control.ColorG = color.G;
			_control.ColorB = color.B;
		}

		var result = new PixelDialogHost.ResultData();
		parent.ShowControl(_control, ref result);
		while (result.Data == null)
		{
			await Task.Delay(25);
		}

		return result.Data is Color data ? data : null;
	}
}