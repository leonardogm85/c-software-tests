using NerdStore.Core.DomainObjects;
using System;

namespace NerdStore.Vendas.Domain
{
    public class PedidoItem
    {
        public PedidoItem(Guid produtoId, string produtoNome, int quantidade, decimal valorUnitario)
        {
            if (quantidade < Pedido.MinUnidadesItem)
            {
                throw new DomainException($"Mínimo de {Pedido.MinUnidadesItem} unidades por produto.");
            }

            ProdutoId = produtoId;
            ProdutoNome = produtoNome;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
        }

        public Guid ProdutoId { get; private set; }
        public string ProdutoNome { get; private set; }
        public int Quantidade { get; private set; }
        public decimal ValorUnitario { get; private set; }

        internal decimal CalcularValor()
        {
            return ValorUnitario * Quantidade;
        }

        internal void AdicionarUnidades(int quantidade)
        {
            Quantidade += quantidade;
        }
    }
}
