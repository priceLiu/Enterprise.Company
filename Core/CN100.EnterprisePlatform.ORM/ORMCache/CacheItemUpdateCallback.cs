﻿using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace CN100.EnterprisePlatform.ORM
{
    public delegate void CacheItemUpdateCallback(string key, CacheItemUpdateReason reason, out object expensiveObject, out CacheDependency dependency, out DateTime absoluteExpiration, out TimeSpan slidingExpiration);
}

