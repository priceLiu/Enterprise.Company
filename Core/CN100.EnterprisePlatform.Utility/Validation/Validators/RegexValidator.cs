using System;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CN100.EnterprisePlatform.Validation
{
    public class RegexValidator<T>:IValidate<T>
    {
        public RegexValidator(string regex, string propertyName, string errorMessage)
        {
            this.Regex = regex;
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
                System.Type targetType = typeof(T);

                object pValue = objInstance;

                string str = (string)pValue;

                if (str == null)
                    return false;

                System.Text.RegularExpressions.Regex reg = new Regex(this.Regex, RegexOptions.None);

                if (reg.IsMatch(str))
                {
                    return true;
                }
                else
                {
                    return false;
                }
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
                    string str = (string)pValue;

                    if (str == null)
                        return false;

                    System.Text.RegularExpressions.Regex reg = new Regex(this.Regex, RegexOptions.None);

                    if (reg.IsMatch(str))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public PropertyInfo TargetPropertyInfo { get; set; }

        public string Regex { get; set; }

        public string ErrorMessage { get; set; }

        public static IValidate<T> GetValidate(IValidateAttribute iValidateAttribute, PropertyInfo pi)
        {
            RegexValidateAttribute cva = (RegexValidateAttribute)iValidateAttribute;

            if (cva != null)
            {
                return new RegexValidator<T>(cva.Regex, pi.Name, cva.ErrorMessage);
            }
            else
            {
                return null;
            }
        }

    }
}
