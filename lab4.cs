using System;
using System.Threading;

namespace ConsoleApplication1
{
    public class Token
    {
        public string Data { get; set; }
        public int Recipient { get; set; }
    }

    public class CustomThread
    {
        public int Number { get; set; }
        public CustomThread Next { get; set; }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            CustomThread[] threads = new CustomThread[51];
            threads[50] = new CustomThread();
            threads[50].Next = null
            threads[50].Number = 50;
            
            for (int i = 49; i >= 1; i--)
            {
                threads[i] = new CustomThread();
                threads[i].Next = threads[i + 1];
                threads[i].Number = i - 1;
            }

            var token = new Token();
            token.Data = "I will show only to chosen one";
            token.Recipient = 23;

            Thread thread = new Thread(delegate () { Func(threads[1], token); });
            thread.Start();
        }

        static void Func(CustomThread th, Token token)
        {
            if (th.Number == token.Recipient)
            {
                Console.WriteLine(token.Data + " (" + th.Number.ToString() + ")");
                Console.Read(); //Приостановим основной поток
            }
            else
            {
                Console.WriteLine("Passed further");
                Thread thread = new Thread(delegate () { Func(th.Next, token); });
                thread.Start();
            }
        }
    }
}
