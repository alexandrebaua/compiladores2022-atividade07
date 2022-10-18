using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Compilador.Lexico;

namespace Compilador.Semantico.Tipos
{
    /// <summary>
    /// Classe para armazenar o programa (função).
    /// </summary>
    public class Programa : ITipo
    {
        private List<Variável> listaVariáveis = null;

        /// <summary>
        /// O construtor da classe.
        /// </summary>
        /// <param name="elementos">Os elementos semânticos que constituem o comando.</param>
        public Programa(List<SemanticoItem> elementos, List<ITipo> listaTiposEscopoMaior)
        {
            if (elementos == null)
                throw new ArgumentNullException(nameof(elementos));
            if (elementos.Count < 6)
                throw new Exception("Um tipo 'programa' deve receber no mínimo 6 elementos!");

            this.Elementos = elementos;
            this.ListaTiposEscopoMaior = listaTiposEscopoMaior;

            this.Nome = elementos[1].Token;
            if (elementos.Count >= 8)
            {
                int length = (elementos.Count - 6) / 2;
                if (length == 1)
                {
                    this.parâmetros = new AtributoFunção[1];
                    this.parâmetros[0] = new AtributoFunção(elementos[3], elementos[4]);
                }
                else if ((elementos.Count - 8) % 3 == 0)
                {
                    length = ((elementos.Count - 8) / 3) + 1;
                    this.parâmetros = new AtributoFunção[length];
                    for (int i = 0, j = 3; i < length; i++, j += 3)
                    {
                        this.parâmetros[i] = new AtributoFunção(elementos[j], elementos[j + 1]);
                    }
                }
            }
        }

        /// <summary>
        /// Os elementos semânticos que constituem o comando.
        /// </summary>
        public List<SemanticoItem> Elementos { get; set; }

        public List<ITipo> ListaTiposEscopoMaior { get; set; }

        /// <summary>
        /// Obtém ou define o nome do programa (função).
        /// </summary>
        public TokenClass Nome { get; set; }

        /// <summary>
        /// Obtém ou define os parâmetros do programa (função).
        /// </summary>
        public AtributoFunção[] parâmetros { get; set; } = null;

        /// <summary>
        /// Verifica as variáveis do programa (função) e dos comandos dos escopos internos.
        /// </summary>
        /// <param name="debug">A lista de saída dos resultados da verificação.</param>
        public void VerificarVariáveis(ListBox debug)
        {
            debug.Items.Add("-----> Programa");

            this.listaVariáveis = new List<Variável>();

            // Se o programa (função) possui parâmetros, então testa:
            if (this.parâmetros != null && this.parâmetros.Length > 1)
            {
                // Percorre todos os parâmetros:
                for (int i = 0; i < this.parâmetros.Length; i++)
                {
                    var elemento = this.parâmetros[i];

                    // Busca se existe declaraçãoes de parâmetros com o mesmo nome do parâmetro atual:
                    if (!this.parâmetros.Where(x => x.Identificador.Lexema.Equals(elemento.Identificador.Lexema)).Count().Equals(1))
                        throw new Exception($"O atributo '{elemento.Identificador.Lexema}' da função '{this.Nome.Lexema}' foi repetido!");

                    // Adiciona o parâmetro atual à lista de variáveis.
                    this.listaVariáveis.Add(new Variável(elemento.Identificador.Lexema, elemento.TipoDeDados.Token, true));

                    debug.Items.Add($"> {this.listaVariáveis.Last()}");
                }
            }

            // Se existirem comandos nos escopos internos, então teste:
            if (this.ListaTiposEscopoMaior != null && this.ListaTiposEscopoMaior.Count > 0)
            {
                // Percorre todos os comandos:
                foreach (var item in this.ListaTiposEscopoMaior)
                {
                    // Obtém o tipo do comando.
                    Type tipoItem = item.GetType();
                    if (tipoItem == typeof(Declaração))  // Se tipo do comando for declaração, então:
                    {
                        var elDec = (Declaração)item;

                        // Busca se existe declaraçãoes de variáveis com o mesmo nome da variável atual:
                        IEnumerable<Variável> elements = this.listaVariáveis.Where(x => x.Identificador.Equals(elDec.Identificador.Lexema));
                        if (!elements.Count().Equals(0))
                            throw new Exception($"A variável '{elDec.Identificador.Lexema}' já foi declarada!");

                        // Adiciona o parâmetro atual à lista de variáveis.
                        this.listaVariáveis.Add(new Variável(elDec.Identificador.Lexema, elDec.TipoDeDados.Token));

                        debug.Items.Add($"> {this.listaVariáveis.Last()}");
                    }
                    else if (tipoItem == typeof(Atribuição))  // Se tipo do comando for atribuição, então:
                    {
                        var elAtr = (Atribuição)item;

                        debug.Items.Add($"-----> {elAtr}");

                        // Verifica se a variável que recebe a operação foi declarada:
                        Variável identificador = this.listaVariáveis.Find(x => x.Identificador.Equals(elAtr.Variável.Lexema));
                        if (identificador == null)
                            throw new Exception($"A variável '{elAtr.Variável.Lexema}' foi usada mas não está declarada!");

                        debug.Items.Add($"=> {identificador}");

                        // Testa as variáveis na expressão de atribuição, se estão declaradas, possuem valor atribuido e o tipo de dados é compativel com a operação:
                        foreach (var itemExpr in elAtr.Expressão)
                        {
                            if (itemExpr.Token.Equals("ID"))
                            {
                                // Verifica se a variável na operação foi declarada:
                                Variável elementsExpr = this.listaVariáveis.Find(x => x.Identificador.Equals(itemExpr.Lexema));
                                if (elementsExpr == null)
                                    throw new Exception($"A variável '{itemExpr.Lexema}' foi usada mas não está declarada!");

                                // Verifica se a variável na operação possui valor atribuido:
                                if (!elementsExpr.ValorAtribuido)
                                    throw new Exception($"A variável '{itemExpr.Lexema}' foi usada mas não possui valor atribuido!");

                                // Verifica se a variáveies envolvidas são compativeis:
                                if (identificador.Tipo.Equals("INT") && elementsExpr.Tipo.Equals("FLOAT"))
                                    throw new Exception($"A variável '{identificador.Identificador}' do tipo inteiro não pode receber a variável '{elementsExpr.Identificador}' do tipo ponto flutuante!");

                                debug.Items.Add($"> {elementsExpr}");
                            }
                        }

                        // Variável que recebe a operação agora possui valor atribuido, então marca:
                        identificador.ValorAtribuido = true;

                        debug.Items.Add($"<----- {elAtr}");
                    }
                    else if (tipoItem == typeof(SeleçãoIf))  // Se tipo do comando for uma seleção 'if', então:
                    {
                        var elSelIf = (SeleçãoIf)item;

                        debug.Items.Add($"-----> {elSelIf}");

                        // Verifica as variáveis na operação condicional, e nos escopos subsequentes do comando de seleção 'if':
                        elSelIf.VerificarVariáveis(this.listaVariáveis, debug);

                        debug.Items.Add($"<----- {elSelIf}");
                    }
                    else if (tipoItem == typeof(Enquanto))  // Se tipo do comando for uma repetição 'enquanto', então:
                    {
                        var elWhile = (Enquanto)item;

                        debug.Items.Add($"-----> {elWhile}");

                        // Verifica as variáveis na operação condicional, e nos escopos subsequentes do comando de repetição 'enquanto':
                        elWhile.VerificarVariáveis(this.listaVariáveis, debug);

                        debug.Items.Add($"<----- {elWhile}");
                    }
                    else if (tipoItem == typeof(Imprime))  // Se tipo do comando for um comando 'imprime', então:
                    {
                        var elPrint = (Imprime)item;

                        debug.Items.Add($"-----> {elPrint}");

                        // Verifica a variável no comando 'imprime':
                        elPrint.VerificarVariáveis(this.listaVariáveis, debug);

                        debug.Items.Add($"<----- {elPrint}");
                    }
                }
            }

            debug.Items.Add("<----- Programa");
        }

