using System;

namespace PaymentGateway
{
    public class Shopper
    {
        public Guid Id { get; set; }
    }

    public class Merchant
    {
        public Guid Id { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
