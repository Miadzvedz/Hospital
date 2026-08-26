using API.CQRS.Interfaces;
using API.Responses;

namespace API.CQRS.Queries
{
    public record class GetAllPatientsQuery() : IQuery<IEnumerable<PatientResponse>>;
}
