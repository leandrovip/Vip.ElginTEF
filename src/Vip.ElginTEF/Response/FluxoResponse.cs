using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Vip.ElginTEF.Enums;
using Vip.ElginTEF.Extensions;

namespace Vip.ElginTEF.Response;

internal class FluxoResponse
{
    #region Propriedades

    [JsonProperty("automacao_coleta_retorno")]
    public string ColetaRetorno { get; set; }

    [JsonProperty("automacao_coleta_sequencial")]
    public string ColetaSequencial { get; set; }

    [JsonProperty("automacao_coleta_palavra_chave")]
    public string ColetaPalavraChave { get; set; }

    [JsonProperty("automacao_coleta_opcao")]
    public string ColetaOpcao { get; set; }

    [JsonProperty("automacao_coleta_tipo")]
    public string ColetaTipo { get; set; }

    [JsonProperty("mensagemResultado")] 
    public string MensagemResultado { get; set; }

    [JsonProperty("sequencial")]
    public string Sequencial { get; set; }

    [JsonProperty("admUsuario")]
    public string Usuario { get; set; }

    [JsonProperty("admSenha")]
    public string Senha { get; set; }

    public TipoInformacao TipoInformacao => ObterTipoInformacao();

    #endregion

    #region Métodos Publicos

    public List<string> ObterListaOpcao()
    {
        if (ColetaOpcao.IsNullOrEmpty()) return new List<string>();

        var lista = ColetaOpcao.Split(';');
        return lista.ToList();
    }

    #endregion

    #region Métodos Privados

    private TipoInformacao ObterTipoInformacao()
    {
        if (ColetaTipo.IsNullOrEmpty()) return TipoInformacao.Geral;

        return ColetaTipo switch
        {
            "*" => TipoInformacao.Geral,
            "A" => TipoInformacao.Alfabetico,
            "D" => TipoInformacao.DataHora,
            "N" => TipoInformacao.Numerico,
            "X" => TipoInformacao.Numerico,
            _ => TipoInformacao.Geral
        };
    }

    #endregion
}