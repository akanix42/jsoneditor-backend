using JsonEditor_Backend.API.NPoco;
using JsonEditor_Backend.API.Repositories;
using Ninject;
using NPoco;

namespace JsonEditor_Backend.API
{
    public static class NinjectConfig
    {
        private static IKernel _kernel;

        public static IKernel Kernel
        {
            get { return _kernel ?? (_kernel = CreateKernel()); }
            set { _kernel = value; }
        }

        static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();

            kernel.Bind<IDocumentsRepository>().To<NPocoDocumentsRepository>();
            kernel.Bind<IDatabase>().To<JsonEditorDb>();
            
            return kernel;
        }
    }
}

