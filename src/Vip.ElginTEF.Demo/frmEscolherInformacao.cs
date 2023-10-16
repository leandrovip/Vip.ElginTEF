using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Vip.ElginTEF.Demo
{
    public partial class frmEscolherInformacao : Form
    {
        #region Propriedades

        public string InformacaoRetorno;

        #endregion

        #region Construtores

        public frmEscolherInformacao(IEnumerable<string> listaOpcoes, string mensagemUsuario)
        {
            InitializeComponent();

            txtMensagemUsuario.Text = mensagemUsuario;

            ltbOpcoes.Items.Clear();
            listaOpcoes.ForEach(x => ltbOpcoes.Items.Add(x));

            if (listaOpcoes.IsEmpty()) return;
            ltbOpcoes.SelectedIndex = 0;
        }

        #endregion

        #region Eventos

        private void ltbOpcoes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) SelecionarOpcao();
        }

        private void frmEscolherInformacao_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                InformacaoRetorno = "";
                Close();
            }
        }

        private void ltbOpcoes_DoubleClick(object sender, EventArgs e)
        {
            SelecionarOpcao();
        }

        private void SelecionarOpcao()
        {
            if (ltbOpcoes.Text.IsNullOrEmpty()) return;

            InformacaoRetorno = ltbOpcoes.Text.TrimVip();
            DialogResult = DialogResult.OK;
            Close();
        }

        #endregion
    }
}