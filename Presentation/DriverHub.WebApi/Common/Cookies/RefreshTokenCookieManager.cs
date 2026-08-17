namespace DriverHub.WebApi.Common.Cookies;

public sealed class RefreshTokenCookieManager(IWebHostEnvironment environment)
{
    private const string CookieName = "refreshToken";

    private readonly IWebHostEnvironment _environment = environment;

    public void Append(HttpResponse response, string refreshToken, DateTime expiresAt)
    {
        response.Cookies.Append(
            CookieName,
            refreshToken,
            CreateCookieOptions(expiresAt));
    }

    public string? Get(HttpRequest request) => 
        request.Cookies[CookieName];

    public void Delete(HttpResponse response)
    {
        response.Cookies.Delete(
            CookieName,
            CreateCookieOptions());
    }

    private CookieOptions CreateCookieOptions(DateTime? expiresAt = null)
    {
        return new CookieOptions
        {
            HttpOnly = true,
            Secure = true,

            // Angular dev:
            // http://localhost:4001
            //
            // API:
            // https://localhost:xxxx
            //
            // Scheme farklı olduğu için development'ta None kullanıyoruz.
            SameSite = _environment.IsDevelopment()
                ? SameSiteMode.None
                : SameSiteMode.Strict,

            Expires = expiresAt,
            Path = "/"
        };
    }
}