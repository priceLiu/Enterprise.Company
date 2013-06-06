using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CN100.EnterprisePlatform.Validation
{
    public class LengthValidator<T>:IValidate<T>
    {
        public LengthValidator(int minLength, int maxLength, string propertyName, string errorMessage)
        {
            this.MaxLength = maxLength;
            this.MinLength = minLength;
            this.TargetPropertyInfo = typeof(T).GetProperty(propertyName);
            this.ErrorMessage = errorMessage;
        }

        public PropertyInfo TargetPropertyInfo { get; set; }

        public bool Validation(T objInstance)
        {
            if (objInstance == null)
            {
                return false;
            }

            if (this.TargetPropertyInfo == null)
            {
                System.Type targetType = typeof(T);

                object pValue = objInstance;

                if (targetType.GetInterface("System.Collections.ICollection", false) != null)
                {
                    ICollection ienum = (ICollection)pValue;

                    if (ienum == null)
                    {
                        return false;
                    }
                    else
                    {
                        return (this.MinLength <= ienum.Count) && (this.MaxLength >= ienum.Count);
                    }
                }
                else
                {
                    string str = (string)pValue;

                    if (str == null)
                        return false;

                    return (this.MinLength <= str.Length) && (this.MaxLength >= str.Length);
                }
            }
            else
            {

                System.Type targetType = typeof(T);

                object pValue = this.TargetPropertyInfo.GetValue(objInstance, null);

                if (targetType.GetInterface("System.Collections.ICollection", false) != null)
                {
                    ICollection ienum = (ICollection)pValue;

                    if (ienum == null)
                    {
                        return false;
                    }
                    else
                    {
                        return (this.MinLength <= ienum.Count) && (this.MaxLength >= ienum.Count);
                    }
                }
                else
                {
                    string str = (string)pValue;

                    if (str == null)
                        return false;

                    return (this.MinLength <= str.Length) && (this.MaxLength >= str.Length);
                }
            }
        }

        public string ErrorMessage { get; set; }

        public int MinLength { get; set; }

        public int MaxLength { get; set; }

        public static IValidate<T> GetValidate(IValidateAttribute iValidateAttribute, PropertyInfo pi)
        {
            LengthValidateAttribute cva = (LengthValidateAttribute)iValidateAttribute;

            if (cva != null)
            {
                return new LengthValidator<T>(cva.MinLength, cva.MaxLength, pi.Name, cva.ErrorMessage);
            }
            else
            {
                return null;
            }
        }
    }
}
