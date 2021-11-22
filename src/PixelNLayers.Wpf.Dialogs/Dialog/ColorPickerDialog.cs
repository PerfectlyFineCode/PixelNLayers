using PixelNLayers.Wpf.Dialogs.Controls.Types.Controls;
using PixelNLayers.Wpf.Dialogs.Host;

namespace PixelNLayers.Wpf.Dialogs.Dialog;

public class ColorPickerDialog
{
    private ColorPickerControl _control;

    public ColorPickerDialog()
    {
        _control = new ColorPickerControl();
    }

    public void ShowDialog(PixelDialogHost parent)
    {
        if (parent.Children.Contains(_control)) return;
        parent.ShowControl(_control);
    }
}