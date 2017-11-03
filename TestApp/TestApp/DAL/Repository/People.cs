using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApp.Model;
using TestApp.PeopleRepositories.Interface;

namespace TestApp.PeopleRepositories.People
{
    public class DataAccessRepository : IDataAccess<Person, int>
    {
        TestAPIContext ctx;
        public DataAccessRepository(TestAPIContext c)
        {
            ctx = c;
        }

        public async Task<List<Person>> GetPersonList()
        {
            try
            {
                var persons = await ctx.People.Include(x=>x.Identities).Where(x => x.isDeleted == false).ToListAsync();
                return persons;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public async Task<List<Person>> GetPersonListByFirstOrLastName(string FName, string LName)
        {
            try
            {
                if (FName.Trim().ToString() == "" && FName.Trim().ToString() == "")
                {
                    var persons = await ctx.People.Include(x => x.Identities).Where(x => x.isDeleted == false).ToListAsync();
                    return persons;
                }
                else if (FName.Trim().ToString() == "")
                {
                    var persons = await ctx.People.Include(x => x.Identities).Where(x => x.LastName == LName && x.isDeleted == false).ToListAsync();
                    return persons;
                }
                else if(LName.Trim().ToString() == "")
                {
                    var persons = await ctx.People.Include(x => x.Identities).Where(x => x.FirstName == FName && x.isDeleted == false).ToListAsync();
                    return persons;
                } else
                {
                    var persons = await ctx.People.Include(x => x.Identities).Where(x => x.FirstName == FName &&  x.LastName == LName && x.isDeleted == false).ToListAsync();
                    return persons;
                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<List<Person>> GetPersonListBySpecificIdentity(int specification)
        {
            try
            {

                var identities = await ctx.Identifiers.Distinct().Where(x => x.Type == specification).ToListAsync();
                List<Person> personList = new List<Person>();
                if (identities != null)
                {
                    foreach (var identity in identities)
                    {
                        Person person = new Person();
                        person = await ctx.People.Include(x => x.Identities).Where(x => x.id == identity.Personid).FirstOrDefaultAsync();
                        personList.Add(person);
                    }
                }

                return personList;


            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> CreatePersonWithIdetifiers(Person person)
        {
            try
            {
                ctx.People.Add(person);
                int i = await ctx.SaveChangesAsync();
                if (i > 0)
                    return true;
                else
                    return false;
                //    int res = ctx.SaveChanges();
                //    return res;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> CreatePersonWithOutIdetifiers(Person person)
        {
            try
            {
                if (person != null)
                {
                    person.Identities = null;
                    ctx.People.Add(person);
                    int i = await ctx.SaveChangesAsync();
                    if (i > 0)
                        return true;
                    else
                        return false;
                }
                else
                {
                    return false;
                }


                //    int res = ctx.SaveChanges();
                //    return res;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> AddIdentifierToPerson(Guid id, Identifier identifierObj)
        {
            try
            {
                Person person = await ctx.People.Where(x => x.id == id && x.isDeleted == false).FirstOrDefaultAsync();
                if (person != null)
                {
                    identifierObj.Personid = person.id;
                    ctx.Identifiers.Add(identifierObj);
                    int i = await ctx.SaveChangesAsync();
                    if (i > 0)
                        return true;
                    else
                        return false;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteIdentifierToPerson(Guid pid, Guid IdenId)
        {
            try
            {
                Person person = new Person();
                person = await ctx.People.Where(x => x.id == pid && x.isDeleted == false).FirstOrDefaultAsync();
                if (person != null)
                {
                    Identifier identifier = await ctx.Identifiers.Where(x => x.id == IdenId && x.Personid == pid).FirstOrDefaultAsync();
                    ctx.Identifiers.Remove(identifier);
                    int i = await ctx.SaveChangesAsync();
                    if (i > 0)
                        return true;
                    else
                        return false;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> UpdatePersonWithoutIdentifier(Person person)
        {
            try
            {

                Person personObj = await ctx.People.Where(x => x.id == person.id && x.isDeleted == false).FirstOrDefaultAsync();
                if (person != null)
                {
                    personObj.FirstName = person.FirstName;
                    personObj.LastName = person.LastName;
                    int i = await ctx.SaveChangesAsync();
                    if (i > 0)
                        return true;
                    else
                        return false;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeletePersonVirtually(Guid id)
        {
            try
            {
                Person personObj = await ctx.People.Where(x => x.id == id && x.isDeleted == false).FirstOrDefaultAsync();
                if (personObj != null)
                {
                    personObj.isDeleted = true;
                    int i = await ctx.SaveChangesAsync();
                    if (i > 0)
                        return true;
                    else
                        return false;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
