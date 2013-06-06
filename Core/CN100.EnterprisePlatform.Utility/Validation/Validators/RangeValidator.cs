using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CN100.EnterprisePlatform.Validation
{
    public class RangeValidator<T>:IValidate<T>
    {
        public RangeValidator(object minValue, object maxValue, string propertyName, string errorMessage)
        {
            this.MaxValue = maxValue;
            this.MinValue = minValue;
            this.TargetPropertyInfo = typeof(T).GetProperty(propertyName);
            this.ErrorMessage = errorMessage;
        }

        public bool Validation(T objInstance)
        {
            //if (this.TargetPropertyInfo == null||objInstance==null)
            //{
            //    return false;
            //}

            if (objInstance == null)
            {
                return false;
            }

            if (this.TargetPropertyInfo == null)
            {
                System.Type targetType = typeof(T);

                object pValue = objInstance;

                if (pValue == null)
                    return this.MinValue == null;

                IComparable iCMinValue = (IComparable)(this.MinValue);
                IComparable iCMaxValue = (IComparable)(this.MaxValue);

                IComparable iPValue = (IComparable)pValue;
                return (iPValue.CompareTo(iCMinValue) >= 0) && (iPValue.CompareTo(iCMaxValue) <= 0);
            }
            else
            {

                System.Type targetType = typeof(T);

                object pValue = this.TargetPropertyInfo.GetValue(objInstance, null);

                if (pValue == null)
                    return this.MinValue == null;

                IComparable iCMinValue = (IComparable)Convert.ChangeType(this.MinValue, this.TargetPropertyInfo.PropertyType);
                IComparable iCMaxValue = (IComparable)Convert.ChangeType(this.MaxValue, this.TargetPropertyInfo.PropertyType);

                IComparable iPValue = (IComparable)pValue;
                return (iPValue.CompareTo(iCMinValue) >= 0) && (iPValue.CompareTo(iCMaxValue) <= 0);

            }
        }

        public PropertyInfo TargetPropertyInfo { get; set; }
        
        public string ErrorMessage { get; set; }

        public object MaxValue { get; set; }

        public object MinValue { get; set; }

        public static IValidate<T> GetValidate(IValidateAttribute iValidateAttribute, PropertyInfo pi)
        {
            RangeValidateAttribute cva = (RangeValidateAttribute)iValidateAttribute;

            if (cva != null)
            {
                return new RangeValidator<T>(cva.MinValue, cva.MaxValue, pi.Name, cva.ErrorMessage);
            }
            else
            {
                return null;
            }
        }
    }
}
