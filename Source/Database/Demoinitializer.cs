using System;
using System.Collections.Generic;
using System.Text;

namespace Database
{
    public class Demoinitializer
    {
       
        public void TestDB()
        {
            using (var db = new CarRentDbContext()) 
            {
                var customerA = new Customer
                {
                    FirstName = "Peter",
                    LastName = "Müller",
                    Address = "Mordorweg 4, 8355 Gondor"
                };


                var customerB = new Customer
                {
                    FirstName = "Maria",
                    LastName = "Geier",
                    Address = "Rohangasse 23, 5564 Auenland",
                };

                db.Customers.Add(customerA);
                db.Customers.Add(customerB);

                db.SaveChanges();

                var brandA = new CarBrand
                {
                    Title = "Mercedes"
                };


                var brandB = new CarBrand
                {
                    Title = "BMW"
                };

                var brandC = new CarBrand
                {
                    Title = "VW"
                };

                db.CarBrands.Add(brandA);
                db.CarBrands.Add(brandB);
                db.CarBrands.Add(brandC);

                db.SaveChanges();

                var classA = new CarClass
                {
                    Title = "Cheap",
                    Cost = 98.50M
                };

                var classB = new CarClass
                {
                    Title = "Midlevel",
                    Cost = 190.00M
                };

                var classC = new CarClass
                {
                    Title = "Highend",
                    Cost = 299.90M
                };

                db.CarClasses.Add(classA);
                db.CarClasses.Add(classB);
                db.CarClasses.Add(classC);

                db.SaveChanges();

                var typeA = new CarType
                {
                    Title = "PKW"
                };

                var typeB = new CarType
                {
                    Title = "Limousine"
                };

                var typeC = new CarType
                {
                    Title = "Coupé"
                };

                db.CarTypes.Add(typeA);
                db.CarTypes.Add(typeB);
                db.CarTypes.Add(typeC);

                db.SaveChanges();

                var carA = new Car
                {
                    BrandId = brandA.Id,
                    ClassId = classA.Id,
                    TypeId = typeA.Id
                };

                var carB = new Car
                {
                    BrandId = brandB.Id,
                    ClassId = classB.Id,
                    TypeId = typeB.Id
                };

                var carC = new Car
                {
                    BrandId = brandC.Id,
                    ClassId = classC.Id,
                    TypeId = typeC.Id
                };

                db.Cars.Add(carA);
                db.Cars.Add(carB);
                db.Cars.Add(carC);

                db.SaveChanges();

                var reservationA = new Reservation
                {
                    CarId = carA.Id,
                    CustomerId = customerA.Id,
                    Days = 10,
                    RentalDate = DateTime.Now.AddDays(10),
                    ReservationDate = DateTime.Now,
                    State = ReservationState.Pending
                };

                db.Reservations.Add(reservationA);

                db.SaveChanges();

                var contractA = new RentalContract
                {
                    CarId = carA.Id,
                    CustomerId = customerA.Id,
                    Days = 10,
                    ReservationId = reservationA.Id,
                    TotalCosts = classC.Cost * 10
                };

                db.RentalContracts.Add(contractA);

                db.SaveChanges();
            }

        }


    }

}
