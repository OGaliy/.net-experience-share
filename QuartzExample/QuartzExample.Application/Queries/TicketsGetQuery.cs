using MediatR;
using QuartzExample.Application.ViewModels;

namespace QuartzExample.Application.Queries;

public record TicketsGetQuery(List<int> TicketIds) : IRequest<IEnumerable<TicketViewModel>>;
