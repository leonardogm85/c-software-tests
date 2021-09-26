using FluentValidation;
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

    public class VoucherAplicavelValidation : AbstractValidator<Voucher>
    {
        public static string CodigoErroMsg => "Voucher sem código válido.";
        public static string DataValidadeErroMsg => "Este voucher está expirado.";
        public static string AtivoErroMsg => "Este voucher não é mais válido.";
        public static string UtilizadoErroMsg => "Este voucher já foi utilizado.";
        public static string QuantidadeErroMsg => "Este voucher não está mais disponível.";
        public static string ValorDescontoErroMsg => "O valor do desconto precisa ser superior a 0.";
        public static string PercentualDescontoErroMsg => "O valor da porcentagem de desconto precisa ser superior a 0.";

        public VoucherAplicavelValidation()
        {
            RuleFor(v => v.Codigo)
                .NotEmpty()
                .WithMessage(CodigoErroMsg);

            RuleFor(v => v.DataValidade)
                .Must(DataVencimentoSuperiorAtual)
                .WithMessage(DataValidadeErroMsg);

            RuleFor(v => v.Ativo)
                .Equal(true)
                .WithMessage(AtivoErroMsg);

            RuleFor(v => v.Utilizado)
                .Equal(false)
                .WithMessage(UtilizadoErroMsg);

            RuleFor(v => v.Quantidade)
                .GreaterThan(0)
                .WithMessage(QuantidadeErroMsg);

            When(v => v.TipoDescontoVoucher == TipoDescontoVoucher.Valor, () =>
            {
                RuleFor(v => v.ValorDesconto)
                .NotNull()
                .WithMessage(ValorDescontoErroMsg)
                .GreaterThan(0)
                .WithMessage(ValorDescontoErroMsg);
            });

            When(v => v.TipoDescontoVoucher == TipoDescontoVoucher.Porcentagem, () =>
            {
                RuleFor(v => v.PercentualDesconto)
                .NotNull()
                .WithMessage(PercentualDescontoErroMsg)
                .GreaterThan(0)
                .WithMessage(PercentualDescontoErroMsg);
            });
        }

        protected static bool DataVencimentoSuperiorAtual(DateTime dataValidade)
        {
            return dataValidade >= DateTime.Now;
        }
    }

    public enum TipoDescontoVoucher
    {
        Porcentagem = 0,
        Valor = 1
    }
}
