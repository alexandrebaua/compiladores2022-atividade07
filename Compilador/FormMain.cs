using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.CodeDom.Compiler;
using System.Diagnostics;
using Microsoft.CSharp;

using Compilador.Lexico;
using Compilador.Semantico;
using Compilador.Sintatico;
using Compilador.Sintatico.Descendente.Recursivo;
using Compilador.Sintatico.Descendente.PreditivoLL1;
using Compilador.Sintatico.Ascendente.SLR;

namespace Compilador
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void btnExec_Click(object sender, EventArgs e)
        {
            // Seleciona a aba de resultados processamento léxico
            //this.tctrlOutputs.SelectedTab = this.tpgLexico;

            // Limpa a lista de tokens e do resultado sintático.
            this.lstTokens.Items.Clear();
            this.lstSintatico.Items.Clear();
            this.lstSemantico.Items.Clear();
            this.lstSemanticoOutput.Items.Clear();
            this.rtxtCodeOuput.Text = String.Empty;
            this.txtCompiladorOutput.Text = String.Empty;

            // Declara o analisador léxico.
            LexicoClass lexico;
            SemanticoClass semantico = new SemanticoClass();

            try
            {
                // Tenta criar uma instância do analisador léxico
                lexico = new LexicoClass(this.rtxtCode.Text);
                lexico = new LexicoClass(this.rtxtCode.Text, marcadorFinal: "$");

                // Adiciona à lista os novos tokens encontrados, se existirem:
                foreach (var token in lexico.ListaTokens)
                    this.lstTokens.Items.Add(token);

                // Seleciona a aba de resultados processamento sintático
                //this.tctrlOutputs.SelectedTab = this.tpgSintatico;

                // Executa a análise sintática usando a lista de tokens encontrados
                //DescRecursivo.Verificar(lexico, this.lstSintatico);
                //AnalizadorAscendente.Verificar(lexico, this.lstSintatico);
                //DescPredLL1.Verificar(lexico, this.lstSintatico);
                AscSLR.Verificar(lexico, this.lstSintatico, semantico);

                foreach (var item in semantico.ListaItens)
                {
                    this.lstSemantico.Items.Add(item);
                }

                semantico.Processar(this.lstSemanticoOutput);

                if (semantico.Programa != null)
                {
                    this.rtxtCodeOuput.Text = semantico.Programa.ObterCódigo();

                    if (chkCompilar.Checked)
                        this.Compilar(semantico.Programa.ObterCódigo());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Seleciona o último item da lista de resultados do processamento sintático:
            if (this.lstSintatico.Items.Count > 0)
                this.lstSintatico.SelectedIndex = this.lstSintatico.Items.Count - 1;
        }

        private void Compilar(string codigo)
        {
            CSharpCodeProvider codeProvider = new CSharpCodeProvider();
            ICodeCompiler icc = codeProvider.CreateCompiler();
            string Output = "Out.exe";

            System.CodeDom.Compiler.CompilerParameters parameters = new CompilerParameters();
            //Make sure we generate an EXE, not a DLL
            parameters.GenerateExecutable = true;
            parameters.OutputAssembly = Output;
            CompilerResults results = icc.CompileAssemblyFromSource(parameters, codigo);

            if (results.Errors.Count > 0)
            {
                this.txtCompiladorOutput.ForeColor = Color.Red;
                foreach (CompilerError CompErr in results.Errors)
                {
                    this.txtCompiladorOutput.Text = this.txtCompiladorOutput.Text +
                                "Line number " + CompErr.Line +
                                ", Error Number: " + CompErr.ErrorNumber +
                                ", '" + CompErr.ErrorText + ";" +
                                Environment.NewLine + Environment.NewLine;
                }
            }
            else
            {
                //Successful Compile
                this.txtCompiladorOutput.ForeColor = Color.Blue;
                this.txtCompiladorOutput.Text = "Success!";
            }
        }
    }
}
