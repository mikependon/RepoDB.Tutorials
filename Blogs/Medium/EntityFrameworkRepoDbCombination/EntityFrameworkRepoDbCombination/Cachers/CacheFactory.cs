using RepoDb;
using RepoDb.Interfaces;

namespace EntityFrameworkRepoDbCombination.Cachers
{
    public class CacheFactory
    {
        private static ICache _cache;
        private static object _syncLock;

        static CacheFactory()
        {
            _syncLock = new object();
        }

        public static ICache Create()
        {
            if (_cache==null)
            {
                lock (_syncLock)
                {
                    _cache = new MemoryCache();
                }
            }
            return _cache;
        }
    }
}
