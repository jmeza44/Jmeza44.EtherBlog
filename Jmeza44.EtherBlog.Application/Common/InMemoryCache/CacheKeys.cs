namespace Jmeza44.EtherBlog.Application.Common.InMemoryCache
{
    public static class CacheKeys
    {
        private const char cacheKeySectionsSeparator = '|';
        private const char entitiesNameSeparator = '.';
        private const int entitiesListPositionInCacheKey = 0;
        private const int requestNamePositionInCacheKey = 1;
        private const int requestParamsPositionInCacheKey = 2;

        private static readonly List<string> _cacheKeys = new(); // Track the in-memory cache keys

        public static List<string> GetCacheKeysThatDependOn(Type[] entities, List<string>? cacheKeysToEvaluate = null)
        {
            cacheKeysToEvaluate ??= _cacheKeys;

            var resultingCacheKeysList = new List<string>();

            foreach (var cacheKey in cacheKeysToEvaluate)
            {
                var entitiesThatCacheDependsOn = ExtractEntitiesThatCacheDependsOnFromCacheKey(cacheKey);
                var cacheDependsOnAtLeastOneOfTheEntities = entities.Any(entity => entitiesThatCacheDependsOn.Contains(entity.Name));
                if (cacheDependsOnAtLeastOneOfTheEntities)
                {
                    resultingCacheKeysList.Add(cacheKey);
                };
            }

            return resultingCacheKeysList;
        }

        public static void AddCacheKey(string cacheKey)
        {
            if (string.IsNullOrWhiteSpace(cacheKey))
            {
                throw new ArgumentException($"'{nameof(cacheKey)}' cannot be null or whitespace.", nameof(cacheKey));
            }

            _cacheKeys.Add(cacheKey);
        }

        public static void RemoveCacheKey(string cacheKey)
        {
            if (string.IsNullOrWhiteSpace(cacheKey))
            {
                throw new ArgumentException($"'{nameof(cacheKey)}' cannot be null or whitespace.", nameof(cacheKey));
            }

            _cacheKeys.Remove(cacheKey);
        }

        public static string BuildCacheKey(string[] namesOfTheEntitiesThatCacheRelaisOn, string requestName, string requestParams)
        {
            var entitiesThatCacheRelaisOnSection = string.Join(entitiesNameSeparator, namesOfTheEntitiesThatCacheRelaisOn);
            var cacheKeySectionsArray = GetCacheSectionsArray(entitiesThatCacheRelaisOnSection, requestName, requestParams); 
            var cacheKey = string.Join(cacheKeySectionsSeparator, cacheKeySectionsArray);
            return cacheKey;
        }

        private static string[] GetCacheSectionsArray(string entitiesThatCacheRelaisOnSection, string requestName, string requestParams)
        {
            string[] cacheKeySections = new string[4];
            cacheKeySections[entitiesListPositionInCacheKey] = entitiesThatCacheRelaisOnSection;
            cacheKeySections[requestNamePositionInCacheKey] = requestName;
            cacheKeySections[requestParamsPositionInCacheKey] = requestParams;
            return cacheKeySections;
        }

        private static List<string> ExtractEntitiesThatCacheDependsOnFromCacheKey(string cacheKey)
        {
            return cacheKey.Split(cacheKeySectionsSeparator)
                           .ElementAt(entitiesListPositionInCacheKey)
                           .Split(entitiesNameSeparator)
                           .ToList();
        }
    }
}
