using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CN100.EnterprisePlatform.Validation
{
    public class LengthValidateAttribute:BaseValidateAttribute
    {
        public int MinLength { get; set; }
        public int MaxLength { get; set; }

        public LengthValidateAttribute(int minLength, int maxLength,string errorMessage):
            base(errorMessage)
        {
            this.MinLength = minLength;
            this.MaxLength = maxLength;
        }
    }
}
