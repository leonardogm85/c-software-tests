using FluentValidation.Results;
using NerdStore.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NerdStore.Vendas.Domain
{
    public class Pedido : Entity, IAggregateRoot
    {
        public static int MaxUnidadesItem => 15;
        public static int MinUnidadesItem => 1;

        protected Pedido()
        {
            _pedidoItems = new List<PedidoItem>();
        }

        public Guid ClienteId { get; private set; }

        public decimal ValorTotal { get; private set; }

        public decimal Desconto { get; private set; }

        public bool VoucherUtiliado { get; private set; }

        public Voucher Voucher { get; private set; }

        public PedidoStatus PedidoStatus { get; private set; }

        private readonly List<PedidoItem> _pedidoItems;

        public IReadOnlyCollection<PedidoItem> PedidoItems => _pedidoItems;

        private void CalcularValorPedido()
        {
            ValorTotal = _pedidoItems.Sum(i => i.CalcularValor());

            CalcularValorComDesconto();
        }

        private void CalcularValorComDesconto()
        {
            if (!VoucherUtiliado)
            {
                return;
            }

            var desconto = 0m;

            if (Voucher.TipoDescontoVoucher == TipoDescontoVoucher.Valor)
            {
                desconto = Voucher.ValorDesconto.GetValueOrDefault();
            }
            else
            {
                desconto = (ValorTotal * Voucher.PercentualDesconto.GetValueOrDefault() / 100);
            }

            Desconto = desconto;

            var valor = ValorTotal - Desconto;

            ValorTotal = valor < 0
                ? 0
                : valor;
        }

        public void TornarRascunho()
        {
            PedidoStatus = PedidoStatus.Rascunho;
        }

        public bool PedidoItemExistente(PedidoItem pedidoItem)
        {
            return _pedidoItems.Any(item => item.ProdutoId == pedidoItem.ProdutoId);
        }

        private void ValidarQuantidadeItemPermitida(PedidoItem pedidoItem)
        {
            var quantidadeItem = pedidoItem.Quantidade;

            if (PedidoItemExistente(pedidoItem))
            {
                var itemExistente = _pedidoItems.FirstOrDefault(item => item.ProdutoId == pedidoItem.ProdutoId);

                quantidadeItem += itemExistente.Quantidade;
            }

            if (quantidadeItem > MaxUnidadesItem)
            {
                throw new DomainException($"Máximo de {MaxUnidadesItem} unidades por produto.");
            }
        }

        private void ValidarPedidoItemInexistente(PedidoItem pedidoItem)
        {
            if (!PedidoItemExistente(pedidoItem))
            {
                throw new DomainException("O item não existe no pedido.");
            }
        }

        public void AdicionarItem(PedidoItem pedidoItem)
        {
            ValidarQuantidadeItemPermitida(pedidoItem);

            if (PedidoItemExistente(pedidoItem))
            {
                var itemExistente = _pedidoItems.FirstOrDefault(item => item.ProdutoId == pedidoItem.ProdutoId);

                itemExistente.AdicionarUnidades(pedidoItem.Quantidade);

                pedidoItem = itemExistente;

                _pedidoItems.Remove(itemExistente);
            }

            _pedidoItems.Add(pedidoItem);

            CalcularValorPedido();
        }

        public void AtualizarItem(PedidoItem pedidoItem)
        {
            ValidarPedidoItemInexistente(pedidoItem);
            ValidarQuantidadeItemPermitida(pedidoItem);

            var itemExistente = _pedidoItems.FirstOrDefault(item => item.ProdutoId == pedidoItem.ProdutoId);

            _pedidoItems.Remove(itemExistente);
            _pedidoItems.Add(pedidoItem);

            CalcularValorPedido();
        }

        public void RemoverItem(PedidoItem pedidoItem)
        {
            ValidarPedidoItemInexistente(pedidoItem);

            _pedidoItems.Remove(pedidoItem);

            CalcularValorPedido();
        }

        public ValidationResult AplicarVoucher(Voucher voucher)
        {
            var result = voucher.ValidarSeAplicavel();

            if (!result.IsValid)
            {
                return result;
            }

            Voucher = voucher;
            VoucherUtiliado = true;

            CalcularValorComDesconto();

            return result;
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
}
