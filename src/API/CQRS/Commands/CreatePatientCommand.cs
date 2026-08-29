using API.CQRS.Interfaces;
using API.Requests;
using API.Responses;

namespace API.CQRS.Commands;

public record class CreatePatientCommand(PatientCreateRequest Request) : ICommand<PatientResponse>;

