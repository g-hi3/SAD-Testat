using System.Collections;
using System.Collections.Generic;
using Database;

namespace CarRent.Web.Server
{
    public class CrudHandler : ICrudHandler
    {
        private CarRentDbContext context;
        
        public CrudHandler(CarRentDbContext context)
        {
            this.context = context;
        }
        
        public void Create(string entityName, IDictionary<string, string> arguments)
        {
            throw new System.NotImplementedException();            
        }

        public IEnumerable Read(string entityName, IDictionary<string, string> arguments)
        {
            throw new System.NotImplementedException();
        }

        public void Update(string entityName, IDictionary<string, string> arguments)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(string entityName, IDictionary<string, string> arguments)
        {
            throw new System.NotImplementedException();
        }
    }
}