using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CN100.EnterprisePlatform.Validation
{
    public class RegexValidateAttribute:BaseValidateAttribute
    {
        public RegexValidateAttribute(string regex, string errorMessage) :
            base(errorMessage)
        {
            this.Regex = regex;
        }

        public string Regex { get; set; }
    }
}
