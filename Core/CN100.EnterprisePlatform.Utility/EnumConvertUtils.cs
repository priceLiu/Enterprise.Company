using System;
using System.Collections.Generic;
using System.Reflection;

namespace CN100.EnterprisePlatform.Utility
{
    public class EnumConvertUtils
    {
        public static object ToDbValue(Enum value)
        {
            if (value == null)
            {
                return null;
            }
            BidirHashtable<object, EnumValueAttribute> map
                = EnumToAttributeMap(value.GetType());
            return map[value].DbValue;
        }

        public static object ToDisplayValue(Enum value)
        {
            if (value == null)
            {
                return null;
            }
            BidirHashtable<object, EnumValueAttribute> map
                = EnumToAttributeMap(value.GetType());
            return map[value].DisplayValue;
        }

        public static object DbValueToEnum<T>(object dbValue)
        {
            if (Convert.IsDBNull(dbValue))
            {
                return null;
            }
            else
            {
                BidirHashtable<object, object> map = EnumToDbValueMap(typeof(T));
                return map.ReverseLookup(dbValue);
            }
        }

        public static object DisplayValueToEnum<T>(object displayValue)
        {
            BidirHashtable<object, EnumValueAttribute> map = EnumToAttributeMap(typeof(T));

            foreach (string e in Enum.GetNames(typeof(T)))
            {
                if (map[Enum.Parse(typeof(T), e)].DisplayValue.Equals(displayValue))
                    return Enum.Parse(typeof(T), e);
            }
            return null;
        }

        public static IDictionary<object, object> ToDictionary<T>()
        {
            IDictionary<object, object> map = new Dictionary<object, object>();
            IDictionary<object, EnumValueAttribute> map2 = EnumToAttributeMap(typeof(T));
            foreach (object key in map2.Keys)
            {
                map.Add(map2[key].DbValue, map2[key].DisplayValue);
            }
            return map;
        }

        public static BidirHashtable<object, EnumValueAttribute> EnumToAttributeMap(Type enumType)
        {
            BidirHashtable<object, EnumValueAttribute> retval
                = new BidirHashtable<object, EnumValueAttribute>();

            foreach (FieldInfo fi in enumType.GetFields())
            {
                if (fi.FieldType.BaseType == typeof(Enum))
                {
                    EnumValueAttribute[] attrs =
                        (EnumValueAttribute[])fi.GetCustomAttributes(
                        typeof(EnumValueAttribute), false);
                    if (attrs.Length > 0)
                    {
                        retval.Add(Enum.Parse(enumType, fi.Name), attrs[0]);
                    }
                }
            }
            return retval;
        }

        private static BidirHashtable<object, object> EnumToDbValueMap(Type enumType)
        {
            BidirHashtable<object, object> retval
                = new BidirHashtable<object, object>();

            foreach (FieldInfo fi in enumType.GetFields())
            {
                if (fi.FieldType.BaseType == typeof(Enum))
                {
                    EnumValueAttribute[] attrs =
                        (EnumValueAttribute[])fi.GetCustomAttributes(
                        typeof(EnumValueAttribute), false);
                    if (attrs.Length > 0)
                    {
                        retval.Add(Enum.Parse(enumType, fi.Name), attrs[0].DbValue);
                    }
                }
            }
            return retval;
        }
    }
}