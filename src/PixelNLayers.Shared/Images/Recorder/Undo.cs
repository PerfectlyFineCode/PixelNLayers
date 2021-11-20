using PixelNLayers.Shared.Images.Recorder.Interfaces;
using PixelNLayers.Shared.Images.Wrapper;

namespace PixelNLayers.Shared.Images.Recorder;

public class Undo
{
	public static void BeginRecord(EditableImage image, string name)
	{
		((IRecordable)image).StartRecord();
	}

	public static bool EndRecord(EditableImage image)
	{
		return ((IRecordable)image).StopRecord();
	}
}