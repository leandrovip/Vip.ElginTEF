namespace Vip.ElginTEF.Demo
{
    partial class frmDigitarInformacao
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
            this.txtInformacao = new System.Windows.Forms.TextBox();
            this.lblDescricao = new System.Windows.Forms.Label();
            this.lblSelecione = new System.Windows.Forms.Label();
            this.txtMensagemUsuario = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtInformacao
            // 
            this.txtInformacao.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtInformacao.Font = new System.Drawing.Font("Verdana", 40F);
            this.txtInformacao.Location = new System.Drawing.Point(12, 111);
            this.txtInformacao.Name = "txtInformacao";
            this.txtInformacao.Size = new System.Drawing.Size(601, 72);
            this.txtInformacao.TabIndex = 0;
            this.txtInformacao.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtInformacao.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtInformacao_KeyDown);
            // 
            // lblDescricao
            // 
            this.lblDescricao.AutoSize = true;
            this.lblDescricao.ForeColor = System.Drawing.Color.White;
            this.lblDescricao.Location = new System.Drawing.Point(131, 252);
            this.lblDescricao.Name = "lblDescricao";
            this.lblDescricao.Size = new System.Drawing.Size(321, 17);
            this.lblDescricao.TabIndex = 3;
            this.lblDescricao.Text = "Vip.ElginTEF - Demonstração - VIP Soluções";
            // 
            // lblSelecione
            // 
            this.lblSelecione.AutoSize = true;
            this.lblSelecione.Font = new System.Drawing.Font("Verdana", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelecione.ForeColor = System.Drawing.Color.White;
            this.lblSelecione.Location = new System.Drawing.Point(117, -169);
            this.lblSelecione.Name = "lblSelecione";
            this.lblSelecione.Size = new System.Drawing.Size(357, 26);
            this.lblSelecione.TabIndex = 2;
            this.lblSelecione.Text = "Selecione a opção desejada";
            // 
            // txtMensagemUsuario
            // 
            this.txtMensagemUsuario.Font = new System.Drawing.Font("Verdana", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMensagemUsuario.ForeColor = System.Drawing.Color.White;
            this.txtMensagemUsuario.Location = new System.Drawing.Point(7, 9);
            this.txtMensagemUsuario.Name = "txtMensagemUsuario";
            this.txtMensagemUsuario.Size = new System.Drawing.Size(606, 86);
            this.txtMensagemUsuario.TabIndex = 4;
            this.txtMensagemUsuario.Text = "Digite a informação";
            this.txtMensagemUsuario.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmDigitarInformacao
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.DarkOrange;
            this.ClientSize = new System.Drawing.Size(625, 291);
            this.ControlBox = false;
            this.Controls.Add(this.txtMensagemUsuario);
            this.Controls.Add(this.lblDescricao);
            this.Controls.Add(this.lblSelecione);
            this.Controls.Add(this.txtInformacao);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Verdana", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "frmDigitarInformacao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmDigitarInformacao";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmDigitarInformacao_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtInformacao;
        private System.Windows.Forms.Label lblDescricao;
        private System.Windows.Forms.Label lblSelecione;
        private System.Windows.Forms.Label txtMensagemUsuario;
    }
}