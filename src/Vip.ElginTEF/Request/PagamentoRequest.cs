using Vip.ElginTEF.Enums;
using Vip.ElginTEF.Extensions;

namespace Vip.ElginTEF.Request;

public class PagamentoRequest
{
    #region Propriedades

    public TipoOperacao TipoOperacao { get; set; }
    public decimal Valor { get; set; }
    public int QuantidadeParcelas { get; set; }

    public int CodigoOperacao => TipoOperacao.ToInt();
    public string ValorTotalFormatado => Valor > 0 ? Valor.ToString("N").OnlyNumbers() : "";

    #endregion

    #region Construtor

    public PagamentoRequest(TipoOperacao tipoOperacao, decimal valor, int quantidadeParcelas = 1)
    {
        TipoOperacao = tipoOperacao;
        Valor = valor;
        QuantidadeParcelas = quantidadeParcelas > 1 && tipoOperacao.Equals(TipoOperacao.CartaoDebito) ? 1 : quantidadeParcelas;
    }

    #endregion

    #region Métodos

    public bool SeParcelado()
    {
        return TipoOperacao.Equals(TipoOperacao.CartaoCredito) && QuantidadeParcelas > 1;
    }

    #endregion
}