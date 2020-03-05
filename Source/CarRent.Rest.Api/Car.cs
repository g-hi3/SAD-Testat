namespace CarRent.Rest.Api
{
    public class CarClass
    {
        public static readonly CarClass FirstClass = new CarClass {DayFare = 3.5m};
        public static readonly CarClass BusinessClass = new CarClass {DayFare = 2.3m};
        public static readonly CarClass EconomyClass = new CarClass {DayFare = 1.3m};
        
        public decimal DayFare { get; private set; }
    }

    public class CarType
    {
        
    }
    
    public interface ICar
    {
        CarClass CarClass { get; set; }
        string Brand { get; set; }
        CarType CarType { get; set; }
        string Identification { get; set; }
    }
    
    public class Car : ICar
    {
        public CarClass CarClass { get; set; }
        public string Brand { get; set; }
        public CarType CarType { get; set; }
        public string Identification { get; set; }
    }

    public class CarController
    {
        // At this point I realized, this will probably be replaced by the Entity Framework anyway ...
    }
}