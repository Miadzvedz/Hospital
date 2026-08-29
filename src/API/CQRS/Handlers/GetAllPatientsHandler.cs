using API.CQRS.Queries;
using API.Data.Models;
using API.Interfaces;
using API.Responses;
using API.ValueTypes;
using AutoMapper;
using API.CQRS.Interfaces;


namespace API.CQRS.Handlers;

public class GetAllPatientsHandler : IQueryHandler<GetAllPatientsQuery, IEnumerable<PatientResponse>>
{
    private readonly IRepository<Patient> _repository;
    private readonly IMapper _mapper;

    public GetAllPatientsHandler(IRepository<Patient> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;       
    }

    public async Task<Result<IEnumerable<PatientResponse>>> Handle(GetAllPatientsQuery query, CancellationToken cancellationToken)
    {
        var collectionFromDb = await _repository.GetAll(orderBy: o => o.OrderBy(i => i.BirthDate),
            token: cancellationToken);

        var response = _mapper.Map<IEnumerable<PatientResponse>>(collectionFromDb);

        return response.Any()
            ? Result.Success(response)
            : Result.Failure<IEnumerable<PatientResponse>>(new Error("nothing found for your request"));
    }
}