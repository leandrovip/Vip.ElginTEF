using System;

namespace Vip.ElginTEF.Events
{
    public sealed class MensagemRetornoEventArgs : EventArgs
    {
        #region Constructor

        public MensagemRetornoEventArgs(int? codigo, string mensagem)
        {
            Codigo = codigo ?? 9;
            Mensagem = mensagem ?? "Sem mensagem de retorno";
        }

        #endregion Constructor

        #region Properties

        public int Codigo { get; private set; }

        public string Mensagem { get; private set; }

        #endregion Properties
    }
}