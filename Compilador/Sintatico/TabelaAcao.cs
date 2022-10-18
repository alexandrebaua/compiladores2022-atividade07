using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador.Sintatico
{
    public static class TabelaAcao
    {
        private static Dictionary<string, int>[] acao = null;
        private static Dictionary<int, int> acoesFinais = null;
        private static List<string> finais = null;

        public static Dictionary<string, int>[] Acao
        {
            get
            {
                if (TabelaAcao.acao == null)
                    TabelaAcao.InitTablesAcao();

                return TabelaAcao.acao;
            }
        }

        public static Dictionary<int, int> AcoesFinais
        {
            get
            {
                if (TabelaAcao.acoesFinais == null)
                    TabelaAcao.InitTableFinais();

                return TabelaAcao.acoesFinais;
            }
        }

        public static List<string> Finais
        {
            get
            {
                if (TabelaAcao.finais == null)
                    TabelaAcao.InitListFinais();

                return TabelaAcao.finais;
            }
        }

        private static void InitTablesAcao()
        {
            TabelaAcao.acao = new Dictionary<string, int>[44];
            
            TabelaAcao.acao[0] = new Dictionary<string, int>();
            TabelaAcao.acao[0].Add("FECHA_PAR", 1);

            TabelaAcao.acao[1] = new Dictionary<string, int>();
            TabelaAcao.acao[1].Add("ID", 2);

            TabelaAcao.acao[2] = new Dictionary<string, int>();
            TabelaAcao.acao[2].Add("INT", 3);

            TabelaAcao.acao[3] = new Dictionary<string, int>();
            TabelaAcao.acao[3].Add("ABRE_PAR", 4);
            TabelaAcao.acao[3].Add("VIRGULA", 1);
            
            TabelaAcao.acao[4] = null;

            TabelaAcao.acao[5] = new Dictionary<string, int>();
            TabelaAcao.acao[5].Add("ID", 6);

            TabelaAcao.acao[6] = new Dictionary<string, int>();
            TabelaAcao.acao[6].Add("FUNCTION", 7);

            TabelaAcao.acao[7] = null;

            TabelaAcao.acao[8] = new Dictionary<string, int>();
            TabelaAcao.acao[8].Add("FECHA_BLOCO", 9);
            TabelaAcao.acao[8].Add("PONTO_VIRGULA", 11);
            TabelaAcao.acao[8].Add("FECHA_PAR", 15);

            TabelaAcao.acao[9] = new Dictionary<string, int>();
            TabelaAcao.acao[9].Add("ABRE_BLOCO", 10);

            TabelaAcao.acao[10] = null;

            TabelaAcao.acao[11] = new Dictionary<string, int>();
            TabelaAcao.acao[11].Add("ID", 12);
            TabelaAcao.acao[11].Add("CONST", 12);

            TabelaAcao.acao[12] = new Dictionary<string, int>();
            TabelaAcao.acao[12].Add("MENOS", 11);
            TabelaAcao.acao[12].Add("MAIS", 11);
            TabelaAcao.acao[12].Add("ASTERISTICO", 11);
            TabelaAcao.acao[12].Add("BARRA_ESQUERDA", 11);
            TabelaAcao.acao[12].Add("ATRIBUICAO", 13);
            TabelaAcao.acao[12].Add("INT", 25);

            TabelaAcao.acao[13] = new Dictionary<string, int>();
            TabelaAcao.acao[13].Add("ID", 14);

            TabelaAcao.acao[14] = null;

            TabelaAcao.acao[15] = new Dictionary<string, int>();
            TabelaAcao.acao[15].Add("ID", 16);
            TabelaAcao.acao[15].Add("CONST", 16);

            TabelaAcao.acao[16] = new Dictionary<string, int>();
            TabelaAcao.acao[16].Add("MAIOR", 17);
            TabelaAcao.acao[16].Add("MENOR", 17);
            TabelaAcao.acao[16].Add("IGUALDADE", 17);
            TabelaAcao.acao[16].Add("DIFERENTE", 17);
            TabelaAcao.acao[16].Add("ABRE_PAR", 21);

            TabelaAcao.acao[17] = new Dictionary<string, int>();
            TabelaAcao.acao[17].Add("ID", 18);
            TabelaAcao.acao[17].Add("CONST", 18);

            TabelaAcao.acao[18] = new Dictionary<string, int>();
            TabelaAcao.acao[18].Add("ABRE_PAR", 19);

            TabelaAcao.acao[19] = new Dictionary<string, int>();
            TabelaAcao.acao[19].Add("IF", 20);
            TabelaAcao.acao[19].Add("WHILE", 20);

            TabelaAcao.acao[20] = null;
            
            TabelaAcao.acao[21] = new Dictionary<string, int>();
            TabelaAcao.acao[21].Add("PRINT", 22);
            
            TabelaAcao.acao[22] = null;

            TabelaAcao.acao[23] = new Dictionary<string, int>();
            TabelaAcao.acao[23].Add("PONTO_VIRGULA", 24);

            TabelaAcao.acao[24] = null;
            TabelaAcao.acao[25] = null;

        }

        private static void InitTableFinais()
        {
            TabelaAcao.acoesFinais = new Dictionary<int, int>();
            TabelaAcao.acoesFinais.Add(4, 8);
            TabelaAcao.acoesFinais.Add(7, 0);
            TabelaAcao.acoesFinais.Add(10, 8);
            TabelaAcao.acoesFinais.Add(14, 8);
            TabelaAcao.acoesFinais.Add(20, 8);
            TabelaAcao.acoesFinais.Add(22, 23);
            TabelaAcao.acoesFinais.Add(24, 8);
            TabelaAcao.acoesFinais.Add(25, 8);
        }

        private static void InitListFinais()
        {
            TabelaAcao.finais = new List<string>();
            TabelaAcao.finais.Add("FECHA_PAR");
            TabelaAcao.finais.Add("FECHA_BLOCO");
            //TabelaAcao.finais.Add("VIRGULA");
            TabelaAcao.finais.Add("PONTO_VIRGULA");
        }
    }
}
