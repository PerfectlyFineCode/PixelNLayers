using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using PixelNLayers.Wpf.Dialogs.Interfaces;

namespace PixelNLayers.Wpf.Dialogs.Host;

#nullable disable
public class PixelDialogHost : Grid
{
	private InternalDialog _currentControl;

	static PixelDialogHost()
	{
		BackgroundProperty.OverrideMetadata(typeof(PixelDialogHost),
			new FrameworkPropertyMetadata(new SolidColorBrush(Colors.Black)
			{
				Opacity = 0.0
			}));

		IsHitTestVisibleProperty.OverrideMetadata(typeof(PixelDialogHost),
			new FrameworkPropertyMetadata(false));
	}

	public PixelDialogHost()
	{
		MouseUp += PixelDialogHost_MouseUp;
	}

	private InternalDialog CurrentControl
	{
		get => _currentControl;
		set
		{
			_currentControl = value;
			IsHitTestVisible = value is { IsEmpty: false };
		}
	}

	private void PixelDialogHost_MouseUp(object sender, MouseButtonEventArgs e)
	{
		if (CurrentControl.Control.IsMouseOver) return;
		if (_currentControl.IsEmpty) return;
		RemoveControl(CurrentControl);
	}

	public void RemoveCurrentControl()
	{
		RemoveControl(CurrentControl);
	}

	private void RemoveControl(InternalDialog dialog)
	{
		if (CurrentControl == null || CurrentControl.IsEmpty || !dialog.Control.Equals(CurrentControl.Control)) return;
		CurrentControl.Data.Data = CurrentControl.DialogControl.Result;
		Children.Remove(dialog.Control);
		CurrentControl = null;
	}

	internal bool ShowControl<T>(T control, ref ResultData data) where T : UIElement, IDialogControl
	{
		if (CurrentControl != null && !_currentControl.IsEmpty) return false;
		control.Host = this;
		CurrentControl = new InternalDialog(control, control, ref data);
		Children.Add(control);
		return true;
	}

	public class InternalDialog
	{
		public InternalDialog(UIElement control, IDialogControl dialogControl, ref ResultData data)
		{
			Control = control;
			DialogControl = dialogControl;
			Data = data;
		}

		public UIElement Control { get; }
		public IDialogControl DialogControl { get; }
		public ResultData Data { get; }

		public bool IsEmpty => Control == null && DialogControl == null;
	}

	public class ResultData
	{
		public object Data { get; set; }
	}
}