namespace Compilador
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.rtxtCode = new System.Windows.Forms.RichTextBox();
            this.lstTokens = new System.Windows.Forms.ListBox();
            this.btnExec = new System.Windows.Forms.Button();
            this.lstSintatico = new System.Windows.Forms.ListBox();
            this.tctrlOutputs = new System.Windows.Forms.TabControl();
            this.tpgLexico = new System.Windows.Forms.TabPage();
            this.tpgSintatico = new System.Windows.Forms.TabPage();
            this.tpgSemantico = new System.Windows.Forms.TabPage();
            this.lstSemantico = new System.Windows.Forms.ListBox();
            this.tpgSemanticoOutput = new System.Windows.Forms.TabPage();
            this.lstSemanticoOutput = new System.Windows.Forms.ListBox();
            this.tpgCodeOuput = new System.Windows.Forms.TabPage();
            this.rtxtCodeOuput = new System.Windows.Forms.RichTextBox();
            this.tpgCompilador = new System.Windows.Forms.TabPage();
            this.txtCompiladorOutput = new System.Windows.Forms.TextBox();
            this.chkCompilar = new System.Windows.Forms.CheckBox();
            this.tctrlOutputs.SuspendLayout();
            this.tpgLexico.SuspendLayout();
            this.tpgSintatico.SuspendLayout();
            this.tpgSemantico.SuspendLayout();
            this.tpgSemanticoOutput.SuspendLayout();
            this.tpgCodeOuput.SuspendLayout();
            this.tpgCompilador.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtxtCode
            // 
            this.rtxtCode.DetectUrls = false;
            this.rtxtCode.Font = new System.Drawing.Font("Lucida Sans Typewriter", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtxtCode.Location = new System.Drawing.Point(12, 12);
            this.rtxtCode.Name = "rtxtCode";
            this.rtxtCode.Size = new System.Drawing.Size(265, 355);
            this.rtxtCode.TabIndex = 1;
            this.rtxtCode.Text = resources.GetString("rtxtCode.Text");
            this.rtxtCode.WordWrap = false;
            // 
            // lstTokens
            // 
            this.lstTokens.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstTokens.FormattingEnabled = true;
            this.lstTokens.HorizontalScrollbar = true;
            this.lstTokens.ItemHeight = 15;
            this.lstTokens.Location = new System.Drawing.Point(6, 6);
            this.lstTokens.Name = "lstTokens";
            this.lstTokens.Size = new System.Drawing.Size(418, 349);
            this.lstTokens.TabIndex = 2;
            // 
            // btnExec
            // 
            this.btnExec.Location = new System.Drawing.Point(12, 373);
            this.btnExec.Name = "btnExec";
            this.btnExec.Size = new System.Drawing.Size(75, 23);
            this.btnExec.TabIndex = 3;
            this.btnExec.Text = "Executar";
            this.btnExec.UseVisualStyleBackColor = true;
            this.btnExec.Click += new System.EventHandler(this.btnExec_Click);
            // 
            // lstSintatico
            // 
            this.lstSintatico.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstSintatico.FormattingEnabled = true;
            this.lstSintatico.HorizontalScrollbar = true;
            this.lstSintatico.ItemHeight = 15;
            this.lstSintatico.Location = new System.Drawing.Point(6, 6);
            this.lstSintatico.Name = "lstSintatico";
            this.lstSintatico.Size = new System.Drawing.Size(418, 349);
            this.lstSintatico.TabIndex = 4;
            // 
            // tctrlOutputs
            // 
            this.tctrlOutputs.Controls.Add(this.tpgLexico);
            this.tctrlOutputs.Controls.Add(this.tpgSintatico);
            this.tctrlOutputs.Controls.Add(this.tpgSemantico);
            this.tctrlOutputs.Controls.Add(this.tpgSemanticoOutput);
            this.tctrlOutputs.Controls.Add(this.tpgCodeOuput);
            this.tctrlOutputs.Controls.Add(this.tpgCompilador);
            this.tctrlOutputs.Location = new System.Drawing.Point(283, 12);
            this.tctrlOutputs.Name = "tctrlOutputs";
            this.tctrlOutputs.SelectedIndex = 0;
            this.tctrlOutputs.Size = new System.Drawing.Size(438, 388);
            this.tctrlOutputs.TabIndex = 5;
            // 
            // tpgLexico
            // 
            this.tpgLexico.Controls.Add(this.lstTokens);
            this.tpgLexico.Location = new System.Drawing.Point(4, 22);
            this.tpgLexico.Name = "tpgLexico";
            this.tpgLexico.Padding = new System.Windows.Forms.Padding(3);
            this.tpgLexico.Size = new System.Drawing.Size(430, 362);
            this.tpgLexico.TabIndex = 0;
            this.tpgLexico.Text = "Léxico";
            this.tpgLexico.UseVisualStyleBackColor = true;
            // 
            // tpgSintatico
            // 
            this.tpgSintatico.Controls.Add(this.lstSintatico);
            this.tpgSintatico.Location = new System.Drawing.Point(4, 22);
            this.tpgSintatico.Name = "tpgSintatico";
            this.tpgSintatico.Padding = new System.Windows.Forms.Padding(3);
            this.tpgSintatico.Size = new System.Drawing.Size(430, 362);
            this.tpgSintatico.TabIndex = 1;
            this.tpgSintatico.Text = "Sintático";
            this.tpgSintatico.UseVisualStyleBackColor = true;
            // 
            // tpgSemantico
            // 
            this.tpgSemantico.Controls.Add(this.lstSemantico);
            this.tpgSemantico.Location = new System.Drawing.Point(4, 22);
            this.tpgSemantico.Name = "tpgSemantico";
            this.tpgSemantico.Size = new System.Drawing.Size(430, 362);
            this.tpgSemantico.TabIndex = 2;
            this.tpgSemantico.Text = "Semântico";
            this.tpgSemantico.UseVisualStyleBackColor = true;
            // 
            // lstSemantico
            // 
            this.lstSemantico.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstSemantico.FormattingEnabled = true;
            this.lstSemantico.HorizontalScrollbar = true;
            this.lstSemantico.ItemHeight = 15;
            this.lstSemantico.Location = new System.Drawing.Point(6, 6);
            this.lstSemantico.Name = "lstSemantico";
            this.lstSemantico.Size = new System.Drawing.Size(418, 349);
            this.lstSemantico.TabIndex = 0;
            // 
            // tpgSemanticoOutput
            // 
            this.tpgSemanticoOutput.Controls.Add(this.lstSemanticoOutput);
            this.tpgSemanticoOutput.Location = new System.Drawing.Point(4, 22);
            this.tpgSemanticoOutput.Name = "tpgSemanticoOutput";
            this.tpgSemanticoOutput.Size = new System.Drawing.Size(430, 362);
            this.tpgSemanticoOutput.TabIndex = 3;
            this.tpgSemanticoOutput.Text = "Semântico (Processamento)";
            this.tpgSemanticoOutput.UseVisualStyleBackColor = true;
            // 
            // lstSemanticoOutput
            // 
            this.lstSemanticoOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstSemanticoOutput.FormattingEnabled = true;
            this.lstSemanticoOutput.ItemHeight = 15;
            this.lstSemanticoOutput.Location = new System.Drawing.Point(6, 6);
            this.lstSemanticoOutput.Name = "lstSemanticoOutput";
            this.lstSemanticoOutput.Size = new System.Drawing.Size(418, 349);
            this.lstSemanticoOutput.TabIndex = 0;
            // 
            // tpgCodeOuput
            // 
            this.tpgCodeOuput.Controls.Add(this.rtxtCodeOuput);
            this.tpgCodeOuput.Location = new System.Drawing.Point(4, 22);
            this.tpgCodeOuput.Name = "tpgCodeOuput";
            this.tpgCodeOuput.Size = new System.Drawing.Size(430, 362);
            this.tpgCodeOuput.TabIndex = 4;
            this.tpgCodeOuput.Text = "Código";
            this.tpgCodeOuput.UseVisualStyleBackColor = true;
            // 
            // rtxtCodeOuput
            // 
            this.rtxtCodeOuput.DetectUrls = false;
            this.rtxtCodeOuput.Font = new System.Drawing.Font("Lucida Sans Typewriter", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtxtCodeOuput.Location = new System.Drawing.Point(6, 6);
            this.rtxtCodeOuput.Name = "rtxtCodeOuput";
            this.rtxtCodeOuput.ReadOnly = true;
            this.rtxtCodeOuput.Size = new System.Drawing.Size(418, 349);
            this.rtxtCodeOuput.TabIndex = 0;
            this.rtxtCodeOuput.Text = "";
            this.rtxtCodeOuput.WordWrap = false;
            // 
            // tpgCompilador
            // 
            this.tpgCompilador.Controls.Add(this.txtCompiladorOutput);
            this.tpgCompilador.Location = new System.Drawing.Point(4, 22);
            this.tpgCompilador.Name = "tpgCompilador";
            this.tpgCompilador.Size = new System.Drawing.Size(430, 362);
            this.tpgCompilador.TabIndex = 5;
            this.tpgCompilador.Text = "Compilador";
            this.tpgCompilador.UseVisualStyleBackColor = true;
            // 
            // txtCompiladorOutput
            // 
            this.txtCompiladorOutput.Location = new System.Drawing.Point(6, 6);
            this.txtCompiladorOutput.Multiline = true;
            this.txtCompiladorOutput.Name = "txtCompiladorOutput";
            this.txtCompiladorOutput.Size = new System.Drawing.Size(418, 349);
            this.txtCompiladorOutput.TabIndex = 0;
            this.txtCompiladorOutput.WordWrap = false;
            // 
            // chkCompilar
            // 
            this.chkCompilar.AutoSize = true;
            this.chkCompilar.Location = new System.Drawing.Point(211, 377);
            this.chkCompilar.Name = "chkCompilar";
            this.chkCompilar.Size = new System.Drawing.Size(66, 17);
            this.chkCompilar.TabIndex = 6;
            this.chkCompilar.Text = "Compilar";
            this.chkCompilar.UseVisualStyleBackColor = true;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(733, 413);
            this.Controls.Add(this.chkCompilar);
            this.Controls.Add(this.tctrlOutputs);
            this.Controls.Add(this.btnExec);
            this.Controls.Add(this.rtxtCode);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.Text = "Compilador - Atividades";
            this.tctrlOutputs.ResumeLayout(false);
            this.tpgLexico.ResumeLayout(false);
            this.tpgSintatico.ResumeLayout(false);
            this.tpgSemantico.ResumeLayout(false);
            this.tpgSemanticoOutput.ResumeLayout(false);
            this.tpgCodeOuput.ResumeLayout(false);
            this.tpgCompilador.ResumeLayout(false);
            this.tpgCompilador.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtxtCode;
        private System.Windows.Forms.ListBox lstTokens;
        private System.Windows.Forms.Button btnExec;
        private System.Windows.Forms.ListBox lstSintatico;
        private System.Windows.Forms.TabControl tctrlOutputs;
        private System.Windows.Forms.TabPage tpgLexico;
        private System.Windows.Forms.TabPage tpgSintatico;
        private System.Windows.Forms.TabPage tpgSemantico;
        private System.Windows.Forms.ListBox lstSemantico;
        private System.Windows.Forms.TabPage tpgSemanticoOutput;
        private System.Windows.Forms.ListBox lstSemanticoOutput;
        private System.Windows.Forms.TabPage tpgCodeOuput;
        private System.Windows.Forms.RichTextBox rtxtCodeOuput;
        private System.Windows.Forms.TabPage tpgCompilador;
        private System.Windows.Forms.TextBox txtCompiladorOutput;
        private System.Windows.Forms.CheckBox chkCompilar;
    }
}

