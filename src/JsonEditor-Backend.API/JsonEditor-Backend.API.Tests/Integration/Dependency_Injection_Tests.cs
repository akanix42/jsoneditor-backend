using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using JsonEditor_Backend.API.Controllers;
using Ninject;
using NUnit.Framework;

namespace JsonEditor_Backend.API.Tests.Integration
{
    [TestFixture]
    class Dependency_Injection_Tests
    {
        [Test]
        public void Ninject_Test()
        {
            var kernel = NinjectConfig.Kernel;

            var mvcAssembly = typeof(DocumentsController).Assembly;

            var controllerTypes =
                from type in mvcAssembly.GetExportedTypes()
                where typeof(ApiController).IsAssignableFrom(type)
                where !type.IsAbstract
                where !type.IsGenericTypeDefinition
                where type.Name.EndsWith("Controller")
                select type;

            foreach (var controllerType in controllerTypes)
                kernel.Get(controllerType);
        }
    }
}
