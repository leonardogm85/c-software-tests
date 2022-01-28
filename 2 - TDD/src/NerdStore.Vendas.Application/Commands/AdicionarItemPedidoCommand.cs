using FluentValidation;
using NerdStore.Core.Messages;
using NerdStore.Vendas.Domain;
using System;

namespace NerdStore.Vendas.Application.Commands
{
    public class AdicionarItemPedidoCommand : Command
    {
        public Guid ClienteId { get; private set; }
        public Guid ProdutoId { get; private set; }
        public string NomeProduto { get; private set; }
        public int Quantidade { get; private set; }
        public decimal ValorUnitario { get; private set; }

        public AdicionarItemPedidoCommand(Guid clienteId, Guid produtoId, string nomeProduto, int quantidade, decimal valorUnitario)
        {
            ClienteId = clienteId;
            ProdutoId = produtoId;
            NomeProduto = nomeProduto;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
        }

        public override bool EhValido()
        {
            ValidationResult = new AdicionarItemPedidoCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class AdicionarItemPedidoCommandValidation : AbstractValidator<AdicionarItemPedidoCommand>
    {
        public static string IdClienteErroMsg => "Id do cliente inválido.";
        public static string IdProdutoErroMsg => "Id do produto inválido.";
        public static string NomeErroMsg => "O nome do produto não foi informado.";
        public static string QuantidadeMaximaErroMsg => $"A quantidade máxima de um item é {Pedido.MaxUnidadesItem}.";
        public static string QuantidadeMinimaErroMsg => $"A quantidade mínima de um item é {Pedido.MinUnidadesItem}.";
        public static string ValorErroMsg => "O valor do item precisa ser maior que 0.";

        public AdicionarItemPedidoCommandValidation()
        {
            RuleFor(i => i.ClienteId)
                .NotEqual(Guid.Empty)
                .WithMessage(IdClienteErroMsg);
            RuleFor(i => i.ProdutoId)
                .NotEqual(Guid.Empty)
                .WithMessage(IdProdutoErroMsg);
            RuleFor(i => i.NomeProduto)
                .NotEmpty()
                .WithMessage(NomeErroMsg);
            RuleFor(i => i.Quantidade)
                .GreaterThanOrEqualTo(Pedido.MinUnidadesItem)
                .WithMessage(QuantidadeMinimaErroMsg)
                .LessThanOrEqualTo(Pedido.MaxUnidadesItem)
                .WithMessage(QuantidadeMaximaErroMsg);
            RuleFor(i => i.ValorUnitario)
                .GreaterThan(0)
                .WithMessage(ValorErroMsg);
        }
    }
}
