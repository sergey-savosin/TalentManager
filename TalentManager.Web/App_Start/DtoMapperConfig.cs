using AutoMapper;
using TalentManager.Domain;
using TalentManager.Web.Models;

namespace TalentManager.Web
{
    public static class DtoMapperConfig
    {
        public static void CreateMaps()
        {
            //Mapper.CreateMap<EmployeeDto, Employee>();
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<EmployeeDto, Employee>();
                cfg.CreateMap<Employee, EmployeeDto>();
            });
        }
    }
}