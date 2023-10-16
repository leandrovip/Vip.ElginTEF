using System;
using System.Text;
using Vip.ElginTEF.Models;

namespace Vip.ElginTEF.Interfaces
{
    public interface ILibrary : IDisposable
    {
        #region Propriedades

        Configuracao Configuracao { get; }
        Encoding Encoding { get; }
        string CaminhoLib { get; }

        #endregion Propriedades

        #region Metodos

        int GetProdutoTef();
        string GetClientTCP();
        string SetClientTCP(string ip, int porta);
        string ConfigurarDadosPDV(string textoPinpad, string versaoAC, string nomeEstabelecimento, string loja, string identificadorPontoCaptura);
        string IniciarOperacaoTEF(string dadosCaptura);
        string RecuperarOperacaoTEF(string dadosCaptura);
        string RealizarPagamentoTEF(int codigoOperacao, string dadosCaptura, bool novaTransacao);
        string RealizarAdmTEF(int codigoOperacao, string dadosCaptura, bool novaTransacao);
        string RealizarPixTEF(string dadosCaptura, bool novaTransacao);
        string ConfirmarOperacaoTEF(int id, int acao);
        string FinalizarOperacaoTEF(int id);
        string RealizarColetaPinPad(int tipoColeta, bool confirmar);
        string ConfirmarCapturaPinPad(int tipoCaptura, string dadosCaptura);

        #endregion Metodos
    }
}