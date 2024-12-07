using System.Security.Cryptography;

namespace WebGoat.NET.Handlers;

using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;
using System.Text;
using System.Security.Claims;

public class BasicAuthenticationHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder)
    : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.TryGetValue("Authorization", out var value))
            return Task.FromResult(AuthenticateResult.Fail("Missing Authorization Header"));

        try
        {
            var authHeader = value.ToString();
            var credentialBytes = Convert.FromBase64String(authHeader["Basic ".Length..].Trim());
            var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':', 2);
            var username = credentials[0];
            var password = credentials[1];

            if (!ValidateCredentials(username, password))
                return Task.FromResult(AuthenticateResult.Fail("Invalid Username or Password"));

            var claims = new[] { new Claim(ClaimTypes.Name, username) };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            return Task.FromResult(AuthenticateResult.Success(ticket));

        }
        catch
        {
            return Task.FromResult(AuthenticateResult.Fail("Invalid Authorization Header"));
        }
    }

    protected override Task HandleChallengeAsync(AuthenticationProperties properties)
    {
        Response.StatusCode = 401;
        Response.Headers["WWW-Authenticate"] = "Basic realm=\"Admin Panel\"";
        return Task.CompletedTask;
    }

    private static bool ValidateCredentials(string username, string password)
    {
        // The password should be the password "password" in "MD5"
        // md5: 5f4dcc3b5aa765d61d8327deb882cf99
        var validPassword = "password";
        using (var md5 = MD5.Create())
        {
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(validPassword));
            var sb = new StringBuilder();
            foreach (var t in hash)
            {
                sb.Append(t.ToString("X2"));
            }
            validPassword = sb.ToString();
        }
        
        return username == "admin" && string.Equals(password, validPassword, StringComparison.CurrentCultureIgnoreCase);
    }
}
