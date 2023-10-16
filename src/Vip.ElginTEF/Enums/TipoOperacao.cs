using System.ComponentModel;

namespace Vip.ElginTEF.Enums;

public enum TipoOperacao
{
    [Description("Nenhum")] Nenhum = 0,
    [Description("Cartão de crédito")] CartaoCredito = 1,
    [Description("Cartão de débito")] CartaoDebito = 2,
    [Description("Voucher (débito)")] Voucher = 3,
    [Description("Frota (débito)")] Frota = 4,
    [Description("Private Label (crédito)")] PrivateLabel = 5
}