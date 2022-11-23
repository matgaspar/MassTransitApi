using MassTransitMQ.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MassTransitMQ.Infra.Data.EntityConfigurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.Property(p => p.Price).HasPrecision(10, 2);
        builder.Property(p => p.Discount).HasPrecision(10, 2);
        builder.Property(p => p.DeliveryPrice).HasPrecision(10, 2);
        builder.Property(p => p.TotalPrice).HasPrecision(10, 2);
    }
}