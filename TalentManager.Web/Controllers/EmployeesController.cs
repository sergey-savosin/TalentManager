using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Tracing;
using TalentManager.Data;
using TalentManager.Domain;

namespace TalentManager.Web.Controllers
{
    public class EmployeesController : ApiController
    {
        private readonly IEmployeeRepository repository = null;
        private readonly ITraceWriter traceWriter = null;


        public EmployeesController()
        {
            this.repository = new EmployeeRepository();
            this.traceWriter = GlobalConfiguration.Configuration.Services.GetTraceWriter();
        }

        public EmployeesController(IEmployeeRepository repository)
        {
            this.repository = repository;
            this.traceWriter = GlobalConfiguration.Configuration.Services.GetTraceWriter();
        }

        public HttpResponseMessage Get(int id)
        {
            var employee = repository.Get(id);
            if (employee == null)
            {
                var response = Request.CreateResponse(HttpStatusCode.NotFound, "Employee not found");
                throw new HttpResponseException(response);
            }
            return Request.CreateResponse<Employee>(HttpStatusCode.OK, employee);
        }

        public HttpResponseMessage GetByDepartment(int departmentId)
        {
            var employees = repository.GetByDepartment(departmentId);
            if (employees != null && employees.Any())
            {
                return Request.CreateResponse<IEnumerable<Employee>>(HttpStatusCode.OK, employees);
            }

            throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        protected override void Dispose(bool disposing)
        {
            if (repository != null)
            {
                repository.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
