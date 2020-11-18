using Investimento.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Investimento.Domain.Infra.Configurations
{
    public static class BaseConfigurationExtension
    {
        public static void ConfigureBaseEntity<T>(this EntityTypeBuilder<T> builder) where T : Entity
        {
            builder.Property(x => x.Id);

            builder
                .Property(x => x.CreatedAt)
                .HasColumnType("datetime")
                .IsRequired();

            builder
                .Property(x => x.CreatedAt)
                .HasColumnType("datetime")
                .IsRequired();
        }
    }
}
