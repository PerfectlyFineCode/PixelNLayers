using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using PixelNLayers.Wpf.Dialogs.Interfaces;

namespace PixelNLayers.Wpf.Dialogs.Host;

#nullable disable
public class PixelDialogHost : Grid
{
    private UIElement _currentControl;

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

    private UIElement CurrentControl
    {
        get => _currentControl;
        set
        {
            _currentControl = value;
            IsHitTestVisible = value != null;
        }
    }

    private void PixelDialogHost_MouseUp(object sender, MouseButtonEventArgs e)
    {
        if (CurrentControl is not IDialogControl diag) return;
        Children.Remove(CurrentControl);
        diag.OnRemoved();
    }

    internal bool ShowControl<T>(T control) where T : UIElement, IDialogControl
    {
        if (_currentControl != null) return false;
        _currentControl = control;
        Children.Add(control);
        return true;
    }
}