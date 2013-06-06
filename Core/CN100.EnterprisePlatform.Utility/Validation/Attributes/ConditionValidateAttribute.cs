using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CN100.EnterprisePlatform.Validation
{
    public class ConditionValidateAttribute:BaseValidateAttribute
    {
        public object Value { get; set; }
        public object[] Values { get; set; }
        public ConditionOperator ConditionOperator { get; set; }

        public ConditionValidateAttribute(object[] objArray,object objValue, ConditionOperator conditionOperator, string errorMessage) :
            base(errorMessage)
        {
            this.Values = objArray;
            this.Value = objValue;
            this.ConditionOperator = conditionOperator;
        }
    }
}
