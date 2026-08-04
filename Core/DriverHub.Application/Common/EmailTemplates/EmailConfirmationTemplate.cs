using System.Net;

namespace DriverHub.Application.Common.EmailTemplates;

public static class EmailConfirmationTemplate
{
    public static string Create(string firstName, string userId, string token)
    {
        string encodedFirstName = WebUtility.HtmlEncode(firstName);
        string encodedUserId = WebUtility.HtmlEncode(userId);
        string encodedToken = WebUtility.HtmlEncode(token);

        return $"""
            <!DOCTYPE html>
            <html lang="en">
            <body>
                <h2>DriverHub Email Confirmation</h2>

                <p>Hello {encodedFirstName},</p>

                <p>Your DriverHub account has been created successfully.</p>

                <p>Use the following information in the email confirmation endpoint:</p>

                <p><strong>User ID:</strong></p>
                <p>{encodedUserId}</p>

                <p><strong>Confirmation Token:</strong></p>
                <p>{encodedToken}</p>

                <p>If you did not create this account, you can ignore this email.</p>
            </body>
            </html>
            """;
    }
}