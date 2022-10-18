using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Compilador.Lexico;

namespace Compilador.Sintatico.Descendente.Recursivo
{
    /// <summary>
    /// Analisador Sintático Descendente Recursivo
    /// </summary>
    public static class DescRecursivo
    {
        private static ListBox debug;
        private static LexicoClass lexico = null;
        private static int inicial;
        private static int contador;

        /// <summary>
        /// Executa a verificação sintática utilizando os tokens passados pelo analisador léxico.
        /// </summary>
        /// <param name="lexico">O analisador léxico contendo os tokens a serem analisados.</param>
        /// <param name="resultado">O ListBox para exibir os resultados da verificação sintática.</param>
        public static void Verificar(LexicoClass lexico, ListBox resultado)
        {
            if (lexico == null)
                throw new Exception("Analizador léxico não pode ser nulo!");

            if (lexico.ListaTokens.Length == 0)
                throw new Exception("Analizador léxico não possui tokens!");

            DescRecursivo.lexico = lexico;
            debug = resultado;
            
            // Inicia a variável auxiliar:
            contador = 0;

            string msg = "P = ";
            bool aceito;
            inicial = contador;

            aceito = P();

            for (int i = inicial; i < contador; i++)
                msg += $"{lexico.ListaTokens[i].Token} ";

            debug.Items.Add(msg);

            debug.Items.Add(aceito ? "--> Linguagem aceita <---" : "--> Erro sintático <---");
        }

        private static bool term(string token)
        {
            debug.Items.Add($"-> term({token})");
            if (contador >= lexico.ListaTokens.Length)
                return false;

            return lexico.ListaTokens[contador++].Token.Equals(token);
        }
        
        // <P> ::= FUNCTION ID ABRE_PAR <LISTA_PARAMETRO> FECHA_PAR ABRE_BLOCO <LISTA_BLOCO> FECHA_BLOCO
        private static bool P()
        {
            debug.Items.Add("-> <P> ::= FUNCTION ID ABRE_PAR <LISTA_PARAMETRO> FECHA_PAR ABRE_BLOCO <LISTA_BLOCO> FECHA_BLOCO");
            return term("FUNCTION") && term("ID") && term("ABRE_PAR") && LISTA_PARAMETRO() && term("FECHA_PAR") && term("ABRE_BLOCO") && LISTA_BLOCO();
        }

        // <LISTA_PARAMETRO> ::= <PARAMETRO> | <PARAMETRO> <LISTA_PARAMETRO>
        private static bool LISTA_PARAMETRO()
        {
            debug.Items.Add("-> <LISTA_PARAMETRO> ::= <PARAMETRO> | <PARAMETRO> <LISTA_PARAMETRO>");
            int anterior = contador;
            if (LISTA_PARAMETRO1())
                return true;

            contador = anterior;
            return LISTA_PARAMETRO2();
        }

        // <LISTA_PARAMETRO> ::= <PARAMETRO>
        private static bool LISTA_PARAMETRO1()
        {
            debug.Items.Add("-> <LISTA_PARAMETRO> ::= <PARAMETRO>");
            return PARAMETRO();
        }

        // <LISTA_PARAMETRO> ::= <PARAMETRO> <LISTA_PARAMETRO>
        private static bool LISTA_PARAMETRO2()
        {
            debug.Items.Add("-> <LISTA_PARAMETRO> ::= <PARAMETRO> <LISTA_PARAMETRO>");
            return PARAMETRO() && LISTA_PARAMETRO();
        }

        // <PARAMETRO> ::= <PARAMETRO> VIRGULA | <PARAMETRO>
        private static bool PARAMETRO()
        {
            debug.Items.Add("-> <PARAMETRO> ::= <PARAMETRO> VIRGULA | <PARAMETRO>");
            int anterior = contador;
            if (PARAMETRO1())
                return true;

            contador = anterior;
            return PARAMETRO2();
        }

        // <PARAMETRO> ::= <DECLARA_VAR> VIRGULA <PARAMETRO>
        private static bool PARAMETRO1()
        {
            debug.Items.Add("-> <PARAMETRO> ::= <DECLARA_VAR> VIRGULA <PARAMETRO>");
            if (DECLARA_VAR() && term("VIRGULA"))
                return PARAMETRO();

            return false;
        }

        // <PARAMETRO> ::= <DECLARA_VAR>
        private static bool PARAMETRO2()
        {
            debug.Items.Add("-> <PARAMETRO> ::= <DECLARA_VAR>");
            return DECLARA_VAR();
        }

