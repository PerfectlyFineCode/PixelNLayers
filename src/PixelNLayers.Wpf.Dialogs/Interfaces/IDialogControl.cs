using System.Windows;
using System.Windows.Input;
using PixelNLayers.Wpf.Dialogs.Host;

namespace PixelNLayers.Wpf.Dialogs.Interfaces;

public interface IDialogControl
{
	public object Result { get; }

	public PixelDialogHost Host { get; set; }

	public void Activate<T>(T instance) where T : UIElement, IDialogControl
	{
		instance.KeyUp += Instance_KeyUp;
	}

	void Instance_KeyUp(object sender, KeyEventArgs e)
	{
		if (e.Key == Key.Enter)
			Host.RemoveCurrentControl();

	}

	public virtual void OnAdded()
	{
	}

	public virtual void OnRemoved()
	{
	}
}