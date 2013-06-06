using System;
using System.Web.UI;
using CN100.EnterprisePlatform.BLLFactory;
using CN100.EnterprisePlatform.IBLL;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        IUserBLL UserBLL = (IUserBLL)BLLAccess.CreateInterFace(BLLAccessEnum.BLLName.UserBLL);

        Page.Response.Write(UserBLL.Login("",""));
    }
}