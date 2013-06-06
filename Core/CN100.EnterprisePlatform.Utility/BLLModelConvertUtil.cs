using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Reflection;

namespace CN100.EnterprisePlatform.Utility
{
    public class BLLModelConvertUtil
    {
        internal static Dictionary<int, PropertyInfo[]> cachedPropertyInfos = new Dictionary<int, PropertyInfo[]>();

        internal static PropertyInfo GetPropertyInfo(string propertyName,PropertyInfo[] propertyInfos)
        {
            if (string.IsNullOrEmpty(propertyName) || string.IsNullOrWhiteSpace(propertyName) || propertyInfos == null || propertyInfos.Length == 0)
            {
                return null;
            }

            foreach (PropertyInfo pi in propertyInfos)
            {
                //if (propertyName.ToUpper() == pi.Name.ToUpper())
                //    return pi;

                if (string.Compare(propertyName, pi.Name, true) == 0)
                    return pi;
            }

            return null;
        }

        internal static PropertyInfo[] SetCachedPropertyInfos(Type type)
        {
            int key = type.GetHashCode();            

            if (cachedPropertyInfos.ContainsKey(key))
                return cachedPropertyInfos[key];

            PropertyInfo[] propertyInfos = type.GetProperties();

            lock (cachedPropertyInfos)
            {
                cachedPropertyInfos.Add(key, propertyInfos);
            }

            return propertyInfos;
        }

        public static T ToDALModel<T>(object objBLLModel)
        {
            try
            {
                object objRtn;

                if (objBLLModel != null)
                {
                    objRtn = Activator.CreateInstance(typeof(T));

                    if (objRtn != null)
                    {
                        Type objRtnType = objRtn.GetType();
                        Type objBLLModelType = objBLLModel.GetType();

                        PropertyInfo[] propertyInfos = SetCachedPropertyInfos(objRtnType);

                        foreach (PropertyInfo pi in objBLLModelType.GetProperties())
                        {
                            PropertyInfo piDALModel = GetPropertyInfo(pi.Name, propertyInfos);

                            if (piDALModel != null)
                            {
                                object objFieldValue = DynamicMethodCompiler.GetCachedGetPropertyHandlerDelegate(objBLLModelType, pi)(objBLLModel);
                                object objVal;

                                if (objFieldValue != null)
                                {
                                    Type[] GenericArgumentsTypes = piDALModel.PropertyType.GetGenericArguments();

                                    if (GenericArgumentsTypes.Count() > 0)
                                    {
                                        objVal = Convert.ChangeType(objFieldValue, GenericArgumentsTypes[0]);
                                    }
                                    else
                                    {
                                        objVal = Convert.ChangeType(objFieldValue, piDALModel.PropertyType);
                                    }
                                    DynamicMethodCompiler.GetCachedSetPropertyHandlerDelegate(objRtnType, piDALModel)(objRtn, objVal);
                                }
                            }

                            //if (piDALModel != null)
                            //    DynamicMethodCompiler.GetCachedSetPropertyHandlerDelegate(objRtn.GetType(), piDALModel)(objRtn, DynamicMethodCompiler.GetCachedGetPropertyHandlerDelegate(objBLLModel.GetType(), pi)(objBLLModel));
                        }
                    }

                    return (T)objRtn;
                }
                else
                {
                    return default(T);
                }
            }
            catch
            {
                return default(T);
            }
        }

        public static T FromDALModel<T>(object objDALModel)
        {
            try
            {
                object objRtn;

                if (objDALModel != null)
                {
                    objRtn = Activator.CreateInstance(typeof(T));

                    if (objRtn != null)
                    {
                        Type objRtnType = objRtn.GetType();
                        Type objDalModelType = objDALModel.GetType();

                        PropertyInfo[] propertyInfos = SetCachedPropertyInfos(objRtnType);

                        foreach (PropertyInfo pi in objDalModelType.GetProperties())
                        {                           
                            PropertyInfo piDALModel = GetPropertyInfo(pi.Name, propertyInfos);

                            if (piDALModel != null)
                            {
                                object objFieldValue = DynamicMethodCompiler.GetCachedGetPropertyHandlerDelegate(objDalModelType, pi)(objDALModel);
                                object objVal;

                                if (objFieldValue != null)
                                {
                                    Type[] GenericArgumentsTypes = piDALModel.PropertyType.GetGenericArguments();

                                    if (GenericArgumentsTypes.Count() > 0)
                                    {
                                        objVal = Convert.ChangeType(objFieldValue, GenericArgumentsTypes[0]);
                                    }
                                    else
                                    {
                                        objVal = Convert.ChangeType(objFieldValue, piDALModel.PropertyType);
                                    }
                                    DynamicMethodCompiler.GetCachedSetPropertyHandlerDelegate(objRtnType, piDALModel)(objRtn, objVal);
                                    //DynamicMethodCompiler.GetCachedSetPropertyHandlerDelegate(objRtn.GetType(), piDALModel)(objRtn, DynamicMethodCompiler.GetCachedGetPropertyHandlerDelegate(objDALModel.GetType(), pi)(objDALModel));
                                }
                            }
                        }
                    }

                    return (T)objRtn;
                }
                else
                {
                    return default(T);
                }
            }
            catch
            {
                throw;
            }
        }

        public static IList<T> ToDALModelList<T>(IList listBLLModel, bool boolReturnNullIfListIsNull=true)
        {
            IList<T> listRtn=null;            

            if (listBLLModel != null)
            {
                listRtn = new List<T>();
                foreach (object objBLLModel in listBLLModel)
                {
                    listRtn.Add(ToDALModel<T>(objBLLModel));
                }
            }
            else
            {
                if (!boolReturnNullIfListIsNull)
                {
                    listRtn = new List<T>();
                }
            }

            return listRtn;
        }

