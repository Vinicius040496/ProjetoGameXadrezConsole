using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabuleiro;

namespace ProjetoXadrezConsole.Xadrez
{
    internal class PosicaoXadrez
    {
        public char Coluna {  get; set; }
        public int Linha { get; set; }

        public PosicaoXadrez()
        {

        }

        public PosicaoXadrez(char coluna, int linha)
        {
            Coluna = coluna;
            Linha = linha;
        }
        public Posicao ToPosicao()
        {
            return new Posicao(8 - Linha, Coluna - 'a');
        }
        public override string ToString()
        {
            return " " + Coluna + Linha;
        }
    }

}
