﻿using System;
using System.Collections.Generic;
using System.Text;
using Smark.Core;
namespace CN100.EnterprisePlatform.Web.Core.Binders
{
    class ClassBinder
    {
        public ClassBinder(Type type)
        {
            ObjType = type;
            OnInit();
        }
        private void OnInit()
        {
            Handler = new InstanceHandler(ObjType);
            foreach (System.Reflection.PropertyInfo pi in ObjType.GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public))
            {
                
               if(pi.CanWrite && pi.GetIndexParameters().Length==0)
                    Properties.Add(new PropertyBinder(pi));
            }
            
        }
        public Type ObjType
        {
            get;
            set;
        }
        public InstanceHandler Handler
        {
            get;
            set;
        }
        private IList<PropertyBinder> mProperties = new List<PropertyBinder>();
        public IList<PropertyBinder> Properties
        {
            get
            {
                return mProperties;
            }
        }
        public object CreateObject(System.Collections.Specialized.NameValueCollection data, string Prefix)
        {
            object result =Handler.Instance();
            object pvalue = null;
            foreach (PropertyBinder pb in Properties)
            {
                if (pb.GetValue(data, Prefix, out pvalue))
                {
                    pb.Handler.Set(result, pvalue);
                }
                
            }
            return result;
        }
        public void FullData(object source, System.Collections.Specialized.NameValueCollection data, string prefix,bool ispostback)
        {
            object getvalue = null;
            object pvalue = null;
          
            foreach (PropertyBinder pb in Properties)
            {

                if (!ispostback || pb.ViewState ==null)
                {
                    if (pb.GetValue(data, prefix, out getvalue))
                    {
                        pb.Handler.Set(source, getvalue);
                    }
                }
                else
                {
                    if (pb.ViewState != null && pb.ViewState.ByPostData)
                    {
                        pvalue = pb.Handler.Get(source);
                        if (pvalue != null)
                        {
                            pb.FullValue(pvalue, data, prefix, ispostback);
                        }
                        else
                        {
                            if (pb.GetValue(data, prefix, out getvalue))
                            {
                                pb.Handler.Set(source, getvalue);
                            }
                        }
                    }
                    
                }
               
               
                
               
                
               
                

            }
        }
        public IList<ValidaterInfo> Validating(object obj)
        {
            IList<ValidaterInfo> result = new List<ValidaterInfo>();
            foreach (PropertyBinder pb in Properties)
            {
               
                foreach (ValidaterAttribute va in pb.Validaters)
                {
                    ValidaterInfo vi = va.Validating(pb.Handler.Get(obj),obj,pb.Handler);
                    if (vi.State == ValidaterState.Error)
                        result.Add(vi);
                }
            }
            return result;
        }
    }
}
