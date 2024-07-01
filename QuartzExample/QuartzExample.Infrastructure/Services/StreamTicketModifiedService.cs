using QuartzExample.Application.Services;
using QuartzExample.Application.ViewModels;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace QuartzExample.Infrastructure.Services;

public class StreamTicketModifiedService : IStreamTicketModifiedService
{
    private readonly ISubject<TicketViewModel> _stream = new ReplaySubject<TicketViewModel>(1);

    public TicketViewModel AddMessage(TicketViewModel ticketViewModel)
    {
        _stream.OnNext(ticketViewModel);
        return ticketViewModel;
    }

    public IObservable<TicketViewModel> GetMessages(DateTime fromTime)
    {
        return _stream;
    }
}
