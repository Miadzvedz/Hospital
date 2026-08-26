using API.CQRS.Commands;
using API.Data.Models;
using API.Interfaces;
using API.ValueTypes;
using AutoMapper;
using API.CQRS.Interfaces;

namespace API.CQRS.Handlers;

public class DeletePatientHandler : ICommandHandler<DeletePatientCommand>
{
    private readonly IRepository<Patient> _repository;
    private readonly IMapper _mapper;

    public DeletePatientHandler(IRepository<Patient> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
        
    }

    public async Task<Result> Handle(DeletePatientCommand command, CancellationToken cancellationToken)
    {
        var patient = await _repository.Get(
            predicate: i => i.Name.Id == command.Id,
            token: cancellationToken);

        if (patient == null)  
            return Result.Failure(new Error($"entity with specified id: {command.Id} not found."));

        var sucssess = await _repository.Delete(patient, cancellationToken);

        return sucssess 
            ? Result.Success() 
            : Result.Failure(new Error("deletion unsuccessful."));
    }
}
