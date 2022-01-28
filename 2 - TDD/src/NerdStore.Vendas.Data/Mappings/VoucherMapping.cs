using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NerdStore.Vendas.Domain;

namespace NerdStore.Vendas.Data.Mappings
{
    public class VoucherMapping : IEntityTypeConfiguration<Voucher>
    {
        public void Configure(EntityTypeBuilder<Voucher> builder)
        {
            builder.HasKey(v => v.Id);

            builder.Property(v => v.Codigo)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.HasMany(v => v.Pedidos)
                .WithOne(p => p.Voucher)
                .HasForeignKey(p => p.VoucherId);

            builder.ToTable("Vouchers");
        }
    }
}
