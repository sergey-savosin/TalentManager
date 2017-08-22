using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TalentManager.Domain;

namespace TalentManager.Data
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private Context context = new Context();
        private bool disposed = false;

        public Employee Get(int id)
        {
            return context.Employees.FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<Employee> GetByDepartment(int departmentId)
        {
            return context.Employees.Where(e => e.DepartmentId == departmentId);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    if (context != null)
                        context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
