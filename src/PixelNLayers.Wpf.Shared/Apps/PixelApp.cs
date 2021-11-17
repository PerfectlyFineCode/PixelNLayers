using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using PixelNLayers.Wpf.Shared.ViewModels;

namespace PixelNLayers.Wpf.Shared.Apps;

public class PixelApp : Application
{
	public PixelApp()
	{
		Services = ConfigureServices();
	}

	public new static PixelApp Current => (PixelApp)Application.Current;
	public IServiceProvider Services { get; }

	public static T? GetService<T>()
	{
		return Current.Services.GetService<T>();
	}

	private static IServiceProvider ConfigureServices()
	{
		var services = new ServiceCollection();
		services.AddSingleton<PixelEditorViewModel>();
		services.AddSingleton<RootViewModel>();
		services.AddSingleton<HomeViewModel>();
		return services.BuildServiceProvider();
	}
}