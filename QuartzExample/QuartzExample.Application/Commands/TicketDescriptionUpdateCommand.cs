using MediatR;

namespace QuartzExample.Application.Commands;

public record TicketDescriptionUpdateCommand(int TicketId, string Description, int ByUserId) : IRequest<bool>;
