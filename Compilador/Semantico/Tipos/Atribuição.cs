using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Compilador.Lexico;

namespace Compilador.Semantico.Tipos
{
    /// <summary>
    /// Classe para armazenar um comando de atribuição.
    /// </summary>
    public class Atribuição : ITipo
    {
        /// <summary>
        /// O construtor da classe.
        /// </summary>
        /// <param name="elementos">Os elementos semânticos que constituem o comando.</param>
        public Atribuição(List<SemanticoItem> elementos)
        {
            this.Elementos = elementos;

            if (elementos == null)
                throw new ArgumentNullException(nameof(elementos));
            if (elementos.Count < 4)
                throw new Exception("Um tipo 'atribuição' deve receber no mínimo 4 elementos!");

            this.Elementos = elementos;

            // O primeiro elemento é a variável que irá receber a atribuição.
            this.Variável = elementos[0].Token;

            // Armazena os elementos relacionados à expressão:
            this.Expressão = new TokenClass[elementos.Count - 3];
            for (int i = 0; i < this.Expressão.Length; i++)
                this.Expressão[i] = elementos[i + 2].Token;
        }

        /// <summary>
        /// Os elementos semânticos que constituem o comando.
        /// </summary>
        public List<SemanticoItem> Elementos { get; set; }

        /// <summary>
        /// Obtém ou define a variável que irá receber a atribuição.
        /// </summary>
        public TokenClass Variável { get; set; }

        /// <summary>
        /// Obtém ou define a expressão da atribuição.
        /// </summary>
        public TokenClass[] Expressão { get; set; }

        /// <summary>
        /// Obtém o resultado da conversão do comando para a linguagem C#.
        /// </summary>
        /// <returns>O código resultante.</returns>
        public string ObterCódigo()
        {
            string code = String.Empty;
            foreach (var item in this.Expressão)
                code += $"{item.Lexema} ";

            code = code.Trim();

            return $"{Environment.NewLine}{this.Variável.Lexema} = {code};";
        }

        public override string ToString()
        {
            string tmp = String.Empty;
            foreach (var item in this.Expressão)
                tmp += $"{item.Lexema} ";

            return $"Atribuição: '{this.Variável.Lexema} = {tmp}'";
        }
    }
}
