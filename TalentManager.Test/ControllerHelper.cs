using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Hosting;

namespace TalentManager.Test
{
    public static class ControllerHelper
    {
        public static void EnsureNotNull(this ApiController controller)
        {
            controller.Configuration = new HttpConfiguration();
            controller.Request = new HttpRequestMessage();
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, controller.Configuration);
        }
    }
}
