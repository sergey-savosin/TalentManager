using AutoMapper;
using StructureMap;
using System;
using System.Linq;
using System.Web.Http;
using TalentManager.Data;
using TalentManager.Domain;

namespace TalentManager.Web
{
    public static class IocConfig
    {
        public static void RegisterDependencyResolver(HttpConfiguration config)
        {
            var c = new Container(cn =>
            {
                cn.Scan(scan =>
                {
                    scan.WithDefaultConventions();

                    AppDomain.CurrentDomain.GetAssemblies()
                        .Where(a => a.GetName().Name.StartsWith("TalentManager"))
                        .ToList()
                        .ForEach(a => scan.Assembly(a));
                });

                cn.For<IMapper>().Use(Mapper.Instance);
                //cn.For<IRepository<Employee>>().Use<Repository<Employee>>();
                cn.For(typeof(IRepository<>)).Use(typeof(Repository<>));
            });

            config.DependencyResolver = new StructureMapContainer(c);
        }
    }
}