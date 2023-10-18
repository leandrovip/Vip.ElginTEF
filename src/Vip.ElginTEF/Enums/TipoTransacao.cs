using System.ComponentModel;

namespace Vip.ElginTEF.Enums;

public enum TipoTransacao
{
    [Description("Não identificado")] NaoIdentificado,
    [Description("Pagamento")] Pagamento,
    [Description("Cancelamento")] Cancelamento,
    [Description("Pendência")] Pendencia,
    [Description("Reimpressão")] Reimpressao,
    [Description("Extrato")] Extrato
}