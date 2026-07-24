using DriverHub.Application.Common.Results;

namespace DriverHub.Application.Contracts.Identity;

public static class IdentityErrors
{
    public static readonly Error EmailAlreadyExists = Error.Conflict(
        "Identity.EmailAlreadyExists",
        "Bu e-posta adresi daha önce kullanılmıştır.");

    public static readonly Error RegistrationFailed = Error.Failure(
        "Identity.RegistrationFailed",
        "Kullanıcı kaydı gerçekleştirilemedi.");

    public static readonly Error InvalidCredentials = Error.Unauthorized(
        "Identity.InvalidCredentials",
        "E-posta veya şifre bilgisi hatalıdır.");

    public static readonly Error UserInactive = Error.Forbidden(
        "Identity.UserInactive",
        "Kullanıcı hesabı aktif değildir.");

    public static Error RegistrationFailedWithMessage(string message)
    {
        return Error.Validation(
            "Identity.RegistrationFailed",
            message);
    }
}