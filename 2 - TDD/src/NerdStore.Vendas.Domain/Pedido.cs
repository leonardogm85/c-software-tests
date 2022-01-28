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
            _pedidoItens = new List<PedidoItem>();
        }

        private readonly List<PedidoItem> _pedidoItens;

        public int Codigo { get; private set; }
        public Guid ClienteId { get; private set; }
        public Guid? VoucherId { get; private set; }
        public bool VoucherUtilizado { get; private set; }
        public decimal Desconto { get; private set; }
        public decimal ValorTotal { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public PedidoStatus PedidoStatus { get; private set; }

        public IReadOnlyCollection<PedidoItem> PedidoItens => _pedidoItens;

        public Voucher Voucher { get; private set; }

        public void TornarRascunho()
        {
            PedidoStatus = PedidoStatus.Rascunho;
        }

        public void IniciarPedido()
        {
            PedidoStatus = PedidoStatus.Iniciado;
        }

        public void FinalizarPedido()
        {
            PedidoStatus = PedidoStatus.Pago;
        }

        public void CancelarPedido()
        {
            PedidoStatus = PedidoStatus.Cancelado;
        }

        public void AtualizarUnidades(PedidoItem item, int unidades)
        {
            item.AtualizarUnidades(unidades);
            AtualizarItem(item);
        }

        private void CalcularValorPedido()
        {
            ValorTotal = _pedidoItens.Sum(i => i.CalcularValor());
            CalcularValorComDesconto();
        }

        private void CalcularValorComDesconto()
        {
            if (!VoucherUtilizado)
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
                desconto = (ValorTotal * Voucher.Percentual.GetValueOrDefault() / 100);
            }

            Desconto = desconto;

            var valor = ValorTotal - Desconto;

            ValorTotal = valor < 0
                ? 0
                : valor;
        }

        public bool PedidoItemExistente(PedidoItem pedidoItem)
        {
            return _pedidoItens.Any(item => item.ProdutoId == pedidoItem.ProdutoId);
        }

        private void ValidarQuantidadeItemPermitida(PedidoItem pedidoItem)
        {
            var quantidadeItem = pedidoItem.Quantidade;

            if (PedidoItemExistente(pedidoItem))
            {
                var itemExistente = _pedidoItens.FirstOrDefault(item => item.ProdutoId == pedidoItem.ProdutoId);

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

            pedidoItem.AssociarPedido(Id);

            if (PedidoItemExistente(pedidoItem))
            {
                var itemExistente = _pedidoItens.FirstOrDefault(item => item.ProdutoId == pedidoItem.ProdutoId);
                itemExistente.AdicionarUnidades(pedidoItem.Quantidade);
                pedidoItem = itemExistente;
                _pedidoItens.Remove(itemExistente);
            }

            _pedidoItens.Add(pedidoItem);
            CalcularValorPedido();
        }

        public void RemoverItem(PedidoItem pedidoItem)
        {
            ValidarPedidoItemInexistente(pedidoItem);

            _pedidoItens.Remove(pedidoItem);

            CalcularValorPedido();
        }

        public void AtualizarItem(PedidoItem pedidoItem)
        {
            ValidarPedidoItemInexistente(pedidoItem);
            ValidarQuantidadeItemPermitida(pedidoItem);

            pedidoItem.AssociarPedido(Id);

            var itemExistente = _pedidoItens.FirstOrDefault(item => item.ProdutoId == pedidoItem.ProdutoId);

            _pedidoItens.Remove(itemExistente);
            _pedidoItens.Add(pedidoItem);

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
            VoucherUtilizado = true;

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
