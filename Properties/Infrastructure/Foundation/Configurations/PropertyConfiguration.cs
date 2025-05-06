using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Foundation.Configurations;

public class PropertyConfiguration : IEntityTypeConfiguration<Property>
{
    public void Configure(EntityTypeBuilder<Property> builder)
    {
        builder.ToTable(nameof(Property));
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Name)
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(p => p.Country)
            .HasMaxLength(50)
            .IsRequired();
        
        builder.Property(p => p.City)
            .HasMaxLength(50)
            .IsRequired();
        
        builder.Property(p => p.Address)
            .HasMaxLength(200)
            .IsRequired();
        
        builder.Property(p => p.Latitude)
            .IsRequired()
            .HasColumnType("float");

        builder.Property(p => p.Longitude)
            .IsRequired()
            .HasColumnType("float");
    }
}