using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Transacciones_en_un_Cajero_Automático
{
    internal class Program
    {
        static int Balance = 1000;
        static object lockobject = new object();

        static Random Aleatorio = new Random();

        static void Main(string[] args)
        {
            Thread Hilo = new Thread(Obtener);
            Hilo.Start();

            Thread Hilo2 = new Thread(Obtener);
            Hilo2.Start();

            Hilo.Join();
            Hilo2.Join();

            Console.WriteLine($"Este es tu Balance final: {Balance}");
            Console.ReadKey();
        }

        static void Obtener()
        {
            int retiro = Aleatorio.Next(10, 100);

            for (int i = 0; i <= 10; i++)
            {
                lock (lockobject)
                {
                    if (Balance >= retiro)
                    {
                        Balance -= retiro;
                        Console.WriteLine($"Hilo({Thread.CurrentThread.ManagedThreadId}) Retiraste: {retiro}");
                        Console.WriteLine($"Este es tu Balance: {Balance}");
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        Console.WriteLine($"Hilo({Thread.CurrentThread.ManagedThreadId}) No se puede completar el retiro devido a fondos insuficientes");
                    }
                }               
            }
            
        }
    }
}
