using System.Windows;
using PixelNLayers.Wpf.Shared.Apps;

namespace PixelNLayers;

/// <summary>
///     Interaction logic for App.xaml
/// </summary>
public partial class App : PixelApp
{
	public App()
	{
		InitializeComponent();
		FrameworkElement.StyleProperty.OverrideMetadata(typeof(Window), new FrameworkPropertyMetadata
		{
			DefaultValue = FindResource(typeof(Window))
		});
	}
}