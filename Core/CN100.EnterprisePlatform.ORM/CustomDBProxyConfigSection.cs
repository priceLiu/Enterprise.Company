using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Web;

namespace CN100.EnterprisePlatform.ORM
{
    //public class CustomDBProxyConfigSection:ConfigurationSection
    //{
    //    public CustomDBProxyConfigSection() { }

    //    public static CustomDBProxyConfigSection GetConfigSection()
    //    {
    //        return System.Configuration.ConfigurationManager.GetSection("CustomDBProxy") as CustomDBProxyConfigSection;
    //    }

    //    [ConfigurationProperty("Version")]
    //    public string Version
    //    {
    //        get
    //        {
    //            return this["Version"] as string;
    //        }
    //    }
        
    //    [ConfigurationProperty("CustomDBGroups")]
    //    public CustomDBGroups CustomDBGroups
    //    {
    //        get
    //        {
    //            return this["CustomDBGroups"] as CustomDBGroups;
    //        }
    //    }

    //}

    //public class CustomDBGroups : ConfigurationElementCollection
    //{
    //    protected CustomDBGroup _defaultCustomDBGroup = null;

    //    protected override object GetElementKey(ConfigurationElement element)
    //    {
    //        return ((CustomDBGroup)(element)).GroupTypeEnum;
    //    }

    //    protected override ConfigurationElement CreateNewElement()
    //    {
    //        return new CustomDBGroup();
    //    }

    //    [ConfigurationProperty("DefaultGroupType")]
    //    public GroupTypeEnum DefaultGroupType
    //    {
    //        get
    //        {
    //            return (GroupTypeEnum)this["DefaultGroupType"];
    //        }
    //    }

    //    public CustomDBGroup DefaultCustomDBGroup
    //    {
    //        get
    //        {
    //            if (_defaultCustomDBGroup != null)
    //            {
    //                return _defaultCustomDBGroup;
    //            }
    //            else
    //            {
    //                foreach (CustomDBGroup cdbg in this)
    //                {
    //                    if (cdbg.GroupTypeEnum == this.DefaultGroupType)
    //                    {
    //                        _defaultCustomDBGroup = cdbg;
    //                        break;
    //                    }
    //                }
    //                return _defaultCustomDBGroup;
    //            }
    //        }
    //    }
    //}

    //public class CustomDBGroup : ConfigurationElement
    //{
    //    [ConfigurationProperty("GroupTypeEnum")]
    //    public GroupTypeEnum GroupTypeEnum
    //    {
    //        get
    //        {
    //            return (GroupTypeEnum)this["GroupTypeEnum"];
    //        }
    //    }

    //    [ConfigurationProperty("Name")]
    //    public string Name
    //    {
    //        get
    //        {
    //            return this["Name"] as string;
    //        }
    //    }

    //    [ConfigurationProperty("Version")]
    //    public string Version
    //    {
    //        get
    //        {
    //            return this["Version"] as string;
    //        }
    //    }

    //    [ConfigurationProperty("DBNodes")]
    //    public DBNodes DBNodes
    //    {
    //        get
    //        {
    //            return this["DBNodes"] as DBNodes;
    //        }
    //    }
    //}

    //public class DBNodes : ConfigurationElementCollection
    //{
    //    protected override object GetElementKey(ConfigurationElement element)
    //    {
    //        return ((DBNode)(element)).Name;
    //    }

    //    protected override ConfigurationElement CreateNewElement()
    //    {
    //        return new DBNode();
    //    }

    //    [ConfigurationProperty("ProrityImpactFactor")]
    //    public int ProrityImpactFactor
    //    {
    //        get
    //        {
    //            return (int)this["ProrityImpactFactor"];
    //        }
    //    }

    //    [ConfigurationProperty("ConstHighPrority")]
    //    public int ConstHighPrority//="3" ConstMiddlePrority="2" ConstLowerPrority="1"
    //    {
    //        get
    //        {
    //            return (int)this["ConstHighPrority"];
    //        }
    //    }

    //    [ConfigurationProperty("ConstMiddlePrority")]
    //    public int ConstMiddlePrority
    //    {
    //        get
    //        {
    //            return (int)this["ConstMiddlePrority"];
    //        }
    //    }

    //    [ConfigurationProperty("ConstLowerPrority")]
    //    public int ConstLowerPrority
    //    {
    //        get
    //        {
    //            return (int)this["ConstLowerPrority"];
    //        }
    //    }

    //    [ConfigurationProperty("ReadWriteImpactFactor")]
    //    public int ReadWriteImpactFactor
    //    {
    //        get
    //        {
    //            return (int)this["ReadWriteImpactFactor"];
    //        }
    //    }

    //    [ConfigurationProperty("ConstReadWrite")]
    //    public int ConstReadWrite
    //    {
    //        get
    //        {
    //            return (int)this["ConstReadWrite"];
    //        }
    //    }

    //    [ConfigurationProperty("ConstRead")]
    //    public int ConstRead
    //    {
    //        get
    //        {
    //            return (int)this["ConstRead"];
    //        }
    //    }

