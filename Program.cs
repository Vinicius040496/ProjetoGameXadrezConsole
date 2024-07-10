using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabuleiro;

namespace ProjetoXadrezConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Posicao P = new Posicao(3,4);
            Console.WriteLine("Posição: " + P);
            Console.ReadLine();
        }
    }
}
