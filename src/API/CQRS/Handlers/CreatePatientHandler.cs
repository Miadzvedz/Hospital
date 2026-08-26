using API.CQRS.Commands;
using API.Data.Models;
using API.Interfaces;
using API.Responses;
using API.ValueTypes;
using AutoMapper;
using API.CQRS.Interfaces;

namespace API.CQRS.Handlers;

public class CreatePatientHandler : ICommandHandler<CreatePatientCommand, PatientResponse>
{
    private readonly IRepository<Patient> _repository;
    private readonly IMapper _mapper;

    public CreatePatientHandler(IRepository<Patient> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
        
    }

    public async Task<Result<PatientResponse>> Handle(CreatePatientCommand command, CancellationToken cancellationToken)
    {
        var patient = _mapper.Map<Patient>(command.Request);
        var patientFromDb = await _repository.Create(patient, cancellationToken);
        var response = _mapper.Map<PatientResponse>(patientFromDb);

        return Result.Create(response);
    }
}
