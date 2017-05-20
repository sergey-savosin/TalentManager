using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalentManager.Data.Configuration;
using TalentManager.Domain;

namespace TalentManager.Data
{
    public class Context : DbContext
    {
        public Context() : base("DefaultConnection") { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations
                .Add(new EmployeeConfiguration())
                .Add(new DepartmentConfiguration());
        }
    }
}
