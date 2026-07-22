namespace DriverHub.Application.Exceptions;

public sealed class NotFoundException : Exception
{
    public NotFoundException() : base("İlgili kayıt bulunamadı.") { }
    public NotFoundException(string message) : base(message) { }
}