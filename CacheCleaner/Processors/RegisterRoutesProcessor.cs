using System.Web.Http;
using Sitecore.Pipelines;

namespace CacheCleaner.Processors
{
    public class RegisterRoutesProcessor
    {
        public virtual void Process(PipelineArgs args)
        {
            GlobalConfiguration.Configure(config =>
            {
                config.Routes.MapHttpRoute("CacheManagement", "api/management/cache/{action}", new
                {
                    controller = "CacheCleaner"
                });
            });
        }
    }
}