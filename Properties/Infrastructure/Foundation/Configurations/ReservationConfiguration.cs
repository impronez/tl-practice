using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Foundation.Configurations;

public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.ToTable("Reservation");
        builder.HasKey(r => r.Id);
        
        builder.HasOne<Property>()
            .WithMany()
            .HasForeignKey(r => r.PropertyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<RoomType>()
            .WithMany()
            .HasForeignKey(r => r.RoomTypeId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.Property(r => r.ArrivalDateTime)
            .IsRequired();
        
        builder.Property(r => r.DepartureDateTime)
            .IsRequired();
        
        builder.Property(r => r.GuestName)
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(r => r.GuestPhoneNumber)
            .HasMaxLength(15)
            .IsRequired();
        
        builder.Property(r => r.Total)
            .IsRequired()
            .HasColumnType("decimal(18,2)");
        
        builder.Property(rt => rt.Currency)
            .HasConversion(
                currency => currency.Value,
                value => Currency.Create(value).Value
            );
    }
}