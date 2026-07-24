namespace DriverHub.Application.Exceptions;

public sealed class ConflictException : Exception
{
    public ConflictException() : base("İlgili kayıt zaten mevcut.") { }
    public ConflictException(string message) : base(message) { }
}