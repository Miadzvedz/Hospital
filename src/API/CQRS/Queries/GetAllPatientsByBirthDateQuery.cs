using API.Constants;
using API.CQRS.Interfaces;
using API.Responses;

namespace API.CQRS.Queries;

public record GetAllPatientsByBirthDateQuery(SearchingPrefix Prefix, DateTime StartDate, DateTime EndDate) : IQuery<IEnumerable<PatientResponse>>;