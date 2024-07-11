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
           Tabuleiro tab = new Tabuleiro(8,8);
            tab.ColocarPeca(new Torre(tab,Cor.Preta), new Posicao(0, 0));
            tab.ColocarPeca(new Torre(tab,Cor.Preta), new Posicao(1, 3));
            tab.ColocarPeca(new Rei(tab,Cor.Preta), new Posicao(2, 4));
            Tela.ImprimirTabuleiro(tab);
            Console.ReadLine();
        }
    }
}
