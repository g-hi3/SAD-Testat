using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Database
{

        public class Car : IDataObj
        {
            public int Id { get; set; }
            public int BrandId { get; set; }
            public int TypeId { get; set; }
            public int ClassId { get; set; }
        }
    
}
