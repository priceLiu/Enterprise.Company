using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CN100.EnterprisePlatform.Validation
{
    public class RequiredValidator<T>:IValidate<T>
    {
        public RequiredValidator(string propertyName, string errorMessage)
        {
            this.TargetPropertyInfo = typeof(T).GetProperty(propertyName);
            this.ErrorMessage = errorMessage;
        }

        public bool Validation(T objInstance)
        {
            if (objInstance == null)
            {
                return false;
            }

            if (this.TargetPropertyInfo == null)
            {
                return true;
            }
            else
            {

                System.Type targetType = typeof(T);

                object pValue = this.TargetPropertyInfo.GetValue(objInstance, null);

                if (pValue == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public PropertyInfo TargetPropertyInfo { get; set; }

        public bool Validation(object objInstance)
        {
            throw new NotImplementedException();
        }

        public string ErrorMessage { get; set; }

        public static IValidate<T> GetValidate(IValidateAttribute iValidateAttribute, PropertyInfo pi)
        {
            RequiredValidateAttribute cva = (RequiredValidateAttribute)iValidateAttribute;

            if (cva != null)
            {
                return new RequiredValidator<T>(pi.Name, cva.ErrorMessage);
            }
            else
            {
                return null;
            }
        }
    }
}
