using AutoMapper;
using QuartzExample.Application.ViewModels;
using QuartzExample.Domain.Aggregates.TicketAggregate;

namespace QuartzExample.Infrastructure;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Ticket, TicketViewModel>();
        CreateMap<Note, NoteViewModel>();
    }
}
