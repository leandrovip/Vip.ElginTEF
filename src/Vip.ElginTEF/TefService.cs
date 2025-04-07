using System;
using System.IO;
using System.Text;
using Vip.ElginTEF.Enums;
using Vip.ElginTEF.Events;
using Vip.ElginTEF.Exceptions;
using Vip.ElginTEF.Extensions;
using Vip.ElginTEF.Interfaces;
using Vip.ElginTEF.Models;
using Vip.ElginTEF.Request;
using Vip.ElginTEF.Response;
using Vip.ElginTEF.Services;

namespace Vip.ElginTEF
{
    public sealed class TefService : IDisposable
    {
        #region Fields

        private ILibrary _library;
        private bool _aguardandoComando;
        private ModeloLib _modeloLib;
        private string _caminhoLib;

        #endregion

        #region Fields Request

        private const string _payloadVazio = "{}";
        private int _parcelasPagamento;
        private DateTime? _dataTransacao;
        private string _nsuTransacao;
        private string _valorTransacao;

        #endregion

        #region Construtor

        public TefService()
        {
            Configuracao = new Configuracao();
            _modeloLib = ModeloLib.StdCall;
            _caminhoLib = @".\E1_Tef01.dll";
            _aguardandoComando = false;
        }

        ~TefService()
        {
            Dispose(false);
        }

        #endregion

        #region Eventos

        /// <summary>
        ///     Ocorre quando o componente entra ou sai de um processamento.
        /// </summary>
        public event EventHandler<EventArgs> AguardandoComandoChanged;

        /// <summary>
        ///     Ocorre quando um retorno é processado pela dll.
        /// </summary>
        public event EventHandler<MensagemRetornoEventArgs> OnMensagemRetorno;

        /// <summary>
        ///     Ocorre quando uma mensagem deve ser exibida ao usuário.
        /// </summary>
        public event EventHandler<MensagemUsuarioEventArgs> OnMensagemUsuario;

        /// <summary>
        ///     Ocorre quando se é esperado uma informação para compor o fluxo de informações das transações, como Recebimento,
        ///     Adm, Opções
        /// </summary>
        public event EventHandler<ReceberInformacaoEventArgs> OnReceberInformacao;

        public event EventHandler<ExibirQrCodePixEventArgs> OnExibirQrCodePix;

        #endregion Events

        #region Propriedades

        public bool Ativo { get; private set; }

        public bool Inativo => !Ativo;

        public Configuracao Configuracao { get; private set; }

        public bool AguardandoComando
        {
            get => _aguardandoComando;
            private set
            {
                if (_aguardandoComando == value) return;

                _aguardandoComando = value;
                AguardandoComandoChanged.Raise(this, EventArgs.Empty);
            }
        }

        public ModeloLib ModeloLib
        {
            get => _modeloLib;
            set
            {
                Guard.Against<VipException>(Ativo, "Não é possível definir a propriedade com o componente ativo");
                _modeloLib = value;
            }
        }

        public string CaminhoLib
        {
            get => _caminhoLib;
            set
            {
                Guard.Against<VipException>(Ativo, "Não é possível definir a propriedade com o componente ativo");
                _caminhoLib = value;
            }
        }

        public string InformacaoColeta { get; set; }

        #endregion

        #region Métodos Publicos

        public void Ativar()
        {
            Guard.Against(!File.Exists(_caminhoLib), $"Arquivo {_caminhoLib} não encontrado");

            _library = Manager.GetLibrary(_modeloLib, Configuracao, _caminhoLib, Encoding.UTF8);
            Ativo = true;
        }

        public void Desativar()
        {
            // Remove temporariamente para funcionamento com o TEF HUB
            //if (_library != null)
            //{
            //    _library.Dispose();
            //    _library = null;
            //}

            _library = null;
            Ativo = false;
        }

        public int ObterProdutoTEF()
        {
            Guard.Against<VipException>(Inativo, "Componente não está ativo");

            var retorno = _library.GetProdutoTef();
            return retorno;
        }