    //}

    //public class DBNode : ConfigurationElementCollection
    //{
    //    [ConfigurationProperty("SequenceNo")]
    //    public int SequenceNo
    //    {
    //        get
    //        {
    //            return (int)this["SequenceNo"];
    //        }
    //    }

    //    [ConfigurationProperty("Name")]
    //    public string Name
    //    {
    //        get
    //        {
    //            return this["Name"] as string;
    //        }
    //    }

    //    [ConfigurationProperty("Version")]
    //    public string Version
    //    {
    //        get
    //        {
    //            return this["Version"] as string;
    //        }
    //    }

    //    [ConfigurationProperty("ProrityEnum")]
    //    public ProrityEnum ProrityEnum
    //    {
    //        get
    //        {
    //            return (ProrityEnum)this["ProrityEnum"];
    //        }
    //    }

    //    [ConfigurationProperty("Duration")]
    //    public int Duration
    //    {
    //        get
    //        {
    //            return (int)this["Duration"];
    //        }
    //    }

    //    protected override object GetElementKey(ConfigurationElement element)
    //    {
    //        return ((ActionNode)(element)).Name;
    //    }

    //    protected override ConfigurationElement CreateNewElement()
    //    {
    //        return new ActionNode();
    //    }
    //}

    //public class ActionNode : ConfigurationElement
    //{
    //    public ActionNode()
    //    { }

    //    public ActionNode(string name, ActionEnum actionEnum, int weight, int compositeWeight, int compositeWeightRangeStart, int compositeWeightRangeEnd, string connectionString)
    //    {
    //        this["Name"] = name;
    //        this["ActionEnum"] = actionEnum;
    //        this["Weight"] = weight;
    //        this.CompositeWeight = compositeWeight;
    //        this.CompositeWeightRangeStart = compositeWeightRangeStart;
    //        this.CompositeWeightRangeEnd = compositeWeightRangeEnd;
    //        this["ConnectionString"] = connectionString;
    //    }

    //    [ConfigurationProperty("Name")]
    //    public string Name
    //    {
    //        get
    //        {
    //            return this["Name"] as string;
    //        }
    //    }

    //    [ConfigurationProperty("ActionEnum")]
    //    public ActionEnum ActionEnum
    //    {
    //        get
    //        {
    //            return (ActionEnum)this["ActionEnum"];
    //        }
    //    }

    //    [ConfigurationProperty("Weight")]
    //    public int Weight
    //    {
    //        get
    //        {
    //            return (int)this["Weight"];
    //        }
    //    }

    //    public int CompositeWeight
    //    {
    //        get;
    //        set;
    //    }

    //    public int CompositeWeightRangeStart
    //    {
    //        get;
    //        set;
    //    }

    //    public int CompositeWeightRangeEnd
    //    {
    //        get;
    //        set;
    //    }

    //    [ConfigurationProperty("ConnectionString")]
    //    public string ConnectionString
    //    {
    //        get
    //        {
    //            return this["ConnectionString"] as string;
    //        }
    //    }
    //}

    //public enum GroupTypeEnum
    //{
    //    MSSQLServer,
    //    OracleServer,
    //    MySQLServer,
    //}

    //public enum ProrityEnum
    //{
    //    High,
    //    Middle,
    //    Lower,
    //}

    //public enum ActionEnum
    //{
    //    Read,
    //    ReadWrite,
    //}

    public class CustomDBProxyConfigSection : ConfigurationSection
    {
        public CustomDBProxyConfigSection() { }

        public static CustomDBProxyConfigSection GetConfigSection()
        {
            return System.Configuration.ConfigurationManager.GetSection("CustomDBProxy") as CustomDBProxyConfigSection;
        }

        [ConfigurationProperty("Version")]
        public string Version
        {
            get
            {
                return this["Version"] as string;
            }
        }

        [ConfigurationProperty("CustomDBGroups")]
        public CustomDBGroups CustomDBGroups
        {
            get
            {
                return this["CustomDBGroups"] as CustomDBGroups;
            }
        }

    }

    public class CustomDBGroups : ConfigurationElementCollection
    {
        protected CustomDBGroup _defaultCustomDBGroup = null;

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((CustomDBGroup)(element)).GroupTypeEnum;
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new CustomDBGroup();
        }

        [ConfigurationProperty("DefaultGroupType")]
        public GroupTypeEnum DefaultGroupType
        {
            get
            {
                return (GroupTypeEnum)this["DefaultGroupType"];
            }
        }

