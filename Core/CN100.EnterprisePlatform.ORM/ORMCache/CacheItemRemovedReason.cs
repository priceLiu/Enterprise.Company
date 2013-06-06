using System;

namespace CN100.EnterprisePlatform.ORM
{
    public enum CacheItemRemovedReason
    {
        DependencyChanged = 4,
        Expired = 2,
        Removed = 1,
        Underused = 3
    }
}

