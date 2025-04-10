using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vip.ElginTEF.Enums;
using Vip.ElginTEF.Events;
using Vip.ElginTEF.Request;
using Vip.ElginTEF.Response;

namespace Vip.ElginTEF.Demo
{
    public partial class frmPrincipal : Form
    {
        private TefService _tefService;

        #region Construtores

        public frmPrincipal()
        {
            InitializeComponent();
        }

        #endregion

        #region Eventos

        private void btncConfigurarPdv_Click(object sender, EventArgs e)
        {
            try
            {
                _tefService = ObterServico();

                var response = _tefService.ConfigurarDados();
                ImprimirRetorno("Configurar PDV", response);

                var responseProdutoTef = _tefService.ObterProdutoTEF();
                ImprimirRetorno("ObterProdutoTEF", responseProdutoTef.ToString());
            }
            catch (Exception exception)
            {
                txtRetorno.Text = exception.Message;
            }
        }

        private async void btnPagamentoTef_Click(object sender, EventArgs e)
        {
            #region Validações

            if (txtValorTotal.Text.IsNullOrEmpty())
            {
                MessageBox.Show("Informe um valor e tente novamente.");
                return;
            }

            #endregion

            _tefService = ObterServico();
            _tefService.OnMensagemUsuario += ImprimirMensagemUsuario;
            _tefService.OnReceberInformacao += EnviarInformacaoFluxo;

            txtMensagemUsuario.Text = "Transação TEF iniciada";
            txtMensagemUsuario.Refresh();

            var request = new PagamentoRequest(ObterTipoOperacao(), txtValorTotal.Text.ToDecimal(), txtParcelas.Text.ToInt(1));
            var response = await Task.Run(() => _tefService.RealizarPagamento(request));
            if (response.IsNull() || !response.Retorno)
            {
                MessageBox.Show($"Houve um erro na transação!\r\nMensagem: {response?.Mensagem}");
                return;
            }

            txtMensagemUsuario.Text = "Transação Finalizada";
            txtMensagemUsuario.Refresh();

            ImprimirRetorno("RealizarPagamento", response);

            if (response.Tef.PodeConfirmar)
            {
                ImprimirRetorno("CodigoAutorizacao", response.Tef.CodigoAutorizacao);
                ImprimirRetorno("NsuTransacao", response.Tef.NsuTransacao);
                ImprimirRetorno("FormaPagamento", response.Tef.FormaPagamento);
                ImprimirRetorno("ComprovanteUsuario", response.Tef.ComprovanteDiferenciadoPortador);
                ImprimirRetorno("ComprovanteLoja", response.Tef.ComprovanteDiferenciadoLoja);
            }

            txtMensagemUsuario.Text = "Aguardando próxima transação";
            txtMensagemUsuario.Refresh();

            _tefService.Dispose();
            _tefService = null;
        }

        private async void btnPagamentoPix_Click(object sender, EventArgs e)
        {
            #region Validações

            if (txtValorPix.Text.IsNullOrEmpty())
            {
                MessageBox.Show("Informe um valor e tente novamente.");
                return;
            }

            #endregion

            _tefService = ObterServico();
            _tefService.OnMensagemUsuario += ImprimirMensagemUsuario;
            _tefService.OnReceberInformacao += EnviarInformacaoFluxo;
            _tefService.OnExibirQrCodePix += ExibirQrCodePix;

            txtMensagemUsuario.Text = "Transação PIX TEF iniciada";
            txtMensagemUsuario.Refresh();

            var valorRequest = txtValorPix.Text.ToDecimal();

            var response = await Task.Run(() => _tefService.RealizarPagamentoPIX(valorRequest));

            if (response.IsNull() || !response.Retorno)
            {
                MessageBox.Show($"Houve um erro na transação!\r\nMensagem: {response?.Mensagem}");
                txtMensagemUsuario.Text = "Aguardando próxima transação";
                txtMensagemUsuario.Refresh();
                ptbQrCode.Image = null;
                ptbQrCode.Refresh();
                return;
            }

            txtMensagemUsuario.Text = "Transação Finalizada";
            txtMensagemUsuario.Refresh();

            ImprimirRetorno("RealizarPagamento", response);

            if (response.Tef.PodeConfirmar)
            {
                ImprimirRetorno("CodigoAutorizacao", response.Tef.CodigoAutorizacao);
                ImprimirRetorno("NsuTransacao", response.Tef.NsuTransacao);
                ImprimirRetorno("FormaPagamento", response.Tef.FormaPagamento);
                ImprimirRetorno("ComprovanteUsuario", response.Tef.ComprovanteDiferenciadoPortador);
                ImprimirRetorno("ComprovanteLoja", response.Tef.ComprovanteDiferenciadoLoja);
            }

            txtMensagemUsuario.Text = "Aguardando próxima transação";
            txtMensagemUsuario.Refresh();
            ptbQrCode.Image = null;
            ptbQrCode.Refresh();

            _tefService.Dispose();
            _tefService = null;
        }

