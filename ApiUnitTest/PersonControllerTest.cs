using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestApp.BAL;
using TestApp.BAL.Interface;
using TestApp.Controllers;
using TestApp.Model;
using Xunit;

namespace ApiUnitTest
{
    public class PersonControllerTest
    {
        //[Fact]
        //public async Task GetPersonList()
        //{
        //    // Arrange
        //    var mockRepo = new Mock<IPeopleRepository>();
        //    mockRepo.Setup(repo => repo.GetPersonList()).Returns(Task.FromResult(GetPerson()));
        //    var controller = new PersonController(mockRepo.Object);

        //    // Act
        //    var result = await controller.GetPersonList();
        //    // Assert
        //    var viewResult = Assert.IsType<Response>(result);

        //    Assert.NotNull(viewResult.data);

        //    List<Person> listPerson = (List<Person>)viewResult.data;

        //    Assert.Equal(2, listPerson.Count);
        //}
            
        
        //private List<Person> GetPerson()
        //{
        //    var sessions = new List<Person>();
        //    sessions.Add(new Person()
        //    {
        //         FirstName = "Jone",
        //          LastName = "sham",
        //           id=Guid.NewGuid(),
        //            isDeleted= false
        //    });
        //    sessions.Add(new Person()
        //    {
        //        FirstName = "Ram",
        //        LastName = "Test",
        //        id = Guid.NewGuid(),
        //        isDeleted = false
        //    });
        //    return sessions;
        //}
    }
}
