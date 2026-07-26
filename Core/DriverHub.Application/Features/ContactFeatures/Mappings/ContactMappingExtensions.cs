using DriverHub.Application.Features.ContactFeatures.Commands.CreateContact;
using DriverHub.Application.Features.ContactFeatures.Commands.UpdateContact;
using DriverHub.Application.Features.ContactFeatures.Queries.GetContactById;
using DriverHub.Domain.Entities;

namespace DriverHub.Application.Features.ContactFeatures.Mappings;

public static class ContactMappingExtensions
{
    public static GetContactByIdQueryResponse ToGetByIdResponse(
        this Contact contact)
    {
        return new GetContactByIdQueryResponse(
            contact.Id,
            contact.Name,
            contact.Email,
            contact.Subject,
            contact.Message);
    }

    public static Contact ToEntity(this CreateContactCommand command)
    {
        return new Contact
        {
            Name = command.Name,
            Email = command.Email,
            Subject = command.Subject,
            Message = command.Message
        };
    }

    public static void ApplyTo(
        this UpdateContactCommand command,
        Contact contact)
    {
        contact.Name = command.Name;
        contact.Email = command.Email;
        contact.Subject = command.Subject;
        contact.Message = command.Message;
    }
}