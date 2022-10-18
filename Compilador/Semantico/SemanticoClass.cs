using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Compilador.Semantico.Tipos;
using Compilador.Sintatico.Ascendente.SLR;

namespace Compilador.Semantico
{
    /// <summary>
    /// Classe do analisador semântico.
    /// </summary>
    public class SemanticoClass
    {
        private int index;
        private ListBox debug;

        /// <summary>
        /// O construtor da classe.
        /// </summary>
        public SemanticoClass() { }

        /// <summary>
        /// O construtor da classe.
        /// </summary>
        /// <param name="lista">Uma lista de itens semânticos</param>
        public SemanticoClass(List<SemanticoItem> lista)
        {
            this.ListaItens = lista;
        }

        /// <summary>
        /// Obtém ou define a lista de itens semânticos (tokens).
        /// </summary>
        public List<SemanticoItem> ListaItens { get; set; }

        /// <summary>
        /// Obtém ou define o progama (árvore de tipos).
        /// </summary>
        public Programa Programa { get; set; } = null;

        /// <summary>
        /// Executa a geração do programa, utilizando a lista de itens semânticos, e a verificação das variáveis.
        /// </summary>
        /// <param name="debug"></param>
        public void Processar(ListBox debug)
        {
            this.debug = debug;
            this.index = 0;
            
            this.debug.Items.Add("=====> Geração de tipos iniciada <=====");

            List<ITipo> item = this.GerarTipos();

            this.debug.Items.Add("=====> Geração de tipos concluida <=====");

            if (item.Count == 1 && item[0] is Programa)
                this.Programa = (Programa)item.First();

            if (this.Programa == null)
                throw new Exception("O programa está vazio!");

            this.debug.Items.Add("+++++> Verificação de variáveis iniciada <+++++");

            this.Programa.VerificarVariáveis(this.debug);

            this.debug.Items.Add("+++++> Verificação de variáveis concluida <+++++");
        }

        /// <summary>
        /// Executa a geração do programa, utilizando a lista de itens semânticos.
        /// </summary>
        /// <returns></returns>
        private List<ITipo> GerarTipos()
        {
            List<SemanticoItem> elementos = new List<SemanticoItem>();  // Elementos (tokens) do escopo atual.
            int escopo = this.ListaItens[index].Escopo;  // Armazena o escopo e grupo atual:
            int grupo = this.ListaItens[index].Grupo;

            List<ITipo> listaTipos = new List<ITipo>();            // Lista de tipos do escopo atual
            List<ITipo> listaTiposEscopoMaior = new List<ITipo>(); // Lista de tipos de escopos maiores

            // Enquanto não alcançar o último elemento, ou encontrar um elemento de escopo menor:
            while (index < this.ListaItens.Count)
            {
                // Se encontrou um escopo maior que o atual, então:
                if (this.ListaItens[index].Escopo > escopo)
                {
                    // Gera os comandos do escopo seguinte, e continua com a geração do escopo atual:
                    this.debug.Items.Add($"-----> Escopo: {this.ListaItens[index].Escopo}");
                    listaTiposEscopoMaior.AddRange(this.GerarTipos());
                    this.debug.Items.Add($"<----- Escopo: {this.ListaItens[index].Escopo}");
                    continue;
                }
                else if (this.ListaItens[index].Escopo < escopo)  // Senão, se o escopo anterior, então sai do laço:
                {
                    break;
                }

                // Se o grupo do elemento atual é do mesmo do grupo atual, então:
                if (this.ListaItens[index].Grupo == grupo)
                {
                    // Adiciona o elemento à lista.
                    elementos.Add(this.ListaItens[index]);

                    // Se o elemento for marcado com uma redução, então:
                    if (this.ListaItens[index].ReduceType != ReduceTypeEnum.None)
                    {
                        // Cria o comando usando a lista atual de elementos do escopo, e comandos dos escopos seguintes.
                        listaTipos.Add(this.Criar(elementos, listaTiposEscopoMaior));
                        // Limpa as listas de elementos e comandos do escopo:
                        elementos = new List<SemanticoItem>();
                        listaTiposEscopoMaior = new List<ITipo>();
                        this.debug.Items.Add($"> {listaTipos.Last()}");
                    }
                }
                else
                {
                    // Troca o grupo atual, para o grupo do elemento selecionado e continua:
                    grupo = this.ListaItens[index].Grupo;
                    continue;
                }

                // Incrementa o indice do seletor de elementos.
                index++;
            }

            // Retorna a lista de comandos do escopo atual.
            return listaTipos;
        }

        /// <summary>
        /// Cria um comando utilizando a lista de elementos e a lista de comandos dos escopos maiores.
        /// </summary>
        /// <param name="elementos">A lista de elementos do escopo atual.</param>
        /// <param name="listaTiposEscopoMaior">A lista de comandos dos escopos maiores.</param>
        /// <returns>O comando resultande para o escopo atual.</returns>
        private ITipo Criar(List<SemanticoItem> elementos, List<ITipo> listaTiposEscopoMaior)
        {
            switch (elementos.Last().ReduceType)
            {
                case ReduceTypeEnum.None:
                    break;
                case ReduceTypeEnum.Programa:
                    return new Programa(elementos, listaTiposEscopoMaior);

                case ReduceTypeEnum.Declaração:
                    return new Declaração(elementos);

                case ReduceTypeEnum.Atribuição:
                    return new Atribuição(elementos);

                case ReduceTypeEnum.Imprime:
                    return new Imprime(elementos);

                case ReduceTypeEnum.SeleçãoIf:
                    return new SeleçãoIf(elementos, listaTiposEscopoMaior);

                case ReduceTypeEnum.Enquanto:
                    return new Enquanto(elementos, listaTiposEscopoMaior);
            }

            return null;
        }
    }
}
