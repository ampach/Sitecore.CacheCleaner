using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

using Newtonsoft.Json;

using Sitecore.Services.Infrastructure.Web.Http;

namespace CacheCleaner.Controllers
{
    public class CacheCleanerController : ServicesApiController
    {
        [HttpPost]
        public HttpResponseMessage ClearAllCache()
        {
            try
            {
                Services.CacheService.ClearAllCaches();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception exception)
            {
                var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                httpResponseMessage.Content = new StringContent(
                    JsonConvert.SerializeObject(exception.Message),
                    System.Text.Encoding.UTF8, "application/json");
                return httpResponseMessage;
            }
        }

        [HttpPost]
        public HttpResponseMessage ClearDatabaseCache([FromBody]string database)
        {
            try
            {
                Services.CacheService.ClearDatabaseLevelCache(database);
                Services.CacheService.ClearSitecoreCache(database);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception exception)
            {
                var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                httpResponseMessage.Content = new StringContent(
                    JsonConvert.SerializeObject(exception.Message),
                    System.Text.Encoding.UTF8, "application/json");
                return httpResponseMessage;
            }
        }
    }
}