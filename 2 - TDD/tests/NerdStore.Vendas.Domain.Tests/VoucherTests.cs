using System;
using System.Linq;
using Xunit;

namespace NerdStore.Vendas.Domain.Tests
{
    public class VoucherTests
    {
        [Fact(DisplayName = "Voucher - Validar Voucher Do Tipo Valor Válido")]
        [Trait("Categoria", "TDD - NerdStore.Vendas.Domain.Tests.VoucherTests")]
        public void Voucher_ValidarVoucherTipoValor_DeveEstarValido()
        {
            // Arrange
            var voucher = new Voucher(
                "PROMO-15-REAIS",
                15,
                null,
                TipoDescontoVoucher.Valor,
                1,
                DateTime.Now.AddDays(1),
                true,
                false);

            // Act
            var result = voucher.ValidarSeAplicavel();

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact(DisplayName = "Voucher - Validar Voucher Do Tipo Valor Inválido")]
        [Trait("Categoria", "TDD - NerdStore.Vendas.Domain.Tests.VoucherTests")]
        public void Voucher_ValidarVoucherTipoValor_DeveEstarInvalido()
        {
            // Arrange
            var voucher = new Voucher(
                string.Empty,
                null,
                null,
                TipoDescontoVoucher.Valor,
                0,
                DateTime.Now.AddDays(-1),
                false,
                true);

            // Act
            var result = voucher.ValidarSeAplicavel();

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal(6, result.Errors.Count());
            Assert.Contains(VoucherAplicavelValidation.AtivoErroMsg, result.Errors.Select(e => e.ErrorMessage));
            Assert.Contains(VoucherAplicavelValidation.CodigoErroMsg, result.Errors.Select(e => e.ErrorMessage));
            Assert.Contains(VoucherAplicavelValidation.DataValidadeErroMsg, result.Errors.Select(e => e.ErrorMessage));
            Assert.Contains(VoucherAplicavelValidation.QuantidadeErroMsg, result.Errors.Select(e => e.ErrorMessage));
            Assert.Contains(VoucherAplicavelValidation.UtilizadoErroMsg, result.Errors.Select(e => e.ErrorMessage));
            Assert.Contains(VoucherAplicavelValidation.ValorDescontoErroMsg, result.Errors.Select(e => e.ErrorMessage));
        }

        [Fact(DisplayName = "Voucher - Validar Voucher Do Tipo Porcentagem Válido")]
        [Trait("Categoria", "TDD - NerdStore.Vendas.Domain.Tests.VoucherTests")]
        public void Voucher_ValidarVoucherTipoPorcentagem_DeveEstarValido()
        {
            // Arrange
            var voucher = new Voucher(
                "PROMO-15-OFF",
                null,
                15,
                TipoDescontoVoucher.Porcentagem,
                1,
                DateTime.Now.AddDays(1),
                true,
                false);

            // Act
            var result = voucher.ValidarSeAplicavel();

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact(DisplayName = "Voucher - Validar Voucher Do Tipo Porcentagem Inválido")]
        [Trait("Categoria", "TDD - NerdStore.Vendas.Domain.Tests.VoucherTests")]
        public void Voucher_ValidarVoucherTipoPorcentagem_DeveEstarInvalido()
        {
            // Arrange
            var voucher = new Voucher(
                string.Empty,
                null,
                null,
                TipoDescontoVoucher.Porcentagem,
                0,
                DateTime.Now.AddDays(-1),
                false,
                true);

            // Act
            var result = voucher.ValidarSeAplicavel();

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal(6, result.Errors.Count());
            Assert.Contains(VoucherAplicavelValidation.AtivoErroMsg, result.Errors.Select(e => e.ErrorMessage));
            Assert.Contains(VoucherAplicavelValidation.CodigoErroMsg, result.Errors.Select(e => e.ErrorMessage));
            Assert.Contains(VoucherAplicavelValidation.DataValidadeErroMsg, result.Errors.Select(e => e.ErrorMessage));
            Assert.Contains(VoucherAplicavelValidation.QuantidadeErroMsg, result.Errors.Select(e => e.ErrorMessage));
            Assert.Contains(VoucherAplicavelValidation.UtilizadoErroMsg, result.Errors.Select(e => e.ErrorMessage));
            Assert.Contains(VoucherAplicavelValidation.PercentualDescontoErroMsg, result.Errors.Select(e => e.ErrorMessage));
        }
    }
}