        public string ObterConfiguracaoTCP()
        {
            Guard.Against<VipException>(Inativo, "Componente não está ativo");

            var retorno = _library.GetClientTCP();
            return retorno;
        }

        public BaseResponse ConfigurarDados()
        {
            Guard.Against<VipException>(Inativo, "Componente não está ativo");

            ConfigurarConexao();
            var retorno = _library.ConfigurarDadosPDV(Configuracao.TextoPinpad, Configuracao.VersaoAC, Configuracao.NomeEstabelecimento, Configuracao.Loja, Configuracao.IdentificadorPontoCaptura);
            return FinalizaComando<BaseResponse>(retorno);
        }

        public BaseResponse<TransacaoResponse> RealizarPagamento(PagamentoRequest request)
        {
            Guard.Against<VipException>(Inativo, "Componente não está ativo");
            Guard.Against<VipException>(request.IsNull(), "Informações inválidas para pagamento: Objeto Pagamento nulo");

            #region Preenche Informações

            _parcelasPagamento = request.QuantidadeParcelas;

            #endregion

            var operacao = IniciarOperacao();
            if (!operacao.Retorno)
            {
                OnMensagemUsuario.Raise(this, MensagemUsuarioEventArgs.Novo("Não foi possível iniciar a transação"));
                var erroResponse = new BaseResponse<TransacaoResponse>();
                erroResponse.SetarErro(operacao.Tef.Retorno.ToInt(9), "Não foi possível iniciar a transação");
                return erroResponse;
            }

            OnMensagemUsuario.Raise(this, MensagemUsuarioEventArgs.Novo("Aguarde, iniciando pagamento"));

            var modelInicial = new {operacao.Tef.Sequencial, ValorTotal = request.ValorTotalFormatado};
            var payload = modelInicial.Serialize();
            var retorno = _library.RealizarPagamentoTEF(request.CodigoOperacao, payload, true);
            var pagamentoCommand = FinalizaComando<BaseResponse<FluxoResponse>>(retorno);
            if (pagamentoCommand.IsNull() || pagamentoCommand.Tef.ColetaRetorno.IsNull() || pagamentoCommand.Tef.ColetaRetorno == "9")
            {
                if (pagamentoCommand.Tef.ColetaRetorno.IsNull())
                    FinalizarOperacaoTEF();

                OnMensagemUsuario.Raise(this, MensagemUsuarioEventArgs.Novo("Não foi possível iniciar o pagamento"));
                var errorResponse = new BaseResponse<TransacaoResponse>();
                errorResponse.SetarErro(pagamentoCommand.Tef.ColetaRetorno.ToInt(), pagamentoCommand.Tef.MensagemResultado);
                return errorResponse;
            }

            OnMensagemUsuario.Raise(this, MensagemUsuarioEventArgs.Novo(pagamentoCommand?.Tef.MensagemResultado));

            AguardandoComando = true;
            var fluxoRequest = ObterNovoFluxoRequest(pagamentoCommand, TipoFluxo.Pagamento);
            var response = ChamarFluxoPagamento(TipoFluxo.Pagamento, request.CodigoOperacao, fluxoRequest);
            AguardandoComando = false;

            if (response.Tef.IsNull() || response.Tef.HouveErro)
            {
                if (response.Retorno) response.SetarErro(response.Tef.MensagemResultado);
            }
            else if (response.Tef.PodeConfirmar)
            {
                OnMensagemUsuario.Raise(this, MensagemUsuarioEventArgs.Novo(response?.Tef.MensagemResultado));
                ConfirmarOperacaoTEF(response.Tef.Sequencial, TipoAcao.Confirmar);
            }

            FinalizarOperacaoTEF();
            return response;
        }

