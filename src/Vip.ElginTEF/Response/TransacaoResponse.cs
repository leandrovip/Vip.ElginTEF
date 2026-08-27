using Vip.ElginTEF.Enums;
using Vip.ElginTEF.Extensions;

namespace Vip.ElginTEF.Response;

public class TransacaoResponse
{
    #region Propriedades

    public string CnpjCredenciadora { get; set; }
    public string CodigoAutorizacao { get; set; }
    public string ComprovanteDiferenciadoLoja { get; set; }
    public string ComprovanteDiferenciadoPortador { get; set; }
    public string DataHoraTransacao { get; set; }
    public string FormaPagamento { get; set; }
    public string IdentificadorEstabelecimento { get; set; }
    public string IdentificadorPontoCaptura { get; set; }
    public string MensagemResultado { get; set; }
    public string NomeBandeira { get; set; }
    public string NomeEstabelecimento { get; set; }
    public string NomeProduto { get; set; }
    public string NomeProvedor { get; set; }
    public string NsuTerminal { get; set; }
    public string NsuTransacao { get; set; }
    public string NumeroParcelas { get; set; }
    public string PanMascarado { get; set; }
    public string ResultadoTransacao { get; set; }
    public string Retorno { get; set; }
    public string Sequencial { get; set; }
    public string Servico { get; set; }
    public string TipoCartao { get; set; }
    public string TipoFinanciamento { get; set; }
    public string Transacao { get; set; }
    public string UniqueID { get; set; }
    public string ValorTotal { get; set; }
    public string Loja { get; set; }
    
    public TipoTransacao TipoTransacao => ObterTipoTransacao();
    public bool PodeConfirmar => SePodeConfirmar();
    public bool PodeFinalizar => SePodeFinalizar();
    public bool HouveErro => SeHouveErro();

    public void RemoverPontuacaoCnpjCredenciadora()
    {
        CnpjCredenciadora = CnpjCredenciadora.TrimVip().Replace(".", "").Replace("-", "").Replace("/", "");
    }

    #endregion

    #region Métodos Privados

    private TipoTransacao ObterTipoTransacao()
    {
        return Transacao.ToLower() switch
        {
            "cartao vender" => TipoTransacao.Pagamento,
            "administracao cancelar" => TipoTransacao.Cancelamento,
            "administracao pendente" => TipoTransacao.Pendencia,
            "administracao extrato transacao" => TipoTransacao.Extrato,
            "administracao reimprimir" => TipoTransacao.Reimpressao,
            _ => TipoTransacao.NaoIdentificado
        };
    }

    private bool SePodeConfirmar() => Retorno.TrimVip() == "0";

    private bool SePodeFinalizar() => Retorno.TrimVip() == "1";

    private bool SeHouveErro() => Retorno is not ("1" or "0");

    #endregion
}