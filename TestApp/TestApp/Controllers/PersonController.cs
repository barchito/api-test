using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestApp.Model;
using TestApp.BAL;
using TestApp.PeopleRepositories.Interface;
using TestApp.BAL.Interface;

namespace TestApp.Controllers
{
    [Produces("application/json")]
    [Route("api/Person")]
    public class PersonController : Controller
    {
        private IPeopleRepository businessAccess;
        public   PersonController(IPeopleRepository businessAccessObj)
        {
            businessAccess = businessAccessObj;
        }
       
      

        /// <summary>
        ///     This method return all people list
        /// </summary>
        /// <returns>People List</returns>
        [Route("GetPersonList")]
        [HttpGet]
        public async Task<Response> GetPersonList()
        {
            try
            {
                List<Person> personList = new List<Person>();
                personList = await businessAccess.GetPersonList();
                
                return new Response { isSuccess = true, data = personList, message = " " };
            }
            catch (Exception ex)
            {
                return new Response { isSuccess = false, data = null, message = "Server error" };
            }

        }

        /// <summary>
        ///     This method return people list according to first name or last name
        /// </summary>
        /// <returns>People List</returns>
        [Route("GetPersonListByFirstOrLastName")]
        [HttpGet]
        public async Task<Response> GetPersonListByFirstOrLastName(string FirstName = "", string LastName = "")
        {
            try
            {
                List<Person> personList = new List<Person>();
                personList = await businessAccess.GetPersonListByFirstOrLastName(FirstName, LastName);
                return new Response { isSuccess = true, data = personList, message = " " };
            }
            catch (Exception ex)
            {
                return new Response { isSuccess = false, data = null, message = "Server error" };
            }

        }

        /// <summary>
        ///     This method return people list according to Identity
        /// </summary>
        /// <returns>People List</returns>
        [Route("GetPersonListBySpecificIdentity")]
        [HttpGet]
        public async Task<Response> GetPersonListBySpecificIdentity(int specification)
        {
            try
            {
                List<Person> personList = new List<Person>();
                personList = await businessAccess.GetPersonListBySpecificIdentity(specification);
                return new Response { isSuccess = true, data = personList, message = " " };
            }
            catch (Exception ex)
            {
                return new Response { isSuccess = false, data = null, message = "Server error" };
            }
        }


        /// <summary>
        ///     This method create people with identifiers
        /// </summary>
        /// <returns></returns>
        [Route("CreatePersonWithIdetifiers")]
        [HttpPost]
        public async Task<Response> CreatePersonWithIdetifiers([FromBody]Person person)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return new Response { isSuccess = false, data = null, message = "Invalid Request data." };
                } else
                {
                    bool success = await businessAccess.CreatePersonWithIdetifiers(person);
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

        /// <summary>
        ///     This method create people without identifiers
        /// </summary>
        /// <returns></returns>
        [Route("CreatePersonWithOutIdetifiers")]
        [HttpPost]
        public async Task<Response> CreatePersonWithOutIdetifiers([FromBody]Person person)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return new Response { isSuccess = false, data = null, message = "Invalid Request data." };

                }
                else
                {                    
                    bool success = await businessAccess.CreatePersonWithOutIdetifiers(person);
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

        /// <summary>
        ///     This method add identity to people 
        /// </summary>
        /// <returns></returns>
        [Route("AddIdentifierToPerson")]
        [HttpPost]
        public async Task<Response> AddIdentifierToPerson(Guid personId, Identifier identityObj)
        {
            try
            {
                if(identityObj !=null  && personId != null)
                {
                    bool success = await businessAccess.AddIdentifierToPerson(personId, identityObj);
                    if (success)
                    {
                        return new Response { isSuccess = true, data = null, message = "Identifier added successfully" };
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


        /// <summary>
        ///     This method delete identity from people 
        /// </summary>
        /// <returns></returns>
        [Route("DeleteIdentifierToPerson")]
        [HttpDelete]
        public async Task<Response> DeleteIdentifierToPerson(Guid pid, Guid IdenId)
        {
            try
            {
                if(pid!=null && IdenId != null)
                {
                    bool success =await businessAccess.DeleteIdentifierToPerson(pid, IdenId);
                    return new Response { isSuccess = true, data = null, message = "Identifier deleted successfully" };
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

        /// <summary>
        ///     This method update person without identity
        /// </summary>
        /// <returns></returns>
        [Route("UpdatePersonWithoutIdentifier")]
        [HttpPost]
        public async Task<Response> UpdatePersonWithoutIdentifier([FromBody]Person person)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return new Response { isSuccess = false, data = null, message = "Invalid Request data." };

                }
                else
                {                   
                    bool success = await businessAccess.UpdatePersonWithoutIdentifier(person);
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

        /// <summary>
        ///     This method delete people virtually
        /// </summary>
        /// <returns></returns>
        [Route("DeletePerson")]
        [HttpDelete]
        public async Task<Response> DeletePerson(Guid id)
        {
            try
            {
                if (id != null )
                {
                    bool success = await businessAccess.DeletePersonVirtually(id);
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
