using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.ViewModels;
using RecipesBook.Common.Helpers;
using RecipesBook.Common.Interfaces;
using RecipesBook.Core.Interfaces;
using RecipesBook.Core.Repositories;
using RecipesBook.Core.Services;

namespace RecipesBook.Core
{
    public class App: MvxApplication
    {
        public override void Initialize()
        {
            Mvx.IoCProvider.RegisterSingleton<IFileAccessHelper>(new FileAccessHelper());
            Mvx.IoCProvider.RegisterSingleton(typeof(IRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            Mvx.IoCProvider.ConstructAndRegisterSingleton<IUnitOfWork, UnitOfWork>();

            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            RegisterCustomAppStart<AppStart>();
        }
    }
}
