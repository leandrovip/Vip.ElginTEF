using System.ComponentModel;

namespace Vip.ElginTEF.Enums;

public enum TipoOperacaoAdm
{
    [Description("Nenhum")] Nenhum = 0,
    [Description("Cancelamento")] Cancelamento = 1,
    [Description("Pendência")] Pendencia = 2,
    [Description("Reimpressão")] Reimpressao = 3
}