using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApp.Model;

namespace TestApp.BAL
{
    
    public class BALClass
    {
        private TestAPIContext _testAPIContextOBj;     
        
        public BALClass(TestAPIContext context)
        {
            _testAPIContextOBj = context;
        }

        public List<Person> GetPersonList()
        {
            try
            {
                List<Person> personList = new List<Person>();
                personList = _testAPIContextOBj.Persons.ToList();
                return personList;
            }
            catch(Exception ex)
            {
                return null;
            }
            
        }
    }
}
