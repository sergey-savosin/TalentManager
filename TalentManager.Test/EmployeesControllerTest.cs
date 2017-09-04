using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TalentManager.Domain;
using TalentManager.Data;
using Rhino.Mocks;
using TalentManager.Web.Models;
using AutoMapper;
using TalentManager.Web.Controllers;
using System.Net.Http;
using System.Net;
using System.Web.Http;

namespace TalentManager.Test
{
    [TestClass]
    public class EmployeesControllerTest
    {
        [TestMethod]
        public void MustReturnEmployeeForGetUsingAValidId()
        {
            // Arrange
            int id = 12345;
            var employee = new Employee()
            {
                Id = id,
                FirstName = "John",
                LastName = "Human"
            };

            IRepository<Employee> repository = MockRepository.GenerateMock<IRepository<Employee>>();
            repository.Stub(x => x.Find(id)).Return(employee);

            IUnitOfWork uow = MockRepository.GenerateMock<IUnitOfWork>();

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Employee, EmployeeDto>();
            });

            var controller = new EmployeesController(uow, repository, Mapper.Instance);
            controller.EnsureNotNull();

            // Act
            HttpResponseMessage response = controller.Get(id);

            // Assert
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Content);
            Assert.IsInstanceOfType(response.Content, typeof(ObjectContent<EmployeeDto>));
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var content = (response.Content as ObjectContent<EmployeeDto>);
            var result = content.Value as EmployeeDto;

            Assert.AreEqual(employee.Id, result.Id);
            Assert.AreEqual(employee.FirstName, result.FirstName);
            Assert.AreEqual(employee.LastName, result.LastName);
        }

        [TestMethod]
        public void MustReturn404WhenForGetUsingAnInvalidId()
        {
            // Arrange
            int invalidId = 12345;

            IRepository<Employee> repository = MockRepository.GenerateMock<IRepository<Employee>>();
            repository.Stub(x => x.Find(invalidId)).Return(null); //Simulate no match

            IUnitOfWork uow = MockRepository.GenerateMock<IUnitOfWork>();

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Employee, EmployeeDto>();
            });

            var controller = new EmployeesController(uow, repository, Mapper.Instance);
            controller.EnsureNotNull();

            // Act
            HttpResponseMessage response = null;
            try
            {
                controller.Get(invalidId);
                Assert.Fail();
            }
            catch(HttpResponseException ex)
            {
                // Assert
                Assert.AreEqual(HttpStatusCode.NotFound, ex.Response.StatusCode);
            }
        }
    }
}
