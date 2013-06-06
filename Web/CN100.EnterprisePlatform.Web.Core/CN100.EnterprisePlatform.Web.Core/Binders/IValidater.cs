using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Reflection;
using Smark.Core;
namespace CN100.EnterprisePlatform.Web.Core.Binders
{
    public enum ValidaterState
    {
        Ok,
        Error
    }
    public class ValidaterInfo
    {
        public ValidaterInfo()
        {
            State = ValidaterState.Ok;
        }
        public string Message
        {
            get;
            set;
        }
        public ValidaterState State
        {
            get;
            set;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public abstract class ValidaterAttribute : Attribute
    {
        public abstract ValidaterInfo Validating(object value,object source,PropertyHandler hander);
        public string Message
        {
            get;
            set;
        }
        
    }
    [AttributeUsage(AttributeTargets.Property)]
    public class NotNull : ValidaterAttribute
    {
        public NotNull(string message)
        {
            Message = message;
        }
        public override ValidaterInfo Validating(object value, object source, PropertyHandler hander)
        {
            ValidaterInfo vi = new ValidaterInfo();
            vi.Message = Message;
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                vi.State = ValidaterState.Error;
            }
            return vi;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class Length : ValidaterAttribute
    {
       
        public Length(string min, string max,string message)
        {
            if(!string.IsNullOrEmpty(min))
                MinLength = int.Parse(min);
            if(!string.IsNullOrEmpty(max))
                MaxLength =int.Parse(max);
            Message = message;
        }
        public int? MinLength
        {
            get;
            set;
        }
        public int? MaxLength
        {
            get;
            set;
        }
        public override ValidaterInfo Validating(object value, object source, PropertyHandler hander)
        {
            ValidaterInfo vi = new ValidaterInfo();
            vi.Message = Message;
            if (value != null && !string.IsNullOrEmpty(value.ToString()))
            {
                string data = Convert.ToString(value);
                if (MinLength != null && MinLength>data.Length)
                {
                    
                        vi.State = ValidaterState.Error;
                        return vi;
                   
                }

                if (MaxLength != null&& data.Length> MaxLength)
                {
                    
                        vi.State = ValidaterState.Error;
                        return vi;
                   
                }
            }
            return vi;

            
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class NumberRegion : ValidaterAttribute
    {
     
        public NumberRegion(string min,string max, string message)
        {
            if(!string.IsNullOrEmpty(min))
                MinValue = int.Parse(min);
            if(!string.IsNullOrEmpty(max))
                 MaxValue =int.Parse(max);
            Message = message;
        }
      
        public int? MinValue
        {
            get;
            set;
        }
        public int? MaxValue
        {
            get;
            set;
        }
        public override ValidaterInfo Validating(object value, object source, PropertyHandler hander)
        {
            ValidaterInfo vi = new ValidaterInfo();
            vi.Message = Message;
            if (value != null && !string.IsNullOrEmpty(value.ToString()))
            {
                int data = Convert.ToInt16(value);
                if (MinValue != null)
                {
                    if (MinValue > data)
                    {
                        vi.State = ValidaterState.Error;
                        return vi;
                    }
                }

                if (MaxValue != null)
                {
                    if (data> MaxValue)
                    {
                        vi.State = ValidaterState.Error;
                        return vi;
                    }
                }
            }
          
            return vi;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class DateRegion : ValidaterAttribute
    {
       
        public DateRegion(string mindate, string maxdate, string message)
        {
            if(!string.IsNullOrEmpty(mindate))
                MinValue = DateTime.Parse(mindate);
            if (!string.IsNullOrEmpty(maxdate))
                MaxValue = DateTime.Parse(maxdate);    
            
            Message = message;
        }
        public DateTime? MinValue
        {
            get;
            set;
        }
        public DateTime? MaxValue
        {
            get;
            set;
        }
        public override ValidaterInfo Validating(object value, object source, PropertyHandler hander)
        {
            ValidaterInfo vi = new ValidaterInfo();
            vi.Message = Message;
            if (value != null && !string.IsNullOrEmpty(value.ToString()))
            {
                DateTime data = Convert.ToDateTime(value);
                if (MinValue != null)
                {
                    if (MinValue > data)
                    {
                        vi.State = ValidaterState.Error;
                        return vi;
                    }
                }

                if (MaxValue != null)
                {
                    if (data > MaxValue)
                    {
                        vi.State = ValidaterState.Error;
                        return vi;
                    }
                }
            }

            return vi;
        }
    }
    [AttributeUsage(AttributeTargets.Property)]
    public class Match : ValidaterAttribute
    {
        public Match(string regex, string message)
        {
            Regex = regex;
            Message = message;
        }
        public string Regex
        {
            get;
            set;
        }
        public override ValidaterInfo Validating(object value, object source, PropertyHandler hander)
        {
            ValidaterInfo vi = new ValidaterInfo();
            vi.Message = Message;
            if (value != null && !string.IsNullOrEmpty(value.ToString()))
            {
                string data = Convert.ToString(value);
                if (System.Text.RegularExpressions.Regex.Match(
                    data, Regex, RegexOptions.IgnoreCase).Length==0)
                {
                    vi.State = ValidaterState.Error;
                }
                
            }
            return vi;
        }
    }
    [AttributeUsage(AttributeTargets.Property)]
    public class EMail : Match
    {
        public EMail(string msg) : base(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", msg) { }
    }

}
