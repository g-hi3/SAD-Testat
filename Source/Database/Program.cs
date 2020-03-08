using System;

namespace Database
{
    class Program
    {
        static void Main(string[] args)
        {
            Demoinitializer demoinitializer = new Demoinitializer();
            demoinitializer.TestDB();
            Console.WriteLine("Daten wurden eingefügt");


            using (var db = new CarRentDbContext())
            {
                foreach (var customer in db.Customers)
                {
                    Console.WriteLine($"{customer.Id} {customer.FirstName} {customer.LastName}");
                }

            }

         

            Console.ReadKey();
        }
    }
}
