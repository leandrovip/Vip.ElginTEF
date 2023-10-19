using System.ComponentModel;

namespace Vip.ElginTEF.Enums;

public enum TipoInformacao
{
    [Description("Geral")] Geral,
    [Description("Texto")] Alfabetico,
    [Description("dd/MM/aaaa")] DataHora,
    [Description("Numérico")] Numerico,
    [Description("Texto e Número")] Alfanumerico
}