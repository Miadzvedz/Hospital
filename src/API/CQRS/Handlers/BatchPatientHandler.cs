using API.CQRS.Commands;
using API.Data.Models;
using API.Interfaces;
using API.Responses;
using API.ValueTypes;
using AutoMapper;
using API.CQRS.Interfaces;


namespace API.CQRS.Handlers;

public class BatchPatientHandler : ICommandHandler<BatchPatientCommand, IEnumerable<PatientResponse>>
{
    private readonly IRepository<Patient> _repository;
    private readonly IMapper _mapper;

    public BatchPatientHandler(IRepository<Patient> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;     
    }

    public async Task<Result<IEnumerable<PatientResponse>>> Handle(BatchPatientCommand command, CancellationToken cancellationToken)
    {
        var patient = _mapper.Map<IEnumerable<Patient>>(command.Request);
        var patientCollectionFromDb = await _repository.Create(patient, cancellationToken);
        var response = _mapper.Map<IEnumerable<PatientResponse>>(patientCollectionFromDb);

        return Result.Create(response);
    }
}
