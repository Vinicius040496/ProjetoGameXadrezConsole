using ProjetoXadrezConsole.Xadrez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabuleiro;
using xadrez;

namespace Xadrez
{
    internal class PartidaXadrez
    {
        public Tabuleiro tab { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool Terminada { get; private set; }
        private HashSet<Peca> pecas;
        private HashSet<Peca> Capturadas;
        public bool Xeque { get; private set; }

        public PartidaXadrez()
        {
            tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branco;
            Terminada = false;
            Xeque = false;
            pecas = new HashSet<Peca>();
            Capturadas = new HashSet<Peca>();
            ColocarPecas();
        }
        public Peca ExecutaMovimento(Posicao Origem, Posicao Destino)
        {
            Peca P = tab.RetirarPeca(Origem);
            P.IncrementarMovimento();
            Peca pecaCapturada = tab.RetirarPeca(Destino);
            tab.ColocarPeca(P, Destino);
            if (pecaCapturada != null)
            {
                Capturadas.Add(pecaCapturada);
            }
            // #jogadaespecial roque pequeno
            if (P is Rei && Destino.Coluna == Origem.Coluna + 2)
            {
                Posicao origemT = new Posicao(Origem.Linha, Origem.Coluna + 3);
                Posicao destinoT = new Posicao(Origem.Linha, Origem.Coluna + 1);
                Peca T = tab.RetirarPeca(origemT);
                T.IncrementarMovimento();
                tab.ColocarPeca(T, destinoT);
            }

            // #jogadaespecial roque grande
            if (P is Rei && Destino.Coluna == Origem.Coluna - 2)
            {
                Posicao origemT = new Posicao(Origem.Linha, Origem.Coluna - 4);
                Posicao destinoT = new Posicao(Origem.Linha, Origem.Coluna - 1);
                Peca T = tab.RetirarPeca(origemT);
                T.IncrementarMovimento();
                tab.ColocarPeca(T, destinoT);
            }

            return pecaCapturada;
        }

        public void DesfazMovimento(Posicao Origem, Posicao Destino, Peca pecaCapturada)
        {
            Peca P = tab.RetirarPeca(Destino);
            P.DecrementarMovimento();
            P.DecrementarMovimento();
            if (pecaCapturada != null)
            {
                tab.ColocarPeca(pecaCapturada, Destino);
                Capturadas.Remove(pecaCapturada);
            }
            tab.ColocarPeca(P, Origem);
        }
        public void RealizaJogada(Posicao origem, Posicao Destino)
        {
            Peca PecaCapturada = ExecutaMovimento(origem, Destino);

            if (EstaEmCheque(JogadorAtual))
            {
                DesfazMovimento(origem, Destino, PecaCapturada);
                throw new TabuleiroExeptions("Você não pode se colocar em xeque!");
            }
            if (EstaEmCheque(CorAdversaria(JogadorAtual)))
            {
                Xeque = true;
            }
            else
            {
                Xeque = false;
            }
            if (TesteXequeMate(JogadorAtual))
            {
                Terminada = true;
            }
            else
            {
                Turno++;
                MudaJogador();
            }
        }
        public void ValidarPosicaoOrigem(Posicao pos)
        {
            if (tab.peca(pos) == null)
            {
                throw new TabuleiroExeptions("Não existe peça na posição de origem escolhida!");
            }
            if (JogadorAtual != tab.peca(pos).Cor)
            {
                throw new TabuleiroExeptions("A peça de origem escolhida não é sua!");
            }
            if (!tab.peca(pos).ExisteMovimentosPossiveis())
            {
                throw new TabuleiroExeptions("Não há movimentos possiveis para a peça de origigem escolhida!");
            }
        }
        public void ValidarPosicaoDestino(Posicao origem, Posicao destino)
        {
            if (!tab.peca(origem).MovimentoPossivel(destino))
            {
                throw new TabuleiroExeptions("Posição de destino invalida!");
            }
        }
        private void MudaJogador()
        {
            if (JogadorAtual == Cor.Branco)
            {
                JogadorAtual = Cor.Preta;
            }
            else
            {
                JogadorAtual = Cor.Branco;
            }
        }
        public HashSet<Peca> pecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in Capturadas)
            {
                if (x.Cor == cor)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }
        public HashSet<Peca> pecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in pecas)
            {
                if (x.Cor == cor)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(pecasCapturadas(cor));
            return aux;
        }
        private Cor CorAdversaria(Cor cor)
        {
            if (cor == Cor.Branco)
            {
                return Cor.Preta;
            }
            else
            {
                return Cor.Branco;
            }
        }
        private Peca Rei(Cor cor)
        {
            foreach (Peca x in pecasEmJogo(cor))
            {
                if (x is Rei)
                {
                    return x;
                }
            }
            return null;
        }
        public bool EstaEmCheque(Cor cor)
        {
            Peca R = Rei(cor);
            if (R == null)
            {
                throw new TabuleiroExeptions("Não ha rei da cor " + cor + " No tabuleiro!");
            }
            {

            }
            foreach (Peca x in pecasEmJogo(CorAdversaria(cor)))
            {
                bool[,] mat = x.MovimentosPossiveis();
                if (mat[R.Posicao.Linha, R.Posicao.Coluna])
                {
                    return true;
                }
            }
            return false;
        }
        public bool TesteXequeMate(Cor cor)
        {
            if (!EstaEmCheque(cor))
            {
                return false;
            }
            foreach (Peca x in pecasEmJogo(cor))
            {
                bool[,] mat = x.MovimentosPossiveis();
                for (int i = 0; i < tab.Linhas; i++)
                {
                    for (int j = 0; j < tab.Colunas; j++)
                    {
                        if (mat[i, j])
                        {
                            Posicao origem = x.Posicao;
                            Posicao Destino = new Posicao(i, j);
                            Peca PecaCapturada = ExecutaMovimento(origem, Destino);
                            bool TesteXeque = EstaEmCheque(cor);
                            DesfazMovimento(origem, Destino, PecaCapturada);
                            if (!TesteXeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
            {

            }
        }
        public void ColocarNovaPeca(char coluna, int linha, Peca peca)
        {
            tab.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            pecas.Add(peca);
        }
        private void ColocarPecas()
        {
            ColocarNovaPeca('a', 1, new Torre(tab, Cor.Branco));
            ColocarNovaPeca('b', 1, new Cavalo(tab, Cor.Branco));
            ColocarNovaPeca('c', 1, new Bispo(tab, Cor.Branco));
            ColocarNovaPeca('d', 1, new Dama(tab, Cor.Branco));
            ColocarNovaPeca('e', 1, new Rei(tab, Cor.Branco,this));
            ColocarNovaPeca('f', 1, new Bispo(tab, Cor.Branco));
            ColocarNovaPeca('g', 1, new Cavalo(tab, Cor.Branco));
            ColocarNovaPeca('a', 2, new Peao(tab, Cor.Branco, this));
            ColocarNovaPeca('b', 2, new Peao(tab, Cor.Branco, this));
            ColocarNovaPeca('c', 2, new Peao(tab, Cor.Branco, this));
            ColocarNovaPeca('d', 2, new Peao(tab, Cor.Branco, this));
            ColocarNovaPeca('e', 2, new Peao(tab, Cor.Branco, this));
            ColocarNovaPeca('f', 2, new Peao(tab, Cor.Branco, this));
            ColocarNovaPeca('g', 2, new Peao(tab, Cor.Branco, this));
            ColocarNovaPeca('h', 2, new Peao(tab, Cor.Branco, this));

            ColocarNovaPeca('a', 8, new Torre(tab, Cor.Preta));
            ColocarNovaPeca('b', 8, new Cavalo(tab, Cor.Preta));
            ColocarNovaPeca('c', 8, new Bispo(tab, Cor.Preta));
            ColocarNovaPeca('d', 8, new Dama(tab, Cor.Preta));
            ColocarNovaPeca('e', 8, new Rei(tab, Cor.Preta,this));
            ColocarNovaPeca('f', 8, new Bispo(tab, Cor.Preta));
            ColocarNovaPeca('g', 8, new Cavalo(tab, Cor.Preta));
            ColocarNovaPeca('h', 8, new Torre(tab, Cor.Preta));
            ColocarNovaPeca('a', 7, new Peao(tab, Cor.Preta, this));
            ColocarNovaPeca('b', 7, new Peao(tab, Cor.Preta, this));
            ColocarNovaPeca('c', 7, new Peao(tab, Cor.Preta, this));
            ColocarNovaPeca('d', 7, new Peao(tab, Cor.Preta, this));
            ColocarNovaPeca('e', 7, new Peao(tab, Cor.Preta, this));
            ColocarNovaPeca('f', 7, new Peao(tab, Cor.Preta, this));
            ColocarNovaPeca('g', 7, new Peao(tab, Cor.Preta, this));
            ColocarNovaPeca('h', 7, new Peao(tab, Cor.Preta, this));
        }
    }
}
