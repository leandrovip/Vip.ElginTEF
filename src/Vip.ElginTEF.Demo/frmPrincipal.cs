using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Vip.ElginTEF.Enums;
using Vip.ElginTEF.Events;
using Vip.ElginTEF.Request;
using Vip.ElginTEF.Response;

namespace Vip.ElginTEF.Demo
{
    public partial class frmPrincipal : Form
    {
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
                var service = ObterServico();

                var response = service.ConfigurarDados();
                ImprimirRetorno("Configurar PDV", response);

                var responseProdutoTef = service.ObterProdutoTEF();
                ImprimirRetorno("ObterProdutoTEF", responseProdutoTef.ToString());
            }
            catch (Exception exception)
            {
                txtRetorno.Text = exception.Message;
            }
        }

        private void btnPagamentoTef_Click(object sender, EventArgs e)
        {
            #region Validações

            if (txtValorTotal.Text.IsNullOrEmpty())
            {
                MessageBox.Show("Informe um valor e tente novamente.");
                return;
            }

            #endregion

            var service = ObterServico();
            service.OnMensagemUsuario += ImprimirMensagemUsuario;
            service.OnReceberInformacao += EnviarInformacaoFluxo;

            txtMensagemUsuario.Text = "Transação TEF iniciada";
            txtMensagemUsuario.Refresh();

            var request = new PagamentoRequest(ObterTipoOperacao(), txtValorTotal.Text.ToDecimal(), txtParcelas.Text.ToInt(1));
            var response = service.RealizarPagamento(request);
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

            service.Dispose();
        }

        private void btnPagamentoPix_Click(object sender, EventArgs e)
        {
            #region Validações

            if (txtValorPix.Text.IsNullOrEmpty())
            {
                MessageBox.Show("Informe um valor e tente novamente.");
                return;
            }

            #endregion

            var service = ObterServico();
            service.OnMensagemUsuario += ImprimirMensagemUsuario;
            service.OnReceberInformacao += EnviarInformacaoFluxo;
            service.OnExibirQrCodePix += ExibirQrCodePix;

            txtMensagemUsuario.Text = "Transação PIX TEF iniciada";
            txtMensagemUsuario.Refresh();

            var valorRequest = txtValorPix.Text.ToDecimal();
            var response = service.RealizarPagamentoPIX(valorRequest);
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
            ptbQrCode.Image = null;
            ptbQrCode.Refresh();

            service.Dispose();
        }

        private void btnAdministracaoTef_Click(object sender, EventArgs e)
        {
            var service = ObterServico();
            service.OnMensagemUsuario += ImprimirMensagemUsuario;
            service.OnReceberInformacao += EnviarInformacaoFluxo;

            txtMensagemUsuario.Text = "Administração TEF iniciada";
            txtMensagemUsuario.Refresh();

            var dataTransacao = txtDataTransacao.Text.IsNullOrEmpty() ? (DateTime?) null : DateTime.Parse(txtDataTransacao.Text);
            var valorTransacao = txtValorTransacao.Text.IsNullOrEmpty() ? (decimal?) null : txtValorTransacao.Text.ToDecimal();
            var request = new AdmRequest(ObterTipoOperacaoAdm(), "lojista", "lojista1#", dataTransacao, txtNsuTransacao.Text, valorTransacao);
            var response = service.RealizarAdm(request);
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

            service.Dispose();
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

        #endregion

        #region Métodos

        private TefService ObterServico()
        {
            var tef = new TefService();

            tef.Configuracao.TextoPinpad = "VipERP PDV";
            tef.Configuracao.VersaoAC = "1.1.157";
            tef.Configuracao.NomeEstabelecimento = "VIP";
            tef.Configuracao.Loja = "001";
            tef.Configuracao.IdentificadorPontoCaptura = "T0004";
            tef.Configuracao.IpClientTCP = "127.0.0.1";
            tef.Configuracao.PortaClientTCP = 60906;
            tef.ModeloLib = ModeloLib.StdCall;
            tef.CaminhoLib = @".\E1_Tef01.dll";

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