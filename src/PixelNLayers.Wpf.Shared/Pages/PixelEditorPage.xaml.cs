using System.Windows.Controls;
using PixelNLayers.Wpf.Shared.Apps;
using PixelNLayers.Wpf.Shared.ViewModels;

namespace PixelNLayers.Wpf.Shared.Pages;

/// <summary>
///     Interaction logic for PixelEditorPage.xaml
/// </summary>
public partial class PixelEditorPage : Page
{
    public PixelEditorPage()
    {
        InitializeComponent();
        DataContext = PixelApp.GetService<PixelEditorViewModel>();
    }
}