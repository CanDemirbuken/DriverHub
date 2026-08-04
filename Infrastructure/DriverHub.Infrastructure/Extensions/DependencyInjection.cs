using DriverHub.Application.Interfaces.Account;
using DriverHub.Application.Interfaces.Authentication;
using DriverHub.Application.Interfaces.Authentication.Token.Access;
using DriverHub.Application.Interfaces.Authentication.Token.Refresh;
using DriverHub.Application.Interfaces.Communication;
using DriverHub.Application.Interfaces.Identity;
using DriverHub.Infrastructure.Options;
using DriverHub.Infrastructure.Services.Communication.Mail;
using DriverHub.Infrastructure.Services.Identity;
using DriverHub.Infrastructure.Services.Identity.Account;
using DriverHub.Infrastructure.Services.Identity.Authentication;
using DriverHub.Infrastructure.Services.Identity.Role;
using DriverHub.Infrastructure.Services.Identity.Session;
using DriverHub.Infrastructure.Services.Identity.Token.Access;
using DriverHub.Infrastructure.Services.Identity.Token.Refresh;
using DriverHub.Infrastructure.Services.Identity.UserRole;
using DriverHub.Persistence.Context;
using DriverHub.Persistence.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using System.Text;

namespace DriverHub.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDataProtection();

        services
            .AddIdentityCore<AppUser>(options =>
            {
                options.User.RequireUniqueEmail = true;

                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 1;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;

                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan =
                    TimeSpan.FromMinutes(5);
            })
            .AddRoles<IdentityRole>()
            .AddSignInManager()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        services
            .AddOptions<JwtOptions>()
            .Bind(configuration.GetSection(JwtOptions.SectionName))
            .Validate(
                options => !string.IsNullOrWhiteSpace(options.Issuer),
                "Jwt:Issuer alanı boş bırakılamaz.")
            .Validate(
                options => !string.IsNullOrWhiteSpace(options.Audience),
                "Jwt:Audience alanı boş bırakılamaz.")
            .Validate(
                options => !string.IsNullOrWhiteSpace(options.SecretKey),
                "Jwt:SecretKey alanı boş bırakılamaz.")
            .Validate(
                options => options.SecretKey.Length >= 32,
                "Jwt:SecretKey en az 32 karakter olmalıdır.")
            .Validate(
                options => options.ExpirationMinutes > 0,
                "Jwt:ExpirationMinutes sıfırdan büyük olmalıdır.")
            .ValidateOnStart();

        services
            .AddOptions<RefreshTokenOptions>()
            .Bind(configuration.GetSection(
                RefreshTokenOptions.SectionName))
            .Validate(
                options => options.ExpireDays > 0,
                "Refresh:ExpireDays sıfırdan büyük olmalıdır.")
            .ValidateOnStart();

        JwtOptions jwtOptions = configuration
            .GetRequiredSection(JwtOptions.SectionName)
            .Get<JwtOptions>()
            ?? throw new InvalidOperationException(
                "JWT ayarları okunamadı.");

        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme =
                    JwtBearerDefaults.AuthenticationScheme;

                options.DefaultChallengeScheme =
                    JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters =
                    new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwtOptions.Issuer,

                        ValidateAudience = true,
                        ValidAudience = jwtOptions.Audience,

                        ValidateLifetime = true,

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey =
                            new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(
                                    jwtOptions.SecretKey)),

                        ClockSkew = TimeSpan.Zero
                    };
            });

        services.AddAuthorization();

        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<ISessionService, SessionService>();
        services.AddScoped<IJwtTokenService, JwtTokenService>();
        services.AddScoped<IRefreshTokenGenerator, RefreshTokenGenerator>();
        services.AddScoped<IRefreshTokenHasher, RefreshTokenHasher>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IUserRoleService, UserRoleService>();
        services.AddScoped<IMailService, MailService>();

        services
            .AddOptions<IdentitySeedOptions>()
            .Bind(configuration.GetSection(
                IdentitySeedOptions.SectionName))
            .Validate(
                options => !string.IsNullOrWhiteSpace(options.AdminEmail),
                "IdentitySeed:AdminEmail alanı boş bırakılamaz.")
            .Validate(
                options => !string.IsNullOrWhiteSpace(options.AdminPassword),
                "IdentitySeed:AdminPassword alanı boş bırakılamaz.")
            .Validate(
                options => !string.IsNullOrWhiteSpace(options.AdminFirstName),
                "IdentitySeed:AdminFirstName alanı boş bırakılamaz.")
            .Validate(
                options => !string.IsNullOrWhiteSpace(options.AdminLastName),
                "IdentitySeed:AdminLastName alanı boş bırakılamaz.")
            .ValidateOnStart();

        services
            .AddOptions<SmtpOptions>()
            .Bind(configuration.GetSection(SmtpOptions.SectionName))
            .Validate(
                options => !string.IsNullOrWhiteSpace(options.Host),
                "Smtp:Host alanı boş bırakılamaz.")
            .Validate(
                options => options.Port is > 0 and <= 65535,
                "Smtp:Port 1 ile 65535 arasında olmalıdır.")
            .Validate(
                options => !string.IsNullOrWhiteSpace(options.UserName),
                "Smtp:UserName alanı boş bırakılamaz.")
            .Validate(
                options => !string.IsNullOrWhiteSpace(options.Password),
                "Smtp:Password alanı boş bırakılamaz.")
            .Validate(
                options => MailboxAddress.TryParse(options.FromEmail, out _),
                "Smtp:FromEmail geçerli bir email adresi olmalıdır.")
            .Validate(
                options => !string.IsNullOrWhiteSpace(options.FromName),
                "Smtp:FromName alanı boş bırakılamaz.")
            .Validate(
                options => Enum.IsDefined(options.SecureSocketOption),
                "Smtp:SecureSocketOption geçerli bir bağlantı seçeneği olmalıdır.")
            .ValidateOnStart();

        services.AddScoped<IMailService, MailService>();

        services.AddScoped<IdentitySeeder>();

        return services;
    }
}