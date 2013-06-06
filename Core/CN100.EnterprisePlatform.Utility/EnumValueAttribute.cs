using System;

namespace CN100.EnterprisePlatform.Utility
{
    public class EnumValueAttribute : Attribute
    {
        object _dbValue;

        public object DbValue
        {
            get { return _dbValue; }
        }
        object _displayValue;

        public object DisplayValue
        {
            get { return _displayValue; }
        }

        public EnumValueAttribute(object dbValue, object displayValue)
        {
            _dbValue = dbValue;
            _displayValue = displayValue;
        }
    }
}