namespace DriverHub.Application.Contracts.Communication.Mail;

public sealed record SendMailRequest(string To, string Subject, string Body, bool IsHtml = true);