        /// <summary>
        /// Obtém o resultado da conversão do comando para a linguagem C#.
        /// </summary>
        /// <returns>O código resultante.</returns>
        public string ObterCódigo()
        {
            string bodyCode = $"using System;{Environment.NewLine}namespace HelloWorld {{{Environment.NewLine}class HelloWorldClass {{{Environment.NewLine}static void Main(string[] args) {{{Environment.NewLine}";

            string mainCode = String.Empty;
            string funcCall = $"{Nome.Lexema}(";
            string funcParametros = $"static void {Nome.Lexema}(";
            for (int i = 0; i < this.parâmetros.Length; i++)
            {
                funcParametros += this.parâmetros[i].ObterCódigo();
                funcCall += this.parâmetros[i].Identificador.Lexema;
                if (i < this.parâmetros.Length - 1)
                {
                    funcParametros += ", ";
                    funcCall += ", ";
                }

                if (i == 0)
                    mainCode += $"string s;{Environment.NewLine}";

                mainCode += this.ObterEntrada(this.parâmetros[i]);
            }
            funcCall += $");{Environment.NewLine}{Environment.NewLine}Console.WriteLine();{Environment.NewLine}";
            funcParametros += ") {";

            string funcCorpo = String.Empty;
            foreach (var item in this.ListaTiposEscopoMaior)
            {
                funcCorpo += item.ObterCódigo();
            }

            return $"{bodyCode}{mainCode}{funcCall}Console.WriteLine(\"Execução concluida. Pressione 'enter' para fechar.\");{Environment.NewLine}Console.ReadLine();{Environment.NewLine}}}{Environment.NewLine}{funcParametros}{funcCorpo}{Environment.NewLine}}}{Environment.NewLine}}}{Environment.NewLine}}}";
        }

        private string ObterEntrada(AtributoFunção atributo)
        {
            string code = $"{(atributo.TipoDeDados.Token.Equals("INT") ? "int" : "double")} {atributo.Identificador.Lexema};{Environment.NewLine}do {{{Environment.NewLine}Console.Write(\"Informe o valor ";
            code += $"({(atributo.TipoDeDados.Token.Equals("INT") ? "inteiro" : "ponto flutuante")}) para o parâmetro '{atributo.Identificador.Lexema}': \");{Environment.NewLine}s = Console.ReadLine();{Environment.NewLine}}} ";
            code += $"while (s == null || !{(atributo.TipoDeDados.Token.Equals("INT") ? "int" : "double")}.TryParse(s, out {atributo.Identificador.Lexema}));{Environment.NewLine}{Environment.NewLine}";
            
            return code;
        }
    }
}
