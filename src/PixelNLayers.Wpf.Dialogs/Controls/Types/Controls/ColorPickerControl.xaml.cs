using System.Windows.Controls;
using PixelNLayers.Wpf.Dialogs.Interfaces;

namespace PixelNLayers.Wpf.Dialogs.Controls.Types.Controls;

/// <summary>
///     Interaction logic for ColorPickerControl.xaml
/// </summary>
internal partial class ColorPickerControl : UserControl, IDialogControl
{
    public ColorPickerControl()
    {
        InitializeComponent();
    }
}