        public static IList<T> FromDALModelList<T>(IList listDALModel, bool boolReturnNullIfListIsNull = true)
        {
            IList<T> listRtn = null;            

            if (listDALModel != null)
            {
                listRtn = new List<T>();
                foreach (object objDALModel in listDALModel)
                {
                    listRtn.Add(FromDALModel<T>(objDALModel));
                }
            }
            else
            {
                if (!boolReturnNullIfListIsNull)
                {
                    listRtn = new List<T>();
                }
            }

            return listRtn;
        }

        public static IList<T> FastToDALModelList<T>(IList listBLLModel, bool boolReturnNullIfListIsNull = true)
        {
            if (listBLLModel == null)
            {
                if (boolReturnNullIfListIsNull)
                {
                    return null;
                }
                else
                {
                    return new List<T>();
                }
            }

            IList<T> listDALModelOne = new List<T>();

            IList<T> listDALModelTwo = new List<T>();

            IList<T> listRtn = new List<T>();

            ArrayList arrayThreadTotal = new ArrayList(listBLLModel);

            int intThreadOne = (int)(listBLLModel.Count / 2);

            object[] arrayThreadOne = new object[intThreadOne];

            arrayThreadTotal.CopyTo(0, arrayThreadOne, 0, arrayThreadOne.Length);

            object[] arrayThreadTwo = new object[listBLLModel.Count - intThreadOne];

            arrayThreadTotal.CopyTo(intThreadOne, arrayThreadTwo, 0, arrayThreadTwo.Length);

            var objListOne = from b in arrayThreadOne.Cast<object>()
                             select b;

            var objListTwo = from b in arrayThreadTwo.Cast<object>()
                             select b;

            DelegateToDalModelList<T> myDelegateToDalModelListOne = new DelegateToDalModelList<T>(ThreadToDALModelList<T>);
            DelegateToDalModelList<T> myDelegateToDalModelListTwo = new DelegateToDalModelList<T>(ThreadToDALModelList<T>);

            IAsyncResult iaOne = myDelegateToDalModelListOne.BeginInvoke(objListOne.ToList(), null, myDelegateToDalModelListOne);

            IAsyncResult iaTwo = myDelegateToDalModelListTwo.BeginInvoke(objListTwo.ToList(), null, myDelegateToDalModelListTwo);

            listDALModelOne = myDelegateToDalModelListOne.EndInvoke(iaOne);

            listDALModelTwo = myDelegateToDalModelListTwo.EndInvoke(iaTwo);

            IEnumerable<T> enu = listDALModelOne.Concat(listDALModelTwo);

            return enu.ToList();


        }

        public static IList<T> FastFromDALModelList<T>(IList listDALModel, bool boolReturnNullIfListIsNull = true)
        {
            if (listDALModel == null)
            {
                if (boolReturnNullIfListIsNull)
                {
                    return null;
                }
                else
                {
                    return new List<T>();
                }
            }

            IList<T> listBLLModelOne = new List<T>();

            IList<T> listBLLModelTwo = new List<T>();

            IList<T> listRtn = new List<T>();

            ArrayList arrayThreadTotal = new ArrayList(listDALModel);

            int intThreadOne = (int)(listDALModel.Count / 2);

            object[] arrayThreadOne = new object[intThreadOne];

            arrayThreadTotal.CopyTo(0, arrayThreadOne, 0, arrayThreadOne.Length);

            object[] arrayThreadTwo = new object[listDALModel.Count - intThreadOne];

            arrayThreadTotal.CopyTo(intThreadOne, arrayThreadTwo, 0, arrayThreadTwo.Length);

            var objListOne = from b in arrayThreadOne.Cast<object>()
                             select b;

            var objListTwo = from b in arrayThreadTwo.Cast<object>()
                             select b;

            DelegateFromDalModelList<T> myDelegateFromDalModelListOne = new DelegateFromDalModelList<T>(ThreadFromDALModelList<T>);
            DelegateFromDalModelList<T> myDelegateFromDalModelListTwo = new DelegateFromDalModelList<T>(ThreadFromDALModelList<T>);

            IAsyncResult iaOne = myDelegateFromDalModelListOne.BeginInvoke(objListOne.ToList(), null, myDelegateFromDalModelListOne);

            IAsyncResult iaTwo = myDelegateFromDalModelListTwo.BeginInvoke(objListTwo.ToList(), null, myDelegateFromDalModelListTwo);

            listBLLModelOne = myDelegateFromDalModelListOne.EndInvoke(iaOne);

            listBLLModelTwo = myDelegateFromDalModelListTwo.EndInvoke(iaTwo);

            IEnumerable<T> enu = listBLLModelOne.Concat(listBLLModelTwo);

            return enu.ToList();
        }
        
        private delegate IList<T> DelegateToDalModelList<T>(IList listBLLModel);

        private delegate IList<T> DelegateFromDalModelList<T>(IList listDALModel);

        private static IList<T> ThreadToDALModelList<T>(IList listBLLModel)
        {
            IList<T> listRtn = new List<T>();

            if (listBLLModel != null)
            {
                foreach (object objBLLModel in listBLLModel)
                {
                    listRtn.Add(ToDALModel<T>(objBLLModel));
                }
            }

            return listRtn;
        }

        private static IList<T> ThreadFromDALModelList<T>(IList listDALModel)
        {
            IList<T> listRtn = new List<T>();

            if (listDALModel != null)
            {
                foreach (object objDALModel in listDALModel)
                {
                    listRtn.Add(FromDALModel<T>(objDALModel));
                }
            }

            return listRtn;
        }
    }
}
