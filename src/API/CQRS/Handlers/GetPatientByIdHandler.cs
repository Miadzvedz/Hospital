using API.CQRS.Queries;
using API.Data.Models;
using API.Interfaces;
using API.Responses;
using API.ValueTypes;
using AutoMapper;
using API.CQRS.Interfaces;


namespace API.CQRS.Handlers;

public class GetPatientByIdHandler : IQueryHandler<GetPatientByIdQuery, PatientResponse>
{
    private readonly IRepository<Patient> _repository;
    private readonly IMapper _mapper;

    public GetPatientByIdHandler(IRepository<Patient> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;     
    }

    public async Task<Result<PatientResponse>> Handle(GetPatientByIdQuery query, CancellationToken cancellationToken)
    {
        var patientFromDb = await _repository.Get(i => i.Name.Id == query.Id, cancellationToken);

        var response = _mapper.Map<PatientResponse>(patientFromDb);

        return Result.Create(response);
    }
}
