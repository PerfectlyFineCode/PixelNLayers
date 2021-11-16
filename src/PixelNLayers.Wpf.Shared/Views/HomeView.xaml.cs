using System.Windows.Controls;
using PixelNLayers.Wpf.Shared.Apps;
using PixelNLayers.Wpf.Shared.ViewModels;

namespace PixelNLayers.Wpf.Shared.Views;

/// <summary>
///     Interaction logic for HomeView.xaml
/// </summary>
public partial class HomeView : Page
{
	public HomeView()
	{
		InitializeComponent();
		DataContext = PixelApp.GetService<HomeViewModel>();
	}
}