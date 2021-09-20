using System;
using System.Collections.Generic;
using System.Linq;

namespace NerdStore.Vendas.Domain
{
    public class Pedido
    {
        protected Pedido()
        {
            _pedidoItems = new List<PedidoItem>();
        }

        public Guid ClienteId { get; private set; }

        public decimal ValorTotal { get; private set; }

        public PedidoStatus PedidoStatus { get; private set; }

        private readonly List<PedidoItem> _pedidoItems;

        public IReadOnlyCollection<PedidoItem> PedidoItems => _pedidoItems;

        public void CalcularValorPedido()
        {
            ValorTotal = _pedidoItems.Sum(i => i.CalcularValor());
        }

        public void TornarRascunho()
        {
            PedidoStatus = PedidoStatus.Rascunho;
        }

        public void AdicionarItem(PedidoItem pedidoItem)
        {
            if (_pedidoItems.Any(item => item.ProdutoId == pedidoItem.ProdutoId))
            {
                var itemExistente = _pedidoItems.FirstOrDefault(item => item.ProdutoId == pedidoItem.ProdutoId);

                itemExistente.AdicionarUnidades(pedidoItem.Quantidade);

                pedidoItem = itemExistente;

                _pedidoItems.Remove(itemExistente);
            }

            _pedidoItems.Add(pedidoItem);

            CalcularValorPedido();
        }

        public static class PedidoFactory
        {
            public static Pedido NovoPedidoRascunho(Guid clienteId)
            {
                var pedido = new Pedido
                {
                    ClienteId = clienteId
                };

                pedido.TornarRascunho();

                return pedido;
            }
        }
    }

    public class PedidoItem
    {
        public PedidoItem(Guid produtoId, string produtoNome, int quantidade, decimal valorUnitario)
        {
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

    public enum PedidoStatus
    {
        Rascunho = 0,
        Iniciado = 1,
        Pago = 4,
        Entregue = 5,
        Cancelado = 6
    }
}