        public BaseResponse<TransacaoResponse> RealizarPagamentoPIX(decimal valorPagamento)
        {
            Guard.Against<VipException>(Inativo, "Componente não está ativo");
            Guard.Against<VipException>(valorPagamento <= 0, "Valor de pagamento inválido");

            var operacao = IniciarOperacao();
            if (!operacao.Retorno)
            {
                OnMensagemUsuario.Raise(this, MensagemUsuarioEventArgs.Novo("Não foi possível iniciar a transação"));
                var erroResponse = new BaseResponse<TransacaoResponse>();
                erroResponse.SetarErro(operacao.Tef.Retorno.ToInt(9), "Não foi possível iniciar a transação");
                return erroResponse;
            }

            OnMensagemUsuario.Raise(this, MensagemUsuarioEventArgs.Novo("Aguarde, iniciando pagamento"));

            var valorFormatado = valorPagamento.ToString("N").OnlyNumbers();
            var modelInicial = new {operacao.Tef.Sequencial, ValorTotal = valorFormatado};
            var payload = modelInicial.Serialize();
            var retorno = _library.RealizarPixTEF(payload, true);
            var pagamentoCommand = FinalizaComando<BaseResponse<FluxoResponse>>(retorno);
            if (pagamentoCommand.IsNull() || pagamentoCommand.Tef.ColetaRetorno.IsNull() || pagamentoCommand.Tef.ColetaRetorno == "9")
            {
                if (pagamentoCommand.Tef.ColetaRetorno.IsNull())
                    FinalizarOperacaoTEF();

                OnMensagemUsuario.Raise(this, MensagemUsuarioEventArgs.Novo("Não foi possível iniciar o pagamento"));
                var errorResponse = new BaseResponse<TransacaoResponse>();
                errorResponse.SetarErro(pagamentoCommand.Tef.ColetaRetorno.ToInt(), pagamentoCommand.Tef.MensagemResultado);
                return errorResponse;
            }

            OnMensagemUsuario.Raise(this, MensagemUsuarioEventArgs.Novo(pagamentoCommand?.Tef.MensagemResultado));

            AguardandoComando = true;
            var fluxoRequest = ObterNovoFluxoRequest(pagamentoCommand, TipoFluxo.PagamentoPix);
            var response = ChamarFluxoPagamento(TipoFluxo.PagamentoPix, 0, fluxoRequest);
            AguardandoComando = false;

            if (response.Tef.IsNull() || response.Tef.HouveErro)
            {
                if (response.Retorno) response.SetarErro(response.Tef.MensagemResultado);
            }
            else if (response.Tef.PodeConfirmar)
            {
                OnMensagemUsuario.Raise(this, MensagemUsuarioEventArgs.Novo(response?.Tef.MensagemResultado));
                ConfirmarOperacaoTEF(response.Tef.Sequencial, TipoAcao.Confirmar);
            }

            FinalizarOperacaoTEF();
            return response;
        }

