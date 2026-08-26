using API.CQRS.Interfaces;
using API.Requests;


namespace API.CQRS.Commands;

public record class UpdatePatientCommand(PatientUpdateRequest Request) : ICommand;