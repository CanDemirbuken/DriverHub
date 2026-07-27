using AutoMapper;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Interfaces.Repositories;
using DriverHub.Application.Interfaces.UnitOfWork;
using DriverHub.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DriverHub.Application.Features.ContactFeatures.Commands.CreateContact;

public sealed class CreateContactCommandHandler(IRepository<Contact> repository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<CreateContactCommand, Result<CreateContactCommandResponse>>
{
    public async Task<Result<CreateContactCommandResponse>> Handle(CreateContactCommand request, CancellationToken cancellationToken)
    {
        Contact contact = mapper.Map<Contact>(request);

        await repository.AddAsync(contact, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        CreateContactCommandResponse data = new CreateContactCommandResponse(contact.Id);
        return Result<CreateContactCommandResponse>.Success(data, StatusCodes.Status201Created);
    }
}