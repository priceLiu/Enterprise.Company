using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CN100.EnterprisePlatform.Validation
{
    public class RangeValidateAttribute:BaseValidateAttribute
    {
        public object MaxValue { get; set; }
        public object MinValue { get; set; }

        public RangeValidateAttribute(object maxValue, object minValue,string errorMessage):
            base(errorMessage)
        {
            this.MaxValue = maxValue;
            this.MinValue = minValue;
        }
    }
}
