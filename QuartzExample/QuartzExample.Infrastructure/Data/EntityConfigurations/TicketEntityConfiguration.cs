using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuartzExample.Domain.Aggregates.TicketAggregate;

namespace QuartzExample.Infrastructure.Data.EntityConfigurations;

public class TicketEntityConfiguration : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.ToTable("tickets");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(t => t.Title)
            .HasColumnName("title")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(t => t.Description)
            .HasColumnName("description")
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(t => t.AssigneeId)
            .HasColumnName("assignee_id")
            .IsRequired();

        builder.Property(t => t.DeadLineDate)
            .HasColumnName("deadline_date");

        builder.OwnsOne(t => t.Created, a =>
        {
            a.Property(ca => ca.At)
                .HasColumnName("created_at");

            a.Property(ca => ca.ByUserId)
                .HasColumnName("created_by")
                .IsRequired();
        }).Navigation(x => x.Created).IsRequired();

        builder.OwnsOne(t => t.Modified, a =>
        {
            a.Property(ca => ca.At)
                .HasColumnName("modified_at");

            a.Property(ca => ca.ByUserId)
                .HasColumnName("modified_by");
        }).Navigation(x => x.Modified).IsRequired(false);
    }
}
