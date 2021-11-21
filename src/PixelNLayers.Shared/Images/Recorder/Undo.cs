using PixelNLayers.Shared.Images.Recorder.Interfaces;

namespace PixelNLayers.Shared.Images.Recorder;

public class Undo
{
	public static void BeginRecord(IRecordable recordable, string name)
	{
		recordable.StartRecord();
	}

	public static void Go(IRecordable recordable, UndoDirection direction)
	{
		switch (direction)
		{
			case UndoDirection.Back:
				recordable.GoBack();
				break;
			case UndoDirection.Forward:
				recordable.GoForward();
				break;
		}
	}

	public static bool EndRecord(IRecordable recordable)
	{
		return recordable.StopRecord();
	}
}

public enum UndoDirection
{
	Back,
	Forward
}