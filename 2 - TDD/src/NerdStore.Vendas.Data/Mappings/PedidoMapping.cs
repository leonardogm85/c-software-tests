using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NerdStore.Vendas.Domain;

namespace NerdStore.Vendas.Data.Mappings
{
    public class PedidoMapping : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Codigo)
                .HasDefaultValueSql("next value for minhasequencia");

            builder.HasMany(p => p.PedidoItens)
                .WithOne(i => i.Pedido)
                .HasForeignKey(i => i.PedidoId);

            builder.ToTable("Pedidos");
        }
    }
}
