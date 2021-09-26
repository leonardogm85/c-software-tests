using FluentValidation.Results;
using System;

namespace NerdStore.Vendas.Domain
{
    public class Voucher
    {
        public Voucher(
            string codigo,
            decimal? valorDesconto,
            decimal? percentualDesconto,
            TipoDescontoVoucher tipoDescontoVoucher,
            int quantidade,
            DateTime dataValidade,
            bool ativo,
            bool utilizado)
        {
            Codigo = codigo;
            ValorDesconto = valorDesconto;
            PercentualDesconto = percentualDesconto;
            TipoDescontoVoucher = tipoDescontoVoucher;
            Quantidade = quantidade;
            DataValidade = dataValidade;
            Ativo = ativo;
            Utilizado = utilizado;
        }

        public string Codigo { get; private set; }
        public decimal? ValorDesconto { get; private set; }
        public decimal? PercentualDesconto { get; private set; }
        public TipoDescontoVoucher TipoDescontoVoucher { get; private set; }
        public int Quantidade { get; private set; }
        public DateTime DataValidade { get; private set; }
        public bool Ativo { get; private set; }
        public bool Utilizado { get; private set; }

        public ValidationResult ValidarSeAplicavel()
        {
            return new VoucherAplicavelValidation().Validate(this);
        }
    }
}
