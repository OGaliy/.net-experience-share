using MediatR;

namespace QuartzExample.Application.Commands;

public record TicketAssigneeUpdateCommand(int TicketId, int AssigneeId, int ByUserId) : IRequest<bool>;
