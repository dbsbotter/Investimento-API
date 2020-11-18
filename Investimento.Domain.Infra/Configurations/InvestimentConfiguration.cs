using Investimento.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Investimento.Domain.Infra.Configurations
{
    class InvestimentConfiguration : IEntityTypeConfiguration<Investiment>
    {
        public void Configure(EntityTypeBuilder<Investiment> builder)
        {
            builder.ToTable("Investiment");

            builder.ConfigureBaseEntity();

            builder
                .Property(x => x.Description)
                .HasMaxLength(50)
                .HasColumnType("varchar")
                .IsRequired();

            builder
                .Property(x => x.Total)
                .HasColumnType("numeric(18,2)")
                .IsRequired();

            builder
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(p => p.UserId);

            builder
                .HasMany(c => c.InvestimentItems)
                .WithOne(e => e.Investiment);
        }
    }
}
