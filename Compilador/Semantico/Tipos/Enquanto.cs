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
    /// Classe para armazenar um comando 'enquanto'.
    /// </summary>
    public class Enquanto : ITipo
    {
        private List<Variável> listaVariáveisLocal = null;
        private List<Variável> listaVariáveisAtribuidas = null;

        /// <summary>
        /// O construtor da classe.
        /// </summary>
        /// <param name="elementos">Os elementos semânticos que constituem o comando.</param>
        /// <param name="listaTiposEscopoMaior">A lista de comandos dos escopos maiores (internos).</param>
        public Enquanto(List<SemanticoItem> elementos, List<ITipo> listaTiposEscopoMaior)
        {
            if (elementos == null)
                throw new ArgumentNullException(nameof(elementos));
            if (elementos.Count < 8)
                throw new Exception("Um tipo 'enquanto' deve receber no mínimo 8 elementos!");

            this.Elementos = elementos;
            this.ListaTiposEscopoMaior = listaTiposEscopoMaior;

            this.Atributos = new TokenClass[2];
            this.Atributos[0] = elementos[2].Token;
            this.Atributos[1] = elementos[4].Token;

            this.OpRelacional = elementos[3].Token;
        }

        /// <summary>
        /// Os elementos semânticos que constituem o comando.
        /// </summary>
        public List<SemanticoItem> Elementos { get; set; }

        /// <summary>
        /// Obtém ou define a lista de comandos dos escopos maiores (internos).
        /// </summary>
        public List<ITipo> ListaTiposEscopoMaior { get; set; }

        /// <summary>
        /// Obtém ou define o operador relacional da expresão lógica.
        /// </summary>
        public TokenClass OpRelacional { get; set; }

        /// <summary>
        /// Obtém ou define os atributos envolvidos na expressão lógica.
        /// </summary>
        public TokenClass[] Atributos { get; set; }

        /// <summary>
        /// Verifica as variáveis do comando 'enquanto' e dos comandos dos escopos internos.
        /// </summary>
        /// <param name="listaVariáveisEscopo">A lista de variáveis visível pelo escopo.</param>
        /// <param name="debug">A lista de saída dos resultados da verificação.</param>
        public void VerificarVariáveis(List<Variável> listaVariáveisEscopo, ListBox debug)
        {
            // Testa os atributos da operação condicional...
            
            // Se o atributo da esquerda é uma variável, então:
            if (this.Atributos[0].Token.Equals("ID"))
            {
                Variável idOpLog = listaVariáveisEscopo.Find(x => x.Identificador.Equals(this.Atributos[0].Lexema));
                if (idOpLog == null)
                    throw new Exception($"A variável '{this.Atributos[0].Lexema}' foi usada mas não está declarada!");

                if (!idOpLog.ValorAtribuido)
                    throw new Exception($"A variável '{idOpLog.Identificador}' foi usada mas não possui valor atribuido!");

                debug.Items.Add($"-> {idOpLog}");
            }

            // Se o atributo da direita é uma variável, então:
            if (this.Atributos[1].Token.Equals("ID"))
            {
                Variável idOpLog = listaVariáveisEscopo.Find(x => x.Identificador.Equals(this.Atributos[1].Lexema));
                if (idOpLog == null)
                    throw new Exception($"A variável '{this.Atributos[1].Lexema}' foi usada mas não está declarada!");

                if (!idOpLog.ValorAtribuido)
                    throw new Exception($"A variável '{idOpLog.Identificador}' foi usada mas não possui valor atribuido!");

                debug.Items.Add($"-> {idOpLog}");
            }

            // E existire itens no escopo (lista bloco), então testa:
            if (this.ListaTiposEscopoMaior != null && this.ListaTiposEscopoMaior.Count > 0)
            {
                this.listaVariáveisLocal = new List<Variável>();
                this.listaVariáveisAtribuidas = new List<Variável>();

                foreach (var item in this.ListaTiposEscopoMaior)
                {
                    Type tipoItem = item.GetType();
                    if (tipoItem == typeof(Declaração))
                    {
                        var elDec = (Declaração)item;

                        // Verifica se a variável foi declarada localmente:
                        IEnumerable<Variável> elements = this.listaVariáveisLocal.Where(x => x.Identificador.Equals(elDec.Identificador.Lexema));
                        if (!elements.Count().Equals(0))
                            throw new Exception($"A variável '{elDec.Identificador.Lexema}' já foi declarada!");

                        // Verifica se a variável foi declarada em outro escopo:
                        elements = listaVariáveisEscopo.Where(x => x.Identificador.Equals(elDec.Identificador.Lexema));
                        if (!elements.Count().Equals(0))
                            throw new Exception($"A variável '{elDec.Identificador.Lexema}' já foi declarada!");

                        this.listaVariáveisLocal.Add(new Variável(elDec.Identificador.Lexema, elDec.TipoDeDados.Token));

                        debug.Items.Add($"> {this.listaVariáveisLocal.Last()}");
                    }
                    else if (tipoItem == typeof(Atribuição))
                    {
                        var elAtr = (Atribuição)item;

                        // Verifica se a variável que recebe a operação foi declarada localmente:
                        Variável identificador = this.listaVariáveisLocal.Find(x => x.Identificador.Equals(elAtr.Variável.Lexema));
                        if (identificador == null)  // Não encontrada no escopo local, então:
                        {
                            // Busca na lista de escopo geral:
                            identificador = listaVariáveisEscopo.Find(x => x.Identificador.Equals(elAtr.Variável.Lexema));
                            if (identificador == null)
                                throw new Exception($"A variável '{elAtr.Variável.Lexema}' foi usada mas não está declarada!");
                        }

                        debug.Items.Add($"=> {identificador}");

                        // Testa as variáveis na expressão de atribuição, se estão declaradas, possuem valor atribuido e o tipo de dados é compativel com a operação:
                        foreach (var itemExpr in elAtr.Expressão)
                        {
                            if (itemExpr.Token.Equals("ID"))
                            {
                                // Verifica se a variável na operação foi declarada localmente:
                                Variável elementsExpr = this.listaVariáveisLocal.Find(x => x.Identificador.Equals(itemExpr.Lexema));
                                if (elementsExpr == null)  // Não encontrada no escopo local, então:
                                {
                                    // Busca na lista de escopo geral:
                                    elementsExpr = listaVariáveisEscopo.Find(x => x.Identificador.Equals(itemExpr.Lexema));
                                    if (elementsExpr == null)
                                        throw new Exception($"A variável '{itemExpr.Lexema}' foi usada mas não está declarada!");
                                }

                                if (!elementsExpr.ValorAtribuido)
                                    throw new Exception($"A variável '{itemExpr.Lexema}' foi usada mas não possui valor atribuido!");

                                if (identificador.Tipo.Equals("INT") && elementsExpr.Tipo.Equals("FLOAT"))
                                    throw new Exception($"A variável '{identificador.Identificador}' do tipo inteiro não pode receber a variável '{elementsExpr.Identificador}' do tipo ponto flutuante!");

                                debug.Items.Add($"> {elementsExpr}");
                            }
                            else if (itemExpr.Token.Equals("CONST_FLOAT"))
                            {
                                if (identificador.Tipo.Equals("INT"))
                                    throw new Exception($"A variável '{identificador.Identificador}' do tipo inteiro não pode receber uma constante '{itemExpr.Lexema}' do tipo ponto flutuante!");

                                debug.Items.Add($"> {itemExpr.Lexema} : {itemExpr.Token}");
                            }
                        }

                        if (!identificador.ValorAtribuido)
                            listaVariáveisAtribuidas.Add(identificador);

                        // Variável que recebe a operação agora possui valor atribuido, então marca:
                        identificador.ValorAtribuido = true;
                    }
                    else if (tipoItem == typeof(SeleçãoIf))
                    {
                        var elSelIf = (SeleçãoIf)item;

                        if (this.listaVariáveisLocal.Count > 0)
                        {
                            listaVariáveisEscopo.AddRange(this.listaVariáveisLocal);
                            debug.Items.Add($">>> Anexado: {this.listaVariáveisLocal.Count} variáveis <<<");
                        }

                        debug.Items.Add($"-----> {elSelIf}");

                        // Verifica as variáveis na operação condicional, e nos escopos subsequentes do comando de seleção 'if':
                        elSelIf.VerificarVariáveis(listaVariáveisEscopo, debug);

                        debug.Items.Add($"<----- {elSelIf}");

                        if (this.listaVariáveisLocal.Count > 0)
                        {
                            listaVariáveisEscopo.RemoveRange(listaVariáveisEscopo.Count - this.listaVariáveisLocal.Count, this.listaVariáveisLocal.Count);
                            debug.Items.Add($">>> Removido: {this.listaVariáveisLocal.Count} variáveis <<<");
                        }
                    }
                    else if (tipoItem == typeof(Enquanto))
                    {
                        var elWhile = (Enquanto)item;

                        if (this.listaVariáveisLocal.Count > 0)
                        {
                            listaVariáveisEscopo.AddRange(this.listaVariáveisLocal);
                            debug.Items.Add($">>> Anexado: {this.listaVariáveisLocal.Count} variáveis <<<");
                        }

                        debug.Items.Add($"-----> {elWhile}");

                        // Verifica as variáveis na operação condicional, e nos escopos subsequentes do comando de repetição 'enquanto':
                        elWhile.VerificarVariáveis(listaVariáveisEscopo, debug);

                        debug.Items.Add($"<----- {elWhile}");

                        if (this.listaVariáveisLocal.Count > 0)
                        {
                            listaVariáveisEscopo.RemoveRange(listaVariáveisEscopo.Count - this.listaVariáveisLocal.Count, this.listaVariáveisLocal.Count);
                            debug.Items.Add($">>> Removido: {this.listaVariáveisLocal.Count} variáveis <<<");
                        }
                    }
                    else if (tipoItem == typeof(Imprime))
                    {
                        var elPrint = (Imprime)item;

                        if (this.listaVariáveisLocal.Count > 0)
                        {
                            listaVariáveisEscopo.InsertRange(0, this.listaVariáveisLocal);
                            debug.Items.Add($">>> Anexado: {this.listaVariáveisLocal.Count} variáveis <<<");
                        }

                        debug.Items.Add($"-----> {elPrint}");

                        // Verifica a variável no comando 'imprime':
                        elPrint.VerificarVariáveis(listaVariáveisEscopo, debug);

                        debug.Items.Add($"<----- {elPrint}");

                        if (this.listaVariáveisLocal.Count > 0)
                        {
                            listaVariáveisEscopo.RemoveRange(0, this.listaVariáveisLocal.Count);
                            debug.Items.Add($">>> Removido: {this.listaVariáveisLocal.Count} variáveis <<<");
                        }
                    }
                }

            }

            if (this.listaVariáveisAtribuidas != null)
            {
                foreach (var item in this.listaVariáveisAtribuidas)
                    item.ValorAtribuido = false;

                this.listaVariáveisAtribuidas = null;
            }
        }

        /// <summary>
        /// Obtém o resultado da conversão do comando para a linguagem C#.
        /// </summary>
        /// <returns>O código resultante.</returns>
        public string ObterCódigo()
        {
            string code = $"while({this.Atributos[0].Lexema} {this.OpRelacional.Lexema} {this.Atributos[1].Lexema}) {{";

            foreach (var item in this.ListaTiposEscopoMaior)
            {
                code += item.ObterCódigo();
            }

            return $"{Environment.NewLine}{Environment.NewLine}{code}{Environment.NewLine}}}{Environment.NewLine}";
        }

        public override string ToString()
        {
            return $"Enquanto: '{this.Atributos[0].Lexema} {this.OpRelacional.Lexema} {this.Atributos[1].Lexema}'";
        }
    }
}
