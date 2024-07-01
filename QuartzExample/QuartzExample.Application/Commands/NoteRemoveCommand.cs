using MediatR;

namespace QuartzExample.Application.Commands;

public record NoteRemoveCommand(int TicketId, int NoteId, int ByUserId) : IRequest<bool>;
