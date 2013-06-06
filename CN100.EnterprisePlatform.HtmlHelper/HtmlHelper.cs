using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CN100.EnterprisePlatform.HtmlHelper
{
public class HTMLHelper   
{
        public static HtmlElement Input(InputType type,string name)
        {
            return new InputControl { Type = type }.Name(name);
        }
        public static HtmlElement Img()
        {
            return new Img();
        }
        public static HtmlElement Textarea(string value, string name)
        {
            return new Textarea(value).Name(name);
        }
        public static HtmlElement Select<T>(string name, System.Collections.IEnumerable data, EventSelectBind<T> bind, bool Multiple,bool addnullitem)
        {
            Select select =(Select) new Select(Multiple).Name(name);
            Select.SelectItem si;
          
            if (addnullitem)
            {
                si = new Select.SelectItem();
                select.Items.Add(si);
            }
            if (data != null && bind != null)
            {
                foreach (T item in data)
                {
                    si = new Select.SelectItem();
                    select.Items.Add(si);
                    bind(item, si);
                }
            }
            return select;
        }
        public static HtmlElement SelectByIEnumerable(string name, bool multiple,bool addnullitem,IEnumerable<Select.SelectItem> items )
        {
            Select select = (Select)new Select(multiple).Name(name);
            if (addnullitem)
                select.Items.Add(new Select.SelectItem());
            foreach (Select.SelectItem item in items)
            {
                select.Items.Add(item);
            }
            return select;
        }
        public static HtmlElement Select(string name, bool multiple,bool addnullitem,params Select.SelectItem[] items)
        {
            return SelectByIEnumerable(name, multiple, addnullitem, items);
        }
        public static HtmlElement SelectByEnum(string name,Type enumtype, string selectvalue, bool multiple,bool addnullitem)
        {
            Select select = (Select)new Select(multiple).Name(name);
            Select.SelectItem si;
            if (addnullitem)
            {
                si = new Select.SelectItem();
                if (string.IsNullOrEmpty(selectvalue))
                    si.Selected = true;
                select.Items.Add(si);
            }
            foreach (object item in Enum.GetValues(enumtype))
            {
                si = new Select.SelectItem();
                si.Name = item.ToString();
                si.Value = item.ToString();
                if (!string.IsNullOrEmpty(selectvalue))
                    si.Selected = item.ToString() == Enum.Parse(enumtype, selectvalue).ToString();
                select.Items.Add(si);
                
            }
            
            return select;
        }
    }
}
