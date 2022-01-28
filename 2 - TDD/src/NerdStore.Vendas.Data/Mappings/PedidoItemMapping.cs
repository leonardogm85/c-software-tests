using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NerdStore.Vendas.Domain;

namespace NerdStore.Vendas.Data.Mappings
{
    public class PedidoItemMapping : IEntityTypeConfiguration<PedidoItem>
    {
        public void Configure(EntityTypeBuilder<PedidoItem> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.NomeProduto)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.HasOne(i => i.Pedido)
                .WithMany(p => p.PedidoItens);

            builder.ToTable("PedidoItens");
        }
    }
}
