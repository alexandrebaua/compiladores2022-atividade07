using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Compilador.Lexico;

namespace Compilador.Semantico.Tipos
{
    /// <summary>
    /// Classe para armazenar um atributo da função.
    /// </summary>
    public class AtributoFunção : ITipo
    {
        /// <summary>
        /// O construtor da classe.
        /// </summary>
        /// <param name="elementos">Os elementos semânticos que constituem o comando.</param>
        /// <param name="identificador">O identificador da declaração.</param>
        public AtributoFunção(SemanticoItem tipo, SemanticoItem identificador)
        {
            this.Elementos = new List<SemanticoItem>();
            this.Elementos.Add(tipo);
            this.Elementos.Add(identificador);

            this.TipoDeDados = tipo.Token;
            this.Identificador = identificador.Token;
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
            return $"{(this.TipoDeDados.Token.Equals("INT") ? "int" : "double")} {this.Identificador.Lexema}";
        }

        public override string ToString()
        {
            return $"Atr. Função: '{this.Identificador.Lexema}' : {this.TipoDeDados.Token}";
        }
    }
}
