using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApp.Model;

namespace TestApp.BAL.Interface
{
    public interface IPeopleRepository
    {
        Task<List<Person>> GetPersonList();
        Task<List<Person>> GetPersonListByFirstOrLastName(string FName, string LName);
        Task<List<Person>> GetPersonListBySpecificIdentity(int specification);
        Task<bool> CreatePersonWithIdetifiers(Person person);
        Task<bool> CreatePersonWithOutIdetifiers(Person person);
        Task<bool> AddIdentifierToPerson(Guid id, Identifier identifierObj);
        Task<bool> DeleteIdentifierToPerson(Guid pid, Guid IdenId);
        Task<bool> UpdatePersonWithoutIdentifier(Person person);
        Task<bool> DeletePersonVirtually(Guid id);
    }
}
