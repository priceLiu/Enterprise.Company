using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CN100.EnterprisePlatform.Validation
{
    public class RequiredValidateAttribute:BaseValidateAttribute
    {
        public RequiredValidateAttribute(string errorMessage):
            base(errorMessage)
        {
        }
    }
}
