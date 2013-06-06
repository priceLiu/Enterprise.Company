using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
namespace CN100.EnterprisePlatform.Web.Core.Binders
{

    class ToEnum : IConvert
    {

        #region IConvert 成员

        public object Parse(System.Collections.Specialized.NameValueCollection data, string key, string prefix, out bool succeed)
        {
            
            
            string input = ConverCore.GetValue(data, key, prefix);
            if (string.IsNullOrEmpty(input))
                succeed = false;
            else
                succeed = true;
            return input;

        }

        #endregion
    }
     class ToEnumArray : IConvert
    {
        public Type EnumType
        {
            get;
            set;
        }
        #region IConvert 成员

        public object Parse(System.Collections.Specialized.NameValueCollection data, string key, string prefix, out bool succeed)
        {
            Array result = Array.CreateInstance(EnumType,0);
            
            object value;
            string[] values = ConverCore.GetValues(data, key, prefix);
            if (values != null)
            {
                result =Array.CreateInstance(EnumType,values.Length);
                for(int i =0;i< values.Length;i++)
                {
                    if (string.IsNullOrEmpty(values[i]))
                    {
                        result.SetValue(Enum.GetValues(EnumType).GetValue(0),i);
                    }
                    else
                    {
                        result.SetValue(Enum.Parse(EnumType, values[i]), i);
                    }
                }
              
            }
            succeed = true;
            return result;

        }

        #endregion
    }
    public class ToEnum<T> : IConvert where T : struct
    {

        #region IConvert 成员

        public object Parse(System.Collections.Specialized.NameValueCollection data, string key, string prefix, out bool succeed)
        {

            string input = ConverCore.GetValue(data, key, prefix);
            if (string.IsNullOrEmpty(input))
            {
                succeed = false;
                return null;
            }
            else
            {
                succeed = true;
                return Enum.Parse(typeof(T), input);
            }


        }

        #endregion
    }

    [ConvertAttribute(typeof(Int16))]
    [ConvertAttribute(typeof(Nullable<Int16>))]
    class ToInt16 : IConvert
    {
        #region IConvert 成员

        public object Parse(System.Collections.Specialized.NameValueCollection data, string key, string prefix, out bool succeed)
        {

            string input = ConverCore.GetValue(data, key, prefix);
            Int16 result = 0;
            succeed = Int16.TryParse(input,out result);
            return result;
        }

        #endregion
    }

    [ConvertAttribute(typeof(Int16[]))]
    [ConvertAttribute(typeof(Nullable<Int16>[]))]
    class ToInt16Array : ToArray<Int16>
    {
        protected override bool Parse(string value, out Int16 result)
        {
            return Int16.TryParse(value,out result);
        }
    }

    [ConvertAttribute(typeof(Int32))]
    [ConvertAttribute(typeof(Nullable<Int32>))]
    class ToInt32 : IConvert
    {
        #region IConvert 成员

        public object Parse(System.Collections.Specialized.NameValueCollection data, string key, string prefix, out bool succeed)
        {
            string input = ConverCore.GetValue(data, key, prefix);
            Int32 result = 0;
            succeed = Int32.TryParse(input, out result);
            return result;
        }

        #endregion
    }
    [ConvertAttribute(typeof(Int32[]))]
    [ConvertAttribute(typeof(Nullable<Int32>[]))]
    class ToInt32Array : ToArray<Int32>
    {
        protected override bool Parse(string value, out Int32 result)
        {
            return Int32.TryParse(value, out result);
        }
    }


    [ConvertAttribute(typeof(Int64))]
    [ConvertAttribute(typeof(Nullable<Int64>))]
    class ToInt64 : IConvert
    {
        #region IConvert 成员

        public object Parse(System.Collections.Specialized.NameValueCollection data, string key, string prefix, out bool succeed)
        {
            string input = ConverCore.GetValue(data, key, prefix);
            Int64 result = 0;
            succeed = Int64.TryParse(input, out result);
            return result;
        }

