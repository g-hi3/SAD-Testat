using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Database
{
    public class RentalContract : IDataObj
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int CarId { get; set; }
        public int Days { get; set; }
        public decimal TotalCosts { get; set; }
        public int ReservationId { get; set; }
    }
}
