using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CN100.EnterprisePlatform.Validation
{
    public class ValidatorProxy
    {
        private static Hashtable mValidates = new Hashtable();

        private static Dictionary<int, Dictionary<int, object>> mTValidates = new Dictionary<int, Dictionary<int, object>>();

        private static readonly object lockObject = new object();

        public static void RegisterValidate<T>(IValidate<T> iTargetValidate)
        {
            lock (lockObject)
            {
                Type TType = typeof(T);
                int TTypeKey = TType.GetHashCode();

                if (!mTValidates.ContainsKey(TTypeKey))
                {
                    Dictionary<int, object> dicValidate = new Dictionary<int, object>();
                    int keyiTargetValidate = iTargetValidate.GetHashCode();
                    dicValidate.Add(keyiTargetValidate, iTargetValidate);
                    mTValidates.Add(TTypeKey, dicValidate);
                }
                else
                {
                    int keyTargetValidate = iTargetValidate.GetHashCode();
                    if (!mTValidates[TTypeKey].ContainsKey(keyTargetValidate))
                    {
                        mTValidates[TTypeKey].Add(keyTargetValidate, iTargetValidate);
                    }
                    else
                    {
                        mTValidates[TTypeKey][keyTargetValidate] = iTargetValidate;
                    }
                }
            }
        }

        public static void RegisterValidateType<T>()
        {
            foreach (PropertyInfo pi in typeof(T).GetProperties())
            {
                foreach (object objCustomAttr in pi.GetCustomAttributes(typeof(IValidateAttribute), true))
                {
                    IValidateAttribute iValidateAttribute = (IValidateAttribute)(objCustomAttr);

                    if (iValidateAttribute is ConditionValidateAttribute)
                    {
                        IValidate<T> iTargetValidate = ConditionValidator<T>.GetValidate(iValidateAttribute, pi);
                        RegisterValidate<T>(iTargetValidate);
                        continue;
                    }

                    if (iValidateAttribute is LengthValidateAttribute)
                    {
                        IValidate<T> iTargetValidate = LengthValidator<T>.GetValidate(iValidateAttribute, pi);
                        RegisterValidate<T>(iTargetValidate);
                        continue;
                    }

                    if (iValidateAttribute is RangeValidateAttribute)
                    {
                        IValidate<T> iTargetValidate = RangeValidator<T>.GetValidate(iValidateAttribute, pi);
                        RegisterValidate<T>(iTargetValidate);
                        continue;
                    }

                    if (iValidateAttribute is RegexValidateAttribute)
                    {
                        IValidate<T> iTargetValidate = RegexValidator<T>.GetValidate(iValidateAttribute, pi);
                        RegisterValidate<T>(iTargetValidate);
                        continue;
                    }

                    if (iValidateAttribute is RequiredValidateAttribute)
                    {
                        IValidate<T> iTargetValidate = RequiredValidator<T>.GetValidate(iValidateAttribute, pi);
                        RegisterValidate<T>(iTargetValidate);
                        continue;
                    }
                }
            }
        }

        public static bool IsValid<T>(T objInstance)
        {
            lock (lockObject)
            {
                Type TType = typeof(T);
                int TTypeKey = TType.GetHashCode();

                if (!mTValidates.ContainsKey(TTypeKey))
                {
                    RegisterValidateType<T>();
                }

                Dictionary<int, object> dicValidates = mTValidates[TTypeKey];

                foreach (KeyValuePair<int, object> de in dicValidates)
                {
                    if (!((IValidate<T>)(de.Value)).Validation(objInstance))
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public static bool IsValid<T>(T objInstance, ref List<string> listErrorMessages)
        {
            lock (lockObject)
            {
                if (listErrorMessages == null)
                {
                    listErrorMessages = new List<string>();
                }
                Type TType = typeof(T);
                int TTypeKey = TType.GetHashCode();

                if (!mTValidates.ContainsKey(TTypeKey))
                {
                    RegisterValidateType<T>();
                }

                Dictionary<int, object> dicValidates = mTValidates[TTypeKey];

                foreach (KeyValuePair<int, object> de in dicValidates)
                {
                    if (!((IValidate<T>)(de.Value)).Validation(objInstance))
                    {
                        listErrorMessages.Add(((IValidate<T>)(de.Value)).ErrorMessage);
                    }
                }

                return listErrorMessages.Count == 0;
            }
        }

        public static bool IsValid<T>(T objInstance, ref Dictionary<int, Dictionary<int, List<string>>> listErrorMessages)
        {
            lock (lockObject)
            {
                if (listErrorMessages == null)
                {
                    listErrorMessages = new Dictionary<int, Dictionary<int, List<string>>>();
                }
                Type TType = typeof(T);
                int TTypeKey = TType.GetHashCode();

                if (!mTValidates.ContainsKey(TTypeKey))
                {
                    RegisterValidateType<T>();
                }

                int intTypeCount = listErrorMessages.Count;
                int intTypeInstanceCount = 0;

                if (listErrorMessages.ContainsKey(TTypeKey))
                {
                    intTypeInstanceCount = listErrorMessages[TTypeKey].Count;
                }

                Dictionary<int, object> dicValidates = mTValidates[TTypeKey];

                foreach (KeyValuePair<int, object> de in dicValidates)
                {
                    if (!((IValidate<T>)(de.Value)).Validation(objInstance))
                    {
                        if (listErrorMessages.ContainsKey(TTypeKey))
                        {
                            if (listErrorMessages[TTypeKey].ContainsKey(intTypeInstanceCount + 1))
                            {
                                listErrorMessages[TTypeKey][intTypeInstanceCount + 1].Add(((IValidate<T>)(de.Value)).ErrorMessage);
                            }
                            else
                            {
                                List<string> listInstanceErrorMessages = new List<string>();
                                listInstanceErrorMessages.Add(((IValidate<T>)(de.Value)).ErrorMessage);
                                listErrorMessages[TTypeKey].Add(intTypeInstanceCount + 1, listInstanceErrorMessages);
                            }
                        }
                        else
                        {
                            listErrorMessages.Add(TTypeKey, new Dictionary<int, List<string>>());
                            Dictionary<int, List<string>> dicInstances = new Dictionary<int, List<string>>();
                            List<string> listInstanceErrorMessages = new List<string>();
                            listInstanceErrorMessages.Add(((IValidate<T>)(de.Value)).ErrorMessage);
                            dicInstances.Add(intTypeInstanceCount + 1, listInstanceErrorMessages);
                            listErrorMessages[TTypeKey] = dicInstances;
                        }
                    }
                }

                return listErrorMessages.Count == 0;
            }
        }
    }
}
