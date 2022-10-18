using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Compilador.Lexico;

namespace Compilador.Semantico.Tipos
{
    /// <summary>
    /// Classe para armazenar um comando 'imprime'.
    /// </summary>
    public class Imprime : ITipo
    {
        /// <summary>
        /// O construtor da classe.
        /// </summary>
        /// <param name="elementos">Os elementos semânticos que constituem o comando.</param>
        public Imprime(List<SemanticoItem> elementos)
        {
            if (elementos == null)
                throw new ArgumentNullException(nameof(elementos));
            if (elementos.Count != 5)
                throw new Exception("Um tipo 'imprime' deve receber 5 elementos!");
            
            this.Elementos = elementos;
            this.Argumento = elementos[2].Token;
        }

        /// <summary>
        /// Os elementos semânticos que constituem o comando.
        /// </summary>
        public List<SemanticoItem> Elementos { get; set; }

        /// <summary>
        /// Obtém ou define argumento que será impresso.
        /// </summary>
        public TokenClass Argumento { get; set; }

        /// <summary>
        /// Verifica as variáveis do comando 'imprime'.
        /// </summary>
        /// <param name="listaVariáveisEscopo">A lista de variáveis visível pelo escopo.</param>
        /// <param name="debug">A lista de saída dos resultados da verificação.</param>
        public void VerificarVariáveis(List<Variável> listaVariáveisEscopo, ListBox debug)
        {
            // Verifica se a variável para saida de impressão foi declarada:
            Variável argumento = listaVariáveisEscopo.Find(x => x.Identificador.Equals(this.Argumento.Lexema));
            argumento = listaVariáveisEscopo.Find(x => x.Identificador.Equals(this.Argumento.Lexema));
            if (argumento == null)
                throw new Exception($"A variável '{this.Argumento.Lexema}' foi usada mas não está declarada!");

            if (!argumento.ValorAtribuido)
                throw new Exception($"A variável '{argumento.Identificador}' foi usada mas não possui valor atribuido!");

            debug.Items.Add($"> {argumento}");
        }

        /// <summary>
        /// Obtém o resultado da conversão do comando para a linguagem C#.
        /// </summary>
        /// <returns>O código resultante.</returns>
        public string ObterCódigo()
        {
            return $"{Environment.NewLine}Console.WriteLine({this.Argumento.Lexema});";
        }

        public override string ToString()
        {
            return $"Imprime: '{this.Argumento.Lexema}'";
        }
    }
}
