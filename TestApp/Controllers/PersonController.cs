using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestApp.Model;
using TestApp.BAL;
using TestApp.Repositories;

namespace TestApp.Controllers
{
    [Produces("application/json")]
    [Route("api/Person")]
    public class PersonController : Controller
    {
        private IDataAccess<Person, int> dataAccess;
        public   PersonController(IDataAccess<Person, int> dataAccessObj)
        {
            dataAccess = dataAccessObj;
        }
       
        // GET: api/Person
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Person/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Person
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Person/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [Route("GetPersonList")]
        [HttpGet]
        public Response GetPersonList()
        {
            try
            {
                List<Person> personList = new List<Person>();
                personList = dataAccess.GetPersonList().ToList();
                return new Response { isSuccess = true, data = personList, message = " " };
            }
            catch (Exception ex)
            {
                return new Response { isSuccess = false, data = null, message = "Server error" };
            }

        }
        [Route("GetPersonListByFirstOrLastName")]
        [HttpGet]
        public Response GetPersonListByFirstOrLastName(string FirstName = "", string LastName = "")
        {
            try
            {
                List<Person> personList = new List<Person>();
                personList = dataAccess.GetPersonListByFirstOrLastName(FirstName, LastName).ToList();
                return new Response { isSuccess = true, data = personList, message = " " };
            }
            catch (Exception ex)
            {
                return new Response { isSuccess = false, data = null, message = "Server error" };
            }

        }
        [Route("GetPersonListBySpecificIdentity")]
        [HttpGet]
        public Response GetPersonListBySpecificIdentity(string specification="")
        {
            try
            {
                List<Person> personList = new List<Person>();
                personList = dataAccess.GetPersonListBySpecificIdentity(specification).ToList();
                return new Response { isSuccess = true, data = personList, message = " " };
            }
            catch (Exception ex)
            {
                return new Response { isSuccess = false, data = null, message = "Server error" };
            }
        }

        [Route("CreatePersonWithIdetifiers")]
        [HttpPost]
        public Response CreatePersonWithIdetifiers([FromBody]Person person)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return new Response { isSuccess = false, data = null, message = "Invalid Request data." };
                } else
                {
                    bool success = dataAccess.CreatePersonWithIdetifiers(person);
                    if (success)
                    {
                        return new Response { isSuccess = true, data = null, message = "User Created successfully" };
                    }
                    else
                    {
                        return new Response { isSuccess = false, data = null, message = "Server Error" };
                    }
                  
                }
               
                
            }
            catch (Exception ex)
            {
                return new Response { isSuccess = false, data = null, message = "Server Error" };
            }
        }
        [Route("CreatePersonWithOutIdetifiers")]
        [HttpPost]
        public Response CreatePersonWithOutIdetifiers([FromBody]Person person)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return new Response { isSuccess = false, data = null, message = "Invalid Request data." };

                }
                else
                {                    
                    bool success = dataAccess.CreatePersonWithOutIdetifiers(person);
                    if (success)
                    {
                        return new Response { isSuccess = true, data = null, message = "User Created successfully" };
                    }
                    else
                    {
                        return new Response { isSuccess = false, data = null, message = "Server Error" };
                    }
                    
                }
               
            }
            catch (Exception ex)
            {
                return new Response { isSuccess = false, data = null, message = "Server Error" };
            }
        }
        [Route("AddIdentifierToPerson")]
        [HttpGet]
        public Response AddIdentifierToPerson(Guid id, int Value)
        {
            try
            {
                if(Value>0 && Value < 4 && id!=null)
                {
                    bool success = dataAccess.AddIdentifierToPerson(id, Value);
                    if (success)
                    {
                        return new Response { isSuccess = true, data = null, message = "User Updated successfully" };
                    }
                    else
                    {
                        return new Response { isSuccess = false, data = null, message = "Server Error" };
                    }
                    
                }
                else
                {
                    return new Response { isSuccess = false, data = null, message = "Invalid Request data." };
                }
               
            }
            catch (Exception ex)
            {
                return new Response { isSuccess = false, data = null, message = "Server Error" };
            }
        }

        [Route("DeleteIdentifierToPerson")]
        [HttpGet]
        public Response DeleteIdentifierToPerson(Guid pid, Guid IdenId)
        {
            try
            {
                if(pid!=null && IdenId != null)
                {
                    bool success = dataAccess.DeleteIdentifierToPerson(pid, IdenId);
                    return new Response { isSuccess = true, data = null, message = "User Updated successfully" };
                }
                else
                {
                    return new Response { isSuccess = false, data = null, message = "Invalid Request data." };
                }
               
            }
            catch (Exception ex)
            {
                return new Response { isSuccess = false, data = null, message = "Server Error" };
            }
        }

        [Route("UpdatePersonWithoutIdentifier")]
        [HttpPost]
        public Response UpdatePersonWithoutIdentifier([FromBody]Person person)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return new Response { isSuccess = false, data = null, message = "Invalid Request data." };

                }
                else
                {                   
                    bool success = dataAccess.UpdatePersonWithoutIdentifier(person);
                    if (success)
                    {
                        return new Response { isSuccess = true, data = null, message = "User Updated successfully" };
                    }
                    else
                    {
                        return new Response { isSuccess = false, data = null, message = "Server Error" };
                    }

                }

               
            }
            catch (Exception ex)
            {
                return new Response { isSuccess = false, data = null, message = "Server Error" };
            }
        }

        [Route("DeleteIdentifierToPerson")]
        [HttpDelete]
        public Response DeletePersonVirtually(Guid id)
        {
            try
            {
                if (id != null )
                {
                    bool success = dataAccess.DeletePersonVirtually(id);
                    return new Response { isSuccess = true, data = null, message = "User Deleted successfully" };
                }
                else
                {
                    return new Response { isSuccess = false, data = null, message = "Invalid Request data." };
                }

            }
            catch (Exception ex)
            {
                return new Response { isSuccess = false, data = null, message = "Server Error" };
            }
        }
    }
}
