using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaAppUpdatedVersion.Services
{
    public interface IAuthService
    {
        Task<AuthResult> LoginAsync(string username, string password);
    }

    public class AuthResult
    {
        public bool Success { get; set; }
        public string? Token { get; set; }  // Kan bruges senere, hvis ønsket
    }

}
