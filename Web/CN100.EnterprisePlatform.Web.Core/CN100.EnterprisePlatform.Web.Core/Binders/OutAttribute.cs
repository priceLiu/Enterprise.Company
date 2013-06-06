using System;
using System.Collections.Generic;
using System.Text;

namespace CN100.EnterprisePlatform.Web.Core.Binders
{
    [AttributeUsage(AttributeTargets.Property| AttributeTargets.Parameter)]
    public class OutputAttribute:Attribute
    {
    }
}
