namespace PixelNLayers.Shared.Images.Recorder.Interfaces;

public interface IRecordable
{
	internal void StartRecord();
	internal bool StopRecord();
	internal void GoBack();
	internal void GoForward();
}