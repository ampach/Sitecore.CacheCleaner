namespace CacheCleaner.Services
{
    using System;

    using Sitecore.Caching;
    using Sitecore.Sites;

    public static class CacheService
    {
        public static bool ClearAllCaches()
        {
            try
            {
                CacheManager.ClearAllCaches();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Faild to clear all cache. Message: {ex.Message}", ex);
            }
            
        }

        public static bool ClearItemLevelCache(Sitecore.Data.Items.Item item)
        {
            try
            {
                if (item == null)
                {
                    throw new Exception("Item is null");
                }

                // Clear item's Item Cache
                Sitecore.Context.Database.Caches.ItemCache.RemoveItem(item.ID);

                // Clear item's Data Cache
                Sitecore.Context.Database.Caches.DataCache.RemoveItemInformation(item.ID);

                // Clear item's Standard Value Cache
                Sitecore.Context.Database.Caches.StandardValuesCache.RemoveKeysContaining(item.ID.ToString());

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Faild to clear Item cache. Message: {ex.Message}", ex);
            }            
            
        }

        public static bool ClearDatabaseLevelCache(string databaseName)
        {
            try
            {
                
                var database = Sitecore.Configuration.Factory.GetDatabase(databaseName);

                if (database == null)
                {
                    throw new Exception("Database is not found");
                }

                // Clear item's Item Cache
                database.Caches.ItemCache.Clear();

                // Clear item's Data Cache
                database.Caches.DataCache.Clear();

                // Clear item's Standard Value Cache
                database.Caches.StandardValuesCache.Clear();

                return true;
            }
            catch(Exception ex) {
                throw new Exception($"Faild to clear cache of {databaseName} database. Message: {ex.Message}", ex);
            }  
        } 
        
        
        public static bool ClearSitecoreCache(string databaseName)
        {
            try
            {
                var database = Sitecore.Configuration.Factory.GetDatabase(databaseName);

                if (database == null)
                {
                    throw new Exception("Database is not found");
                }

                CacheManager.GetPathCache(database).Clear();

                return true;
            }
            catch(Exception ex) {
                throw new Exception($"Faild to clear cache of {databaseName} database. Message: {ex.Message}", ex);
            }           

        }

        public static bool ClearSiteCache(string siteName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(siteName))
                {
                    throw new Exception("siteName is null or empty");
                }

                var site = Sitecore.Configuration.Factory.GetSite(siteName);
                
                if (site == null)
                {
                    throw new Exception("site is null");
                }

                SiteCaches siteCache = site.Caches;

                // Clear HTML Cache
                siteCache.HtmlCache.Clear();

                // Clear Registry Cache
                siteCache.RegistryCache.Clear();

                // Clear ViewState Cache
                siteCache.ViewStateCache.Clear();

                // Clear FilteredItemsCache
                siteCache.FilteredItemsCache.Clear();

                // Clear XSL Cache
                siteCache.XslCache.Clear();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Faild to clear Site cache. Message: {ex.Message}", ex);
            }

            
        }
    }
}