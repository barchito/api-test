using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApp.Model;

namespace TestApp.Repositories
{
    
    public interface IDataAccess<TEntity, U> where TEntity : class
    {
        IEnumerable<TEntity> GetPersonList();
        IEnumerable<TEntity> GetPersonListByFirstOrLastName(string FName,string LName);
        IEnumerable<TEntity> GetPersonListBySpecificIdentity(string specification);
        bool CreatePersonWithIdetifiers(Person person);
        bool CreatePersonWithOutIdetifiers(Person person);
        bool AddIdentifierToPerson(Guid id, int Value);
        bool DeleteIdentifierToPerson(Guid pid, Guid IdenId);
        bool UpdatePersonWithoutIdentifier(Person person);
        bool DeletePersonVirtually(Guid id);
        //TEntity GetBook(U id);
        //int AddBook(TEntity b);
        //int UpdateBook(U id, TEntity b);
        //int DeleteBook(U id);
    }

    public class DataAccessRepository : IDataAccess<Person, int>
    {
        TestAPIContext ctx;
        public DataAccessRepository(TestAPIContext c)
        {
            ctx = c;
        }
        //public int AddBook(Book b)
        //{
        //    ctx.Books.Add(b);
        //    int res = ctx.SaveChanges();
        //    return res;
        //}

        //public int DeleteBook(int id)
        //{
        //    int res = 0;
        //    var book = ctx.Books.FirstOrDefault(b => b.Id == id);
        //    if (book != null)
        //    {
        //        ctx.Books.Remove(book);
        //        res = ctx.SaveChanges();
        //    }
        //    return res;
        //}

        //public Book GetBook(int id)
        //{
        //    var book = ctx.Books.FirstOrDefault(b => b.Id == id);
        //    return book;
        //}

        public IEnumerable<Person> GetPersonList()
        {
            try
            {
                var persons = ctx.Persons.Where(x=>x.isDeleted==false).ToList();
                return persons;
            }
            catch (Exception ex)
            {
                return null;
            }
            
        }
        public IEnumerable<Person> GetPersonListByFirstOrLastName(string FName, string LName)
        {
            try
            {
                if (FName.Trim().ToString() == "" && FName.Trim().ToString() == "")
                {
                    var persons = ctx.Persons.Where(x=>x.isDeleted==false).ToList();
                    return persons;
                } else if(FName.Trim().ToString() == "")
                {
                    var persons = ctx.Persons.Where(x=>x.LastName==LName && x.isDeleted==false).ToList();
                    return persons;
                } else
                {
                    var persons = ctx.Persons.Where(x => x.FirstName== FName && x.isDeleted==false).ToList();
                    return persons;
                }

            }
            catch(Exception ex)
            {
                return null;
            }
        }
        public IEnumerable<Person> GetPersonListBySpecificIdentity(string specification)
        {
            try
            {
                if (specification.Trim().ToString() == "")
                {
                    var persons = ctx.Persons.ToList();
                    return persons;
                }                
                else
                {
                    var number = (int)((IdentificationType)Enum.Parse(typeof(IdentificationType), specification));
                    var identities = ctx.Identifier.Distinct().Where(x => x.Value == number.ToString()).ToList();
                    List<Person> personList = new List<Person>();
                    if (identities != null)
                    {
                        foreach (var identity in identities)
                        {
                            Person person = new Person();
                            person = ctx.Persons.Where(x => x.id == identity.Personid).FirstOrDefault();
                            personList.Add(person);
                        }
                    }
                                                            
                    return personList;
                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool CreatePersonWithIdetifiers(Person person)
        {
            try
            {
                ctx.Persons.Add(person);
                ctx.SaveChanges();
                return true;
                //    int res = ctx.SaveChanges();
                //    return res;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool CreatePersonWithOutIdetifiers(Person person)
        {
            try
            {
                Person personObj = new Person();
                if (person != null)
                {
                    personObj.FirstName = person.FirstName;
                    personObj.LastName = person.LastName;
                    personObj.isDeleted = false;
                    personObj.Identities = null;
                    ctx.Persons.Add(personObj);
                    ctx.SaveChanges();
                    return true;
                } else
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
        public bool AddIdentifierToPerson(Guid id, int Value)
        {
            try
            {
                Person person = new Person();
                person = ctx.Persons.Where(x => x.id == id && x.isDeleted==false).FirstOrDefault();
                if (person != null)
                {
                    Identifier identifier = new Identifier();
                    identifier.Value = Value.ToString();
                    identifier.Personid = person.id;
                    ctx.Identifier.Add(identifier);
                    ctx.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool DeleteIdentifierToPerson(Guid pid, Guid IdenId)
        {
            try
            {
                Person person = new Person();
                person = ctx.Persons.Where(x => x.id == pid && x.isDeleted==false).FirstOrDefault();
                if (person != null)
                {
                    Identifier identifier = new Identifier();
                    identifier = ctx.Identifier.Where(x => x.id == IdenId && x.Personid == pid).FirstOrDefault();
                    ctx.Identifier.Remove(identifier);
                    ctx.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        public bool UpdatePersonWithoutIdentifier(Person person)
        {
            try
            {
                Person personObj = new Person();
                personObj = ctx.Persons.Where(x => x.id == person.id && x.isDeleted==false).FirstOrDefault();
                if (person!= null)
                {
                    personObj.FirstName = person.FirstName;
                    personObj.LastName = person.LastName;                    
                    ctx.SaveChanges();
                    return true;
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

        public bool DeletePersonVirtually(Guid id)
        {
            try
            {
                Person personObj = new Person();
                personObj = ctx.Persons.Where(x => x.id == id && x.isDeleted == false).FirstOrDefault();
                if (personObj != null)
                {
                    personObj.isDeleted= true;                   
                    ctx.SaveChanges();
                    return true;
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
        //public int UpdateBook(int id, Book b)
        //{
        //    int res = 0;
        //    var book = ctx.Books.Find(id);
        //    if (book != null)
        //    {
        //        book.BookTitle = b.BookTitle;
        //        book.AuthorName = b.AuthorName;
        //        book.Publisher = b.Publisher;
        //        book.Genre = b.Genre;
        //        book.Price = b.Price;
        //        res = ctx.SaveChanges();
        //    }
        //    return res;
        //}
    }
}
