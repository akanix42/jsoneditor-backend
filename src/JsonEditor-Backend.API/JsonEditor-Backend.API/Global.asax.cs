using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace JsonEditor_Backend.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configuration.DependencyResolver = new NinjectResolver(NinjectConfig.Kernel);

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
