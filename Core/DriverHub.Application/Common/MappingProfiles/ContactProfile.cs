using AutoMapper;
using DriverHub.Application.Features.ContactFeatures.Commands.CreateContact;
using DriverHub.Application.Features.ContactFeatures.Commands.UpdateContact;
using DriverHub.Application.Features.ContactFeatures.Queries.GetContactById;
using DriverHub.Domain.Entities;

namespace DriverHub.Application.Common.MappingProfiles;

public sealed class ContactProfile : Profile
{
    public ContactProfile()
    {
        CreateMap<Contact, GetContactByIdQueryResponse>();

        CreateMap<CreateContactCommand, Contact>()
            .ForMember(destination => destination.Id, options => options.Ignore())
            .ForMember(destination => destination.CreatedDate, options => options.Ignore())
            .ForMember(destination => destination.UpdatedDate, options => options.Ignore());

        CreateMap<UpdateContactCommand, Contact>()
            .ForMember(destination => destination.Id, options => options.Ignore())
            .ForMember(destination => destination.CreatedDate, options => options.Ignore())
            .ForMember(destination => destination.UpdatedDate, options => options.Ignore());
    }
}
