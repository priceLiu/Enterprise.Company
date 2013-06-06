
namespace CN100.EnterprisePlatform.Utility
{
    public class DBEnum
    {
        public enum ActionResult
        {
            SUCCESS = (int)1,
            FAIL = (int)-1
        }
        public enum UserDALValidationEnum
        {
            SUCCESS = (int)1,
            FAIL = (int)-1,
            Exist=(int)0,
            PasswordError = (int)2,
            UserNameNotExist = (int)3
        }
    }
}