using System;

namespace Reggie.Upward.Blockchain.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Hello Blockchain!");

            var chain = new BlockChain();
            var server = new WebServer(chain);

            System.Console.ReadLine();
        }
    }
}
