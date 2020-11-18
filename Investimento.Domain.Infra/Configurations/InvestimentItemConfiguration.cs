using Investimento.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Investimento.Domain.Infra.Configurations
{
    class InvestimentItemConfiguration : IEntityTypeConfiguration<InvestimentItem>
    {
        public void Configure(EntityTypeBuilder<InvestimentItem> builder)
        {
            builder.ToTable("InvestimentItem");

            builder.ConfigureBaseEntity();

            builder
                .Property(x => x.Ticker)
                .HasMaxLength(5)
                .HasColumnType("varchar")
                .IsRequired();

            builder
                .Property(x => x.Quotation)
                .HasColumnType("numeric(18,4)")
                .IsRequired();

            builder
                .Property(x => x.Amount)
                .HasColumnType("integer")
                .IsRequired();

            builder
                .Property(x => x.Total)
                .HasColumnType("numeric(18,4)")
                .IsRequired();

            builder
                .HasOne<Investiment>()
                .WithMany(x => x.InvestimentItems)
                .HasForeignKey(p => p.InvestimentId);
        }
    }
}