        #endregion
    }
    [ConvertAttribute(typeof(Int64[]))]
    [ConvertAttribute(typeof(Nullable<Int64>[]))]
    class ToInt64Array : ToArray<Int64>
    {
        protected override bool Parse(string value, out Int64 result)
        {
            return Int64.TryParse(value, out result);
        }
    }

    [Convert(typeof(float))]
    [Convert(typeof(Nullable<float>))]
    class ToFloat : IConvert
    {
        #region IConvert 成员

        public object Parse(System.Collections.Specialized.NameValueCollection data, string key, string prefix, out bool succeed)
        {
            string input = ConverCore.GetValue(data, key, prefix);
            float result = 0;
            succeed = float.TryParse(input, out result);
            return result;
        }

        #endregion
    }
    [ConvertAttribute(typeof(float[]))]
    [Convert(typeof(Nullable<float>[]))]
    class ToFloatArray : ToArray<float>
    {
        protected override bool Parse(string value, out float result)
        {
            return float.TryParse(value, out result);
        }
    }

    [Convert(typeof(Boolean))]
    [Convert(typeof(Nullable<Boolean>))]
    class ToBoolean : IConvert
    {
        #region IConvert 成员

        public object Parse(System.Collections.Specialized.NameValueCollection data, string key, string prefix, out bool succeed)
        {
            string input = ConverCore.GetValue(data, key, prefix);
            Boolean result = false;
            succeed = Boolean.TryParse(input, out result);
            return result;
        }

        #endregion
    }

    [ConvertAttribute(typeof(Boolean[]))]
    [Convert(typeof(Nullable<Boolean>[]))]
    class ToBooleanArray : ToArray<Boolean>
    {
        protected override bool Parse(string value, out Boolean result)
        {
            return Boolean.TryParse(value, out result);
        }
    }

    [Convert(typeof(Double))]
    [Convert(typeof(Nullable<double>))]
    class ToDouble : IConvert
    {
        #region IConvert 成员

        public object Parse(System.Collections.Specialized.NameValueCollection data, string key, string prefix, out bool succeed)
        {
            string input = ConverCore.GetValue(data, key, prefix);
            Double result = 0;
            succeed = Double.TryParse(input, out result);
            return result;
        }

        #endregion
    }
    [ConvertAttribute(typeof(Double[]))]
    [Convert(typeof(Nullable<double>[]))]
    class ToDoubleArray : ToArray<Double>
    {
        protected override bool Parse(string value, out Double result)
        {
            return Double.TryParse(value, out result);
        }
    }


    [Convert(typeof(String))]
    class ToString : IConvert
    {
        #region IConvert 成员

        public object Parse(System.Collections.Specialized.NameValueCollection data, string key, string prefix, out bool succeed)
        {
            string input = ConverCore.GetValue(data, key, prefix);
            if (string.IsNullOrEmpty(input))
                succeed = false;
            else
                succeed = true;
            return input;
        }

        #endregion
    }
    [Convert(typeof(String[]))]
    class ToStringArray : IConvert
    {

        #region IConvert 成员

        public object Parse(System.Collections.Specialized.NameValueCollection data, string key, string prefix, out bool succeed)
        {
            string[] values = ConverCore.GetValues(data, key, prefix);
            succeed = true;
            if (values == null)
                values = new string[0];
            return values;
        }

        #endregion
    }

    
    [Convert(typeof(byte))]
    [Convert(typeof(Nullable<byte>))]
    class ToByte : IConvert
    {
        #region IConvert 成员

        public object Parse(System.Collections.Specialized.NameValueCollection data, string key, string prefix, out bool succeed)
        {
            string input = ConverCore.GetValue(data, key, prefix);
            Byte result = 0;
            succeed = Byte.TryParse(input, out result);
            return result;
            
        }

        #endregion
    }
    [ConvertAttribute(typeof(byte[]))]
    [Convert(typeof(Nullable<byte>[]))]
    class ToByteArray : ToArray<byte>
    {
        protected override bool Parse(string value, out byte result)
        {
            return byte.TryParse(value, out result);
        }
    }

    [Convert(typeof(Decimal))]
    [Convert(typeof(Nullable<decimal>))]
    class ToDecimal : IConvert
    {

