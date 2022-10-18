using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Compilador.Lexico;

namespace Compilador.Sintatico.Descendente.PreditivoLL1
{
    /// <summary>
    /// Analisador Sintático Descendente Preditivo LL(1)
    /// </summary>
    public static class DescPredLL1
    {
        private static ListBox debug;
        private static LexicoClass lexico = null;
        private static Stack<string> pilhaTokens = null;

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

            DescPredLL1.lexico = lexico;
            debug = resultado;

            // Inicia a variável auxiliar:
            pilhaTokens = new Stack<string>();

            pilhaTokens.Push("<FUNCTION>");
            TokenClass token = lexico.NextToken();

            debug.Items.Add("Empilha: <FUNCTION>");
            debug.Items.Add($"Próximo Token: {token.Token}");

            string topoPilha;
            string[] producao;

            while (true)
            {
                // Se o topo da pilha é terminal:
                topoPilha = pilhaTokens.Pop();
                debug.Items.Add($"Desempilha: {topoPilha}");
                if (Tabelas.Terminais.Contains(topoPilha))
                //if (Tabelas.IsTerminal(topoPilha))
                {
                    // Se topo = X:
                    if (topoPilha.Equals(token.Token))
                    {
                        // Se não existir mais tokens, sai do laço de repetição:
                        if (!lexico.AsNextToken())
                            break;

                        // Obtém o próximo token e reinicia o laço de repetição:
                        token = lexico.NextToken();
                        debug.Items.Add($"Próximo Token: {token.Token}");
                        continue;
                    }

                    // senão, erro:
                    throw new Exception($"Erro Sintático!{Environment.NewLine}Esperado: {topoPilha}{Environment.NewLine}Encontrado: {token.Token}{Environment.NewLine}{token}");
                }

                debug.Items.Add($"> Busca Produção M({topoPilha}, {token.Token})");

                // senão, busca produção na tabela M:
                producao = Tabelas.BuscaProducao(topoPilha, token.Token);

                // Se não encontrou uma produção, então lança erro:
                if (producao == null)
                    throw new Exception($"Produção não encontrada!{Environment.NewLine}Topo: {topoPilha}{Environment.NewLine}X: {token.Token}{Environment.NewLine}Linha: {token.Linha}, Posição: {token.Inicio}");

                // Empilha produção (em ordem reversa na pilha, pois o último a entrar é o primeiro a sair da pilha):
                for (int i = producao.Length-1; i >=0 ; i--)
                {
                    pilhaTokens.Push(producao[i]);
                    debug.Items.Add($"Empilha: {producao[i]}");
                }
            }

            // Se a pilha não estiver vazia, então lança erro:
            if (pilhaTokens.Count != 0)
            {
                debug.Items.Add(">>> Pilha não está vazia! <<<");
                throw new Exception($"A pilha não está vazia!{Environment.NewLine}{pilhaTokens.Count} itens restantes.");
            }

            debug.Items.Add("--> Linguagem aceita <---");
        }
    }
}
