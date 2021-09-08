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
    using Models;

    using Sitecore.Data;
    using Sitecore.Services.Infrastructure.Security;

    [RequiredApiKey]
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

        [HttpPost]
        public HttpResponseMessage ClearSiteCache([FromBody]string site)
        {
            try
            {
                Services.CacheService.ClearSiteCache(site);
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
        public HttpResponseMessage ClearItemCacheByPath([FromBody] ItemCacheCleanRequestModel model)
        {
            try
            {
                Services.CacheService.ClearItemLevelCache(model.Item, model.Database);
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
        public HttpResponseMessage ClearItemCacheByID([FromBody] ItemCacheCleanRequestModel model)
        {
            try
            {
                ID itemId;
                if (!ID.TryParse(model.Item, out itemId))
                {
                    throw new Exception("if format is wrong");
                }

                Services.CacheService.ClearItemLevelCache(itemId, model.Database);

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