using System;
using System.Runtime.CompilerServices;

namespace CN100.EnterprisePlatform.ORM
{
    public delegate void CacheItemRemovedCallback(string key, object value, CacheItemRemovedReason reason);
}

