using System.Windows.Forms;
using Vip.ElginTEF.Enums;

namespace Vip.ElginTEF.Demo
{
    public partial class frmDigitarInformacao : Form
    {
        #region Propriedades

        public string InformacaoRetorno;
        private readonly TipoInformacao _tipoInformacao;

        #endregion

        #region Construtores

        public frmDigitarInformacao(TipoInformacao tipoInformacao, string mensagemUsuario)
        {
            InitializeComponent();

            _tipoInformacao = tipoInformacao;
            txtMensagemUsuario.Text = mensagemUsuario;
        }

        #endregion

        #region Eventos

        private void txtInformacao_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) SelecionarInformacao();
        }

        private void frmDigitarInformacao_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                InformacaoRetorno = "";
                Close();
            }
        }

        #endregion

        #region Métodos Públicos

        private void SelecionarInformacao()
        {
            if (txtMensagemUsuario.Text.IsNullOrEmpty()) return;

            InformacaoRetorno = txtInformacao.Text;
            DialogResult = DialogResult.OK;
            Close();
        }

        #endregion
    }
}