using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using AvaloniaAppUpdatedVersion.Services;

namespace AvaloniaAppUpdatedVersion.ViewModels
{
    public partial class LoginPageViewModel : ViewModelBase
    {
        public string Greeting => "Scale Manager";


        [ObservableProperty]
        private string _loginText = "Please login with your username and password";

        private readonly IAuthService _authService;

        [ObservableProperty]
        private string username;

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private string _errorMessage = "";

        [ObservableProperty]
        private bool isBusy = false;

        [ObservableProperty]
        private bool loginSuccessful;

        // Constructor for design mode
        public LoginPageViewModel()
        {
#if DEBUG
            if (Design.IsDesignMode)
            {
                _authService = new MockAuthService(); // Lav en simpel dummy direkte her
                Username = "admin";
                Password = "1234";
                return;
            }
#endif

            throw new InvalidOperationException("LoginPageViewModel should be created via DI");
        }
        // Constructor for runtime

        public LoginPageViewModel(IAuthService authService)
        {
            _authService = authService;

            // Test
            Console.WriteLine("LoginPageViewModel constructor called");
        }

        [RelayCommand]
        public async Task LoginAsync()
        {
            // Test
            Console.WriteLine("LoginAsync called");

            IsBusy = true;
            ErrorMessage = string.Empty;

            try
            {
                var result = await _authService.LoginAsync(Username, Password);

                if (result.Success)
                {
                    LoginSuccessful = true;
                    // Naviger til admin-hovedview eller luk loginvinduet
                }
                else
                {
                    ErrorMessage = "Error, username or password incorrect.";
                    LoginSuccessful = false;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = "An error occurred during login.";
                // Log evt. ex.Message
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
