using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador.Sintatico.Descendente.PreditivoLL1
{
    /// <summary>
    /// Tabela M e lista de terminais, utilizadas pelo analisador sintático descentente preditivo.
    /// </summary>
    public static class Tabelas
    {
        private static Dictionary<string, Dictionary<string, string[]>> tabelaM = null;
        private static List<string> terminais = null;

        /// <summary>
        /// Obtém ou Define a Tabela M.
        /// </summary>
        public static Dictionary<string, Dictionary<string, string[]>> TabelaM
        {
            get
            {
                if (Tabelas.tabelaM == null)
                    Tabelas.InitTabelaM();

                return Tabelas.tabelaM;
            }
        }

        /// <summary>
        /// Obtém ou Define a lista de simbolos terminais.
        /// </summary>
        public static List<string> Terminais
        {
            get
            {
                if (Tabelas.terminais == null)
                    Tabelas.InitListTerminais();

                return Tabelas.terminais;
            }
        }

        /// <summary>
        /// Executa a busca por uma produção na tabela M, para o simbolo no topo da pilha (Não Terminal) e o token retornado pelo analizador léxico.
        /// </summary>
        /// <param name="topo">Simbolo no topo da pilha (Não Terminal).</param>
        /// <param name="x">O token retornado pelo analisador léxico.</param>
        /// <returns>A produção encontrada, ou nulo se não encontrar uma produção.</returns>
        public static string[] BuscaProducao(string topo, string x)
        {
            if (Tabelas.TabelaM.ContainsKey(topo))
            {
                if (Tabelas.tabelaM[topo].ContainsKey(x))
                    return Tabelas.tabelaM[topo][x];
            }

            return null;
        }

        /// <summary>
        /// Testa se o simbolo informado é um terminal.
        /// </summary>
        /// <param name="x">O simbolo a testar.</param>
        /// <returns>Verdadeiro se um terminal, ou falso se não terminal.</returns>
        public static bool IsTerminal(string x)
        {
            return !(x.StartsWith("<") && x.EndsWith(">"));
            //return !x.StartsWith("<");
        }

        /// <summary>
        /// Inicializa a estrutura da Tabela M.
        /// </summary>
        private static void InitTabelaM()
        {
            Tabelas.tabelaM = new Dictionary<string, Dictionary<string, string[]>>();

            // <FUNCTION> ::= FUNCTION ID ABRE_PAR <FUNC_BLOCO_PARAMETRO> FECHA_PAR ABRE_BLOCO <LISTA_BLOCO> FECHA_BLOCO
            Dictionary<string, string[]> temp = new Dictionary<string, string[]>();
            temp.Add("FUNCTION", new string[] { "FUNCTION", "ID", "ABRE_PAR", "<FUNC_BLOCO_PARAMETRO>", "ABRE_BLOCO", "<LISTA_BLOCO>" });
            Tabelas.tabelaM.Add("<FUNCTION>", temp);

            // <FUNC_BLOCO_PARAMETRO> ::= <FUNC_PARAMETRO> <FUNC_LISTA_PARAMETRO> | EPSILON
            temp = new Dictionary<string, string[]>();
            temp.Add("INT", new string[] { "<FUNC_PARAMETRO>", "<FUNC_LISTA_PARAMETRO>" });
            temp.Add("FLOAT", new string[] { "<FUNC_PARAMETRO>", "<FUNC_LISTA_PARAMETRO>" });
            temp.Add("FECHA_PAR", new string[] { "FECHA_PAR" });
            Tabelas.tabelaM.Add("<FUNC_BLOCO_PARAMETRO>", temp);

            // <FUNC_PARAMETRO> ::= INT ID | FLOAT ID
            temp = new Dictionary<string, string[]>();
            temp.Add("INT", new string[] { "INT", "ID" });
            temp.Add("FLOAT", new string[] { "FLOAT", "ID" });
            Tabelas.tabelaM.Add("<FUNC_PARAMETRO>", temp);

            // <FUNC_LISTA_PARAMETRO> ::= VIRGULA <FUNC_PARAMETRO> <FUNC_LISTA_PARAMETRO> | EPSILON
            temp = new Dictionary<string, string[]>();
            temp.Add("VIRGULA", new string[] { "VIRGULA", "<FUNC_PARAMETRO>", "<FUNC_LISTA_PARAMETRO>" });
            temp.Add("FECHA_PAR", new string[] { "FECHA_PAR" });
            Tabelas.tabelaM.Add("<FUNC_LISTA_PARAMETRO>", temp);

            // <LISTA_BLOCO> ::= <DECLARA_VAR> <LISTA_BLOCO> | <ATRIB> <LISTA_BLOCO> | <SEL_IF> <LISTA_BLOCO> | <ENQUANTO> <LISTA_BLOCO> | <IMPRIME> <LISTA_BLOCO> | EPSILON
            temp = new Dictionary<string, string[]>();
            temp.Add("INT", new string[] { "<DECLARA_VAR>", "<LISTA_BLOCO>" });
            temp.Add("FLOAT", new string[] { "<DECLARA_VAR>", "<LISTA_BLOCO>" });
            temp.Add("ID", new string[] { "<ATRIB>", "<LISTA_BLOCO>" });
            temp.Add("IF", new string[] { "<SEL_IF>", "<LISTA_BLOCO>" });
            temp.Add("WHILE", new string[] { "<ENQUANTO>", "<LISTA_BLOCO>" });
            temp.Add("PRINT", new string[] { "<IMPRIME>", "<LISTA_BLOCO>" });
            temp.Add("FECHA_BLOCO", new string[] { "FECHA_BLOCO" });
            Tabelas.tabelaM.Add("<LISTA_BLOCO>", temp);

            // <DECLARA_VAR> ::= INT ID PONTO_VIRGULA | FLOAT ID PONTO_VIRGULA
            temp = new Dictionary<string, string[]>();
            temp.Add("INT", new string[] { "INT", "ID", "PONTO_VIRGULA" });
            temp.Add("FLOAT", new string[] { "FLOAT", "ID", "PONTO_VIRGULA" });
            Tabelas.tabelaM.Add("<DECLARA_VAR>", temp);

            // <ATRIB> ::= ID ATRIBUICAO <VAR>
            temp = new Dictionary<string, string[]>();
            temp.Add("ID", new string[] { "ID", "ATRIBUICAO", "<VAR>" });
            Tabelas.tabelaM.Add("<ATRIB>", temp);

            // <VAR> ::= ID <OPERACAO> | CONST <OPERACAO>
            temp = new Dictionary<string, string[]>();
            temp.Add("ID", new string[] { "ID", "<OPERACAO>" });
            temp.Add("CONST", new string[] { "CONST", "<OPERACAO>" });
            Tabelas.tabelaM.Add("<VAR>", temp);

            // <OPERACAO> ::= MENOS" "<VAR> | MAIS" "<VAR> | ASTERISTICO" "<VAR> | BARRA_ESQUERDA" "<VAR> | EPSILON
            temp = new Dictionary<string, string[]>();
            temp.Add("MENOS", new string[] { "MENOS", "<VAR>" });
            temp.Add("MAIS", new string[] { "MAIS", "<VAR>" });
            temp.Add("ASTERISTICO", new string[] { "ASTERISTICO", "<VAR>" });
            temp.Add("BARRA_ESQUERDA", new string[] { "BARRA_ESQUERDA", "<VAR>" });
            temp.Add("PONTO_VIRGULA", new string[] { "PONTO_VIRGULA" });
            Tabelas.tabelaM.Add("<OPERACAO>", temp);

            // <SEL_IF> ::= IF ABRE_PAR <COMP_CONDICIONAL> FECHA_PAR ABRE_BLOCO <LISTA_BLOCO> FECHA_BLOCO
            temp = new Dictionary<string, string[]>();
            temp.Add("IF", new string[] { "IF", "ABRE_PAR", "<COMP_CONDICIONAL>", "FECHA_PAR", "ABRE_BLOCO", "<LISTA_BLOCO>" });
            Tabelas.tabelaM.Add("<SEL_IF>", temp);

            // <COMP_CONDICIONAL> ::= ID <COMPARADOR> <VAR_COMP> | CONST <COMPARADOR> <VAR_COMP>
            temp = new Dictionary<string, string[]>();
            temp.Add("ID", new string[] { "ID", "<COMPARADOR>", "<VAR_COMP>" });
            temp.Add("CONST", new string[] { "CONST", "<COMPARADOR>", "<VAR_COMP>" });
            Tabelas.tabelaM.Add("<COMP_CONDICIONAL>", temp);

            // <COMPARADOR> ::= MAIOR | MENOR | IGUALDADE | DIFERENTE
            temp = new Dictionary<string, string[]>();
            temp.Add("MAIOR", new string[] { "MAIOR" });
            temp.Add("MENOR", new string[] { "MENOR" });
            temp.Add("IGUALDADE", new string[] { "IGUALDADE" });
            temp.Add("DIFERENTE", new string[] { "DIFERENTE" });
            Tabelas.tabelaM.Add("<COMPARADOR>", temp);

            // <VAR_COMP> ::= ID | CONST
            temp = new Dictionary<string, string[]>();
            temp.Add("ID", new string[] { "ID" });
            temp.Add("CONST", new string[] { "CONST" });
            Tabelas.tabelaM.Add("<VAR_COMP>", temp);

            // <ENQUANTO> ::= WHILE ABRE_PAR <COMP_CONDICIONAL> FECHA_PAR ABRE_BLOCO <LISTA_BLOCO> FECHA_BLOCO
            temp = new Dictionary<string, string[]>();
            temp.Add("WHILE", new string[] { "WHILE", "ABRE_PAR", "<COMP_CONDICIONAL>", "FECHA_PAR", "ABRE_BLOCO", "<LISTA_BLOCO>" });
            Tabelas.tabelaM.Add("<ENQUANTO>", temp);

            // <IMPRIME> ::= PRINT ABRE_PAR ID FECHA_PAR PONTO_VIRGULA
            temp = new Dictionary<string, string[]>();
            temp.Add("PRINT", new string[] { "PRINT", "ABRE_PAR", "ID", "FECHA_PAR", "PONTO_VIRGULA" });
            Tabelas.tabelaM.Add("<IMPRIME>", temp);

            // <TIPO> ::= INT | FLOAT
            temp = new Dictionary<string, string[]>();
            temp.Add("INT", new string[] { "INT" });
            temp.Add("FLOAT", new string[] { "FLOAT" });
            Tabelas.tabelaM.Add("<TIPO>", temp);
            /*
            temp = new Dictionary<string, string[]>();
            temp.Add("", new string[] { "", "" });
            Tabelas.tabelaM.Add("<>", temp);
            */
        }

        /// <summary>
        /// Inicializa a lista de terminais.
        /// </summary>
        private static void InitListTerminais()
        {
            Tabelas.terminais = new List<string>();
            Tabelas.terminais.Add("ID");
            Tabelas.terminais.Add("IF");
            Tabelas.terminais.Add("CONST");
            Tabelas.terminais.Add("ESPACO");
            Tabelas.terminais.Add("ABRE_PAR");
            Tabelas.terminais.Add("FECHA_PAR");
            Tabelas.terminais.Add("MAIOR");
            Tabelas.terminais.Add("ABRE_BLOCO");
            Tabelas.terminais.Add("FOR");
            Tabelas.terminais.Add("PRINT");
            Tabelas.terminais.Add("MENOR");
            Tabelas.terminais.Add("FECHA_BLOCO");
            Tabelas.terminais.Add("MENOS");
            Tabelas.terminais.Add("MAIS");
            Tabelas.terminais.Add("ASTERISTICO");
            Tabelas.terminais.Add("BARRA_ESQUERDA");
            Tabelas.terminais.Add("ATRIBUICAO");
            Tabelas.terminais.Add("IGUALDADE");
            Tabelas.terminais.Add("?");
            Tabelas.terminais.Add("DIFERENTE");
            Tabelas.terminais.Add("WHILE");
            Tabelas.terminais.Add("INT");
            Tabelas.terminais.Add("VIRGULA");
            Tabelas.terminais.Add("PONTO_VIRGULA");
            Tabelas.terminais.Add("FUNCTION");
            Tabelas.terminais.Add("FLOAT");
        }
    }
}
