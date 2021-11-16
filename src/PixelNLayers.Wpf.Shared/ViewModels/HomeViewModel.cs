using PixelNLayers.Wpf.Shared.Pages;
using PixelNLayers.Wpf.Shared.Paging;

namespace PixelNLayers.Wpf.Shared.ViewModels;

public class HomeViewModel : Pagination
{
	public HomeViewModel()
	{
		SetPage<PixelEditorPage>();
	}
}