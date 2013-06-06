using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CN100.EnterprisePlatform.Validation
{
    public interface IValidateAttribute
    {
        string ErrorMessage { get; set; }
    }
}
