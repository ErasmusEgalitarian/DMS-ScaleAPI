using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaAppUpdatedVersion.Services
{
    public class MockAuthService : IAuthService
    {
        public Task<AuthResult> LoginAsync(string username, string password)
        {
            var isValid = username == "admin" && password == "1234";

            return Task.FromResult(new AuthResult
            {
                Success = isValid,
                Token = isValid ? "dummy-token" : null
            });
        }
    }
}
