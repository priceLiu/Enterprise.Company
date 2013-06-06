using CN100.EnterprisePlatform.ORM.Config;
using CN100.EnterprisePlatform.ORM.DB;

namespace CN100.EnterprisePlatform.ORM
{
    //public class DbFactory
    //{
    //    public static readonly DbFactory Instance = new DbFactory();

    //    private SqlProvider provider;

    //    private DbFactory()
    //    {
    //        string connectString = Configuration.SQLServerConnectionString;
    //        provider = new SqlProvider(connectString);
    //    }

    //    public IDb GetDb()
    //    {
    //        return provider.OpenDb();
    //    }

    //    public IDb GetDb(string sqlConnectionString)
    //    {
    //        provider = new SqlProvider(sqlConnectionString);
    //        return provider.OpenDb();
    //    }
    //}

    //public class DbFactory
    //{
    //    public static readonly DbFactory Instance = new DbFactory();

    //    private IProvider provider;

    //    private IDBNode _defaultDBNode;

    //    private ActionEnum _actionEnum=ActionEnum.ReadWrite;

    //    private CustomDBProxyConfigManagement customDBProxyConfigManagement;

    //    private bool boolInitalProvider = false;

    //    private DbFactory()
    //    {
    //    }

    //    //public IDb GetDb()
    //    //{
    //    //    string connectString = Configuration.SQLServerConnectionString;
    //    //    provider = new SqlProvider(connectString);
    //    //    return provider.OpenDb();
    //    //}

    //    //public IDb GetDb(string sqlConnectionString)
    //    //{
    //    //    provider = new SqlProvider(sqlConnectionString);
    //    //    return provider.OpenDb();
    //    //}

    //    public IDBNode DefaultDBNode
    //    {
    //        get 
    //        {
    //            return _defaultDBNode;
    //        }
    //        set
    //        {
    //            _defaultDBNode = value;
    //            InitalProvider(ref provider);
    //        }
    //    }

    //    public ActionEnum DefaultAction
    //    {
    //        get
    //        {
    //            return _actionEnum;
    //        }
    //        set
    //        {
    //            _actionEnum = value;
    //        }
    //    }

    //    public IDb GetDb()
    //    {
    //        if (!boolInitalProvider)
    //        {
    //            InitalProvider(ref provider);
    //            boolInitalProvider = true;
    //        }
    //        return provider.OpenDb();
    //    }

    //    public IDb GetDb(string sqlConnectionString)
    //    {
    //        IProvider sqlProvider = new SqlProvider(sqlConnectionString);
    //        return sqlProvider.OpenDb();
    //    }

    //    public IDb GetDb(IDBNode idbNode,ActionEnum actionEnum)
    //    {
    //        IProvider tempProvider = null;
    //        InitalProvider(idbNode, actionEnum, ref tempProvider);
    //        return tempProvider.OpenDb();
    //    }

    //    private void InitalProvider(ref IProvider tempProvider)
    //    {
    //        InitalProvider(_defaultDBNode, _actionEnum, ref tempProvider);
    //    }

    //    private void InitalProvider(IDBNode idbNode,ActionEnum actionEnum,ref IProvider tempProvider)
    //    {
    //        string connectString = string.Empty;

    //        customDBProxyConfigManagement = new CustomDBProxyConfigManagement(idbNode.Name, idbNode.GroupType);

    //        switch (customDBProxyConfigManagement.CurrentDBGroup.GroupTypeEnum)
    //        {
    //            case GroupTypeEnum.MSSQLServer:
    //                connectString = customDBProxyConfigManagement.GetMSSQLServerConnectionString(actionEnum, idbNode.Name);
    //                tempProvider = new SqlProvider(connectString);
    //                break;
    //            case GroupTypeEnum.OracleServer:
    //                connectString = customDBProxyConfigManagement.GetOracleServerConnectionString(actionEnum, idbNode.Name);
    //                tempProvider = new OrlProvider(connectString);
    //                break;
    //        }
    //    }
    //}

    public class DbFactory
    {
        public static readonly DbFactory Instance = new DbFactory();

        private IDBNode _defaultDBNode;

        private ActionEnum _actionEnum = ActionEnum.ReadWrite;

        private bool boolInitalProvider = false;
        
        private DbFactory()
        {
        }
        
        public IDBNode DefaultDBNode
        {
            get
            {
                return _defaultDBNode;
            }
            set
            {
                _defaultDBNode = value;
            }
        }

        public ActionEnum DefaultAction
        {
            get
            {
                return _actionEnum;
            }
            set
            {
                _actionEnum = value;
            }
        }

        public IDb GetDb()
        {
            IProvider provider=null;
            InitalProvider(_defaultDBNode, _actionEnum, ref provider);
            return provider.OpenDb();
        }

        public IDb GetDb(string sqlConnectionString,GroupTypeEnum groupTypeEnum=GroupTypeEnum.MSSQLServer)
        {
            IProvider sqlProvider=null;

            switch (groupTypeEnum)
            {
                case GroupTypeEnum.MSSQLServer:
                    sqlProvider = new SqlProvider(sqlConnectionString);
                    break;
                case GroupTypeEnum.OracleServer:
                    sqlProvider = new OrlProvider(sqlConnectionString);
                    break;
                case GroupTypeEnum.LiteSQLServer:
                    sqlProvider = new LitProvider(sqlConnectionString);
                    break;
            }
            
            return sqlProvider.OpenDb();
        }

        public IDb GetDb(IDBNode idbNode, ActionEnum actionEnum)
        {
            IProvider tempProvider = null;
            InitalProvider(idbNode, actionEnum, ref tempProvider);
            return tempProvider.OpenDb();
        }

        private void InitalProvider(ref IProvider tempProvider)
        {
            InitalProvider(_defaultDBNode, _actionEnum, ref tempProvider);
        }

        private void InitalProvider(IDBNode idbNode, ActionEnum actionEnum, ref IProvider tempProvider)
        {
            string connectString = string.Empty;

            CustomDBProxyConfigManagement customDBProxyConfigManagement = new CustomDBProxyConfigManagement(idbNode.Name, idbNode.GroupType);

            switch (customDBProxyConfigManagement.CurrentDBGroup.GroupTypeEnum)
            {
                case GroupTypeEnum.MSSQLServer:
                    connectString = customDBProxyConfigManagement.GetMSSQLServerConnectionString(actionEnum, idbNode.Name);
                    tempProvider = new SqlProvider(connectString);
                    break;
                case GroupTypeEnum.OracleServer:
                    connectString = customDBProxyConfigManagement.GetOracleServerConnectionString(actionEnum, idbNode.Name);
                    tempProvider = new OrlProvider(connectString);
                    break;
            }
        }
    }
}
