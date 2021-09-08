# Sitecore.CacheCleaner

CacheCleaner is a simple Sitecore module which is served to parge a vaious Sitecore caches via POST request to the API. This module will help you to build your CI/CD pipelines, for example, in case of restoring databases from backups on fly during Blue/Green deployments or on case of programmatically uploading a batch of items in the Event Disabling mode. On those cases, we need to Purge (Clean) cache to see changes.

The module can be sounload [here](Sitecore%20Packages/CacheCleaner-1.0.zip)

There are three available methods to clear cache:

## ClearAllCache

The ClearAllCache method is available by the following path: `/api/management/cache/ClearAllCache?sc_apikey={SITECORE_API_KEY}`

It clears all Sitecore caches. It is the same as cleaning cache on /sitecore/admin/cache.aspx page.

```powershell
$endpoint = 'https://$(CM_SITE_DOMAIN)/api/management/cache/ClearAllCache?sc_apikey={SITECORE_API_KEY}'

Invoke-RestMethod -Method POST -ContentType 'application/json' -Uri "$endpoint"
```

## ClearDatabaseCache

The ClearAllCache method is available by the following path: `/api/management/cache/ClearDatabaseCache?sc_apikey={SITECORE_API_KEY}`

Method requires the `[string] database` parameter passed in request body. It clears all database-related cache: ItemCache, DataCache and StandardValuesCache.


```powershell
$endpoint = 'https://$(CM_SITE_DOMAIN)/api/management/cache/ClearDatabaseCache?sc_apikey={SITECORE_API_KEY}'
$database= Arg 'master'

$json = "$database" | ConvertTo-Json
Invoke-RestMethod -Method POST -ContentType 'application/json' -Uri "$endpoint" -body $database
```

## ClearSiteCache

Method requires the `[string] site` parameter passed in request body. It clears all site-related caches.

```powershell
$endpoint = 'https://$(CM_SITE_DOMAIN)/api/management/cache/ClearSiteCache?sc_apikey={SITECORE_API_KEY}'
$site = 'website'

$json = "$database" | ConvertTo-Json
Invoke-RestMethod -Method POST -ContentType 'application/json' -Uri "$endpoint" -body $site
```
