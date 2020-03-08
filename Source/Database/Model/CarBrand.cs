using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Database
{
    public class CarBrand : IDataObj
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
