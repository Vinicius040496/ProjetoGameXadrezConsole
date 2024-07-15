using ProjetoXadrezConsole.Xadrez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using tabuleiro;
using Xadrez;

namespace ProjetoXadrezConsole
{
    internal class Tela
    {
        public static void ImprimirPartida(PartidaXadrez partida)
        {
            Tela.ImprimirTabuleiro(partida.tab);
            Console.WriteLine();
            ImprimirPecaCapturada(partida);
            Console.WriteLine();
            Console.WriteLine("Turno: " + partida.Turno);
            if (!partida.Terminada)
            {
                Console.WriteLine("Aguardando Jogada: " + partida.JogadorAtual);
                if (partida.Xeque)
                {
                    Console.WriteLine("Xeque!!");
                }
                else
                {
                    Console.WriteLine("XequeMate!!");
                    Console.WriteLine("Vencedor: " + partida.JogadorAtual);
                }
            }
        }
        public static void ImprimirPecaCapturada(PartidaXadrez partida)
        {
            Console.WriteLine("Peças capturadas: ");
            Console.Write("Brancas: ");
            ImprimirConjunto(partida.pecasCapturadas(Cor.Branco));
            Console.WriteLine();
            Console.Write("Pretas: ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            ImprimirConjunto(partida.pecasCapturadas(Cor.Preta));
            Console.ForegroundColor = aux;
            Console.WriteLine();
        }
        public static void ImprimirConjunto(HashSet<Peca> conjunto)
        {
            Console.Write("[");
            foreach (Peca x in conjunto)
            {
                Console.Write(x + " ");
            }
            Console.Write("]");
        }
        

        public static void ImprimirTabuleiro(Tabuleiro tab)
        {
            

            for (int i = 0; i < tab.Linhas; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < tab.Colunas; j++)
                {

                    ImprimirPeca(tab.peca(i, j));
                    Console.Write(" ");

                }
                Console.WriteLine();
            }
            Console.WriteLine("  a  b  c  d  e  f  g  h");
        }

        public static void ImprimirTabuleiro(Tabuleiro tab, bool[,] PosicaoPossiveis)
        {
            ConsoleColor FundoOriginal = Console.BackgroundColor;
            ConsoleColor FundoAlterado = ConsoleColor.DarkGray;

            for (int i = 0; i < tab.Linhas; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < tab.Colunas; j++)
                {
                    if (PosicaoPossiveis[i, j])
                    {
                       Console.BackgroundColor = FundoAlterado;
                    }
                    else
                    {
                        Console.BackgroundColor= FundoOriginal;
                    }
                    ImprimirPeca(tab.peca(i, j));
                    Console.BackgroundColor = FundoOriginal;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a  b  c  d  e  f  g  h");
            Console.BackgroundColor = FundoOriginal;
        }
        public static PosicaoXadrez LerPosicaoXadrez()
        {
            string s = Console.ReadLine();
            char coluna = s[0];
            int linha = int.Parse(s[1] + "");
            return new PosicaoXadrez(coluna, linha);
        }
        public static void ImprimirPeca(Peca peca)
        {
            if (peca == null)
            {
                Console.Write("- ");

            }
            else
            {
                if (peca.Cor == Cor.Branco)
                {
                    Console.Write(peca);
                }
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(peca);
                    Console.ForegroundColor = aux;
                }
                Console.Write(" ");
            }
        }
    }
}
