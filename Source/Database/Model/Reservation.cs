using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Database
{
    public class Reservation : IDataObj
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public int CustomerId { get; set; }
        public int Days { get; set; }
        public ReservationState State { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime ReservationDate { get; set; }
    }
}
