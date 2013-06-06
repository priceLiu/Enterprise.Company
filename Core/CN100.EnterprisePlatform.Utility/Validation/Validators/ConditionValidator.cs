using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CN100.EnterprisePlatform.Validation
{
    public class ConditionValidator<T> : IValidate<T>
    {
        public ConditionValidator(object[] objArray, object objValue, string propertyName, ConditionOperator conditionOperator, string errorMessage)
        {
            this.Values = objArray;
            this.Value = objValue;
            this.TargetPropertyInfo = typeof(T).GetProperty(propertyName);
            this.ConditionOperator = conditionOperator;
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
                if (this.ConditionOperator == ConditionOperator.In)
                {
                    if (!this.Values.Contains(objInstance))
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    IComparable iCValue = (IComparable)Convert.ChangeType(this.Value, objInstance.GetType());

                    IComparable iPValue = (IComparable)objInstance;

                    int intResult = iPValue.CompareTo(iCValue);

                    switch (this.ConditionOperator)
                    {
                        case ConditionOperator.Equal:
                            if (intResult == 0)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        case ConditionOperator.GreaterThan:
                            if (intResult > 0)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        case ConditionOperator.GreaterThanOrEqual:
                            if (intResult >= 0)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        case ConditionOperator.LessThan:
                            if (intResult < 0)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        case ConditionOperator.LessThanOrEqual:
                            if (intResult <= 0)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        case ConditionOperator.NotEqual:
                            if (intResult != 0)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        case ConditionOperator.In:
                            return false;
                    }
                    return false;
                }
            }
            else
            {

                System.Type targetType = typeof(T);

                object pValue = this.TargetPropertyInfo.GetValue(objInstance, null);

                if (this.ConditionOperator == ConditionOperator.In)
                {
                    if (!this.Values.Contains(pValue))
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    if (this.TargetPropertyInfo.PropertyType.GetInterface("System.IComparable", false) != null)
                    {
                        IComparable iCValue = (IComparable)Convert.ChangeType(this.Value, this.TargetPropertyInfo.PropertyType);

                        IComparable iPValue = (IComparable)pValue;

                        int intResult = iPValue.CompareTo(iCValue);

                        switch (this.ConditionOperator)
                        {
                            case ConditionOperator.Equal:
                                if (intResult == 0)
                                {
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            case ConditionOperator.GreaterThan:
                                if (intResult > 0)
                                {
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            case ConditionOperator.GreaterThanOrEqual:
                                if (intResult >= 0)
                                {
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            case ConditionOperator.LessThan:
                                if (intResult < 0)
                                {
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            case ConditionOperator.LessThanOrEqual:
                                if (intResult <= 0)
                                {
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            case ConditionOperator.NotEqual:
                                if (intResult != 0)
                                {
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            case ConditionOperator.In:
                                return false;
                        }

                        return false;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public PropertyInfo TargetPropertyInfo { get; set; }

        public string ErrorMessage { get; set; }

        public object Value { get; set; }

        public object[] Values { get; set; }

        public ConditionOperator ConditionOperator { get; set; }


        public static IValidate<T> GetValidate(IValidateAttribute iValidateAttribute, PropertyInfo pi)
        {
            ConditionValidateAttribute cva = (ConditionValidateAttribute)iValidateAttribute;

            if (cva != null)
            {
                return new ConditionValidator<T>(cva.Values, cva.Value, pi.Name, cva.ConditionOperator, cva.ErrorMessage);
            }
            else
            {
                return null;
            }
        }
    }
}
