using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.ComponentModel;
using System.Collections;
using System.Runtime.Serialization;

namespace CN100.EnterprisePlatform.Utility.Error
{

    /// <summary>
    /// 
    /// </summary>
    [DebuggerDisplay("ErrorType = {ErrorType}, Message = {Message}")]
    [DataContract]
    public class ErrorMessage
    {
        private ErrorType resultType;
        /// <summary>
        /// Gets or sets the type of the result.
        /// </summary>
        /// <value>
        /// The type of the result.
        /// </value>
        [DataMember]
        public ErrorType ResultType
        {
            get { return resultType; }
            set { resultType = value; }
        }

        /// <summary>
        /// 结果
        /// </summary>
        [DataMember]
        public bool Result
        { 
            get; 
            set; 
        }

        private string message;
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        [DataMember]
        public string Message
        {
            get 
            {
                if (resultType == ErrorType.Unspecified)
                {
                    return string.Format("ErrorCode: {0};CustomMessage: {1}", ErrorCode, CustomMessage);
                }
                else
                {
                    return string.Format("ErrorCode: {0};SystemMessage: {1};CustomMessage: {2}", ErrorCode, SystemMessage, CustomMessage);
                }
            }
            set { message = value; }
        }


        /// <summary>
        /// Gets or sets the system message.
        /// </summary>
        /// <value>
        /// The system message.
        /// </value>
        [DataMember]
        public string SystemMessage
        {
            get { return (string)ErrorHashTable[ResultType]; }
            set {SystemMessage=value;}
        }

        /// <summary>
        /// Gets or sets the custom message.
        /// </summary>
        /// <value>
        /// The custom message.
        /// </value>
        [DataMember]
        public string CustomMessage
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the error code.
        /// </summary>
        /// <value>
        /// The error code.
        /// </value>
        [DataMember]
        public int ErrorCode
        {
            get { return (int)ResultType; }
            set { ResultType = (ErrorType)value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorMessage"/> class.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        public ErrorMessage(string msg)
        {
            resultType = ErrorType.Unspecified;
            CustomMessage = msg;
            Result = false;
        }

        public ErrorMessage(string msg, bool result)
        {
            resultType = ErrorType.Unspecified;
            CustomMessage = msg;
            Result = result;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorMessage"/> class.
        /// </summary>
        /// <param name="errorType">Type of the error.</param>
        public ErrorMessage(ErrorType errorType)
        {
            resultType = errorType;
            CustomMessage = string.Empty;
            Result = false;
        }

        public ErrorMessage(ErrorType errorType, bool result)
        {
            resultType = errorType;
            CustomMessage = string.Empty;
            Result = result;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorMessage"/> class.
        /// </summary>
        /// <param name="errorType">Type of the error.</param>
        /// <param name="msg">The MSG.</param>
        public ErrorMessage(ErrorType errorType, string msg)
        {
            resultType = errorType;
            CustomMessage = msg;
            Result = false;
        }
        public ErrorMessage(ErrorType errorType, string msg,bool result)
        {
            resultType = errorType;
            CustomMessage = msg;
            Result = result;
        }

        private static Hashtable ht = null;
        private static Hashtable ErrorHashTable
        {
            get
            {
                if (ht == null)
                {
                    ht = new Hashtable();
                    ht.Add(ErrorType.Success, ErrorString.Success);
                    ht.Add(ErrorType.Unspecified, ErrorString.Unspecified);
                    ht.Add(ErrorType.MethodError, ErrorString.MethodError);
                    ht.Add(ErrorType.UserForbidden, ErrorString.UserForbidden);
                    ht.Add(ErrorType.WriteHistoryError, ErrorString.WriteHistoryError);
                    ht.Add(ErrorType.DataNotFound, ErrorString.DataNotFound);
                    ht.Add(ErrorType.IllegalNumber, ErrorString.IllegalNumber);
                    ht.Add(ErrorType.OutOfLimited, ErrorString.OutOfLimited);
                    ht.Add(ErrorType.StringOverLength, ErrorString.StringOverLength);
                    ht.Add(ErrorType.FAIL, ErrorString.Fail);
                }
                return ht;
            }
        }
        
    }

    /// <summary>
    /// 结果类型
    /// </summary>
    [DataContract]
    public enum ErrorType
    {
        /// <summary>
        /// 成功
        /// </summary>
        [DataMember]
        Success = 0,
        /// <summary>
        /// 未指定的
        /// </summary>
        [DataMember]
        Unspecified = 1,
        /// <summary>
        /// 插入失败
        /// </summary>
        [DataMember]
        InsertFailure  = 2,
        /// <summary>
        /// 用户受限
        /// </summary>
        [DataMember]
        UserForbidden = 3,
        /// <summary>
        /// 写历史失败
        /// </summary>
        [DataMember]
        WriteHistoryError = 4,
        /// <summary>
        /// 数据库中找不到数据
        /// </summary>
        [DataMember]
        DataNotFound = 5,
        /// <summary>
        /// 非法数字
        /// </summary>
        [DataMember]
        IllegalNumber = 6,
        /// <summary>
        /// 超出数量限制
        /// </summary>
        [DataMember]
        OutOfLimited = 7,
        /// <summary>
        /// 字符串超长
        /// </summary>
        [DataMember]
        StringOverLength = 8,
        /// <summary>
        /// 方法出错
        /// </summary>
        [DataMember]
        MethodError = 9,
        /// <summary>
        /// 失败
        /// </summary>
        [DataMember]
        FAIL=10,
        //新的类型请插入在此行上面，务必指定数字，别忘了要上面的hashTable和ErrorString添加相应的文字说明
    }

    /// <summary>
    /// 错误信息
    /// </summary>
    public static class ErrorString
    {
        public const string Success = "成功";
        public const string Unspecified = "未指定错误类型";
        public const string MethodError = "调用其他方法出错";
        public const string UserForbidden = "用户状态受限";
        public const string WriteHistoryError = "写历史失败";
        public const string DataNotFound = "数据库中找不到记录";
        public const string IllegalNumber = "非常数字";
        public const string OutOfLimited = "超出数量限制";
        public const string StringOverLength = "字符串超出长度";
        public const string Fail = "失败";
    }

}