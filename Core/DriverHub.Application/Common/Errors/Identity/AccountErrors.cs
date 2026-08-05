namespace DriverHub.Application.Common.Errors.Identity;

public static class AccountErrors
{
    public static readonly Error EmailAlreadyExists = new(
        "Account.EmailAlreadyExists",
        "Bu e-mail adresi ile kayıtlı bir kullanıcı bulunmaktadır.",
        ErrorType.Conflict,
        "Email");

    public static readonly Error DefaultRoleAssignmentFailed = new(
        "Account.DefaultRoleAssignmentFailed",
        "Kullanıcıya varsayılan rol atanamadı.",
        ErrorType.Failure);

    public static readonly Error UserNotFound = Error.NotFound(
        "Account.UserNotFound",
        "Kullanıcı kaydı bulunamadı.");

    public static readonly Error InvalidEmailConfirmationToken = Error.Validation(
        "Account.InvalidEmailConfirmationToken",
        "E-mail doğrulama kodu geçersiz.",
        "ConfirmationToken");

    public static readonly Error EmailConfirmationFailed = new(
        "Account.EmailConfirmationFailed",
        "E-mail onayı başarısız oldu.",
        ErrorType.Failure);

    public static readonly Error EmailConfirmationDeliveryFailed = Error.Failure(
        "Account.EmailConfirmationDeliveryFailed",
        "Hesap oluşturuldu, ancak onay e-postası gönderilemedi.");

    public static readonly Error ForgotPasswordEmailDeliveryFailed = Error.Failure(
        "Account.ForgotPasswordEmailDeliveryFailed",
        "Şifre sıfırlama e-postası gönderilemedi.");

    public static readonly Error InvalidPasswordResetToken = Error.Validation(
        "Account.InvalidPasswordResetToken",
        "Şifre sıfırlama kodu geçersiz.",
        "ResetToken");
}