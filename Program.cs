using ProjetoXadrezConsole.Xadrez;
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
            PosicaoXadrez pos = new PosicaoXadrez('c', 7);
            Console.WriteLine(pos.ToPosicao());
           
            Console.ReadLine();
        }
    }
}
