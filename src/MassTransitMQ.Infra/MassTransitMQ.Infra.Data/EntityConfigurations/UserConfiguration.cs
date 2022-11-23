using MassTransitMQ.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MassTransitMQ.Infra.Data.EntityConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(p => p.CreatedOn).ValueGeneratedOnAdd();
        builder.Property(p => p.UpdatedOn).ValueGeneratedOnAddOrUpdate();
    }
}