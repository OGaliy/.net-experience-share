using MediatR;

namespace QuartzExample.Application.Commands;

public record NoteAddCommand(int TicketId, string Content, int CreatedBy) : IRequest<bool>;
