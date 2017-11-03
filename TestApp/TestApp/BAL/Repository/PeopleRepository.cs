using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApp.BAL.Interface;
using TestApp.Model;
using TestApp.PeopleRepositories.Interface;

namespace TestApp.BAL
{
    
    public class PeopleRepository: IPeopleRepository
    {
        
        private IDataAccess<Person, int> dataAccess;

        public PeopleRepository(IDataAccess<Person, int> dataAccessObj)
        {
            dataAccess = dataAccessObj;
        }
        public async Task<List<Person>> GetPersonList()
        {
            return await dataAccess.GetPersonList();
        }
        public async Task<List<Person>> GetPersonListByFirstOrLastName(string FName, string LName)
        {
            return await dataAccess.GetPersonListByFirstOrLastName(FName, LName);
        }

        public async Task<List<Person>> GetPersonListBySpecificIdentity(int specification)
        {
            return await dataAccess.GetPersonListBySpecificIdentity(specification);
        }

        public async Task<bool> CreatePersonWithIdetifiers(Person person)
        {
            return await dataAccess.CreatePersonWithIdetifiers(person);
        }
        public async Task<bool> CreatePersonWithOutIdetifiers(Person person)
        {
            return await dataAccess.CreatePersonWithOutIdetifiers(person);
        }
        public async Task<bool> AddIdentifierToPerson(Guid id, Identifier identifierObj)
        {
            return await dataAccess.AddIdentifierToPerson(id, identifierObj);
        }
        public async Task<bool> DeleteIdentifierToPerson(Guid pid, Guid IdenId)
        {
            return await dataAccess.DeleteIdentifierToPerson(pid, IdenId);
        }
        public async Task<bool> UpdatePersonWithoutIdentifier(Person person)
        {
            return await dataAccess.UpdatePersonWithoutIdentifier(person);
        }
        public async Task<bool> DeletePersonVirtually(Guid id)
        {
            return await dataAccess.DeletePersonVirtually(id);
        }

    }
}
