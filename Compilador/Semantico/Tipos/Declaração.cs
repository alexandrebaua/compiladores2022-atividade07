using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Compilador.Lexico;

namespace Compilador.Semantico.Tipos
{
    /// <summary>
    /// Classe para armazenar uma declaração de variável.
    /// </summary>
    public class Declaração : ITipo
    {
        /// <summary>
        /// O construtor da classe.
        /// </summary>
        /// <param name="elementos">Os elementos semânticos que constituem o comando.</param>
        public Declaração(List<SemanticoItem> elementos)
        {
            if (elementos == null)
                throw new ArgumentNullException(nameof(elementos));
            if (elementos.Count != 3)
                throw new Exception("Um tipo 'declaração' deve receber 3 elementos!");

            this.Elementos = elementos;

            this.TipoDeDados = elementos.First().Token;
            this.Identificador = elementos[1].Token;
        }

        /// <summary>
        /// Os elementos semânticos que constituem o comando.
        /// </summary>
        public List<SemanticoItem> Elementos { get; set; }

        /// <summary>
        /// Obtém ou define o tipo de dados da variável.
        /// </summary>
        public TokenClass TipoDeDados { get; set; }

        /// <summary>
        /// Obtém ou define o identificador da declaração.
        /// </summary>
        public TokenClass Identificador { get; set; }

        /// <summary>
        /// Obtém o resultado da conversão do comando para a linguagem C#.
        /// </summary>
        /// <returns>O código resultante.</returns>
        public string ObterCódigo()
        {
            return $"{Environment.NewLine}{(this.TipoDeDados.Token.Equals("INT") ? "int" : "double")} {this.Identificador.Lexema};";
        }

        public override string ToString()
        {
            return $"Declaração: '{this.Identificador.Lexema}' : {this.TipoDeDados.Token}";
        }
    }
}
