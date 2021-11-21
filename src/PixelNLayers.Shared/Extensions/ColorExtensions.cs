using System.Windows.Media;

namespace PixelNLayers.Shared.Extensions;

public static class ColorExtensions
{
	public static Color Reverse(this Color color)
	{
		var B = (byte)(255 - color.B);
		var G = (byte)(255 - color.G);
		var R = (byte)(255 - color.R);
		return Color.FromArgb(255, R, G, B);
	}
}