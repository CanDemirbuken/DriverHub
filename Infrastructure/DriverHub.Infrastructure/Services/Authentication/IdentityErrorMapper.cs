using DriverHub.Application.Common.Results;
using DriverHub.Application.Contracts.Authentication;
using Microsoft.AspNetCore.Identity;

namespace DriverHub.Infrastructure.Services.Authentication;

internal static class IdentityErrorMapper
{
    public static IReadOnlyCollection<Error> Map(IEnumerable<IdentityError> identityErrors)
    {
        ArgumentNullException.ThrowIfNull(identityErrors);

        return identityErrors
            .Select(Map)
            .DistinctBy(error => error.Code)
            .ToArray();
    }

    private static Error Map(IdentityError identityError)
    {
        return identityError.Code switch
        {
            nameof(IdentityErrorDescriber.DuplicateEmail)
                => AuthenticationErrors.EmailAlreadyExists,

            nameof(IdentityErrorDescriber.DuplicateUserName)
                => AuthenticationErrors.EmailAlreadyExists,

            nameof(IdentityErrorDescriber.InvalidEmail)
                => AuthenticationErrors.InvalidEmail,

            nameof(IdentityErrorDescriber.InvalidUserName)
                => AuthenticationErrors.InvalidUserName,

            nameof(IdentityErrorDescriber.PasswordTooShort)
                => AuthenticationErrors.PasswordTooShort,

            nameof(IdentityErrorDescriber.PasswordRequiresDigit)
                => AuthenticationErrors.PasswordRequiresDigit,

            nameof(IdentityErrorDescriber.PasswordRequiresLower)
                => AuthenticationErrors.PasswordRequiresLowercase,

            nameof(IdentityErrorDescriber.PasswordRequiresUpper)
                => AuthenticationErrors.PasswordRequiresUppercase,

            nameof(IdentityErrorDescriber.PasswordRequiresNonAlphanumeric)
                => AuthenticationErrors.PasswordRequiresNonAlphanumeric,

            nameof(IdentityErrorDescriber.PasswordRequiresUniqueChars)
                => AuthenticationErrors.PasswordRequiresUniqueChars,

            _ => AuthenticationErrors.RegistrationFailed
        };
    }
}