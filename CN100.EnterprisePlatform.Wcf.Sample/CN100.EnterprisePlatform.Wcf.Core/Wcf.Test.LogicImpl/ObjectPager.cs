using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smark.Data.Mappings;
namespace Wcf.Test.LogicImpl
{
    [Proc(Name= "st_pager")]
    public class ObjectPager
    {
        [PorcParameter]
        public string SQL
        {
            get;
            set;
        }
        [PorcParameter]
        public string Order
        {
            get;
            set;
        }
        [PorcParameter]
        public int PageIndex
        {
            get;
            set;
        }
        [PorcParameter]
        public int PageSize
        {
            get;
            set;
        }
    }
    [Proc(Name="up_sysPagingWithSingle")]
    public class UserSingle
    {
        [PorcParameter]
        public string TableName { get; set; }
        [PorcParameter]
        public string Cols { get; set; }
        [PorcParameter]
        public string Where { get; set; }
        [PorcParameter]
        public string OrderBy { get; set; }
        [PorcParameter]
        public int PageIndex { get; set; }
        [PorcParameter]
        public int PageSize { get; set; }
    }
}
