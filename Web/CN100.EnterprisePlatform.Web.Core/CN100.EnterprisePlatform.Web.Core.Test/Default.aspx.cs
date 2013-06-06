using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CN100.EnterprisePlatform.Web.Core;
namespace CN100.EnterprisePlatform.Web.Core.Test
{
    public partial class _Default : System.Web.UI.Page
    {
        public List<Code.User> Users
        {
            get;
            set;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            string url= ImageHelper.ImagePathHelper("00320120821006026f49a990c14bf5abd441b5faff8d43.jpg");
            Response.Write(url);
            //Users = new List<Code.User>();
            //Users.Add(new Code.User { Name = "adsf1", EMail = "sdfsd@msn.com" });
            //Users.Add(new Code.User { Name = "adsf2", EMail = "sdfsd@msn.com" });
            //Users.Add(new Code.User { Name = "adsf3", EMail = "sdfsd@msn.com" });
           
        }
    }
}
