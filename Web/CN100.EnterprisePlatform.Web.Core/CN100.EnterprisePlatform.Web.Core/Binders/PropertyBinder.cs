using System;
using System.Collections.Generic;
using System.Text;
using Smark.Core;
namespace CN100.EnterprisePlatform.Web.Core.Binders
{

    class ParameterBinder
    {
        public ParameterBinder(System.Reflection.ParameterInfo pi)
        {
            Info = pi;
            BindAttribute[] bas = Functions.GetParemeterAttributes<BindAttribute>(pi, false);
            if (bas.Length > 0)
                Binder = bas[0];
            ViewStateAttribute[] vsa = Functions.GetParemeterAttributes<ViewStateAttribute>(pi, false);
            if (vsa.Length > 0)
                ViewState = vsa[0];
            foreach (ValidaterAttribute va in Functions.GetParemeterAttributes<ValidaterAttribute>(pi, false))
            {
                Validaters.Add(va);
            }
            mIsOut = Functions.GetParemeterAttributes<OutputAttribute>(pi, false).Length > 0;
        }
        public ViewStateAttribute ViewState
        {
            get;
            set;
        }
        private bool mIsOut = false;
        public bool IsOutput
        {
            get
            {
                return mIsOut;
            }
        }
        public System.Reflection.ParameterInfo Info
        {
            get;
            set;
        }
        public BindAttribute Binder
        {
            get;
            set;
        }
        private IList<ValidaterAttribute> mValidaters = new List<ValidaterAttribute>();
        public IList<ValidaterAttribute> Validaters
        {
            get
            {
                return mValidaters;
            }
        }
        private bool GetValueTypeValue(System.Collections.Specialized.NameValueCollection data, string Prefix, out object value)
        {
            IConvert convert = null;
            value = null;
            Type createtype = Info.ParameterType;
            if (Binder != null && Binder.Convert == null && Binder.Fungible != null)
                createtype = Binder.Fungible;
            bool succed = false;
            if (Binder != null && !string.IsNullOrEmpty(Binder.Prefix))
                Prefix = Binder.Prefix;
            if (Binder != null)
                convert = Binder.GetConvert();
            if (convert == null)
            {
                if (ConverCore.Converts.ContainsKey(createtype))
                    convert = ConverCore.Converts[createtype];
            }
            if (convert != null)
            {
                value = convert.Parse(data, Info.Name, Prefix, out succed);
            }
            else
            {
                if (createtype.IsEnum)
                {
                    string pvalue = (string)new ToEnum().Parse(data, Info.Name, Prefix, out succed);
                    if (string.IsNullOrEmpty(pvalue))
                        value = Enum.GetValues(createtype).GetValue(0);
                    else
                        value = Enum.Parse(createtype, pvalue);

                }

            }
            return succed;
        }
        private bool GetClassValue(System.Collections.Specialized.NameValueCollection data, string Prefix, out object value)
        {
            IConvert convert = null;
            value = null;
            Type createtype = Info.ParameterType;
            if (Binder != null && Binder.Convert == null && Binder.Fungible != null)
                createtype = Binder.Fungible;
            bool succed = false;
            if (Binder != null && !string.IsNullOrEmpty(Binder.Prefix))
                Prefix = Binder.Prefix;
            if (Binder != null)
                convert = Binder.GetConvert();
            if (convert == null)
            {
                if (ConverCore.Converts.ContainsKey(createtype))
                    convert = ConverCore.Converts[createtype];
            }
            if (convert != null)
            {
                value = convert.Parse(data, Info.Name, Prefix, out succed);
            }
            else if (createtype.IsArray)
            {
                if (createtype.GetElementType().IsEnum)
                {
                    ToEnumArray tea = new ToEnumArray();
                    tea.EnumType = createtype.GetElementType();
                    value = tea.Parse(data, Info.Name, Prefix, out succed);
                }
            }
            else
            {
                if (createtype.IsClass && !createtype.IsInterface &&
                     !createtype.IsAbstract)
                {
                    ClassBinder cb = ConverCore.GetBinder(createtype);
                    succed = true;
                    value = cb.CreateObject(data, Prefix);
                }
            }

            return succed;
        }
        public bool GetValue(System.Collections.Specialized.NameValueCollection data, string Prefix, out object value)
        {
            Type createtype = Info.ParameterType;
            if (createtype.IsValueType || createtype.IsEnum || createtype == typeof(string))
            {
                return GetValueTypeValue(data, Prefix, out value);
            }
            else
            {
                return GetClassValue(data, Prefix, out value);
            }

        }
        
    }
    class PropertyBinder
    {
        public PropertyBinder(System.Reflection.PropertyInfo pi)
        {
            Handler = new PropertyHandler(pi);
            BindAttribute[] bas = Functions.GetPropertyAttributes<BindAttribute>(pi, false);
            if (bas.Length > 0)
                Binder = bas[0];
            ViewStateAttribute[] vsa = Functions.GetPropertyAttributes<ViewStateAttribute>(pi, false);
            if (vsa.Length > 0)
                ViewState = vsa[0];
            foreach (ValidaterAttribute va in Functions.GetPropertyAttributes<ValidaterAttribute>(pi, false))
            {
                Validaters.Add(va);
            }
        }
        public ViewStateAttribute ViewState
        {
            get;
            set;
        }
        public PropertyHandler Handler
        {
            get;
            set;
        }
        public BindAttribute Binder
        {
            get;
            set;
        }
        private IList<ValidaterAttribute> mValidaters = new List<ValidaterAttribute>();
        public IList<ValidaterAttribute> Validaters
        {
            get
            {
                return mValidaters;
            }
        }
        public bool GetValueTypeValue(System.Collections.Specialized.NameValueCollection data, string Prefix, out object value)
        {
            IConvert convert = null;
            value = null;
            Type createtype = Handler.Property.PropertyType;
            if (Binder != null && Binder.Convert == null && Binder.Fungible != null)
                createtype = Binder.Fungible;
            bool succed = false;
            if (Binder != null && !string.IsNullOrEmpty(Binder.Prefix))
                Prefix = Binder.Prefix;
            if (Binder != null)
                convert = Binder.GetConvert();
            if (convert == null)
            {
                if (ConverCore.Converts.ContainsKey(createtype))
                    convert = ConverCore.Converts[createtype];
            }
            if (convert != null)
            {
                value = convert.Parse(data, Handler.Property.Name, Prefix, out succed);
            }
            else
            {
                if (createtype.IsEnum)
                {
                    string pvalue = (string)new ToEnum().Parse(data, Handler.Property.Name, Prefix, out succed);
                    if (string.IsNullOrEmpty(pvalue))
                        value = Enum.GetValues(createtype).GetValue(0);
                    else
                        value = Enum.Parse(createtype, pvalue);

                }

            }
            return succed;
        }
        public bool GetClassValue(System.Collections.Specialized.NameValueCollection data, string Prefix, out object value)
        {
            IConvert convert = null;
            value = null;
            Type createtype = Handler.Property.PropertyType;
            if (Binder != null && Binder.Convert == null && Binder.Fungible != null)
                createtype = Binder.Fungible;
            bool succed = false;
            if (Binder != null && !string.IsNullOrEmpty(Binder.Prefix))
                Prefix = Binder.Prefix;
            if (Binder != null)
                convert = Binder.GetConvert();
            if (convert == null)
            {
                if (ConverCore.Converts.ContainsKey(createtype))
                    convert = ConverCore.Converts[createtype];
            }
            if (convert != null)
            {
                value = convert.Parse(data, Handler.Property.Name, Prefix, out succed);
            }
            else
            {
               if (createtype.IsClass && !createtype.IsInterface &&
                    !createtype.IsAbstract)
                {
                    if (createtype.IsArray)
                    {
                        succed = true;
                        value = Activator.CreateInstance(createtype, 0);
                    }
                    else
                    {
                        ClassBinder cb = ConverCore.GetBinder(createtype);
                        succed = true;
                        value = cb.CreateObject(data, Prefix);
                    }
                }
            }

            return succed;
        }
        public bool GetValue(System.Collections.Specialized.NameValueCollection data, string Prefix, out object value)
        {
            Type createtype = Handler.Property.PropertyType;
            if (createtype.IsValueType || createtype.IsEnum|| createtype == typeof(string))
            {
                return GetValueTypeValue(data, Prefix, out value);
            }
            else
            {
                return GetClassValue(data, Prefix, out value);
            }
            
        }
        public void FullValue(object source, System.Collections.Specialized.NameValueCollection data, string Prefix, bool ispostback)
        {
            BinderAdapter.Full(source, data, Prefix,ispostback);
        }
    }
}
