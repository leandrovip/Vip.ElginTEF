using System.Text;
using Vip.ElginTEF.Core;
using Vip.ElginTEF.Interfaces;
using Vip.ElginTEF.Models;

namespace Vip.ElginTEF.Services
{
    public abstract class TefLibrary : VipSafeHandle, ILibrary
    {
        #region Constructors

        protected TefLibrary(Configuracao configuracao, string caminhoLib, Encoding encoding) : base(caminhoLib)
        {
            Configuracao = configuracao;
            CaminhoLib = caminhoLib;
            Encoding = encoding;
        }

        #endregion Constructors

        #region Propriedades

        public Configuracao Configuracao { get; protected set; }
        public Encoding Encoding { get; protected set; }
        public string CaminhoLib { get; protected set; }
        public string ModeloLib { get; protected set; }

        #endregion Propriedades

        #region Metodos Abstratos

        public abstract int GetProdutoTef();
        public abstract string GetClientTCP();
        public abstract string SetClientTCP(string ip, int porta);
        public abstract string ConfigurarDadosPDV(string textoPinpad, string versaoAC, string nomeEstabelecimento, string loja, string identificadorPontoCaptura);
        public abstract string IniciarOperacaoTEF(string dadosCaptura);
        public abstract string RecuperarOperacaoTEF(string dadosCaptura);
        public abstract string RealizarPagamentoTEF(int codigoOperacao, string dadosCaptura, bool novaTransacao);
        public abstract string RealizarAdmTEF(int codigoOperacao, string dadosCaptura, bool novaTransacao);
        public abstract string RealizarPixTEF(string dadosCaptura, bool novaTransacao);
        public abstract string ConfirmarOperacaoTEF(int id, int acao);
        public abstract string FinalizarOperacaoTEF(int id);
        public abstract string RealizarColetaPinPad(int tipoColeta, bool confirmar);
        public abstract string ConfirmarCapturaPinPad(int tipoCaptura, string dadosCaptura);

        #endregion

        #region Metodos

        protected string FromEncoding(string str) => Encoding.GetString(Encoding.Default.GetBytes(str));

        protected string ToEncoding(string str) => Encoding.Default.GetString(Encoding.GetBytes(str));

        #endregion
    }
}