        #region IConvert 成员

        public object Parse(System.Collections.Specialized.NameValueCollection data, string key, string prefix, out bool succeed)
        {
            string input = ConverCore.GetValue(data, key, prefix);
            decimal result = 0;
            succeed = decimal.TryParse(input, out result);
            return result;
        }

        #endregion
    }
    [ConvertAttribute(typeof(Decimal[]))]
    [Convert(typeof(Nullable<decimal>[]))]
    class ToDecimalArray : ToArray<Decimal>
    {
        protected override bool Parse(string value, out Decimal result)
        {
            return Decimal.TryParse(value, out result);
        }
    }


    [Convert(typeof(DateTime))]
    [Convert(typeof(Nullable<DateTime>))]
    class ToDateTime : IConvert
    {
        #region IConvert 成员

        public object Parse(System.Collections.Specialized.NameValueCollection data, string key, string prefix, out bool succeed)
        {
            string input = ConverCore.GetValue(data, key, prefix);
            DateTime result;
            succeed = DateTime.TryParse(input, out result);
            return result;
        }

        #endregion
    }
    [ConvertAttribute(typeof(DateTime[]))]
    [Convert(typeof(Nullable<DateTime>[]))]
    class ToDateTimeArray : ToArray<DateTime>
    {
        protected override bool Parse(string value, out DateTime result)
        {
            return DateTime.TryParse(value, out result);
        }
    }
    public class ToIList<T> : IConvert where T : new()
    {

        private static List<System.Reflection.PropertyInfo> mProperties = null;
        public static List<System.Reflection.PropertyInfo> Properties
        {
            get
            {
                lock (typeof(ToIList<T>))
                {
                    if (mProperties == null)
                    {
                        mProperties = new List<System.Reflection.PropertyInfo>();
                        foreach (System.Reflection.PropertyInfo pi in typeof(T).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
                        {
                            mProperties.Add(pi);
                        }
                    }
                    return mProperties;
                }
            }
        }
        #region IConvert 成员

        public object Parse(System.Collections.Specialized.NameValueCollection data, string key, string prefix, out bool succeed)
        {
            IList<T> items = null;
            succeed = true;
            System.Type itemstype = System.Type.GetType("System.Collections.Generic.List`1");
            itemstype = itemstype.MakeGenericType(typeof(T));
            items = (IList<T>)Activator.CreateInstance(itemstype);
            int count=0;
            string[] values;
            Dictionary<System.Reflection.PropertyInfo, string[]> valueCollection = new Dictionary<System.Reflection.PropertyInfo, string[]>();
            foreach (System.Reflection.PropertyInfo pi in Properties)
            {
                values = ConverCore.GetValues(data, pi.Name, prefix);
                if (values!=null && values.Length > count)
                    count = values.Length;
                valueCollection.Add(pi, values);
            }
           
            for (int i = 0; i < count; i++)
            {
                System.Collections.Specialized.NameValueCollection itemdata = new System.Collections.Specialized.NameValueCollection();
                foreach (System.Reflection.PropertyInfo pi in Properties)
                {
                    values = valueCollection[pi];
                    if (values != null && i < values.Length)
                        itemdata.Add(pi.Name, values[i]);
                }
                T item = BinderAdapter.CreateInstance<T>(itemdata);
                items.Add(item);
            }
            return items;
        }

        #endregion
    }
    abstract class ToArray<T> : IConvert where T:struct
    {

        #region IConvert 成员

        public object Parse(System.Collections.Specialized.NameValueCollection data, string key, string prefix, out bool succeed)
        {
            T[] results = new T[0];
            T result;
            succeed = true;
            string[] values = ConverCore.GetValues(data, key, prefix);
            succeed = (values != null && values.Length > 0);
            if (values != null)
            {
                results = new T[values.Length];
                for (int i = 0; i < values.Length; i++)
                {
                    if (Parse(values[i], out result))
                    {
                        results[i] = result;
                    }
                }
            }
            
            return results;
        }
        protected abstract bool Parse(string value, out T result);
        
        #endregion
    }
   
}
