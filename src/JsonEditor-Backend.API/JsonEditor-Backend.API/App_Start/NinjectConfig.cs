using Ninject;

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

            //kernel.Bind<>().To<>();
            
            return kernel;
        }
    }
}

