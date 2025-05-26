using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
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
        private readonly APIService _apiService;

        private CancellationTokenSource? _uploadStatusCts;


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
        public UploadFirmwarePageViewModel(INavigationService navigationService, APIService apiService)
        {
            _apiService = apiService;
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
        public async Task CallUploadFirmware()
        {
            if (string.IsNullOrEmpty(SelectedFilePath)) return;

            // Start animation in background
            _uploadStatusCts = new CancellationTokenSource();
            var token = _uploadStatusCts.Token;

            var animationTask = Task.Run(async () =>
            {
                string baseText = "Uploading firmware";
                string[] dots = ["", ".", "..", "..."];
                int i = 0;

                while (!token.IsCancellationRequested)
                {
                    StatusMessage = baseText + dots[i % dots.Length];
                    i++;
                    await Task.Delay(500); // Update every 500ms
                }
            }, token);

            // Upload itself
            string result;
            try
            {
                result = await _apiService.UploadFirmware(SelectedFilePath);
            }
            finally
            {
                _uploadStatusCts.Cancel(); // Stop animation
                await animationTask;       // Await for animation to finish nicely
            }

            // Set final status
            StatusMessage = result;
        }
    }
}