        public BaseResponse<TransacaoResponse> RealizarAdm(AdmRequest request)
        {
            Guard.Against<VipException>(Inativo, "Componente não está ativo");
            Guard.Against<VipException>(request.IsNull(), "Informações inválidas para pagamento: Objeto Pagamento nulo");

            #region Preenche Informações

            _dataTransacao = request.DataTransacao;
            _nsuTransacao = request.NsuTransacao;
            _valorTransacao = request.ValorTransacaoFormatado;

            #endregion

            var operacao = IniciarOperacao();
            if (!operacao.Retorno)
            {
                OnMensagemUsuario.Raise(this, MensagemUsuarioEventArgs.Novo("Não foi possível iniciar a operação ADM"));
                var erroResponse = new BaseResponse<TransacaoResponse>();
                erroResponse.SetarErro(operacao.Tef.Retorno.ToInt(9), "Erro na abertura da operação ADM");
                return erroResponse;
            }

            OnMensagemUsuario.Raise(this, MensagemUsuarioEventArgs.Novo("Aguarde, iniciando operação"));

            var modelInicial = new {operacao.Tef.Sequencial, AdmUsuario = request.Usuario, AdmSenha = request.Senha};
            var payload = modelInicial.Serialize();
            var retorno = _library.RealizarAdmTEF(request.CodigoOperacao, payload, true);
            var admCommand = FinalizaComando<BaseResponse<FluxoResponse>>(retorno);
            if (admCommand.IsNull() || admCommand.Tef.ColetaRetorno.IsNull() || admCommand.Tef.ColetaRetorno == "9")
            {
                if (admCommand.Tef.ColetaRetorno.IsNull())
                    FinalizarOperacaoTEF();

                OnMensagemUsuario.Raise(this, MensagemUsuarioEventArgs.Novo(admCommand?.Tef.MensagemResultado));
                var errorResponse = new BaseResponse<TransacaoResponse>();
                errorResponse.SetarErro(admCommand?.Tef.ColetaRetorno.ToInt(9), admCommand?.Tef.MensagemResultado);
                return errorResponse;
            }

            OnMensagemUsuario.Raise(this, MensagemUsuarioEventArgs.Novo(admCommand?.Tef.MensagemResultado));

            AguardandoComando = true;
            var fluxoRequest = ObterNovoFluxoRequest(admCommand, TipoFluxo.Adm);
            var response = ChamarFluxoPagamento(TipoFluxo.Adm, request.CodigoOperacao, fluxoRequest);
            AguardandoComando = false;

            if (response.Tef.IsNull() || response.Tef.HouveErro)
            {
                if (response.Retorno) response.SetarErro(response.Tef.MensagemResultado);
            }
            else if (response.Tef.PodeConfirmar)
            {
                OnMensagemUsuario.Raise(this, MensagemUsuarioEventArgs.Novo(response?.Tef.MensagemResultado));
                ConfirmarOperacaoTEF(response.Tef.Sequencial, TipoAcao.Confirmar);
            }

            FinalizarOperacaoTEF();
            return response;
        }

        #endregion

        #region Métodos Privados

        private string ConfirmarOperacaoTEF(string sequencial, TipoAcao tipoAcao)
        {
            var retorno = _library.ConfirmarOperacaoTEF(sequencial.ToInt(), tipoAcao.ToInt());
            return retorno;
        }

        private void FinalizarOperacaoTEF()
        {
            Guard.Against<VipException>(Inativo, "Componente não está ativo");
            _library.FinalizarOperacaoTEF(1);
        }

        private string ConfigurarConexao()
        {
            var retorno = _library.SetClientTCP(Configuracao.IpClientTCP, Configuracao.PortaClientTCP);
            return retorno;
        }

        private BaseResponse<IniciarOperacaoResponse> IniciarOperacao()
        {
            var retorno = _library.IniciarOperacaoTEF(_payloadVazio);
            return FinalizaComando<BaseResponse<IniciarOperacaoResponse>>(retorno);
        }

        private BaseResponse<TransacaoResponse> ChamarFluxoPagamento(TipoFluxo tipoFluxo, int codigoOperacao, FluxoRequest fluxoRequest)
        {
            Guard.Against<VipException>(fluxoRequest.IsNull(), "Requisição de fluxo vazio");

            var retornoFluxo = "";
            while (fluxoRequest.ColetaRetorno.IsNotNull())
            {
                var payload = fluxoRequest.Serialize();
                retornoFluxo = tipoFluxo switch
                {
                    TipoFluxo.Pagamento => _library.RealizarPagamentoTEF(codigoOperacao, payload, false),
                    TipoFluxo.PagamentoPix => _library.RealizarPixTEF(payload, false),
                    TipoFluxo.Adm => _library.RealizarAdmTEF(codigoOperacao, payload, false),
                    _ => throw new NotImplementedException("Sem implementação para o tipo selecionado"),
                };

                var fluxoCommand = FinalizaComando<BaseResponse<FluxoResponse>>(retornoFluxo);

                if (tipoFluxo == TipoFluxo.PagamentoPix && retornoFluxo.Contains("QRCODE"))
                    OnExibirQrCodePix.Raise(this, ExibirQrCodePixEventArgs.Map(fluxoCommand.Tef.MensagemResultado));

                fluxoRequest = ObterNovoFluxoRequest(fluxoCommand, tipoFluxo);
                if (fluxoRequest.IsNull() || fluxoRequest.ColetaRetorno == "9")
                {
                    OnMensagemUsuario.Raise(this, MensagemUsuarioEventArgs.Novo(fluxoCommand?.Tef.MensagemResultado));
                    break;
                }

                OnMensagemUsuario.Raise(this, MensagemUsuarioEventArgs.Novo(fluxoCommand?.Tef.MensagemResultado));
            }

            AguardandoComando = false;
            var response = FinalizaComando<BaseResponse<TransacaoResponse>>(retornoFluxo);
            return response;
        }

