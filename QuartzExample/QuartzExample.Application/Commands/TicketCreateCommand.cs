using MediatR;

namespace QuartzExample.Application.Commands;

public record TicketCreateCommand(
    string Title,
    string Description,
    int CreatedBy,
    int AssignedTo,
    List<NoteAddCommand> NoteCreateCommands,
    DateTimeOffset? DeadLine = null) : IRequest<int>;
