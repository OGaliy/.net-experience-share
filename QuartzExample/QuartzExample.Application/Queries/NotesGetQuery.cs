using MediatR;
using QuartzExample.Application.ViewModels;

namespace QuartzExample.Application.Queries;

public record NotesGetQuery(int TicketId) : IRequest<IEnumerable<NoteViewModel>>;