        // <DECLARA_VAR> ::= <TIPO_VAR> ID
        private static bool DECLARA_VAR()
        {
            debug.Items.Add("-> <DECLARA_VAR> ::= <TIPO_VAR> ID");
            return TIPO_VAR() && term("ID");
        }

        // <TIPO_VAR> ::= INT | FLOAT
        private static bool TIPO_VAR()
        {
            debug.Items.Add("-> <TIPO_VAR> ::= INT | FLOAT");
            int anterior = contador;
            if (TIPO_VAR1())
                return true;

            contador = anterior;
            return TIPO_VAR2();
        }

        // <TIPO_VAR> ::= INT
        private static bool TIPO_VAR1()
        {
            debug.Items.Add("-> <TIPO_VAR> ::= INT");
            return term("INT");
        }

        // <TIPO_VAR> ::= FLOAT
        private static bool TIPO_VAR2()
        {
            debug.Items.Add("-> <TIPO_VAR> ::= FLOAT");
            return term("FLOAT");
        }

        // <LISTA_BLOCO> ::= <BLOCO> FECHA_BLOCO | <BLOCO> <LISTA_BLOCO>
        private static bool LISTA_BLOCO()
        {
            debug.Items.Add("-> <LISTA_BLOCO> ::= <BLOCO> FECHA_BLOCO | <BLOCO> <LISTA_BLOCO>");
            int anterior = contador;
            if (LISTA_BLOCO1())
                return true;

            contador = anterior;
            return LISTA_BLOCO2();
        }

        // <LISTA_BLOCO> ::= <BLOCO> FECHA_BLOCO
        private static bool LISTA_BLOCO1()
        {
            debug.Items.Add("-> <LISTA_BLOCO> ::= <BLOCO> FECHA_BLOCO");
            return BLOCO() && term("FECHA_BLOCO");
        }

        // <LISTA_BLOCO> ::= <BLOCO> <LISTA_BLOCO> FECHA_BLOCO
        private static bool LISTA_BLOCO2()
        {
            debug.Items.Add("-> <LISTA_BLOCO> ::= <BLOCO> <LISTA_BLOCO> FECHA_BLOCO");
            return BLOCO() && LISTA_BLOCO();
        }

        // <BLOCO> ::= <DECLARA_VAR> PONTO_VIRGULA | <ATR> | <SEL_IF> | <IMPRIME> | <ENQUANTO>
        private static bool BLOCO()
        {
            debug.Items.Add("-> <BLOCO> ::= <DECLARA_VAR> PONTO_VIRGULA | <ATR> | <SEL_IF> | <IMPRIME> | <ENQUANTO>");
            int anterior = contador;
            if (BLOCO1())
                return true;

            contador = anterior;
            if (BLOCO2())
                return true;

            contador = anterior;
            if (BLOCO3())
                return true;

            contador = anterior;
            if (BLOCO4())
                return true;

            contador = anterior;
            return BLOCO5();
        }

        // <BLOCO> ::= <DECLARA_VAR> PONTO_VIRGULA
        private static bool BLOCO1()
        {
            debug.Items.Add("-> <BLOCO> ::= <DECLARA_VAR> PONTO_VIRGULA");
            return DECLARA_VAR() && term("PONTO_VIRGULA");
        }

        // <BLOCO> ::= <ATR>
        private static bool BLOCO2()
        {
            debug.Items.Add("-> <BLOCO> ::= <ATR>");
            return ATR();
        }

        // <BLOCO> ::= <SEL_IF>
        private static bool BLOCO3()
        {
            debug.Items.Add("-> <BLOCO> ::= <SEL_IF>");
            return SEL_IF();
        }

        // <BLOCO> ::= <IMPRIME>
        private static bool BLOCO4()
        {
            debug.Items.Add("-> <BLOCO> ::= <IMPRIME>");
            return IMPRIME();
        }

        // <BLOCO> ::= <ENQUANTO>
        private static bool BLOCO5()
        {
            debug.Items.Add("-> <BLOCO> ::= <ENQUANTO>");
            return ENQUANTO();
        }

        // <ATR> ::= ID ATRIBUICAO <VAR>
        private static bool ATR()
        {
            debug.Items.Add("-> <ATR> ::= ID ATRIBUICAO <VAR>");
            return term("ID") && term("ATRIBUICAO") && VAR();
        }

        // <SEL_IF> ::= IF ABRE_PAR ID COMPARA ID FECHA_PAR ABRE_BLOCO <LISTA_BLOCO> FECHA_BLOCO
        private static bool SEL_IF()
        {
            debug.Items.Add("-> <SEL_IF> ::= IF ABRE_PAR ID COMPARA ID FECHA_PAR ABRE_BLOCO <LISTA_BLOCO> FECHA_BLOCO");
            return term("IF") && term("ABRE_PAR") && COMPARACAO() && term("FECHA_PAR") && term("ABRE_BLOCO") && LISTA_BLOCO();
        }