        private FluxoRequest ObterNovoFluxoRequest(BaseResponse<FluxoResponse> command, TipoFluxo tipoFluxo)
        {
            #region Validação

            if (command.IsNull() || command.Tef.IsNull()) return null;

            #endregion

            #region Objeto

            var fluxo = new FluxoRequest
            {
                ColetaRetorno = command.Tef.ColetaRetorno,
                ColetaSequencial = command.Tef.ColetaSequencial.TrimVip()
            };

            #endregion

            #region Se houver Coleta de Dados

            if (command.Tef.ColetaOpcao.IsNotNullOrEmpty() || command.Tef.ColetaTipo.IsNotNullOrEmpty())
            {
                var args = ReceberInformacaoEventArgs.Novo(tipoFluxo, command.Tef.ObterListaOpcao(), command.Tef.TipoInformacao, command.Tef.MensagemResultado);
                switch (command.Tef.ColetaPalavraChave?.ToLower())
                {
                    case "transacao_pagamento":
                        fluxo.ColetaInformacao = _parcelasPagamento > 1 ? "Parcelado" : "A vista";
                        break;
                    case "transacao_parcela":
                        fluxo.ColetaInformacao = _parcelasPagamento.ToString();
                        break;
                    case "transacao_data":
                        fluxo.ColetaInformacao = _dataTransacao.HasValue ? _dataTransacao.Value.ToString("dd/MM/yyyy") : ObterInformacaoColeta(args);
                        break;
                    case "transacao_nsu":
                        fluxo.ColetaInformacao = _nsuTransacao.IsNotNullOrEmpty() ? _nsuTransacao : ObterInformacaoColeta(args);
                        break;
                    case "transacao_valor":
                        fluxo.ColetaInformacao = _valorTransacao.IsNotNullOrEmpty() ? _valorTransacao : ObterInformacaoColeta(args);
                        break;
                    case "terminal":
                        fluxo.ColetaInformacao = ObterInformacaoColeta(args);
                        break;
                    default:
                        var informacao = ObterInformacaoColeta(args);
                        if (informacao.IsNullOrEmpty()) fluxo.ColetaRetorno = "9";

                        fluxo.ColetaInformacao = informacao;
                        break;
                }
            }

            #endregion

            return fluxo;
        }

        private T FinalizaComando<T>(string response) where T : IBaseResponse
        {
            var retorno = response.Deserialize<T>();

            var e = new MensagemRetornoEventArgs(retorno?.Codigo, retorno?.DescricaoRetorno);
            OnMensagemRetorno.Raise(this, e);
            AguardandoComando = false;
            return retorno;
        }

        private string ObterInformacaoColeta(ReceberInformacaoEventArgs args)
        {
            OnReceberInformacao.Raise(this, args);
            return InformacaoColeta;
        }

        #endregion

        #region Dispose

        private void Dispose(bool disposing)
        {
            if (Ativo) Desativar();
            if (disposing) GC.SuppressFinalize(this);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion
    }
}