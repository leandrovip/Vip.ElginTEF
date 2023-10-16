namespace Vip.ElginTEF.Demo
{
    partial class frmEscolherInformacao
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblSelecione = new System.Windows.Forms.Label();
            this.ltbOpcoes = new System.Windows.Forms.ListBox();
            this.lblDescricao = new System.Windows.Forms.Label();
            this.txtMensagemUsuario = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblSelecione
            // 
            this.lblSelecione.Font = new System.Drawing.Font("Verdana", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelecione.ForeColor = System.Drawing.Color.White;
            this.lblSelecione.Location = new System.Drawing.Point(12, 17);
            this.lblSelecione.Name = "lblSelecione";
            this.lblSelecione.Size = new System.Drawing.Size(811, 26);
            this.lblSelecione.TabIndex = 0;
            this.lblSelecione.Text = "Selecione a opção desejada";
            this.lblSelecione.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ltbOpcoes
            // 
            this.ltbOpcoes.Font = new System.Drawing.Font("Verdana", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ltbOpcoes.FormattingEnabled = true;
            this.ltbOpcoes.ItemHeight = 32;
            this.ltbOpcoes.Location = new System.Drawing.Point(12, 61);
            this.ltbOpcoes.Name = "ltbOpcoes";
            this.ltbOpcoes.Size = new System.Drawing.Size(526, 356);
            this.ltbOpcoes.TabIndex = 0;
            this.ltbOpcoes.DoubleClick += new System.EventHandler(this.ltbOpcoes_DoubleClick);
            this.ltbOpcoes.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ltbOpcoes_KeyDown);
            // 
            // lblDescricao
            // 
            this.lblDescricao.AutoSize = true;
            this.lblDescricao.ForeColor = System.Drawing.Color.White;
            this.lblDescricao.Location = new System.Drawing.Point(109, 421);
            this.lblDescricao.Name = "lblDescricao";
            this.lblDescricao.Size = new System.Drawing.Size(321, 17);
            this.lblDescricao.TabIndex = 1;
            this.lblDescricao.Text = "Vip.ElginTEF - Demonstração - VIP Soluções";
            // 
            // txtMensagemUsuario
            // 
            this.txtMensagemUsuario.ForeColor = System.Drawing.Color.White;
            this.txtMensagemUsuario.Location = new System.Drawing.Point(544, 61);
            this.txtMensagemUsuario.Name = "txtMensagemUsuario";
            this.txtMensagemUsuario.Size = new System.Drawing.Size(279, 356);
            this.txtMensagemUsuario.TabIndex = 2;
            this.txtMensagemUsuario.Text = "Mensagem Usuário";
            this.txtMensagemUsuario.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmEscolherInformacao
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Orange;
            this.ClientSize = new System.Drawing.Size(834, 442);
            this.ControlBox = false;
            this.Controls.Add(this.txtMensagemUsuario);
            this.Controls.Add(this.lblDescricao);
            this.Controls.Add(this.ltbOpcoes);
            this.Controls.Add(this.lblSelecione);
            this.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "frmEscolherInformacao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmEscolherInformacao";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmEscolherInformacao_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSelecione;
        private System.Windows.Forms.ListBox ltbOpcoes;
        private System.Windows.Forms.Label lblDescricao;
        private System.Windows.Forms.Label txtMensagemUsuario;
    }
}