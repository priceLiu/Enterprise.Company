using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CN100.EnterprisePlatform.Utility;

namespace CN100.EnterprisePlatform.CacheServerStatus
{
    public partial class CacheServerStatus : System.Web.UI.Page
    {
        public string strTD = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            DateTime datStart = DateTime.Now;


            //CN100Cache.Set("test","123");

            Dictionary<string, Dictionary<string, string>> d1 = CN100Cache.Status();
            Dictionary<string, Dictionary<string, string>> d2 = CN100Cache.Stats();

            List<string> list1 = new List<string>();
            List<string> list2 = new List<string>();

            foreach (KeyValuePair<string, Dictionary<string, string>> k in d1)
            {
                string item = "";

                item += string.Format("<p>{0}", k.Key);
                foreach (KeyValuePair<string, string> i in k.Value)
                {
                    item += string.Format("　　　　{0}：{1}<br/>", i.Key, i.Value);
                }
                item += string.Format("</p>");

                list1.Add(item);
            }

            foreach (KeyValuePair<string, Dictionary<string, string>> k in d2)
            {
                string item = "";

                item += string.Format("<p>{0}", k.Key);
                foreach (KeyValuePair<string, string> i in k.Value)
                {
                    item += string.Format("　　　　{0}：{1}<br/>", i.Key, i.Value);
                }
                item += string.Format("</p>");

                list2.Add(item);
            }



            if (list1.Count == list2.Count)
            {
                for (int i = 0; i < list2.Count; i++)
                {
                    strTD += string.Format("<tr><td>{0}</td><td>{1}</td></tr><tr><td colspan=2><hr/></td></tr>", list1[i], list2[i]);
                }
            }

            DateTime datEnd = DateTime.Now;

            strTD += string.Format("<tr><td colspan=2>耗时{0}</td></tr>", datEnd - datStart);
        }
    }
}