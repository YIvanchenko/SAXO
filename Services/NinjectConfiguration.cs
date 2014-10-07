using Ninject;
using Persistence.Services;
using Persistence.Services.Interface;

namespace Services
{
    public static class NinjectConfiguration
    {
        public static void Configure(IKernel kernel)
        {
            kernel.Bind<IBookListsDataService>().To<BookListsDataService>();
        }
    }
}