        private void btnAdministracaoTef_Click(object sender, EventArgs e)
        {
            _tefService = ObterServico();
            _tefService.OnMensagemUsuario += ImprimirMensagemUsuario;
            _tefService.OnReceberInformacao += EnviarInformacaoFluxo;

            txtMensagemUsuario.Text = "Administração TEF iniciada";
            txtMensagemUsuario.Refresh();

            var dataTransacao = txtDataTransacao.Text.IsNullOrEmpty() ? (DateTime?) null : DateTime.Parse(txtDataTransacao.Text);
            var valorTransacao = txtValorTransacao.Text.IsNullOrEmpty() ? (decimal?) null : txtValorTransacao.Text.ToDecimal();
            var request = new AdmRequest(ObterTipoOperacaoAdm(), "lojista", "lojista1#", dataTransacao, txtNsuTransacao.Text, valorTransacao);
            var response = _tefService.RealizarAdm(request);
            if (response.IsNull() || !response.Retorno)
            {
                MessageBox.Show($"Houve um erro na operação!\r\nMensagem: {response?.Mensagem}");
                return;
            }

            txtMensagemUsuario.Text = "Operação Finalizada";
            txtMensagemUsuario.Refresh();

            ImprimirRetorno("RealizarAdm", response);

            if (response.Tef.PodeConfirmar || response.Tef.PodeFinalizar)
            {
                ImprimirRetorno("CodigoAutorizacao", response?.Tef?.CodigoAutorizacao);
                ImprimirRetorno("NsuTransacao", response?.Tef?.NsuTransacao);
                ImprimirRetorno("NomeBandeira", response?.Tef?.NomeBandeira);
                ImprimirRetorno("FormaPagamento", response?.Tef?.FormaPagamento);
                ImprimirRetorno("ComprovanteUsuario", response?.Tef?.ComprovanteDiferenciadoPortador);
                ImprimirRetorno("ComprovanteLoja", response?.Tef?.ComprovanteDiferenciadoLoja);
            }

            txtMensagemUsuario.Text = "Aguardando próxima transação";
            txtMensagemUsuario.Refresh();

            _tefService.Dispose();
            _tefService = null;
        }

        private void EnviarInformacaoFluxo(object sender, ReceberInformacaoEventArgs e)
        {
            var informacaoRetorno = "";
            if (e.SeEsperaOpcoes && e.Opcoes.IsNotEmpty())
            {
                using (var form = new frmEscolherInformacao(e.Opcoes, e.MensagemUsuario))
                    if (form.ShowDialog() == DialogResult.OK)
                        informacaoRetorno = form.InformacaoRetorno;
            }
            else
            {
                using (var form = new frmDigitarInformacao(e.TipoInformacao, e.MensagemUsuario))
                    if (form.ShowDialog() == DialogResult.OK)
                        informacaoRetorno = form.InformacaoRetorno;
            }

            ((TefService) sender).InformacaoColeta = informacaoRetorno;
        }

