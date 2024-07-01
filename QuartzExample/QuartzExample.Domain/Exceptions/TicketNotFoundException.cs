namespace QuartzExample.Domain.Exceptions;

public class TicketNotFoundException(int ticketId) : Exception($"Ticket with id {ticketId} was not found.")
{
}
