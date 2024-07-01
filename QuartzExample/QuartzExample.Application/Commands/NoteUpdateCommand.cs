using MediatR;

namespace QuartzExample.Application.Commands;

public record NoteUpdateCommand(int TicketId, int NoteId, string Content, int ByUserId) : IRequest<bool>;
