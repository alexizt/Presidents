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