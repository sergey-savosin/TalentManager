using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Tracing;
using TalentManager.Data;
using TalentManager.Domain;
using TalentManager.Web.Models;

namespace TalentManager.Web.Controllers
{
    public class EmployeesController : ApiController
    {
        private readonly IUnitOfWork uow = null;
        private readonly IRepository<Employee> repository = null;
        private readonly ITraceWriter traceWriter = null;
        private readonly IMapper mapper = null;

        public EmployeesController(IUnitOfWork uow, IRepository<Employee> repository, IMapper mapper)
        {
            this.uow = uow;
            this.repository = repository;
            this.traceWriter = GlobalConfiguration.Configuration.Services.GetTraceWriter();
            this.mapper = mapper;
        }

        [OptimisticLock]
        public HttpResponseMessage Get(int id)
        {
            var employee = repository.Find(id);
            if (employee == null)
            {
                var response = Request.CreateResponse(HttpStatusCode.NotFound, "Employee not found");
                throw new HttpResponseException(response);
            }
            return Request.CreateResponse<EmployeeDto>(
                HttpStatusCode.OK,
                mapper.Map<Employee, EmployeeDto>(employee)
                );

        }

        public HttpResponseMessage GetByDepartment(int departmentId)
        {
            var employees = repository.All.Where(e => e.DepartmentId == departmentId);
            if (employees != null && employees.Any())
            {
                var ar = employees.ToList().Select(e => mapper.Map<Employee, EmployeeDto>(e));
                return Request.CreateResponse<IEnumerable<EmployeeDto>>(HttpStatusCode.OK, ar);
            }

            throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        public HttpResponseMessage Post(EmployeeDto employeeDto)
        {
            var employee = mapper.Map<EmployeeDto, Employee>(employeeDto);

            repository.Insert(employee);
            uow.Save();

            var response = Request.CreateResponse<Employee>(HttpStatusCode.Created, employee);

            string uri = Url.Link("DefaultApi", new { id = employee.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        [OptimisticLock]
        [ConflictExceptionHandler]
        public void Put(int id, EmployeeDto employeeDto)
        {
            var employee = mapper.Map<EmployeeDto, Employee>(employeeDto);

            repository.Update(employee);
            uow.Save();
        }


        public void Delete(int id)
        {
            repository.Delete(id);
            uow.Save();
        }

        protected override void Dispose(bool disposing)
        {
            if (repository != null)
            {
                repository.Dispose();
            }

            if (uow != null)
            {
                uow.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
