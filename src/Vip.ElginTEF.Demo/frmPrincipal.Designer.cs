namespace Vip.ElginTEF.Demo
{
    partial class frmPrincipal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrincipal));
            this.btnConfiguraPdv = new System.Windows.Forms.Button();
            this.txtRetorno = new System.Windows.Forms.TextBox();
            this.lblRetorno = new System.Windows.Forms.Label();
            this.btnPagamentoTef = new System.Windows.Forms.Button();
            this.txtValorTotal = new System.Windows.Forms.TextBox();
            this.lblValor = new System.Windows.Forms.Label();
            this.rdbDebito = new System.Windows.Forms.RadioButton();
            this.gpbTipoOperacao = new System.Windows.Forms.GroupBox();
            this.rdbPrivateLabel = new System.Windows.Forms.RadioButton();
            this.rdbFrota = new System.Windows.Forms.RadioButton();
            this.rdbCredito = new System.Windows.Forms.RadioButton();
            this.rdbNenhum = new System.Windows.Forms.RadioButton();
            this.rdbVoucher = new System.Windows.Forms.RadioButton();
            this.lblParcelas = new System.Windows.Forms.Label();
            this.txtParcelas = new System.Windows.Forms.TextBox();
            this.lblMensagemUsuario = new System.Windows.Forms.Label();
            this.txtMensagemUsuario = new System.Windows.Forms.TextBox();
            this.btnLimpar = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdbCancelamento = new System.Windows.Forms.RadioButton();
            this.rdbNenhumAdm = new System.Windows.Forms.RadioButton();
            this.rdbReimpressao = new System.Windows.Forms.RadioButton();
            this.rdbPendencias = new System.Windows.Forms.RadioButton();
            this.btnAdministracaoTef = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.tbpFuncoes = new System.Windows.Forms.TabControl();
            this.tbpPagamento = new System.Windows.Forms.TabPage();
            this.tbpPagamentoPix = new System.Windows.Forms.TabPage();
            this.tbpAdministracao = new System.Windows.Forms.TabPage();
            this.gpbTipoOperacao.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tbpFuncoes.SuspendLayout();
            this.tbpPagamento.SuspendLayout();
            this.tbpAdministracao.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConfiguraPdv
            // 
            this.btnConfiguraPdv.Location = new System.Drawing.Point(12, 12);
            this.btnConfiguraPdv.Name = "btnConfiguraPdv";
            this.btnConfiguraPdv.Size = new System.Drawing.Size(129, 52);
            this.btnConfiguraPdv.TabIndex = 0;
            this.btnConfiguraPdv.Text = "Configurar PDV";
            this.btnConfiguraPdv.UseVisualStyleBackColor = true;
            this.btnConfiguraPdv.Click += new System.EventHandler(this.btncConfigurarPdv_Click);
            // 
            // txtRetorno
            // 
            this.txtRetorno.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRetorno.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRetorno.Location = new System.Drawing.Point(12, 458);
            this.txtRetorno.Multiline = true;
            this.txtRetorno.Name = "txtRetorno";
            this.txtRetorno.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtRetorno.Size = new System.Drawing.Size(956, 277);
            this.txtRetorno.TabIndex = 1;
            // 
            // lblRetorno
            // 
            this.lblRetorno.AutoSize = true;
            this.lblRetorno.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRetorno.Location = new System.Drawing.Point(12, 441);
            this.lblRetorno.Name = "lblRetorno";
            this.lblRetorno.Size = new System.Drawing.Size(137, 14);
            this.lblRetorno.TabIndex = 2;
            this.lblRetorno.Text = "Retorno / Chamadas";
            // 
            // btnPagamentoTef
            // 
            this.btnPagamentoTef.Location = new System.Drawing.Point(16, 147);
            this.btnPagamentoTef.Name = "btnPagamentoTef";
            this.btnPagamentoTef.Size = new System.Drawing.Size(129, 52);
            this.btnPagamentoTef.TabIndex = 3;
            this.btnPagamentoTef.Text = "Pagamento TEF";
            this.btnPagamentoTef.UseVisualStyleBackColor = true;
            this.btnPagamentoTef.Click += new System.EventHandler(this.btnPagamentoTef_Click);
            // 
            // txtValorTotal
            // 
            this.txtValorTotal.Location = new System.Drawing.Point(16, 98);
            this.txtValorTotal.Name = "txtValorTotal";
            this.txtValorTotal.Size = new System.Drawing.Size(138, 22);
            this.txtValorTotal.TabIndex = 4;
            this.txtValorTotal.Text = "15,00";
            this.txtValorTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblValor
            // 
            this.lblValor.AutoSize = true;
            this.lblValor.Location = new System.Drawing.Point(13, 82);
            this.lblValor.Name = "lblValor";
            this.lblValor.Size = new System.Drawing.Size(58, 14);
            this.lblValor.TabIndex = 5;
            this.lblValor.Text = "R$ Valor";
            // 
            // rdbDebito
            // 
            this.rdbDebito.AutoSize = true;
            this.rdbDebito.Location = new System.Drawing.Point(170, 25);
            this.rdbDebito.Name = "rdbDebito";
            this.rdbDebito.Size = new System.Drawing.Size(66, 18);
            this.rdbDebito.TabIndex = 6;
            this.rdbDebito.Text = "Débito";
            this.rdbDebito.UseVisualStyleBackColor = true;
            // 
            // gpbTipoOperacao
            // 
            this.gpbTipoOperacao.Controls.Add(this.rdbPrivateLabel);
            this.gpbTipoOperacao.Controls.Add(this.rdbFrota);
            this.gpbTipoOperacao.Controls.Add(this.rdbCredito);
            this.gpbTipoOperacao.Controls.Add(this.rdbNenhum);
            this.gpbTipoOperacao.Controls.Add(this.rdbVoucher);
            this.gpbTipoOperacao.Controls.Add(this.rdbDebito);
            this.gpbTipoOperacao.Location = new System.Drawing.Point(16, 15);
            this.gpbTipoOperacao.Name = "gpbTipoOperacao";
            this.gpbTipoOperacao.Size = new System.Drawing.Size(495, 54);
            this.gpbTipoOperacao.TabIndex = 7;
            this.gpbTipoOperacao.TabStop = false;
            this.gpbTipoOperacao.Text = "Tipo Operação";
            // 
            // rdbPrivateLabel
            // 
            this.rdbPrivateLabel.AutoSize = true;
            this.rdbPrivateLabel.Location = new System.Drawing.Point(387, 25);
            this.rdbPrivateLabel.Name = "rdbPrivateLabel";
            this.rdbPrivateLabel.Size = new System.Drawing.Size(107, 18);
            this.rdbPrivateLabel.TabIndex = 11;
            this.rdbPrivateLabel.Text = "Private Label";
            this.rdbPrivateLabel.UseVisualStyleBackColor = true;
            // 
            // rdbFrota
            // 
            this.rdbFrota.AutoSize = true;
            this.rdbFrota.Location = new System.Drawing.Point(323, 25);
            this.rdbFrota.Name = "rdbFrota";
            this.rdbFrota.Size = new System.Drawing.Size(58, 18);
            this.rdbFrota.TabIndex = 10;
            this.rdbFrota.Text = "Frota";
            this.rdbFrota.UseVisualStyleBackColor = true;
            // 
            // rdbCredito
            // 
            this.rdbCredito.AutoSize = true;
            this.rdbCredito.Checked = true;
            this.rdbCredito.Location = new System.Drawing.Point(93, 25);
            this.rdbCredito.Name = "rdbCredito";
            this.rdbCredito.Size = new System.Drawing.Size(71, 18);
            this.rdbCredito.TabIndex = 9;
            this.rdbCredito.TabStop = true;
            this.rdbCredito.Text = "Crédito";
            this.rdbCredito.UseVisualStyleBackColor = true;
            // 
            // rdbNenhum
            // 
            this.rdbNenhum.AutoSize = true;
            this.rdbNenhum.Location = new System.Drawing.Point(10, 25);
            this.rdbNenhum.Name = "rdbNenhum";
            this.rdbNenhum.Size = new System.Drawing.Size(77, 18);
            this.rdbNenhum.TabIndex = 8;
            this.rdbNenhum.Text = "Nenhum";
            this.rdbNenhum.UseVisualStyleBackColor = true;
            // 
            // rdbVoucher
            // 
            this.rdbVoucher.AutoSize = true;
            this.rdbVoucher.Location = new System.Drawing.Point(242, 25);
            this.rdbVoucher.Name = "rdbVoucher";
            this.rdbVoucher.Size = new System.Drawing.Size(75, 18);
            this.rdbVoucher.TabIndex = 7;
            this.rdbVoucher.Text = "Voucher";
            this.rdbVoucher.UseVisualStyleBackColor = true;
            // 
            // lblParcelas
            // 
            this.lblParcelas.AutoSize = true;
            this.lblParcelas.Location = new System.Drawing.Point(170, 82);
            this.lblParcelas.Name = "lblParcelas";
            this.lblParcelas.Size = new System.Drawing.Size(60, 14);
            this.lblParcelas.TabIndex = 10;
            this.lblParcelas.Text = "Parcelas";
            this.lblParcelas.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtParcelas
            // 
            this.txtParcelas.Location = new System.Drawing.Point(173, 98);
            this.txtParcelas.Name = "txtParcelas";
            this.txtParcelas.Size = new System.Drawing.Size(122, 22);
            this.txtParcelas.TabIndex = 9;
            this.txtParcelas.Text = "1";
            this.txtParcelas.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblMensagemUsuario
            // 
            this.lblMensagemUsuario.AutoSize = true;
            this.lblMensagemUsuario.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.lblMensagemUsuario.Location = new System.Drawing.Point(12, 381);
            this.lblMensagemUsuario.Name = "lblMensagemUsuario";
            this.lblMensagemUsuario.Size = new System.Drawing.Size(132, 14);
            this.lblMensagemUsuario.TabIndex = 11;
            this.lblMensagemUsuario.Text = "Mensagem Usuário";
            // 
            // txtMensagemUsuario
            // 
            this.txtMensagemUsuario.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMensagemUsuario.Font = new System.Drawing.Font("Verdana", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMensagemUsuario.Location = new System.Drawing.Point(12, 398);
            this.txtMensagemUsuario.Name = "txtMensagemUsuario";
            this.txtMensagemUsuario.Size = new System.Drawing.Size(923, 30);
            this.txtMensagemUsuario.TabIndex = 12;
            this.txtMensagemUsuario.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnLimpar
            // 
            this.btnLimpar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLimpar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnLimpar.BackgroundImage")));
            this.btnLimpar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnLimpar.Location = new System.Drawing.Point(941, 401);
            this.btnLimpar.Name = "btnLimpar";
            this.btnLimpar.Size = new System.Drawing.Size(27, 28);
            this.btnLimpar.TabIndex = 14;
            this.btnLimpar.UseVisualStyleBackColor = true;
            this.btnLimpar.Click += new System.EventHandler(this.btnLimpar_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdbCancelamento);
            this.groupBox1.Controls.Add(this.rdbNenhumAdm);
            this.groupBox1.Controls.Add(this.rdbReimpressao);
            this.groupBox1.Controls.Add(this.rdbPendencias);
            this.groupBox1.Location = new System.Drawing.Point(10, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(430, 54);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tipo Operação - Administração";
            // 
            // rdbCancelamento
            // 
            this.rdbCancelamento.AutoSize = true;
            this.rdbCancelamento.Checked = true;
            this.rdbCancelamento.Location = new System.Drawing.Point(93, 25);
            this.rdbCancelamento.Name = "rdbCancelamento";
            this.rdbCancelamento.Size = new System.Drawing.Size(115, 18);
            this.rdbCancelamento.TabIndex = 9;
            this.rdbCancelamento.TabStop = true;
            this.rdbCancelamento.Text = "Cancelamento";
            this.rdbCancelamento.UseVisualStyleBackColor = true;
            // 
            // rdbNenhumAdm
            // 
            this.rdbNenhumAdm.AutoSize = true;
            this.rdbNenhumAdm.Location = new System.Drawing.Point(10, 25);
            this.rdbNenhumAdm.Name = "rdbNenhumAdm";
            this.rdbNenhumAdm.Size = new System.Drawing.Size(77, 18);
            this.rdbNenhumAdm.TabIndex = 8;
            this.rdbNenhumAdm.Text = "Nenhum";
            this.rdbNenhumAdm.UseVisualStyleBackColor = true;
            // 
            // rdbReimpressao
            // 
            this.rdbReimpressao.AutoSize = true;
            this.rdbReimpressao.Location = new System.Drawing.Point(317, 25);
            this.rdbReimpressao.Name = "rdbReimpressao";
            this.rdbReimpressao.Size = new System.Drawing.Size(106, 18);
            this.rdbReimpressao.TabIndex = 7;
            this.rdbReimpressao.Text = "Reimpressão";
            this.rdbReimpressao.UseVisualStyleBackColor = true;
            // 
            // rdbPendencias
            // 
            this.rdbPendencias.AutoSize = true;
            this.rdbPendencias.Location = new System.Drawing.Point(214, 25);
            this.rdbPendencias.Name = "rdbPendencias";
            this.rdbPendencias.Size = new System.Drawing.Size(97, 18);
            this.rdbPendencias.TabIndex = 6;
            this.rdbPendencias.Text = "Pendências";
            this.rdbPendencias.UseVisualStyleBackColor = true;
            // 
            // btnAdministracaoTef
            // 
            this.btnAdministracaoTef.Location = new System.Drawing.Point(10, 146);
            this.btnAdministracaoTef.Name = "btnAdministracaoTef";
            this.btnAdministracaoTef.Size = new System.Drawing.Size(129, 52);
            this.btnAdministracaoTef.TabIndex = 12;
            this.btnAdministracaoTef.Text = "Administração TEF";
            this.btnAdministracaoTef.UseVisualStyleBackColor = true;
            this.btnAdministracaoTef.Click += new System.EventHandler(this.btnAdministracaoTef_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 14);
            this.label1.TabIndex = 18;
            this.label1.Text = "Data";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(10, 37);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(122, 22);
            this.textBox1.TabIndex = 17;
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(299, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 14);
            this.label2.TabIndex = 16;
            this.label2.Text = "R$ Valor";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(302, 37);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(122, 22);
            this.textBox2.TabIndex = 15;
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.textBox3);
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.textBox2);
            this.groupBox2.Location = new System.Drawing.Point(10, 75);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(430, 65);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Dados Cancelamento";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(154, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 14);
            this.label3.TabIndex = 20;
            this.label3.Text = "NSU Transação";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(157, 37);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(122, 22);
            this.textBox3.TabIndex = 19;
            this.textBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbpFuncoes
            // 
            this.tbpFuncoes.Controls.Add(this.tbpPagamento);
            this.tbpFuncoes.Controls.Add(this.tbpPagamentoPix);
            this.tbpFuncoes.Controls.Add(this.tbpAdministracao);
            this.tbpFuncoes.Location = new System.Drawing.Point(12, 70);
            this.tbpFuncoes.Name = "tbpFuncoes";
            this.tbpFuncoes.SelectedIndex = 0;
            this.tbpFuncoes.Size = new System.Drawing.Size(956, 241);
            this.tbpFuncoes.TabIndex = 20;
            // 
            // tbpPagamento
            // 
            this.tbpPagamento.Controls.Add(this.gpbTipoOperacao);
            this.tbpPagamento.Controls.Add(this.btnPagamentoTef);
            this.tbpPagamento.Controls.Add(this.lblValor);
            this.tbpPagamento.Controls.Add(this.txtValorTotal);
            this.tbpPagamento.Controls.Add(this.lblParcelas);
            this.tbpPagamento.Controls.Add(this.txtParcelas);
            this.tbpPagamento.Location = new System.Drawing.Point(4, 23);
            this.tbpPagamento.Name = "tbpPagamento";
            this.tbpPagamento.Padding = new System.Windows.Forms.Padding(3);
            this.tbpPagamento.Size = new System.Drawing.Size(948, 214);
            this.tbpPagamento.TabIndex = 0;
            this.tbpPagamento.Text = "Pagamento";
            this.tbpPagamento.UseVisualStyleBackColor = true;
            // 
            // tbpPagamentoPix
            // 
            this.tbpPagamentoPix.Location = new System.Drawing.Point(4, 23);
            this.tbpPagamentoPix.Name = "tbpPagamentoPix";
            this.tbpPagamentoPix.Padding = new System.Windows.Forms.Padding(3);
            this.tbpPagamentoPix.Size = new System.Drawing.Size(948, 214);
            this.tbpPagamentoPix.TabIndex = 1;
            this.tbpPagamentoPix.Text = "Pagamento PIX";
            this.tbpPagamentoPix.UseVisualStyleBackColor = true;
            // 
            // tbpAdministracao
            // 
            this.tbpAdministracao.Controls.Add(this.groupBox1);
            this.tbpAdministracao.Controls.Add(this.groupBox2);
            this.tbpAdministracao.Controls.Add(this.btnAdministracaoTef);
            this.tbpAdministracao.Location = new System.Drawing.Point(4, 23);
            this.tbpAdministracao.Name = "tbpAdministracao";
            this.tbpAdministracao.Padding = new System.Windows.Forms.Padding(3);
            this.tbpAdministracao.Size = new System.Drawing.Size(948, 214);
            this.tbpAdministracao.TabIndex = 2;
            this.tbpAdministracao.Text = "Administração";
            this.tbpAdministracao.UseVisualStyleBackColor = true;
            // 
            // frmPrincipal
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(980, 747);
            this.Controls.Add(this.tbpFuncoes);
            this.Controls.Add(this.btnLimpar);
            this.Controls.Add(this.txtMensagemUsuario);
            this.Controls.Add(this.lblMensagemUsuario);
            this.Controls.Add(this.lblRetorno);
            this.Controls.Add(this.txtRetorno);
            this.Controls.Add(this.btnConfiguraPdv);
            this.Font = new System.Drawing.Font("Verdana", 9F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Vip.ElginTEF - Demonstração - Contato: Leandro 17.99176-5035";
            this.gpbTipoOperacao.ResumeLayout(false);
            this.gpbTipoOperacao.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tbpFuncoes.ResumeLayout(false);
            this.tbpPagamento.ResumeLayout(false);
            this.tbpPagamento.PerformLayout();
            this.tbpAdministracao.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConfiguraPdv;
        private System.Windows.Forms.TextBox txtRetorno;
        private System.Windows.Forms.Label lblRetorno;
        private System.Windows.Forms.Button btnPagamentoTef;
        private System.Windows.Forms.TextBox txtValorTotal;
        private System.Windows.Forms.Label lblValor;
        private System.Windows.Forms.RadioButton rdbDebito;
        private System.Windows.Forms.GroupBox gpbTipoOperacao;
        private System.Windows.Forms.RadioButton rdbVoucher;
        private System.Windows.Forms.Label lblParcelas;
        private System.Windows.Forms.TextBox txtParcelas;
        private System.Windows.Forms.Label lblMensagemUsuario;
        private System.Windows.Forms.TextBox txtMensagemUsuario;
        private System.Windows.Forms.Button btnLimpar;
        private System.Windows.Forms.RadioButton rdbPrivateLabel;
        private System.Windows.Forms.RadioButton rdbFrota;
        private System.Windows.Forms.RadioButton rdbCredito;
        private System.Windows.Forms.RadioButton rdbNenhum;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdbCancelamento;
        private System.Windows.Forms.RadioButton rdbNenhumAdm;
        private System.Windows.Forms.RadioButton rdbReimpressao;
        private System.Windows.Forms.RadioButton rdbPendencias;
        private System.Windows.Forms.Button btnAdministracaoTef;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TabControl tbpFuncoes;
        private System.Windows.Forms.TabPage tbpPagamento;
        private System.Windows.Forms.TabPage tbpPagamentoPix;
        private System.Windows.Forms.TabPage tbpAdministracao;
    }
}

