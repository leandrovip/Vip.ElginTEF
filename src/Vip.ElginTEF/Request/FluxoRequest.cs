using Newtonsoft.Json;

namespace Vip.ElginTEF.Request;

internal class FluxoRequest
{
    #region Propriedades

    [JsonProperty("automacao_coleta_retorno")]
    public string ColetaRetorno { get; set; }

    [JsonProperty("automacao_coleta_sequencial")]
    public string ColetaSequencial { get; set; }

    [JsonProperty("automacao_coleta_informacao")]
    public string ColetaInformacao { get; set; }

    #endregion
}