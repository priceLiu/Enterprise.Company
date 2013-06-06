using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CN100.EnterprisePlatform.Web.Core
{
    public abstract class HtmlElement
    {
        protected StringBuilder mAttributes = new StringBuilder();
        public HtmlElement Attr(HtmlAttribute attributre, string value)
        {
            return Attr(attributre.ToString().ToLower(), value);
        }
        public HtmlElement Attr(string attributre, string value)
        {
            mAttributes.Append(string.Format(" {0}=\"{1}\" ", attributre, value));
            return this;
        }
        public HtmlElement Accesskey(string value)
        {
            return Attr(HtmlAttribute.Accesskey, value);
        }
        public HtmlElement Class(string value)
        {
            return Attr(HtmlAttribute.Class, value);
        }
        public HtmlElement Dir(string value)
        {
            return Attr(HtmlAttribute.Dir, value);
        }
        public HtmlElement Disabled(string value)
        {
            return Attr(HtmlAttribute.Disabled, value);
        }
        public HtmlElement Id(string value)
        {
            return Attr(HtmlAttribute.Id, value);
        }
        public HtmlElement Lang(string value)
        {
            return Attr(HtmlAttribute.Lang, value);
        }
        public HtmlElement Maxlength(string value)
        {
            return Attr(HtmlAttribute.Maxlength, value);
        }
        public HtmlElement Name(string value)
        {
            return Attr(HtmlAttribute.Name, value);
        }
        public HtmlElement Readonly(string value)
        {
            return Attr(HtmlAttribute.Readonly, value);
        }
        public HtmlElement Size(string value)
        {
            return Attr(HtmlAttribute.Size, value);
        }
        public HtmlElement Style(string value)
        {
            return Attr(HtmlAttribute.Style, value);
        }
        public HtmlElement Tabindex(string value)
        {
            return Attr(HtmlAttribute.Tabindex, value);
        }
        public HtmlElement Title(string value)
        {
            return Attr(HtmlAttribute.Title, value);
        }
        public HtmlElement Value(object value)
        {
            string data = value == null ? "" : value.ToString();
            return Attr(HtmlAttribute.Value, data);
        }
        public HtmlElement Visible(string value)
        {
            return Attr(HtmlAttribute.Visible, value);
        }
        public HtmlElement Checked(bool value)
        {
            if (value)
                return Attr(HtmlAttribute.Checked, "Checked");
            return this;
        }
        public virtual HtmlElement Src(string value)
        {
            return Attr(HtmlAttribute.Src, value);
        }
        public HtmlElement CustomAttr(object attribute)
        {
            mAttributes.Append(" " + attribute + " ");
            return this;
        }
        public void Output()
        {
            System.Web.HttpContext.Current.Response.Write(ToString());
        }
        private IList<HtmlElement> mControls = new List<HtmlElement>();
        protected IList<HtmlElement> Controls
        {
            get
            {
                return mControls;
            }
        }

    }
    public enum HtmlAttribute
    {

        Accesskey,
        Class,
        Dir,
        Disabled,
        Id,
        Lang,
        Maxlength,
        Name,
        Readonly,
        Size,
        Style,
        Tabindex,
        Title,
        Value,
        Visible,
        Checked,
        Src



    }
    public enum InputType
    {
        button,
        checkbox,
        file,
        hidden,
        image,
        password,
        radio,
        reset,
        submit,
        text

    }
    public class Img : HtmlElement
    {
        public override string ToString()
        {
            return "<Img " + mAttributes.ToString() + "/>";
        }
        public override HtmlElement Src(string value)
        {
            return base.Src(ImageHelper.ImagePathHelper(value));
        }
       
    }
    public class InputControl : HtmlElement
    {
        public InputType Type
        {
            get;
            set;
        }
        public override string ToString()
        {

            return "<input type=\"" + Type.ToString() + "\"" + mAttributes.ToString() + "/>";
        }
    }
    public class Textarea : HtmlElement
    {
        public Textarea(string value)
        {
            if (string.IsNullOrEmpty(value))
                value = "";
            Text = value;
        }
        public string Text
        {
            get;
            set;
        }
        public override string ToString()
        {
            return "<textarea " + mAttributes.ToString() + ">" + Text + "</Textarea>";
        }
    }
    public delegate void EventSelectBind<T>(T data, Select.SelectItem item);
    public class Select : HtmlElement
    {
        public Select()
        {
            Items = new List<SelectItem>();
            Attr("multiple", "multiple");
        }
        public Select(bool multiple)
        {
            Items = new List<SelectItem>();
            if (multiple)
                Attr("multiple", "multiple");
        }


        public IList<SelectItem> Items
        {
            get;
            set;
        }
        public override string ToString()
        {
            StringBuilder html = new StringBuilder();
            html.Append("<select " + mAttributes.ToString() + ">");
            foreach (SelectItem item in Items)
            {
                html.Append(string.Format("<option value=\"{0}\" {2}>{1}</option>", string.IsNullOrEmpty(item.Value)?item.Name:item.Value, item.Name, item.Selected ? "selected" : ""));
            }
            html.Append("</select>");
            return html.ToString();
        }
        public class SelectItem
        {
            public string Name
            {
                get;
                set;
            }
            public string Value
            {
                get;
                set;
            }
            public bool Selected
            {
                get;
                set;
            }
        }
    }
    public class Link : HtmlElement
    {
        public Link(string url, string name)
        {
            Attr("href", url);
            Label = name;
        }
        public string Label
        { get; set; }
        public override string ToString()
        {
            return "<a " + mAttributes.ToString() + ">" + Label + "</a>";
        }
    }
}
