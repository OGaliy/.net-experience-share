using QuartzExample.Application.ViewModels;

namespace QuartzExample.Application.Services;

public interface IStreamTicketModifiedService
{
    TicketViewModel AddMessage(TicketViewModel ticketViewModel);

    IObservable<TicketViewModel> GetMessages(DateTime fromTime);
}
