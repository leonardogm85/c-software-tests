using NerdStore.Core.DomainObjects;
using System;

namespace NerdStore.Vendas.Domain
{
    public class PedidoItem : Entity
    {
        public PedidoItem(Guid produtoId, string nomeProduto, int quantidade, decimal valorUnitario)
        {
            if (quantidade < Pedido.MinUnidadesItem)
            {
                throw new DomainException($"Mínimo de {Pedido.MinUnidadesItem} unidades por produto.");
            }

            ProdutoId = produtoId;
            NomeProduto = nomeProduto;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
        }

        protected PedidoItem()
        {
        }

        public Guid PedidoId { get; private set; }
        public Guid ProdutoId { get; private set; }
        public string NomeProduto { get; private set; }
        public int Quantidade { get; private set; }
        public decimal ValorUnitario { get; private set; }

        public Pedido Pedido { get; private set; }

        public decimal CalcularValor()
        {
            return Quantidade * ValorUnitario;
        }

        internal void AdicionarUnidades(int unidades)
        {
            Quantidade += unidades;
        }

        internal void AtualizarUnidades(int unidades)
        {
            Quantidade = unidades;
        }

        internal void AssociarPedido(Guid pedidoId)
        {
            PedidoId = pedidoId;
        }
    }
}
