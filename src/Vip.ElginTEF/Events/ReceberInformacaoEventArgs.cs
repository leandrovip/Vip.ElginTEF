using System;
using System.Collections.Generic;
using System.Linq;
using Vip.ElginTEF.Enums;

namespace Vip.ElginTEF.Events;

public class ReceberInformacaoEventArgs : EventArgs
{
    #region Propriedades

    public TipoFluxo TipoFluxo { get; set; }
    public IEnumerable<string> Opcoes { get; set; }
    public TipoInformacao TipoInformacao { get; set; }
    public string MensagemUsuario { get; set; }
    public bool SeEsperaOpcoes => Opcoes.Any();

    #endregion

    #region Construtores

    public ReceberInformacaoEventArgs(TipoFluxo tipoFluxo, IEnumerable<string> opcoes, TipoInformacao tipoInformacao, string mensagem)
    {
        TipoFluxo = tipoFluxo;
        Opcoes = opcoes ?? new List<string>();
        TipoInformacao = tipoInformacao;
        MensagemUsuario = mensagem;
    }

    #endregion

    #region Métodos Estáticos

    public static ReceberInformacaoEventArgs Novo(TipoFluxo tipoFluxo, IEnumerable<string> opcoes, TipoInformacao tipo, string mensagem) => new(tipoFluxo, opcoes, tipo, mensagem);

    #endregion
}