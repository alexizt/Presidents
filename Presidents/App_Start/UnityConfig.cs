using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Presidents.Models;
using System.Web.Http;
using Unity;
using Unity.Injection;
using Unity.WebApi;

namespace Presidents
{
    public static class UnityConfig
    {


        public static void RegisterComponents()
        {

            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<IPresidentsRepository, PresidentsRepository>();


            // Instanciate SheetService 
            SheetsService service = new SheetsService(new BaseClientService.Initializer
            {
                ApplicationName = System.Configuration.ConfigurationManager.AppSettings["SheetsService.ApplicationName"],
                ApiKey = System.Configuration.ConfigurationManager.AppSettings["SheetsService.ApiKey"]
            });

            // Register PresidentsRepository with service parameter
            container.RegisterType<IPresidentsRepository, PresidentsRepository>(new InjectionConstructor(service));
            



            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}