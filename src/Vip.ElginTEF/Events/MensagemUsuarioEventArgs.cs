using System;
using Vip.ElginTEF.Extensions;

namespace Vip.ElginTEF.Events;

public class MensagemUsuarioEventArgs : EventArgs
{
    #region Properties

    public string Mensagem { get; private set; }

    #endregion Properties

    #region Constructor

    public MensagemUsuarioEventArgs(string mensagem)
    {
        Mensagem = mensagem.TrimVip();
    }

    #endregion Constructor

    #region Método Estático

    public static MensagemUsuarioEventArgs Novo(string mensagem)
    {
        if (mensagem.Contains("QRCODE;"))
            mensagem = "Aguardando leitura do QRCode";

        return new MensagemUsuarioEventArgs(mensagem);
    }

    #endregion
}