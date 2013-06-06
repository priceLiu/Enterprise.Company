using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CN100.EnterprisePlatform.Validation
{
    public interface IValidate<T>
    {
        bool Validation(T objInstance);

        PropertyInfo TargetPropertyInfo { get; set; }

        string ErrorMessage { get; set; }
    }
}
