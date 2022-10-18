using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador.Sintatico.Ascendente.SLR
{
    /// <summary>
    /// Enumerador contendo os tipos de reduções executadas pelo analisador sintático.
    /// </summary>
    public enum ReduceTypeEnum : byte
    {
        /// <summary>
        /// Sem tipo de redução.
        /// </summary>
        None = 0x00,

        /// <summary>
        /// Redução principal do programa.
        /// </summary>
        Programa = 0x01,

        /// <summary>
        /// Redução da declaração de variáveis.
        /// </summary>
        Declaração = 0x02,

        /// <summary>
        /// Redução da operação de atribuição.
        /// </summary>
        Atribuição = 0x03,

        /// <summary>
        /// Redução da operação de imprime.
        /// </summary>
        Imprime = 0x04,

        /// <summary>
        /// Redução da operação de seleção condicional If.
        /// </summary>
        SeleçãoIf = 0x05,

        /// <summary>
        /// Redução da operação do laço de repeticção 'Enquanto'.
        /// </summary>
        Enquanto = 0x06
    }

    /// <summary>
    /// Classe de armazenamento de uma redução do analisador sintático SLR.
    /// </summary>
    public class ReducaoClass
    {
        /// <summary>
        /// Construtor da classe.
        /// </summary>
        /// <param name="id">Identificação da redução.</param>
        /// <param name="to">Simbolo não terminal da redução.</param>
        /// <param name="from">Vetor com os simbolos a serem reduzidos (desempilhados).</param>
        /// <param name="reduceType">O tipo de redução encontrado durante a analise sintática.</param>
        public ReducaoClass(int id, string to, string[] from, ReduceTypeEnum reduceType = ReduceTypeEnum.None)
        {
            this.ID = id;
            this.To = to;
            this.From = from;
            this.ReduceType = reduceType;
        }

        /// <summary>
        /// Obtém ou define o código de identificação da redução.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Obtém ou define o simbolo não terminal da redução.
        /// </summary>
        public string[] From { get; set; }

        /// <summary>
        /// Obtém ou define o vetor com os simbolos a serem reduzidos (desempilhados).
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// Obtém ou define o tipo de redução encontrado durante a analise sintática.
        /// </summary>
        public ReduceTypeEnum ReduceType { get; set; }

        /// <summary>
        /// Retorna um texto contendo as informações do token armazenado.
        /// </summary>
        public override string ToString()
        {
            string from = String.Empty;
            foreach (var item in this.From)
                from += $"{item} ";

            if (String.IsNullOrEmpty(from))
                from = "ε";

            return $"r{this.ID} = {this.To} --> {from.Trim()}";
        }
    }
}
