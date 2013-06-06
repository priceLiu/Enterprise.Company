using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CN100.EnterprisePlatform.Validation
{
    public class BaseValidateAttribute:System.Attribute,IValidateAttribute
    {
        public BaseValidateAttribute(string errorMessage)
        {
            this.ErrorMessage = errorMessage;
        }

        public string ErrorMessage { get; set; }
    }
}
