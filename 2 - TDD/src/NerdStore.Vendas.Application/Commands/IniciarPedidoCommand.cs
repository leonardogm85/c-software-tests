﻿using FluentValidation;
using NerdStore.Core.Messages;
using System;

namespace NerdStore.Vendas.Application.Commands
{
    public class IniciarPedidoCommand : Command
    {
        public IniciarPedidoCommand(Guid pedidoId, Guid clienteId, decimal total, string nomeCartao, string numeroCartao, string expiracaoCartao, string cvvCartao)
        {
            PedidoId = pedidoId;
            ClienteId = clienteId;
            Total = total;
            NomeCartao = nomeCartao;
            NumeroCartao = numeroCartao;
            ExpiracaoCartao = expiracaoCartao;
            CvvCartao = cvvCartao;
        }

        public Guid PedidoId { get; private set; }
        public Guid ClienteId { get; private set; }
        public decimal Total { get; private set; }
        public string NomeCartao { get; private set; }
        public string NumeroCartao { get; private set; }
        public string ExpiracaoCartao { get; private set; }
        public string CvvCartao { get; private set; }

        public override bool EhValido()
        {
            ValidationResult = new IniciarPedidoValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class IniciarPedidoValidation : AbstractValidator<IniciarPedidoCommand>
    {
        public IniciarPedidoValidation()
        {
            RuleFor(c => c.ClienteId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do cliente inválido.");
            RuleFor(c => c.PedidoId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do pedido inválido.");
            RuleFor(c => c.NomeCartao)
                .NotEmpty()
                .WithMessage("O nome do cartão não foi informado.");
            RuleFor(c => c.NumeroCartao)
                .CreditCard()
                .WithMessage("Número do cartão inválido.");
            RuleFor(c => c.ExpiracaoCartao)
                .NotEmpty()
                .WithMessage("A data de expiração do cartão não foi informado.");
            RuleFor(c => c.CvvCartao)
                .Length(3, 4)
                .WithMessage("O CVV do cartão não foi informado corretamente.");
        }
    }
}
