using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Foundation.Configurations;

public class RoomTypeConfiguration : IEntityTypeConfiguration<RoomType>
{
    public void Configure(EntityTypeBuilder<RoomType> builder)
    {
        builder.ToTable("RoomType");
        builder.HasKey(rt => rt.Id);

        builder.HasOne<Property>()
            .WithMany()
            .HasForeignKey(rt => rt.PropertyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(rt => rt.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(rt => rt.DailyPrice)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(rt => rt.Currency)
            .HasConversion(
                currency => currency.Value,
                value => Currency.Create(value).Value
            );

        builder.Property(rt => rt.MinPersonCount)
            .IsRequired();

        builder.Property(rt => rt.MaxPersonCount)
            .IsRequired();

        builder.Property(rt => rt.Services)
            .HasMaxLength(1000)
            .IsRequired();

        builder.Property(rt => rt.Amenities)
            .HasMaxLength(1000)
            .IsRequired();
    }
}