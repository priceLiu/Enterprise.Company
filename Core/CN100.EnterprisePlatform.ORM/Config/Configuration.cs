using System.Configuration;

namespace CN100.EnterprisePlatform.ORM.Config
{
    public class Configuration
    {
        //private static readonly NameValueCollection configuration = ((NameValueCollection)ConfigurationManager.GetSection("connectionStrings"));
        private const string DefaultSQLServerConnectionString = "Data Source=192.168.0.115,115;Initial Catalog=TESTDB;uid=DEV@CN100;pwd=dev@cn100.cn~!2012";
        private const string KeySQLServerConnectionString = "TESTDB";

        private static string GetConfigurationOrDefault(string configurationKey, string defaultValue)
        {
            string retValue = null;
            //if (configuration != null)
            //{
            //    retValue = configuration[configurationKey];
            //}
            retValue = ConfigurationManager.ConnectionStrings[KeySQLServerConnectionString].ConnectionString;
            if ((retValue == null) || (retValue.Trim().Length == 0))
            {
                retValue = defaultValue;
            }
            return retValue;
        }
        
        public static string SQLServerConnectionString
        {
            get
            {
                return GetConfigurationOrDefault(KeySQLServerConnectionString, DefaultSQLServerConnectionString);
            }    
        }

    }
}

