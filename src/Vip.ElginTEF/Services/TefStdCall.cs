using System.Runtime.InteropServices;
using System.Text;
using Vip.ElginTEF.Core;
using Vip.ElginTEF.Models;

namespace Vip.ElginTEF.Services
{
    internal sealed class TefStdCall : TefLibrary
    {
        #region Delegates

        private class Delegates
        {
            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            public delegate int GetProdutoTef();

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(VipLPStr))]
            public delegate string GetClientTCP();

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(VipLPStr))]
            public delegate string SetClientTCP(
                [MarshalAs(UnmanagedType.LPStr)] string ip,
                int porta);

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(VipLPStr))]
            public delegate string ConfigurarDadosPDV(
                [MarshalAs(UnmanagedType.LPStr)] string textoPinpad,
                [MarshalAs(UnmanagedType.LPStr)] string versaoAC,
                [MarshalAs(UnmanagedType.LPStr)] string nomeEstabelecimento,
                [MarshalAs(UnmanagedType.LPStr)] string loja,
                [MarshalAs(UnmanagedType.LPStr)] string identifcadorPontoCaptura);

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(VipLPStr))]
            public delegate string IniciarOperacaoTEF(
                [MarshalAs(UnmanagedType.LPStr)] string dadosCaptura);

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(VipLPStr))]
            public delegate string RecuperarOperacaoTEF(
                [MarshalAs(UnmanagedType.LPStr)] string dadosCaptura);

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(VipLPStr))]
            public delegate string RealizarPagamentoTEF(
                int codigoOperacao,
                [MarshalAs(UnmanagedType.LPStr)] string dadosCaptura,
                bool novaTransacao);

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(VipLPStr))]
            public delegate string RealizarAdmTEF(
                int codigoOperacao,
                [MarshalAs(UnmanagedType.LPStr)] string dadosCaptura,
                bool novaTransacao);

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(VipLPStr))]
            public delegate string RealizarPixTEF(
                [MarshalAs(UnmanagedType.LPStr)] string dadosCaptura,
                bool novaTransacao);

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(VipLPStr))]
            public delegate string ConfirmarOperacaoTEF(
                int id,
                int acao);

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(VipLPStr))]
            public delegate string FinalizarOperacaoTEF(
                int id);

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(VipLPStr))]
            public delegate string RealizarColetaPinPad(
                int tipoColeta,
                bool confirmar);

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(VipLPStr))]
            public delegate string ConfirmarCapturaPinPad(
                int tipoCaptura,
                [MarshalAs(UnmanagedType.LPStr)] string dadosCaptura);
        }

        #endregion InnerTypes

        #region Construtor

        public TefStdCall(Configuracao configuracao, string caminhoLib, Encoding encoding) : base(configuracao, caminhoLib, encoding)
        {
            ModeloLib = "StdCall";

            AddMethod<Delegates.GetProdutoTef>("GetProdutoTef");
            AddMethod<Delegates.GetClientTCP>("GetClientTCP");
            AddMethod<Delegates.SetClientTCP>("SetClientTCP");
            AddMethod<Delegates.ConfigurarDadosPDV>("ConfigurarDadosPDV");
            AddMethod<Delegates.IniciarOperacaoTEF>("IniciarOperacaoTEF");
            AddMethod<Delegates.RecuperarOperacaoTEF>("RecuperarOperacaoTEF");
            AddMethod<Delegates.RealizarPagamentoTEF>("RealizarPagamentoTEF");
            AddMethod<Delegates.RealizarAdmTEF>("RealizarAdmTEF");
            AddMethod<Delegates.RealizarPixTEF>("RealizarPixTEF");
            AddMethod<Delegates.ConfirmarOperacaoTEF>("ConfirmarOperacaoTEF");
            AddMethod<Delegates.FinalizarOperacaoTEF>("FinalizarOperacaoTEF");
            AddMethod<Delegates.RealizarColetaPinPad>("RealizarColetaPinPad");
            AddMethod<Delegates.ConfirmarCapturaPinPad>("ConfirmarCapturaPinPad");
        }

        #endregion Constructors

        #region Métodos

        public override int GetProdutoTef()
        {
            var funcao = GetMethod<Delegates.GetProdutoTef>();
            return ExecuteMethod(() => funcao());
        }

        public override string GetClientTCP()
        {
            var funcao = GetMethod<Delegates.GetClientTCP>();
            return ExecuteMethod(() => funcao());
        }

        public override string SetClientTCP(string ip, int porta)
        {
            var funcao = GetMethod<Delegates.SetClientTCP>();
            return ExecuteMethod(() => funcao(ip, porta));
        }

        public override string ConfigurarDadosPDV(string textoPinpad, string versaoAC, string nomeEstabelecimento, string loja, string identificadorPontoCaptura)
        {
            var funcao = GetMethod<Delegates.ConfigurarDadosPDV>();
            return ExecuteMethod(() => funcao(textoPinpad, versaoAC, nomeEstabelecimento, loja, identificadorPontoCaptura));
        }

        public override string IniciarOperacaoTEF(string dadosCaptura)
        {
            var funcao = GetMethod<Delegates.IniciarOperacaoTEF>();
            return ExecuteMethod(() => funcao(dadosCaptura));
        }

        public override string RecuperarOperacaoTEF(string dadosCaptura)
        {
            var funcao = GetMethod<Delegates.RecuperarOperacaoTEF>();
            return ExecuteMethod(() => funcao(dadosCaptura));
        }

        public override string RealizarPagamentoTEF(int codigoOperacao, string dadosCaptura, bool novaTransacao)
        {
            var funcao = GetMethod<Delegates.RealizarPagamentoTEF>();
            return ExecuteMethod(() => funcao(codigoOperacao, dadosCaptura, novaTransacao));
        }

        public override string RealizarAdmTEF(int codigoOperacao, string dadosCaptura, bool novaTransacao)
        {
            var funcao = GetMethod<Delegates.RealizarAdmTEF>();
            return ExecuteMethod(() => funcao(codigoOperacao, dadosCaptura, novaTransacao));
        }

        public override string RealizarPixTEF(string dadosCaptura, bool novaTransacao)
        {
            var funcao = GetMethod<Delegates.RealizarPixTEF>();
            return ExecuteMethod(() => funcao(dadosCaptura, novaTransacao));
        }

        public override string ConfirmarOperacaoTEF(int id, int acao)
        {
            var funcao = GetMethod<Delegates.ConfirmarOperacaoTEF>();
            return ExecuteMethod(() => funcao(id, acao));
        }

        public override string FinalizarOperacaoTEF(int id)
        {
            var funcao = GetMethod<Delegates.FinalizarOperacaoTEF>();
            return ExecuteMethod(() => funcao(id));
        }

        public override string RealizarColetaPinPad(int tipoColeta, bool confirmar)
        {
            var funcao = GetMethod<Delegates.RealizarColetaPinPad>();
            return ExecuteMethod(() => funcao(tipoColeta, confirmar));
        }

        public override string ConfirmarCapturaPinPad(int tipoCaptura, string dadosCaptura)
        {
            var funcao = GetMethod<Delegates.ConfirmarCapturaPinPad>();
            return ExecuteMethod(() => funcao(tipoCaptura, dadosCaptura));
        }

        #endregion Methods
    }
}