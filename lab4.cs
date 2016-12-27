using System;
using System.Threading;

namespace ConsoleApplication1
{
    public class Token
    {
        public string Data { get; set; }
        public int Recipient { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var token = new Token();
            token.Data = "I will show only to chosen one";
            token.Recipient = 24;

            Thread thread = new Thread(delegate () { Func(1, token); });
            thread.Start();
        }

        static void Func(int id, Token token)
        {
            if (id == token.Recipient)
            {
                Console.WriteLine(token.Data + " (" + id.ToString() + ")");
                Console.Read(); //Приостановим основной поток
            }
            else
            {
                Console.WriteLine("Passed further");
                Thread thread = new Thread(delegate () { Func(id + 1, token); });
                thread.Start();
            }
        }
    }
}
