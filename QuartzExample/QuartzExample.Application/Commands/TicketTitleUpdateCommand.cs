using MediatR;

namespace QuartzExample.Application.Commands;

public record TicketTitleUpdateCommand(int TicketId, string Title, int ByUserId) : IRequest<bool>;
