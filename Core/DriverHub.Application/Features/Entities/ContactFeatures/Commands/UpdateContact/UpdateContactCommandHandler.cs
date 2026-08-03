using AutoMapper;
using DriverHub.Application.Common.Errors;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Interfaces.Repositories;
using DriverHub.Application.Interfaces.UnitOfWork;
using DriverHub.Domain.Entities;
using MediatR;

namespace DriverHub.Application.Features.Entities.ContactFeatures.Commands.UpdateContact;

public sealed class UpdateContactCommandHandler(IRepository<Contact> repository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UpdateContactCommand, Result>
{
    public async Task<Result> Handle(UpdateContactCommand request, CancellationToken cancellationToken)
    {
        var contact = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (contact is null)
            return Result.Failure(Error.NotFound($"{request.Id} kimlik bilgisine sahip kayıt bulunamadı.", nameof(request.Id)));

        mapper.Map(request, contact);

        repository.Update(contact);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}