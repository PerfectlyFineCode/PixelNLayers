using System.Windows;
using PixelNLayers.Wpf.Shared.Apps;
using PixelNLayers.Wpf.Shared.ViewModels;

namespace PixelNLayers;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
	public MainWindow()
	{
		InitializeComponent();
		DataContext = PixelApp.GetService<RootViewModel>();
	}
}