using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.ViewModels;
using RecipesBook.Common.Helpers;
using RecipesBook.Common.Interfaces;
using RecipesBook.Core.Interfaces;
using RecipesBook.Core.Repositories;

namespace RecipesBook.Core
{
    public class App: MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            CreatableTypes()
                .EndingWith("Client")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            IoCRegistration();

            RegisterCustomAppStart<AppStart>();
        }

        private void IoCRegistration()
        {
            Mvx.IoCProvider.RegisterSingleton<IFileAccessHelper>(new FileAccessHelper());
            Mvx.IoCProvider.RegisterType(typeof(IRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            Mvx.IoCProvider.ConstructAndRegisterSingleton<IUnitOfWork, UnitOfWork>();
        }
    }
}
