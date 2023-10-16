using System;
using Vip.ElginTEF.Enums;
using Vip.ElginTEF.Extensions;

namespace Vip.ElginTEF.Request;

public class AdmRequest
{
    #region Propriedades

    public TipoOperacaoAdm TipoOperacao { get; set; }
    public string Usuario { get; set; }
    public string Senha { get; set; }

    public DateTime? DataTransacao { get; set; }
    public string NsuTransacao { get; set; }
    public decimal? ValorTransacao { get; set; }

    public int CodigoOperacao => TipoOperacao.ToInt();
    public string ValorTransacaoFormatado => ValorTransacao.HasValue ? ValorTransacao.Value.ToString("N").OnlyNumbers() : "";

    #endregion

    #region Construtores

    public AdmRequest(TipoOperacaoAdm tipoOperacao, string usuario, string senha)
    {
        TipoOperacao = tipoOperacao;
        Usuario = usuario;
        Senha = senha;
    }

    public AdmRequest(TipoOperacaoAdm tipoOperacao, string usuario, string senha, DateTime? dataTransacao, string nsuTransacao, decimal? valorTransacao) : this(tipoOperacao, usuario, senha)
    {
        DataTransacao = dataTransacao;
        NsuTransacao = nsuTransacao;
        ValorTransacao = valorTransacao;
    }

    #endregion
}