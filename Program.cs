using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabuleiro;
using Xadrez;

namespace ProjetoXadrezConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                PartidaXadrez Partida = new PartidaXadrez();
                while (!Partida.Terminada)
                {
                    Console.Clear();
                   
                    Tela.ImprimirPartida(Partida);
                    Console.WriteLine();
                    Console.Write("Origem: ");
                    Posicao origem = Tela.LerPosicaoXadrez().ToPosicao();
                    Partida.ValidarPosicaoOrigem(origem);

                    bool[,] PosicaoPossiveis = Partida.tab.peca(origem).MovimentosPossiveis();
                    Console.Clear();
                    Tela.ImprimirTabuleiro(Partida.tab,PosicaoPossiveis);

                    Console.WriteLine();
                    Console.Write("Destino: ");
                    Posicao destino = Tela.LerPosicaoXadrez().ToPosicao();
                    Partida.ValidarPosicaoDestino(origem,destino);

                    Partida.RealizaJogada(origem, destino);
                }
                Console.Clear();

                Tela.ImprimirPartida(Partida);
            }
            catch (TabuleiroExeptions ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
           
        }
    }
}
