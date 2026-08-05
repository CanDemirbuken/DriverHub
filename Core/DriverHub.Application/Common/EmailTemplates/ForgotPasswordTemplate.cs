using System.Net;

namespace DriverHub.Application.Common.EmailTemplates;

public static class ForgotPasswordTemplate
{
    public static string Create(string firstName, string email, string token)
    {
        string encodedFirstName = WebUtility.HtmlEncode(firstName);
        string encodedEmail = WebUtility.HtmlEncode(email);
        string encodedToken = WebUtility.HtmlEncode(token);

        return $"""
            <!DOCTYPE html>
            <html lang="en">
            <body>
                <h2>DriverHub Password Reset</h2>

                <p>Hello {encodedFirstName},</p>

                <p>We received a request to reset the password for your DriverHub account.</p>

                <p>Use the following information in the reset password endpoint:</p>

                <p><strong>Email:</strong></p>
                <p>{encodedEmail}</p>

                <p><strong>Password Reset Token:</strong></p>
                <p>{encodedToken}</p>

                <p>If you did not request a password reset, you can safely ignore this email. Your password will remain unchanged.</p>
            </body>
            </html>
            """;
    }
}