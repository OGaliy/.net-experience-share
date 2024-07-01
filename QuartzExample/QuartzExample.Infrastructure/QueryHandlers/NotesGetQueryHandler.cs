using AutoMapper;
using MediatR;
using QuartzExample.Application.Queries;
using QuartzExample.Application.ViewModels;
using QuartzExample.Domain.Aggregates.TicketAggregate;
using QuartzExample.Domain.Exceptions;
using QuartzExample.Domain.Specifications;
using QuartzExample.Infrastructure.Data.Repositories;

namespace QuartzExample.Infrastructure.QueryHandlers;

public class NotesGetQueryHandler(
    IRepository<Ticket> repository,
    IMapper mapper) : IRequestHandler<NotesGetQuery, IEnumerable<NoteViewModel>>
{
    private readonly IRepository<Ticket> _repository = repository;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<NoteViewModel>> Handle(NotesGetQuery request, CancellationToken cancellationToken)
    {
        var ticket = (await _repository.GetBySpecAsync(new TicketByIdWithNotesSpec(request.TicketId))).FirstOrDefault();

        if (ticket == null)
        {
            throw new TicketNotFoundException(request.TicketId);
        }

        return _mapper.Map<IEnumerable<NoteViewModel>>(ticket.Notes);
    }
}
