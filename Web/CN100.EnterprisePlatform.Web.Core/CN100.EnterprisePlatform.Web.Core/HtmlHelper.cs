using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
namespace CN100.EnterprisePlatform.Web.Core
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
        public static HtmlElement A(string text,string url)
        {
            Link link = new Link(url, text);
            return link;
        }
        public static HtmlElement A(string text, string url, params string[] urlparams)
        {
            Link link = new Link(string.Format( url,urlparams), text);
            return link;
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
        public static void Include(System.Web.UI.Page page, string controlfile)
        {
            page.LoadControl(controlfile).RenderControl(new HtmlTextWriter(HttpContext.Current.Response.Output));
        }

        public static LoadControl Include(System.Web.UI.Page page)
        {
            return new LoadControl() { Page= page };
        }
        public static WebContext SetProperty(string name, object value)
        {
            WebContext.Context[name] = value;
            return WebContext.Context;
        }
        public static object GetProperty(string name)
        {
            return WebContext.Context[name];
        }
        public static void Each<T>(IEnumerable<T> data, Action<EachItem<T>> view)
        {
            int i = 0;
            foreach (T item in data)
            {
                EachItem<T> ei = new EachItem<T> { Data= item, Index=i };
                view(ei);
                i++;
            }

        }
        public class EachItem<T>
        {
            public T Data
            {
                get;
                set;
            }
            public int Index
            {
                get;
                set;
            }
        }

        public class LoadControl
        {
            public System.Web.UI.Page Page
            {
                get;
                set;
            }
            public LoadControl SetProperty(string name, object value)
            {
                HTMLHelper.SetProperty(name, value);
                return this;
            }
            public void Include(string controlfile)
            {
                Page.LoadControl(controlfile).RenderControl(new HtmlTextWriter(HttpContext.Current.Response.Output));
            }
        }
    }
}