        // <IMPRIME> ::= PRINT ABRE_PAR ID FECHA_PAR PONTO_VIRGULA
        private static bool IMPRIME()
        {
            debug.Items.Add("-> <IMPRIME> ::= PRINT ABRE_PAR ID FECHA_PAR PONTO_VIRGULA");
            return term("PRINT") && term("ABRE_PAR") && term("ID") && term("FECHA_PAR") && term("PONTO_VIRGULA");
        }

        // <ENQUANTO> ::= WHILE ABRE_PAR <COMPARACAO> ABRE_BLOCO <LISTA_BLOCO> FECHA_BLOCO
        private static bool ENQUANTO()
        {
            debug.Items.Add("-> <ENQUANTO> ::= WHILE ABRE_PAR <COMPARACAO> ABRE_BLOCO <LISTA_BLOCO> FECHA_BLOCO");
            return term("WHILE") && term("ABRE_PAR") && COMPARACAO() && term("FECHA_PAR") && term("ABRE_BLOCO") && LISTA_BLOCO();
        }

        // <COMPARACAO> ::= <VAR_COMP> <COMPARADOR> <VAR_COMP>
        private static bool COMPARACAO()
        {
            debug.Items.Add("-> <COMPARACAO> ::= <VAR_COMP> <COMPARADOR> <VAR_COMP>");
            return VAR_COMP() && COMPARADOR() && VAR_COMP();
        }

        // <VAR_COMP> ::= ID | CONST
        private static bool VAR_COMP()
        {
            debug.Items.Add("-> <VAR_COMP> ::= ID | CONST");
            int anterior = contador;
            if (term("ID"))
                return true;

            contador = anterior;
            return term("CONST");
        }

        // <COMPARADOR> ::= MAIOR | MENOR | IGUALDADE | DIFERENTE
        private static bool COMPARADOR()
        {
            debug.Items.Add("-> <COMPARADOR> ::= MAIOR | MENOR | IGUALDADE | DIFERENTE");
            int anterior = contador;
            if (term("MAIOR"))
                return true;

            contador = anterior;
            if (term("MENOR"))
                return true;

            contador = anterior;
            if (term("IGUALDADE"))
                return true;

            contador = anterior;
            return term("DIFERENTE");
        }

        // <VAR> ::= ID PONTO_VIRGULA | ID <OPERACAO> <VAR> | CONST PONTO_VIRGULA | CONST <OPERACAO> <VAR>
        private static bool VAR()
        {
            debug.Items.Add("-> <VAR> ::= ID PONTO_VIRGULA | ID <OPERACAO> <VAR> | CONST PONTO_VIRGULA | CONST <OPERACAO> <VAR>");
            int anterior = contador;
            if (VAR1())
                return true;
            
            contador = anterior;
            return VAR2();
        }

        // <VAR> ::= ID PONTO_VIRGULA | ID <OPERACAO> <VAR>
        private static bool VAR1()
        {
            debug.Items.Add("-> <VAR> ::= ID PONTO_VIRGULA | ID <OPERACAO> <VAR>");
            if (!term("ID"))
                return false;

            int anterior = contador;
            if (term("PONTO_VIRGULA"))
                return true;

            contador = anterior;
            return OPERACAO() && VAR();
        }

        // <VAR> ::= CONST PONTO_VIRGULA | CONST <OPERACAO> <VAR>
        private static bool VAR2()
        {
            debug.Items.Add("-> <VAR> ::= CONST PONTO_VIRGULA | CONST <OPERACAO> <VAR>");
            if (!term("CONST"))
                return false;

            int anterior = contador;
            if (term("PONTO_VIRGULA"))
                return true;

            contador = anterior;
            return OPERACAO() && VAR();
        }

        // <OPERACAO> ::= MENOS | MAIS | ASTERISTICO | BARRA_ESQUERDA
        private static bool OPERACAO()
        {
            debug.Items.Add("-> <OPERACAO> ::= MENOS | MAIS | ASTERISTICO | BARRA_ESQUERDA");
            int anterior = contador;
            if (term("MENOS"))
                return true;

            contador = anterior;
            if (term("MAIS"))
                return true;

            contador = anterior;
            if (term("ASTERISTICO"))
                return true;

            contador = anterior;
            return term("BARRA_ESQUERDA");
        }
    }
}
