$endpoint = 'https://$(CM_SITE_DOMAIN)/api/management/cache/ClearAllCache?sc_apikey={SITECORE_API_KEY}'

Invoke-RestMethod -Method POST -ContentType 'application/json' -Uri "$endpoint"



$endpoint = 'https://$(CM_SITE_DOMAIN)/api/management/cache/ClearDatabaseCache?sc_apikey={SITECORE_API_KEY}'
$database= Arg 'master'

$json = "$database" | ConvertTo-Json
Invoke-RestMethod -Method POST -ContentType 'application/json' -Uri "$endpoint" -body $database



$endpoint = 'https://$(CM_SITE_DOMAIN)/api/management/cache/ClearSiteCache?sc_apikey={SITECORE_API_KEY}'
$site = 'website'

$json = "$database" | ConvertTo-Json
Invoke-RestMethod -Method POST -ContentType 'application/json' -Uri "$endpoint" -body $site