        private void ExibirQrCodePix(object sender, ExibirQrCodePixEventArgs e)
        {
            if (e.QrCode.IsNull()) return;
            ptbQrCode.Image = ByteArrayToImage(e.QrCode);
            ptbQrCode.Refresh();
        }

        private void ImprimirMensagemUsuario(object sender, MensagemUsuarioEventArgs e)
        {
            txtMensagemUsuario.Text = e?.Mensagem;
            txtMensagemUsuario.Refresh();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txtMensagemUsuario.Text = "";
            txtRetorno.Text = "";
        }

        private void btnCancelarTransacao_Click(object sender, EventArgs e)
        {
            _tefService?.CancelarOperacaoTEF();
        }

        #endregion

        #region Métodos

        private TefService ObterServico()
        {
            var tef = new TefService();

            tef.Configuracao.TextoPinpad = "VipERP PDV";
            tef.Configuracao.VersaoAC = "1.1.600";
            tef.Configuracao.NomeEstabelecimento = "VIP";
            tef.Configuracao.Loja = "001";
            tef.Configuracao.IdentificadorPontoCaptura = "T0004";
            tef.Configuracao.IpClientTCP = "127.0.0.1";
            tef.Configuracao.PortaClientTCP = 60906;
            tef.ModeloLib = ModeloLib.StdCall;
            tef.CaminhoLib = $@".\{txtNomeDll.Text}";
            //tef.Timeout = 30;

            tef.Ativar();

            return tef;
        }

        private TipoOperacao ObterTipoOperacao()
        {
            if (rdbNenhum.Checked) return TipoOperacao.Nenhum;
            if (rdbCredito.Checked) return TipoOperacao.CartaoCredito;
            if (rdbDebito.Checked) return TipoOperacao.CartaoDebito;
            if (rdbVoucher.Checked) return TipoOperacao.Voucher;
            if (rdbFrota.Checked) return TipoOperacao.Frota;
            if (rdbPrivateLabel.Checked) return TipoOperacao.PrivateLabel;

            return TipoOperacao.Nenhum;
        }

        private TipoOperacaoAdm ObterTipoOperacaoAdm()
        {
            if (rdbNenhumAdm.Checked) return TipoOperacaoAdm.Nenhum;
            if (rdbCancelamento.Checked) return TipoOperacaoAdm.Cancelamento;
            if (rdbPendencias.Checked) return TipoOperacaoAdm.Pendencia;
            if (rdbReimpressao.Checked) return TipoOperacaoAdm.Reimpressao;

            return TipoOperacaoAdm.Nenhum;
        }

        private void ImprimirRetorno(string chamada, BaseResponse response)
        {
            var mensagem = new string('#', 20) + "\r\n";
            mensagem += $"Chamada: {chamada}\r\n";
            mensagem += $"Código: {response.Codigo}\r\n";
            mensagem += $"Descrição: {response.DescricaoRetorno}\r\n";
            mensagem += $"Mensagem: {response.Mensagem}\r\n";
            mensagem += new string('#', 20) + "\r\n\r\n";

            txtRetorno.Text += mensagem;
            txtRetorno.SelectionStart = txtRetorno.TextLength;
            txtRetorno.ScrollToCaret();
        }

        private void ImprimirRetorno(string chamada, string retorno)
        {
            var mensagem = new string('#', 20) + "\r\n";
            mensagem += $"Chamada: {chamada}\r\n";
            mensagem += $"Retorno: {retorno.TrimVip()}\r\n";
            mensagem += new string('#', 20) + "\r\n\r\n";

            txtRetorno.Text += mensagem;
            txtRetorno.SelectionStart = txtRetorno.TextLength;
            txtRetorno.ScrollToCaret();
        }

        private Image ByteArrayToImage(byte[] byteArray)
        {
            using (var ms = new MemoryStream(byteArray))
                return Image.FromStream(ms);
        }

        #endregion
    }
}