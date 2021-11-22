using System.Windows;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Win32;
using Ookii.Dialogs.Wpf;
using PixelNLayers.Imaging.Extensions.Saving;
using PixelNLayers.Wpf.Shared.Apps;
using PixelNLayers.Wpf.Shared.Paging;
using PixelNLayers.Wpf.Shared.Views;

namespace PixelNLayers.Wpf.Shared.ViewModels;

public class RootViewModel : Pagination
{
	public RootViewModel()
	{
		SetPage<HomeView>();
		_editorViewModel = PixelApp.GetService<PixelEditorViewModel>();
		SaveCommand = new RelayCommand(SaveImage, () => _editorViewModel?.Image is not null);
	}

	public PixelEditorViewModel? _editorViewModel { get; }

	public RelayCommand SaveCommand { get; }

	private void SaveImage()
	{
		if (_editorViewModel?.Image is not { } _image) return;

		if (VistaFileDialog.IsVistaFileDialogSupported)
		{
			var sfd = new VistaSaveFileDialog
			{
				Title = "Save sprite",
				Filter = "Image files (.*png)|*.png",
				AddExtension = true,
				OverwritePrompt = true,
				RestoreDirectory = true,
				DefaultExt = ".png"
			};
			if (sfd.ShowDialog() != true) return;
			if (!_image.Save(sfd.FileName)) return;
			if (TaskDialog.OSSupportsTaskDialogs)
			{
				var dialog = new TaskDialog
				{
					Buttons =
					{
						new TaskDialogButton("Ok")
					},
					Content = "Successfully saved image",
					WindowTitle = "Saved image!"
				};
				dialog.ShowDialog();
			}
			else
			{
				MessageBox.Show("Successfully saved image", "Saved image!", MessageBoxButton.OK);
			}
		}
		else
		{
			var sfd = new SaveFileDialog
			{
				Title = "Save sprite",
				Filter = "Image files (.*png)|*.png",
				AddExtension = true,
				OverwritePrompt = true,
				RestoreDirectory = true,
				DefaultExt = ".png"
			};

			if (sfd.ShowDialog() != true) return;
			if (!_image.Save(sfd.FileName)) return;
			if (TaskDialog.OSSupportsTaskDialogs)
			{
				var dialog = new TaskDialog
				{
					Buttons =
					{
						new TaskDialogButton("Ok")
					},
					Content = "Successfully saved image",
					WindowTitle = "Saved image!"
				};
				dialog.ShowDialog();
			}
			else
			{
				MessageBox.Show("Successfully saved image", "Saved image!", MessageBoxButton.OK);
			}
		}
	}
}