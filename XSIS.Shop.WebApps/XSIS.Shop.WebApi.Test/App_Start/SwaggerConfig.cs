using System.Web.Http;
using WebActivatorEx;
using XSIS.Shop.WebApi.Test;
using Swashbuckle.Application;
using System.Linq;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace XSIS.Shop.WebApi.Test
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                    {
                        
                        //
                        c.SingleApiVersion("v1", "XSIS.Shop.WebApi.Test");
                        c.ResolveConflictingActions(apiDescription => apiDescription.First());
                        
                    })
                .EnableSwaggerUi(c =>
                    {
                        
                    });
        }
    }
}
