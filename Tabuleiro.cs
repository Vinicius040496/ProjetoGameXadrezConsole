using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using tabuleiro;

namespace tabuleiro
{
    internal class Tabuleiro
    {
        public int Linhas { get; set; }
        public int Colunas { get; set; }
        private Peca[,] pecas;

        public Tabuleiro()
        {

        }

        public Tabuleiro(int linhas, int colunas)
        {
            Linhas = linhas;
            Colunas = colunas;
            pecas = new Peca[linhas, colunas];
        }
        public Peca peca (int linhas, int colunas)
        {
            return pecas[linhas, colunas];
        }
        public Peca peca (Posicao pos)
        {
            return pecas[pos.Linha,pos.Coluna];
        }
        public bool ExistePeca(Posicao pos)
        {
            ValidarPosicao(pos);
            return peca(pos) != null;
        }
        public void ColocarPeca(Peca P, Posicao pos)
        {
            if (ExistePeca(pos))
            {
                throw new TabuleiroExeptions("Já existe uma pecça nessa posição!");
            }
            pecas[pos.Linha,pos.Coluna] = P;
            P.Posicao = pos;
        }
        public Peca RetirarPeca(Posicao pos)
        {
            if (peca (pos) == null)
            {
                return null;
            }
            Peca aux = peca (pos);
            aux.Posicao = null;
            pecas[pos.Linha,pos.Coluna] = null;
            return aux;
        }
        public bool PosicaoValida(Posicao pos)
        {
            if (pos.Linha <0 || pos.Linha >=Linhas || pos.Coluna<0 || pos.Coluna > Colunas)
            {
                return false;
            }else
            {
                return true;
            }
           
        }
        public void ValidarPosicao(Posicao pos)
        {
            if (!PosicaoValida(pos))
            {
                throw new TabuleiroExeptions("Posição invalida!");
            }
        }
    }
}
