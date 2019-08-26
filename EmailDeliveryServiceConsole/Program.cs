using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmailDelivery;

namespace EmailDeliveryServiceConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            using (EmailDeliveryService eds = new EmailDeliveryService())
            {
                Console.WriteLine("EmailDeliveryService!");
                System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);
            }

            Console.ReadKey();
        }
    }
}
