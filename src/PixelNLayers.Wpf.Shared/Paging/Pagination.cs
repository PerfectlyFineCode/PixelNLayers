using System.Diagnostics;
using System.Windows.Controls;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using PixelNLayers.Wpf.Shared.Interfaces;

namespace PixelNLayers.Wpf.Shared.Paging;

public class Pagination : ObservableObject, IPagination
{
	private Page? _currentPage;

	public Dictionary<Type, Page?> PageCache = new();

	public Page? CurrentPage
	{
		get => _currentPage;
		set => SetProperty(ref _currentPage, value);
	}

	public void SetPage<T>() where T : Page
	{
		if (!PageCache.ContainsKey(typeof(T)))
		{
			var instance = Activator.CreateInstance<T>();
			PageCache[typeof(T)] = instance;
			CurrentPage = instance;
		}
		else
		{
			CurrentPage = PageCache[typeof(T)];
		}

		Debug.WriteLine("Set page");
	}

	public Page? GetPage<T>() where T : Page
	{
		return PageCache.ContainsKey(typeof(T)) ? PageCache[typeof(T)] : null;
	}
}