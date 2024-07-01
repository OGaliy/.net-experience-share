using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuartzExample.Domain.Aggregates.TicketAggregate;

namespace QuartzExample.Infrastructure.Data.EntityConfigurations;

public class NoteEntityConfiguration : IEntityTypeConfiguration<Note>
{
    public void Configure(EntityTypeBuilder<Note> builder)
    {
        builder.ToTable("notes");

        builder.HasKey(n => n.Id);

        builder.Property(n => n.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(n => n.Content)
            .HasColumnName("content")
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(n => n.TicketId)
            .HasColumnName("ticket_id")
            .IsRequired();

        builder.OwnsOne(n => n.Created, a =>
        {
            a.Property(ca => ca.At)
                .HasColumnName("created_at");

            a.Property(ca => ca.ByUserId)
                .HasColumnName("created_by")
                .IsRequired();
        }).Navigation(x => x.Created).IsRequired();

        builder.HasOne<Ticket>()
            .WithMany(x => x.Notes)
            .HasForeignKey(x => x.TicketId)
            .IsRequired();

        builder.OwnsOne(t => t.Modified, a =>
        {
            a.Property(ca => ca.At)
                .HasColumnName("modified_at");

            a.Property(ca => ca.ByUserId)
                .HasColumnName("modified_by");
        }).Navigation(x => x.Modified).IsRequired(false);
    }
}
