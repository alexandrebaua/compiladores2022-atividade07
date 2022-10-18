using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Compilador.Lexico;
using Compilador.Semantico;

namespace Compilador.Sintatico.Ascendente.SLR
{
    /// <summary>
    /// Analisador Sintático Ascendente SLR
    /// </summary>
    public static class AscSLR
    {
        private static ListBox debug = null;
        private static LexicoClass lexico = null;
        private static Stack<int> pilha = null;
        private static Stack<string> simbolos = null;

        /// <summary>
        /// Executa a verificação sintática utilizando os tokens passados pelo analisador léxico.
        /// </summary>
        /// <param name="lexico">O analisador léxico contendo os tokens a serem analisados.</param>
        /// <param name="resultado">O ListBox para exibir os resultados da verificação sintática.</param>
        public static void Verificar(LexicoClass lexico, ListBox resultado, SemanticoClass semantico)
        {
            if (lexico == null)
                throw new Exception("Analizador léxico não pode ser nulo!");

            if (lexico.ListaTokens.Length == 0)
                throw new Exception("Analizador léxico não possui tokens!");

            AscSLR.lexico = lexico;
            debug = resultado;

            // Inicia as pilhas de armazenamento de estados e de símbolos:
            pilha = new Stack<int>();
            simbolos = new Stack<string>();

            // Insere o estado inicial e símbolo inicial nas pilhas:
            pilha.Push(0);
            simbolos.Push("$");

            ImprimePilha();
            ImprimeSimbolos();

            int s = 0;      // Estado no topo da pilha
            string a = "";  // Simbolo atual na entrada
            int t = 0;      // Estado lido da tabela de ação

            ActionClass action;  // Variável auxiliar para armazenar a ação retornada pela tabela de ação.
            TokenClass aToken;   // Variável auxiliar para armazenar o token atual.

            // Controle de escopo e agrupamento:
            List<SemanticoItem> listaSemantica = new List<SemanticoItem>();
            semantico.ListaItens = listaSemantica;
            SemanticoItem itemSemantico, itemSemanticoAnterior = null;
            Stack<SemanticoItem> pilhaBloco = new Stack<SemanticoItem>();
            int escopo = 0;
            int grupo = 0;

            // Lê o primeiro token do analisador léxico.
            aToken = lexico.NextToken();
            a = aToken.Token;
            itemSemantico = new SemanticoItem(escopo, grupo, aToken);
            listaSemantica.Add(itemSemantico);

            while (true)
            {
                s = pilha.Peek();
                debug.Items.Add($"s: {s}");

                action = Tabelas.BuscaAcao(s, a);
                debug.Items.Add($"> Ação[{s}, {a}]: {action}");

                if (action == null)
                {
                    throw new Exception($"Ação não encontrada!{Environment.NewLine}ACTION[{s}, {a}]");
                }

                if (action.TipoAcao == ActionTypeEnum.Shift)
                {
                    t = action.Estado;
                    pilha.Push(t);
                    simbolos.Push(a);
                    debug.Items.Add($"Empilha Pilha: {t}");
                    debug.Items.Add($"Empilha Simbolo: {a}");

                    // Lê o próximo token do analisador léxico.
                    aToken = lexico.NextToken();
                    a = aToken.Token;
                    debug.Items.Add($"Próximo Simbolo: {a}");

                    itemSemanticoAnterior = itemSemantico;
                    itemSemantico = new SemanticoItem(escopo, grupo, aToken);
                    listaSemantica.Add(itemSemantico);

                    // Controle de escopo e agrupamento:
                    if (a.Equals("ABRE_BLOCO"))
                    {
                        escopo++;
                        grupo++;
                        pilhaBloco.Push(itemSemantico);
                    }
                    else if (a.Equals("FECHA_BLOCO"))
                    {
                        escopo--;
                    }
                    else if (a.Equals("PONTO_VIRGULA"))
                        grupo++;
                }
                else if (action.TipoAcao == ActionTypeEnum.Reduce)
                {
                    Desempilha(action);
                    t = pilha.Peek();
                    debug.Items.Add($"t: {t}");

                    int estado = Tabelas.BuscaGOTO(t, action.Reducao.To);
                    debug.Items.Add($"> GOTO[{t}, {action.Reducao.To}]: {estado}");

                    pilha.Push(estado);
                    simbolos.Push(action.Reducao.To);
                    debug.Items.Add($"Empilha Pilha: {estado}");
                    debug.Items.Add($"Empilha Simbolo: {action.Reducao.To}");

                    // Controle de escopo e agrupamento:
                    if (action.Reducao.ReduceType != ReduceTypeEnum.None)
                    {
                        if (a.Equals("FECHA_BLOCO") && pilhaBloco.Count > 0)
                        {
                            SemanticoItem itemBloco = pilhaBloco.Pop();
                            itemSemantico.Escopo = itemBloco.Escopo;
                            itemSemantico.Grupo = itemBloco.Grupo;
                        }

                        if (itemSemanticoAnterior != null)
                            itemSemanticoAnterior.ReduceType = action.Reducao.ReduceType;
                    }
                }
                else if (action.TipoAcao == ActionTypeEnum.Accept)
                {
                    break;
                }

                ImprimePilha();
                ImprimeSimbolos();
            }

            if (pilha.Count != 2)
                throw new Exception("O analisador sintático deve finalizar com 2 itens na pilha.");

            debug.Items.Add("--> Linguagem aceita <---");
            
            if (debug.Items.Count > 0)
                debug.SelectedIndex = debug.Items.Count - 1;
        }

        /// <summary>
        /// Desempilha o número de itens nas pilhas, relativo à redução contida na ação.
        /// </summary>
        /// <param name="action">A ação contendo a informação de redução.</param>
        private static void Desempilha(ActionClass action)
        {
            if (pilha.Count < action.Reducao.From.Length)
                throw new Exception($"A redução é maior que a quantidade de itens na pilha!{Environment.NewLine}Redução: {action.Reducao.From.Length} itens ({action.Reducao}){Environment.NewLine}Pilha: {pilha.Count} itens");

            for (int i = 0; i < action.Reducao.From.Length; i++)
            {
                //pilha.Pop();
                debug.Items.Add($"Desempilha Pilha: {pilha.Pop()}");
            }

            for (int i = 0; i < action.Reducao.From.Length; i++)
            {
                //simbolos.Pop();
                debug.Items.Add($"Desempilha Simbolo: {simbolos.Pop()}");
            }
        }
        
        private static void ImprimePilha()
        {
            string p = String.Empty;
            foreach (var item in pilha)
                p = $"{item} {p}";

            debug.Items.Add($"Pilha: {p.Trim()}");
        }

        private static void ImprimeSimbolos()
        {
            string p = String.Empty;
            foreach (var item in simbolos)
                p = $"{item} {p}";

            debug.Items.Add($"Simbolos: {p.Trim()}");
        }

    }
}
