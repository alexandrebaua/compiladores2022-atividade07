using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador.Semantico.Tipos
{
    /// <summary>
    /// Classe para armazenar a informação de uma variável.
    /// </summary>
    public class Variável
    {
        /// <summary>
        /// O construtor da classe.
        /// </summary>
        /// <param name="identificador">O identificador (lexema) da variável.</param>
        /// <param name="tipo">O tipo da variável.</param>
        /// <param name="valorAtribuido">O marcador de atribuição da variável (opcional).</param>
        public Variável(string identificador, string tipo, bool valorAtribuido = false)
        {
            this.Identificador = identificador;
            this.Tipo = tipo;
            this.ValorAtribuido = valorAtribuido;
        }

        /// <summary>
        /// Obtém ou define o identificador (lexema) da variável.
        /// </summary>
        public string Identificador { get; set; }

        /// <summary>
        /// Obtém ou define o tipo da variável
        /// </summary>
        public string Tipo { get; set; }

        /// <summary>
        /// Obtém ou define o marcador de atribuição da variável (opcional).
        /// </summary>
        public bool ValorAtribuido { get; set; }

        public override string ToString()
        {
            return $"Variável: '{this.Identificador}' : {this.Tipo}{(this.ValorAtribuido ? " : >Atribuido<" : "")}";
        }
    }
}
