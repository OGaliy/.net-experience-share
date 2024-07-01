namespace QuartzExample.Domain.Exceptions;

public class NoteNotFoundException(int ticketId, int noteId) : Exception($"Note with id {noteId} not found for ticket with id {ticketId}")
{
}
