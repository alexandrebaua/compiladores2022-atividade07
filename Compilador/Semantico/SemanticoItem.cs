using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Compilador.Lexico;
using Compilador.Sintatico.Ascendente.SLR;

namespace Compilador.Semantico
{
    /// <summary>
    /// Classe auxiliar semântica para armazenar o escopo e grupo à que um token pertence.
    /// </summary>
    public class SemanticoItem
    {
        /// <summary>
        /// O Construtor da classe.
        /// </summary>
        /// <param name="escopo">O escopo à qual o token pertence.</param>
        /// <param name="grupo">O grupo à que o token pertence.</param>
        /// <param name="token">O token à ser armazenado.</param>
        /// <param name="reduceType">O tipo de redução encontrado durante a análise sintática.</param>
        public SemanticoItem(int escopo, int grupo, TokenClass token, ReduceTypeEnum reduceType = ReduceTypeEnum.None)
        {
            this.Escopo = escopo;
            this.Grupo = grupo;
            this.Token = token;
            this.ReduceType = reduceType;
        }

        /// <summary>
        /// Obtém o token armazenado.
        /// </summary>
        public TokenClass Token { get; }

        /// <summary>
        /// Obtém ou define o escopo do token armazenado.
        /// </summary>
        public int Escopo { get; set; }

        /// <summary>
        /// Obtém ou define o grupo do token armazenado.
        /// </summary>
        public int Grupo { get; set; }

        /// <summary>
        /// Obtém ou define o tipo de redução encontrado durante a análise sintática.
        /// </summary>
        public ReduceTypeEnum ReduceType { get; set; }

        public override string ToString()
        {
            if (this.ReduceType != ReduceTypeEnum.None)
                return $"({this.ReduceType}) Escopo: {this.Escopo}, Grupo: {this.Grupo}, Token - {this.Token}";

            return $"Escopo: {this.Escopo}, Grupo: {this.Grupo}, Token - {this.Token}";
        }
    }
}
