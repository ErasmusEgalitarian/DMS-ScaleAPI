using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls;
using AvaloniaAppUpdatedVersion.Data;
using AvaloniaAppUpdatedVersion.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AvaloniaAppUpdatedVersion.ViewModels
{
    public partial class UploadFirmwarePageViewModel : PageViewModel
    {
        private readonly INavigationService? _navigationService;

        [ObservableProperty]
        private string _selectedFilePath;

        [ObservableProperty]
        private string _statusMessage;

        public bool IsUploadEnabled => !string.IsNullOrEmpty(SelectedFilePath);

        partial void OnSelectedFilePathChanged(string oldValue, string newValue)
        {
            OnPropertyChanged(nameof(IsUploadEnabled));
        }

        // Runtime constructor
        public UploadFirmwarePageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            PageName = ApplicationPageNames.UploadFirmware;
        }

        // Design-time constructor
        public UploadFirmwarePageViewModel()
        {
            if (!Design.IsDesignMode)
                throw new InvalidOperationException("This constructor is only for design-time.");

            PageName = ApplicationPageNames.UploadFirmware;
        }
        [RelayCommand]
        public async Task SelectFirmwareFile(Window parent)
        {
            var dialog = new OpenFileDialog
            {
                AllowMultiple = false,
                Filters =
        {
            new FileDialogFilter { Name = "BIN files", Extensions = { "bin" } }
        }
            };

            var result = await dialog.ShowAsync(parent);
            if (result is { Length: > 0 })
            {
                SelectedFilePath = result[0];
            }
        }

        [RelayCommand]
        public async Task UploadFirmware()
        {
            if (string.IsNullOrEmpty(SelectedFilePath)) return;

            try
            {
                StatusMessage = "Uploading...";
                byte[] fileBytes = await File.ReadAllBytesAsync(SelectedFilePath);

                using var httpClient = new HttpClient();
                var content = new ByteArrayContent(fileBytes);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");

                var response = await httpClient.PostAsync("http://vistimalik.com:4242/api/uploadFirmware", content);
                StatusMessage = response.IsSuccessStatusCode ? "Upload successful!" : "Upload failed.";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
            }
        }
    }
}