        public CustomDBGroup DefaultCustomDBGroup
        {
            get
            {
                if (_defaultCustomDBGroup != null)
                {
                    return _defaultCustomDBGroup;
                }
                else
                {
                    foreach (CustomDBGroup cdbg in this)
                    {
                        if (cdbg.GroupTypeEnum == this.DefaultGroupType)
                        {
                            _defaultCustomDBGroup = cdbg;
                            break;
                        }
                    }
                    return _defaultCustomDBGroup;
                }
            }
        }
    }

    public class CustomDBGroup : ConfigurationElement
    {
        [ConfigurationProperty("GroupTypeEnum")]
        public GroupTypeEnum GroupTypeEnum
        {
            get
            {
                return (GroupTypeEnum)this["GroupTypeEnum"];
            }
        }

        [ConfigurationProperty("Name")]
        public string Name
        {
            get
            {
                return this["Name"] as string;
            }
        }

        [ConfigurationProperty("Version")]
        public string Version
        {
            get
            {
                return this["Version"] as string;
            }
        }

        [ConfigurationProperty("DBNodes")]
        public DBNodes DBNodes
        {
            get
            {
                return this["DBNodes"] as DBNodes;
            }
        }
    }

    public class DBNodes : ConfigurationElementCollection
    {
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((DBNode)(element)).Name;
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new DBNode();
        }

        [ConfigurationProperty("ProrityImpactFactor")]
        public int ProrityImpactFactor
        {
            get
            {
                return (int)this["ProrityImpactFactor"];
            }
        }

        [ConfigurationProperty("ConstHighPrority")]
        public int ConstHighPrority//="3" ConstMiddlePrority="2" ConstLowerPrority="1"
        {
            get
            {
                return (int)this["ConstHighPrority"];
            }
        }

        [ConfigurationProperty("ConstMiddlePrority")]
        public int ConstMiddlePrority
        {
            get
            {
                return (int)this["ConstMiddlePrority"];
            }
        }

        [ConfigurationProperty("ConstLowerPrority")]
        public int ConstLowerPrority
        {
            get
            {
                return (int)this["ConstLowerPrority"];
            }
        }

        [ConfigurationProperty("ReadWriteImpactFactor")]
        public int ReadWriteImpactFactor
        {
            get
            {
                return (int)this["ReadWriteImpactFactor"];
            }
        }

        [ConfigurationProperty("ConstReadWrite")]
        public int ConstReadWrite
        {
            get
            {
                return (int)this["ConstReadWrite"];
            }
        }

        [ConfigurationProperty("ConstRead")]
        public int ConstRead
        {
            get
            {
                return (int)this["ConstRead"];
            }
        }

    }

    public class DBNode : ConfigurationElementCollection
    {
        [ConfigurationProperty("SequenceNo")]
        public int SequenceNo
        {
            get
            {
                return (int)this["SequenceNo"];
            }
        }

        [ConfigurationProperty("Name")]
        public string Name
        {
            get
            {
                return this["Name"] as string;
            }
        }

        [ConfigurationProperty("Version")]
        public string Version
        {
            get
            {
                return this["Version"] as string;
            }
        }

        [ConfigurationProperty("ProrityEnum")]
        public ProrityEnum ProrityEnum
        {
            get
            {
                return (ProrityEnum)this["ProrityEnum"];
            }
        }

        [ConfigurationProperty("Duration")]
        public int Duration
        {
            get
            {
                return (int)this["Duration"];
            }
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ActionNode)(element)).Name;
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new ActionNode();
        }
    }

    public class ActionNode : ConfigurationElement
    {
        public ActionNode()
        { }

        public ActionNode(string name, ActionEnum actionEnum, int weight, int compositeWeight, int compositeWeightRangeStart, int compositeWeightRangeEnd, string connectionString)
        {
            this["Name"] = name;
            this["ActionEnum"] = actionEnum;
            this["Weight"] = weight;
            this.CompositeWeight = compositeWeight;
            this.CompositeWeightRangeStart = compositeWeightRangeStart;
            this.CompositeWeightRangeEnd = compositeWeightRangeEnd;
            this["ConnectionString"] = connectionString;
        }

        [ConfigurationProperty("Name")]
        public string Name
        {
            get
            {
                return this["Name"] as string;
            }
        }

        [ConfigurationProperty("ActionEnum")]
        public ActionEnum ActionEnum
        {
            get
            {
                return (ActionEnum)this["ActionEnum"];
            }
        }

        [ConfigurationProperty("Weight")]
        public int Weight
        {
            get
            {
                return (int)this["Weight"];
            }
        }

        public int CompositeWeight
        {
            get;
            set;
        }

        public int CompositeWeightRangeStart
        {
            get;
            set;
        }

        public int CompositeWeightRangeEnd
        {
            get;
            set;
        }

        [ConfigurationProperty("ConnectionString")]
        public string ConnectionString
        {
            get
            {
                return this["ConnectionString"] as string;
            }
        }
    }

    public enum GroupTypeEnum
    {
        MSSQLServer,
        OracleServer,
        MySQLServer,
        LiteSQLServer,
    }

    public enum ProrityEnum
    {
        High,
        Middle,
        Lower,
    }

    public enum ActionEnum
    {
        Read,
        ReadWrite,
    }

    public interface IDBNode
    {
        string Name { get; set; }
        GroupTypeEnum GroupType { get; set; }
    }

    public class CustomDBProxyConfigManagement
    {
        protected CustomDBGroup _currentDBGroup=null;
        protected DBNode _currentDBNode;
        protected TimeSpan _lastWriteOprationTimeSpan;
        protected string _lastWriteOprationConnectionString;
        protected IList<DBNode> _listDBNode;
        protected int _dbNodeTotalCompositeWeight = 0;
        protected Random _randomDBNodeTotalCompositeWeight;
        
        protected IList<ActionNode> _currentReadWriteActionNodes;
        protected int _dbNodeReadWriteTotalCompositeWeight = 0;
        protected Random _randomDBNodeReadWriteTotalCompositeWeight;
        protected bool _boolInital = false;

        public static readonly CustomDBProxyConfigManagement instance = new CustomDBProxyConfigManagement();

        private CustomDBProxyConfigManagement()
        {
        }

        public CustomDBProxyConfigManagement(IDBNode idbNode)
        {
            InitalCustomDBProxyConfigManagement(idbNode.GroupType, idbNode.Name);
        }

        public CustomDBProxyConfigManagement(string dbNodeName,GroupTypeEnum groupTypeEnum=GroupTypeEnum.MSSQLServer)
        {
            InitalCustomDBProxyConfigManagement(groupTypeEnum, dbNodeName);
        }

        protected void InitalCustomDBProxyConfigManagement(GroupTypeEnum groupTypeEnum, string dbNodeName)
        {
            _currentDBGroup = GetCurrentDBGroup(groupTypeEnum);
            _listDBNode = _currentDBGroup.DBNodes.Cast<DBNode>().ToList();
            _currentDBNode = SetActionNodesCompositeWeight(_currentDBGroup, dbNodeName, out _dbNodeTotalCompositeWeight, out _currentReadWriteActionNodes, out _dbNodeReadWriteTotalCompositeWeight);
            _randomDBNodeTotalCompositeWeight = new Random(1);
            _randomDBNodeReadWriteTotalCompositeWeight = new Random(1);
            _boolInital = true;
        }
        
        public string GetCustomDBConnectionString(ActionEnum actionEnum)
        {
            if (!_boolInital)
            {
                InitalCustomDBProxyConfigManagement(this.CurrentDBGroup.GroupTypeEnum, this.CurrentDBNode.Name);
            }

            TimeSpan tsEnd = TimeSpan.FromTicks(DateTime.Now.Ticks);

            if (actionEnum == ActionEnum.Read)
            {
                if (tsEnd.Subtract(LastWriteOprationTimeSpan).TotalSeconds >= CurrentDBNode.Duration)
                {
                    int actionNodeNewCompositeWeight = _randomDBNodeTotalCompositeWeight.Next(1, _dbNodeTotalCompositeWeight);
                    ActionNode actionNode = GetActionNodeByNewCompositeWeight(actionNodeNewCompositeWeight, _currentDBNode);
                    return actionNode.ConnectionString;
                }
                else
                {
                    return _lastWriteOprationConnectionString;
                }
            }
            else
            {
                if (tsEnd.Subtract(LastWriteOprationTimeSpan).TotalSeconds >= CurrentDBNode.Duration)
                {
                    int readWriteActionNodeNewCompositeWeight = _randomDBNodeReadWriteTotalCompositeWeight.Next(1, _dbNodeReadWriteTotalCompositeWeight);
                    ActionNode actionNode = GetReadWriteActionNodeByNewCompositeWeight(readWriteActionNodeNewCompositeWeight, _currentReadWriteActionNodes);
                    LastWriteOprationTimeSpan = TimeSpan.FromTicks(DateTime.Now.Ticks);
                    _lastWriteOprationConnectionString = actionNode.ConnectionString;
                    return actionNode.ConnectionString;
                }
                else
                {
                    return _lastWriteOprationConnectionString;
                }
            }
        }

        public string GetCustomDBConnectionString(ActionEnum actionEnum, string dbNodeName, GroupTypeEnum groupTypeEnum = GroupTypeEnum.MSSQLServer)
        {
            if (!_boolInital)
            {
                InitalCustomDBProxyConfigManagement(groupTypeEnum, dbNodeName);
            }

            TimeSpan tsEnd = TimeSpan.FromTicks(DateTime.Now.Ticks);

            if (actionEnum == ActionEnum.Read)
            {
                if (tsEnd.Subtract(LastWriteOprationTimeSpan).TotalSeconds >= CurrentDBNode.Duration)
                {
                    int actionNodeNewCompositeWeight = _randomDBNodeTotalCompositeWeight.Next(1, _dbNodeTotalCompositeWeight);
                    ActionNode actionNode = GetActionNodeByNewCompositeWeight(actionNodeNewCompositeWeight, _currentDBNode);
                    return actionNode.ConnectionString;
                }
                else
                {
                    return _lastWriteOprationConnectionString;
                }
            }
            else
            {
                if (tsEnd.Subtract(LastWriteOprationTimeSpan).TotalSeconds >= CurrentDBNode.Duration)
                {
                    int readWriteActionNodeNewCompositeWeight = _randomDBNodeReadWriteTotalCompositeWeight.Next(1, _dbNodeReadWriteTotalCompositeWeight);
                    ActionNode actionNode = GetReadWriteActionNodeByNewCompositeWeight(readWriteActionNodeNewCompositeWeight, _currentReadWriteActionNodes);
                    LastWriteOprationTimeSpan = TimeSpan.FromTicks(DateTime.Now.Ticks);
                    _lastWriteOprationConnectionString = actionNode.ConnectionString;
                    return actionNode.ConnectionString;
                }
                else
                {
                    return _lastWriteOprationConnectionString;
                }
            }
        }

        public string GetMSSQLServerConnectionString(ActionEnum actionEnum, string dbNodeName)
        {
            if (!_boolInital)
            {
                InitalCustomDBProxyConfigManagement(GroupTypeEnum.MSSQLServer, dbNodeName);
            }

            TimeSpan tsEnd = TimeSpan.FromTicks(DateTime.Now.Ticks);

            if (actionEnum == ActionEnum.Read)
            {
                if (tsEnd.Subtract(LastWriteOprationTimeSpan).TotalSeconds >= CurrentDBNode.Duration)
                {
                    int actionNodeNewCompositeWeight = _randomDBNodeTotalCompositeWeight.Next(1, _dbNodeTotalCompositeWeight);
                    ActionNode actionNode = GetActionNodeByNewCompositeWeight(actionNodeNewCompositeWeight, _currentDBNode);
                    return actionNode.ConnectionString;
                }
                else
                {
                    return _lastWriteOprationConnectionString;
                }
            }
            else
            {
                if (tsEnd.Subtract(LastWriteOprationTimeSpan).TotalSeconds >= CurrentDBNode.Duration)
                {
                    int readWriteActionNodeNewCompositeWeight = _randomDBNodeReadWriteTotalCompositeWeight.Next(1, _dbNodeReadWriteTotalCompositeWeight);
                    ActionNode actionNode = GetReadWriteActionNodeByNewCompositeWeight(readWriteActionNodeNewCompositeWeight, _currentReadWriteActionNodes);
                    LastWriteOprationTimeSpan = TimeSpan.FromTicks(DateTime.Now.Ticks);
                    _lastWriteOprationConnectionString = actionNode.ConnectionString;
                    return actionNode.ConnectionString;
                }
                else
                {
                    return _lastWriteOprationConnectionString;
                }
            }
        }

        public string GetOracleServerConnectionString(ActionEnum actionEnum, string dbNodeName)
        {
            if (!_boolInital)
            {
                InitalCustomDBProxyConfigManagement(GroupTypeEnum.OracleServer, dbNodeName);
            }

            TimeSpan tsEnd = TimeSpan.FromTicks(DateTime.Now.Ticks);

            if (actionEnum == ActionEnum.Read)
            {
                if (tsEnd.Subtract(LastWriteOprationTimeSpan).TotalSeconds >= CurrentDBNode.Duration)
                {
                    int actionNodeNewCompositeWeight = _randomDBNodeTotalCompositeWeight.Next(1, _dbNodeTotalCompositeWeight);
                    ActionNode actionNode = GetActionNodeByNewCompositeWeight(actionNodeNewCompositeWeight, _currentDBNode);
                    return actionNode.ConnectionString;
                }
                else
                {
                    return _lastWriteOprationConnectionString;
                }
            }
            else
            {
                if (tsEnd.Subtract(LastWriteOprationTimeSpan).TotalSeconds >= CurrentDBNode.Duration)
                {
                    int readWriteActionNodeNewCompositeWeight = _randomDBNodeReadWriteTotalCompositeWeight.Next(1, _dbNodeReadWriteTotalCompositeWeight);
                    ActionNode actionNode = GetReadWriteActionNodeByNewCompositeWeight(readWriteActionNodeNewCompositeWeight, _currentReadWriteActionNodes);
                    LastWriteOprationTimeSpan = TimeSpan.FromTicks(DateTime.Now.Ticks);
                    _lastWriteOprationConnectionString = actionNode.ConnectionString;
                    return actionNode.ConnectionString;
                }
                else
                {
                    return _lastWriteOprationConnectionString;
                }
            }
        }

        public string GetCustomDBConnectionString(ref System.Data.IDbCommand iCommand, ActionEnum actionEnum)
        {
            if (!_boolInital)
            {
                InitalCustomDBProxyConfigManagement(this.CurrentDBGroup.GroupTypeEnum, this.CurrentDBNode.Name);
            }

            string strRegEx = "[.\\n]*[\\s]*[select][\\s]+";
            string strSQL = iCommand.CommandText;

            string strRtnConn = string.Empty;

            Regex reg = new Regex(strRegEx);

            TimeSpan tsEnd = TimeSpan.FromTicks(DateTime.Now.Ticks);

            if (reg.IsMatch(strSQL.ToLower()) && iCommand.CommandType == System.Data.CommandType.Text && iCommand.Transaction == null && actionEnum == ActionEnum.Read)
            {
                if (tsEnd.Subtract(LastWriteOprationTimeSpan).TotalSeconds >= CurrentDBNode.Duration)
                {
                    int actionNodeNewCompositeWeight = _randomDBNodeTotalCompositeWeight.Next(1,_dbNodeTotalCompositeWeight);
                    ActionNode actionNode = GetActionNodeByNewCompositeWeight(actionNodeNewCompositeWeight, _currentDBNode);
                    iCommand.Connection.ConnectionString = actionNode.ConnectionString;
                    return actionNode.ConnectionString;
                }
                else
                {
                    strRtnConn = iCommand.Connection.ConnectionString;
                    return strRtnConn;
                }
            }
            else
            {
                if (tsEnd.Subtract(LastWriteOprationTimeSpan).TotalSeconds >= CurrentDBNode.Duration)
                {
                    int readWriteActionNodeNewCompositeWeight = _randomDBNodeReadWriteTotalCompositeWeight.Next(1,_dbNodeReadWriteTotalCompositeWeight);
                    ActionNode actionNode = GetReadWriteActionNodeByNewCompositeWeight(readWriteActionNodeNewCompositeWeight, _currentReadWriteActionNodes);
                    iCommand.Connection.ConnectionString = actionNode.ConnectionString;
                    LastWriteOprationTimeSpan = TimeSpan.FromTicks(DateTime.Now.Ticks);
                    return actionNode.ConnectionString;
                }
                else
                {
                    strRtnConn = iCommand.Connection.ConnectionString;
                    return strRtnConn;
                }
            }
        }

        public string GetCustomDBConnectionString(ref System.Data.IDbCommand iCommand, ActionEnum actionEnum, string dbNodeName, GroupTypeEnum groupTypeEnum=GroupTypeEnum.MSSQLServer)
        {
            if (!_boolInital)
            {
                InitalCustomDBProxyConfigManagement(groupTypeEnum, dbNodeName);
            }

            string strRegEx = "[.\\n]*[\\s]*[select][\\s]+";
            string strSQL = iCommand.CommandText;

            string strRtnConn = string.Empty;

            Regex reg = new Regex(strRegEx);

            TimeSpan tsEnd = TimeSpan.FromTicks(DateTime.Now.Ticks);

            if (reg.IsMatch(strSQL.ToLower()) && iCommand.CommandType == System.Data.CommandType.Text && iCommand.Transaction == null && actionEnum == ActionEnum.Read)
            {
                if (tsEnd.Subtract(LastWriteOprationTimeSpan).TotalSeconds >= CurrentDBNode.Duration)
                {
                    int actionNodeNewCompositeWeight = _randomDBNodeTotalCompositeWeight.Next(1,_dbNodeTotalCompositeWeight);
                    ActionNode actionNode = GetActionNodeByNewCompositeWeight(actionNodeNewCompositeWeight, _currentDBNode);
                    iCommand.Connection.ConnectionString = actionNode.ConnectionString;
                    return actionNode.ConnectionString;
                }
                else
                {
                    strRtnConn = iCommand.Connection.ConnectionString;
                    return strRtnConn;
                }
            }
            else
            {
                if (tsEnd.Subtract(LastWriteOprationTimeSpan).TotalSeconds >= CurrentDBNode.Duration)
                {
                    int readWriteActionNodeNewCompositeWeight = _randomDBNodeReadWriteTotalCompositeWeight.Next(1,_dbNodeReadWriteTotalCompositeWeight);
                    ActionNode actionNode = GetReadWriteActionNodeByNewCompositeWeight(readWriteActionNodeNewCompositeWeight, _currentReadWriteActionNodes);
                    iCommand.Connection.ConnectionString = actionNode.ConnectionString;
                    LastWriteOprationTimeSpan = TimeSpan.FromTicks(DateTime.Now.Ticks);
                    return actionNode.ConnectionString;
                }
                else
                {
                    strRtnConn = iCommand.Connection.ConnectionString;
                    return strRtnConn;
                }
            }
        }

        public string GetMSSQLServerConnectionString(ref System.Data.IDbCommand iCommand, ActionEnum actionEnum, string dbNodeName)
        {
            if (!_boolInital)
            {
                InitalCustomDBProxyConfigManagement(GroupTypeEnum.MSSQLServer, dbNodeName);
            }

            string strRegEx = "[.\\n]*[\\s]*[select][\\s]+";
            string strSQL = iCommand.CommandText;

            string strRtnConn = string.Empty;

            Regex reg = new Regex(strRegEx);

            TimeSpan tsEnd = TimeSpan.FromTicks(DateTime.Now.Ticks);

            if (reg.IsMatch(strSQL.ToLower()) && iCommand.CommandType == System.Data.CommandType.Text && iCommand.Transaction == null && actionEnum == ActionEnum.Read)
            {
                if (tsEnd.Subtract(LastWriteOprationTimeSpan).TotalSeconds >= CurrentDBNode.Duration)
                {
                    int actionNodeNewCompositeWeight = _randomDBNodeTotalCompositeWeight.Next(1,_dbNodeTotalCompositeWeight);
                    ActionNode actionNode = GetActionNodeByNewCompositeWeight(actionNodeNewCompositeWeight, _currentDBNode);
                    iCommand.Connection.ConnectionString = actionNode.ConnectionString;
                    return actionNode.ConnectionString;
                }
                else
                {
                    strRtnConn = iCommand.Connection.ConnectionString;
                    return strRtnConn;
                }
            }
            else
            {
                if (tsEnd.Subtract(LastWriteOprationTimeSpan).TotalSeconds >= CurrentDBNode.Duration)
                {
                    int readWriteActionNodeNewCompositeWeight = _randomDBNodeReadWriteTotalCompositeWeight.Next(1,_dbNodeReadWriteTotalCompositeWeight);
                    ActionNode actionNode = GetReadWriteActionNodeByNewCompositeWeight(readWriteActionNodeNewCompositeWeight, _currentReadWriteActionNodes);
                    iCommand.Connection.ConnectionString = actionNode.ConnectionString;
                    LastWriteOprationTimeSpan = TimeSpan.FromTicks(DateTime.Now.Ticks);
                    return actionNode.ConnectionString;
                }
                else
                {
                    strRtnConn = iCommand.Connection.ConnectionString;
                    return strRtnConn;
                }
            }
        }

        public string GetOracleServerConnectionString(ref System.Data.IDbCommand iCommand, ActionEnum actionEnum, string dbNodeName)
        {
            if (!_boolInital)
            {
                InitalCustomDBProxyConfigManagement(GroupTypeEnum.OracleServer, dbNodeName);
            }

            string strRegEx = "[.\\n]*[\\s]*[select][\\s]+";
            string strSQL = iCommand.CommandText;

            string strRtnConn = string.Empty;

            Regex reg = new Regex(strRegEx);

            TimeSpan tsEnd = TimeSpan.FromTicks(DateTime.Now.Ticks);

            if (reg.IsMatch(strSQL.ToLower()) && iCommand.CommandType == System.Data.CommandType.Text && iCommand.Transaction == null && actionEnum == ActionEnum.Read)
            {
                if (tsEnd.Subtract(LastWriteOprationTimeSpan).TotalSeconds >= CurrentDBNode.Duration)
                {
                    int actionNodeNewCompositeWeight = _randomDBNodeTotalCompositeWeight.Next(1,_dbNodeTotalCompositeWeight);
                    ActionNode actionNode = GetActionNodeByNewCompositeWeight(actionNodeNewCompositeWeight, _currentDBNode);
                    iCommand.Connection.ConnectionString = actionNode.ConnectionString;
                    return actionNode.ConnectionString;
                }
                else
                {
                    strRtnConn = iCommand.Connection.ConnectionString;
                    return strRtnConn;
                }
            }
            else
            {
                if (tsEnd.Subtract(LastWriteOprationTimeSpan).TotalSeconds >= CurrentDBNode.Duration)
                {
                    int readWriteActionNodeNewCompositeWeight = _randomDBNodeReadWriteTotalCompositeWeight.Next(1, _dbNodeReadWriteTotalCompositeWeight);
                    ActionNode actionNode = GetReadWriteActionNodeByNewCompositeWeight(readWriteActionNodeNewCompositeWeight, _currentReadWriteActionNodes);
                    iCommand.Connection.ConnectionString = actionNode.ConnectionString;
                    LastWriteOprationTimeSpan = TimeSpan.FromTicks(DateTime.Now.Ticks);
                    return actionNode.ConnectionString;
                }
                else
                {
                    strRtnConn = iCommand.Connection.ConnectionString;
                    return strRtnConn;
                }
            }
        }

        protected IList<DBNode> GetDBNodeList(CustomDBGroup customDBGroup)
        {
            IList<DBNode> listRtn = new List<DBNode>();

            var objList = from node in customDBGroup.DBNodes.Cast<DBNode>()
                          select node;

            return (IList<DBNode>)objList;
        }

        protected DBNode SetActionNodesCompositeWeight(CustomDBGroup customDBGroup, string dbNodeName, out int dbNodeTotalCompositeWeight, out IList<ActionNode> readWriteActionNodes, out int dbNodeReadWriteTotalCompositeWeight)
        {
            int intReadWriteImpactFactor = customDBGroup.DBNodes.ReadWriteImpactFactor;
            int intConstReadWrite = customDBGroup.DBNodes.ConstReadWrite;
            int intConstRead = customDBGroup.DBNodes.ConstRead;

            DBNode dbNode = GetCurrentDBNode(customDBGroup.GroupTypeEnum, dbNodeName);

            foreach (ActionNode aNode in dbNode)
            {
                switch (aNode.ActionEnum)
                {
                    case ActionEnum.Read:
                        aNode.CompositeWeight = intReadWriteImpactFactor * intConstRead + aNode.Weight;
                        break;
                    case ActionEnum.ReadWrite:
                        aNode.CompositeWeight = intReadWriteImpactFactor * intConstReadWrite + aNode.Weight;
                        break;
                }
            }

            var objActionList = from a in dbNode.Cast<ActionNode>()
                                orderby a.CompositeWeight ascending
                                select a;

            foreach (ActionNode aNode in objActionList)
            {
                var objListP = from n in objActionList
                               where n.CompositeWeight <= aNode.CompositeWeight
                               select n;

                int intTotalCompositeWeight = objListP.Sum<ActionNode>(actionNode => actionNode.CompositeWeight);

                aNode.CompositeWeightRangeEnd = intTotalCompositeWeight;
                aNode.CompositeWeightRangeStart = intTotalCompositeWeight - aNode.CompositeWeight + 1;
            }

            dbNodeTotalCompositeWeight = objActionList.Sum<ActionNode>(actionNode => actionNode.CompositeWeight);


            var objReadWriteActionNodeList = from aNode in objActionList
                                             where aNode.ActionEnum == ActionEnum.ReadWrite
                                             select aNode;

            dbNodeReadWriteTotalCompositeWeight = objReadWriteActionNodeList.Sum<ActionNode>(aNode => aNode.CompositeWeight);

            List<ActionNode> listReadWriteActionNodes = new List<ActionNode>();

            foreach (ActionNode aNode in objReadWriteActionNodeList)
            {
                var objListP = from n in objReadWriteActionNodeList
                               where n.CompositeWeight <= aNode.CompositeWeight
                               select n;

                int intTotalReadWriteCompositeWeight = objListP.Sum<ActionNode>(actionNode => actionNode.CompositeWeight);

                ActionNode tempNode = new ActionNode(aNode.Name, aNode.ActionEnum, aNode.Weight, aNode.CompositeWeight, intTotalReadWriteCompositeWeight - aNode.CompositeWeight + 1, intTotalReadWriteCompositeWeight, aNode.ConnectionString);

                listReadWriteActionNodes.Add(tempNode);
            }

            readWriteActionNodes = listReadWriteActionNodes;                       

            return dbNode;
        }

        protected ActionNode GetActionNodeByNewCompositeWeight(int actionNodeNewCompositeWeight, DBNode dbNode)
        {
            var objList = from aNode in dbNode.Cast<ActionNode>()
                          where aNode.CompositeWeightRangeStart <= actionNodeNewCompositeWeight && aNode.CompositeWeightRangeEnd >= actionNodeNewCompositeWeight
                          select aNode;

            foreach (ActionNode aNode in dbNode)
            {
                if (aNode.CompositeWeightRangeStart <= actionNodeNewCompositeWeight && aNode.CompositeWeightRangeEnd >= actionNodeNewCompositeWeight)
                {
                    ActionNode node = aNode;
                }
            }

            return objList.FirstOrDefault();
        }

        protected ActionNode GetReadWriteActionNodeByNewCompositeWeight(int readWriteActionNodeNewCompositeWeight, IList<ActionNode> listActionNode)
        {
            var objList = from aNode in listActionNode
                          where aNode.CompositeWeightRangeStart <= readWriteActionNodeNewCompositeWeight && aNode.CompositeWeightRangeEnd >= readWriteActionNodeNewCompositeWeight
                          select aNode;

            return objList.FirstOrDefault();
        }

        protected DBNode GetCurrentDBNode(GroupTypeEnum groupTypeEnum,string dbNodeName)
        {
            CustomDBGroup objGroup = GetCurrentDBGroup(groupTypeEnum);

            var objList = from dbNode in objGroup.DBNodes.Cast<DBNode>()
                          where dbNode.Name.Equals(dbNodeName, StringComparison.CurrentCultureIgnoreCase)
                          select dbNode;

            DBNode node = null;

            node = objList.FirstOrDefault<DBNode>();

            return node;
        }

        protected CustomDBGroup GetCurrentDBGroup(GroupTypeEnum groupTypeEnum)
        {
            CustomDBGroup objGroup = null;

            CustomDBProxyConfigSection section = CustomDBProxyConfigSection.GetConfigSection();

            foreach (CustomDBGroup grp in CustomDBProxyConfigSection.GetConfigSection().CustomDBGroups)
            {
                if (grp.GroupTypeEnum == groupTypeEnum)
                {
                    objGroup = grp;
                    break;
                }
            }

            if (objGroup == null)
            {
                objGroup = CustomDBProxyConfigSection.GetConfigSection().CustomDBGroups.DefaultCustomDBGroup;
            }

            return objGroup;
        }

        public CustomDBGroup CurrentDBGroup
        {
            get
            {
                return _currentDBGroup;
            }
        }

        public DBNode CurrentDBNode
        {
            get
            {
                return _currentDBNode;
            }
        }

        public TimeSpan LastWriteOprationTimeSpan
        {
            get
            {
                return _lastWriteOprationTimeSpan;
            }

            set
            {
                _lastWriteOprationTimeSpan = value;
            }
        }
        
    }

}
