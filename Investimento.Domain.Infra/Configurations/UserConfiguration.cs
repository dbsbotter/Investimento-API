using Investimento.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Investimento.Domain.Infra.Configurations
{
    class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder
                .Property(x => x.Username)
                .HasMaxLength(100)
                .HasColumnType("varchar")
                .IsRequired();

            builder
                .Property(x => x.Password)
                .HasMaxLength(1000)
                .HasColumnType("varchar")
                .IsRequired();
        }
    }
}
