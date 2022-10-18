using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Compilador.Lexico;

namespace Compilador.Sintatico
{
    public static class AnalizadorAscendente
    {
        private static ListBox debug;
        private static LexicoClass lexico = null;
        private static Stack<TokenClass> pilhaTokens = null;
        private static int acao;

        public static void Verificar(LexicoClass lexico, ListBox resultado)
        {
            if (lexico == null)
                throw new Exception("Analizador léxico não pode ser nulo!");

            if (lexico.ListaTokens.Length == 0)
                throw new Exception("Analizador léxico não possui tokens!");

            AnalizadorAscendente.lexico = lexico;
            debug = resultado;

            // Inicia a variável auxiliar:
            pilhaTokens = new Stack<TokenClass>();
            acao = 0;
            debug.Items.Add("Ação: 0");

            while (lexico.AsNextToken())
            {
                TokenClass token = lexico.NextToken();

                pilhaTokens.Push(token);
                debug.Items.Add($"Empilha: {token.Token}");

                if (TabelaAcao.Finais.Contains(token.Token))
                    Executa();
            }

            acao = 5;
            debug.Items.Add("Ação: 5");
            Executa();

            debug.Items.Add(pilhaTokens.Count == 0 ? "--> Linguagem aceita <---" : "--> Erro sintático <---");
        }

        private static void Executa()
        {
            TokenClass token = pilhaTokens.Pop();
            debug.Items.Add($"Desempilha: {token.Token}");

            while (TabelaAcao.Acao[acao] != null && TabelaAcao.Acao[acao].ContainsKey(token.Token))
            {
                acao = TabelaAcao.Acao[acao][token.Token];
                debug.Items.Add($"Ação: {acao}");

                if (TabelaAcao.Acao[acao] != null)
                {
                    token = pilhaTokens.Pop();
                    debug.Items.Add($"Desempilha: {token.Token}");
                }
            }

            if (TabelaAcao.Acao[acao] == null)
            {
                acao = TabelaAcao.AcoesFinais[acao];
                debug.Items.Add($"Ação: {acao}");
                return;
            }
            
            throw new Exception($"Token inesperado!{Environment.NewLine}{token}");
        }
    }
}
