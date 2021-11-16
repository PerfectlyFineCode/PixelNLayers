using PixelNLayers.Wpf.Shared.Paging;
using PixelNLayers.Wpf.Shared.Views;

namespace PixelNLayers.Wpf.Shared.ViewModels;

public class RootViewModel : Pagination
{
	public RootViewModel()
	{
		SetPage<HomeView>();
	}
}