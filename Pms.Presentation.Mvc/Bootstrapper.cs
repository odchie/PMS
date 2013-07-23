using System.Web.Mvc;
using System.Configuration;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Unity.Mvc3;

namespace Pms.Presentation.Mvc
{
    public static class Bootstrapper
    {
        public static void Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            UnityConfigurationSection configurationsection = ConfigurationManager.GetSection("unity") as UnityConfigurationSection;

            configurationsection.Configure(container);
            return container;
        }
    }
}