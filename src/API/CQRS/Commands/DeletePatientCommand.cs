using API.CQRS.Interfaces;

namespace API.CQRS.Commands;

public record class DeletePatientCommand(Guid Id) : ICommand;
