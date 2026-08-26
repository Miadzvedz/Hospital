using API.CQRS.Interfaces;
using API.Requests;
using API.Responses;

namespace API.CQRS.Commands;

public record BatchPatientCommand(IEnumerable<PatientCreateRequest> Request) : ICommand<IEnumerable<PatientResponse>>;

