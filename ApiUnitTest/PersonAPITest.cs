using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestApp;
using TestApp.Model;
using Xunit;

namespace ApiUnitTest
{
    [TestCaseOrderer("ApiUnitTest.TestCaseOrdering.PriorityOrderer", "ApiUnitTest")]
    public class PersonAPITest : IntegrationTestsBase<Startup>
    {
        [Fact, TestPriority(1)]
        public async Task CreatePersonWithIdetifiers()
        {
            List<Identifier> lstIdentifier = new List<Identifier>();
            lstIdentifier.Add(new Identifier() { Type = 1, Value = "Test" });
            var person = new Person()
            {
                FirstName = "Nitesh",
                LastName = "T",
                isDeleted = false,
                Identities = lstIdentifier
            };

            var content = new StringContent(JsonConvert.SerializeObject(person),
                                                Encoding.UTF8, "application/json");

            var response = await this.Client.PostAsync("api/Person/CreatePersonWithIdetifiers", content);
            response.EnsureSuccessStatusCode();

            var obj = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.NotNull(obj);

            //Deserialize Json response
            Response res = Newtonsoft.Json.JsonConvert.DeserializeObject<Response>(obj);

            //chaeck Success response
            Assert.Equal(true, res.isSuccess);
        }

        [Fact, TestPriority(2)]
        public async Task CreatePersonWithOutIdetifiers()
        {
            var person = new Person()
            {
                FirstName = "Norman",
                LastName = "F",
                isDeleted = false
            };

            var content = new StringContent(JsonConvert.SerializeObject(person),
                                                Encoding.UTF8, "application/json");

            var response = await this.Client.PostAsync("api/Person/CreatePersonWithOutIdetifiers", content);
            response.EnsureSuccessStatusCode();

            var obj = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.NotNull(obj);

            //Deserialize Json response
            Response res = Newtonsoft.Json.JsonConvert.DeserializeObject<Response>(obj);

            //chaeck Success response
            Assert.Equal(true, res.isSuccess);
        }

        [Fact, TestPriority(5)]
        public async Task GetPersonList()
        {
            // Act
            //var responseString = await GetCheckPrimeResponseString();
            var response = await this.Client.GetAsync("api/Person/GetPersonList");
            response.EnsureSuccessStatusCode();

            var obj = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.NotNull(obj);

            //Deserialize Json response
            Response res = Newtonsoft.Json.JsonConvert.DeserializeObject<Response>(obj);

            //chaeck Success response
            Assert.Equal(true, res.isSuccess);
        }

        [Fact, TestPriority(7)]
        public async Task DeletePerson()
        {

            var response = await this.Client.DeleteAsync("api/Person/DeletePerson?id=647ba025-dbaa-4448-f34d-08d5235b324b");
            response.EnsureSuccessStatusCode();

            var obj = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.NotNull(obj);

            //Deserialize Json response
            Response res = Newtonsoft.Json.JsonConvert.DeserializeObject<Response>(obj);

            //chaeck Success response
            Assert.Equal(true, res.isSuccess);
        }

        [Fact, TestPriority(8)]
        public async Task DeleteIdentifierToPerson()
        {
            var response = await this.Client.DeleteAsync("api/Person/DeleteIdentifierToPerson?pid=647ba025-dbaa-4448-f34d-08d5235b324b&IdenId=905807ec-fbbc-452a-07c4-08d524e89500");
            response.EnsureSuccessStatusCode();

            var obj = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.NotNull(obj);

            //Deserialize Json response
            Response res = Newtonsoft.Json.JsonConvert.DeserializeObject<Response>(obj);

            //chaeck Success response
            Assert.Equal(true, res.isSuccess);
        }

        [Fact, TestPriority(9)]
        public async Task GetPersonListBySpecificIdentity()
        {

            var response = await this.Client.GetAsync("api/Person/GetPersonListBySpecificIdentity?specification=1");
            response.EnsureSuccessStatusCode();

            var obj = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.NotNull(obj);

            //Deserialize Json response
            Response res = Newtonsoft.Json.JsonConvert.DeserializeObject<Response>(obj);

            //chaeck Success response
            Assert.Equal(true, res.isSuccess);
        }

        //[Fact, TestPriority(3)]
        //public async Task AddIdentifierToPerson()
        //{
        //    var identifier = new Identifier()
        //    {
        //        Type = 2,
        //        Value = "Test 3",
        //        Personid = Guid.Parse("647ba025-dbaa-4448-f34d-08d5235b324b")
        //    };

        //    var content = new StringContent(JsonConvert.SerializeObject(identifier),
        //                                        Encoding.UTF8, "application/json");

        //    var response = await this.Client.PostAsync("api/Person/AddIdentifierToPerson", content);
        //    response.EnsureSuccessStatusCode();

        //    var obj = await response.Content.ReadAsStringAsync();

        //    // Assert
        //    Assert.NotNull(obj);

        //    //Deserialize Json response
        //    Response res = Newtonsoft.Json.JsonConvert.DeserializeObject<Response>(obj);

        //    //chaeck Success response
        //    Assert.Equal(true, res.isSuccess);
        //}


        //[Fact, TestPriority(4)]
        //public async Task UpdatePersonWithoutIdentifier()
        //{

        //    var person = new Person()
        //    {
        //        FirstName = "Nitesh",
        //        LastName = "T",
        //        isDeleted = false,
        //        id = Guid.Parse("647ba025-dbaa-4448-f34d-08d5235b324b")
        //    };

        //    var content = new StringContent(JsonConvert.SerializeObject(person),
        //                                        Encoding.UTF8, "application/json");

        //    var response = await this.Client.PostAsync("api/Person/UpdatePersonWithoutIdentifier", content);
        //    response.EnsureSuccessStatusCode();

        //    var obj = await response.Content.ReadAsStringAsync();

        //    // Assert
        //    Assert.NotNull(obj);

        //    //Deserialize Json response
        //    Response res = Newtonsoft.Json.JsonConvert.DeserializeObject<Response>(obj);

        //    //chaeck Success response
        //    Assert.Equal(true, res.isSuccess);
        //}




        //[Fact, TestPriority(6)]
        //public async Task GetPersonListByFirstOrLastName()
        //{
        //    string firstName = "";
        //    string lastName = "";

        //    var response = await this.Client.GetAsync("api/Person/GetPersonListByFirstOrLastName?firstName=" + firstName + "&lastName=" + lastName);
        //    response.EnsureSuccessStatusCode();

        //    var obj = await response.Content.ReadAsStringAsync();
        //    // Assert
        //    Assert.NotNull(obj);

        //    //Deserialize Json response
        //    Response res = Newtonsoft.Json.JsonConvert.DeserializeObject<Response>(obj);

        //    //chaeck Success response
        //    Assert.Equal(true, res.isSuccess);

        //    Assert.NotNull(res.data);

        //    var jobj = JObject.Parse(obj);
        //    Assert.NotNull(jobj);
        //    //Assert.Equal("nitesh", (string)jobj["data"][0]["firstName"]);
        //    //Assert.Equal("last", (string)jobj["data"][0]["lastName"]);
        //}


    }
}
