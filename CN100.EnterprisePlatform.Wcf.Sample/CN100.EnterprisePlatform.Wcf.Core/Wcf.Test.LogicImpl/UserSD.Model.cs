﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.269
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using Smark.Data;
using Smark.Data.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Wcf.Test.LogicImpl {
    
    
    [Serializable()]
    [Table("tblUser")]
    public partial class UserSD : Smark.Data.Mappings.DataObject, IUserSD {
        
        private Guid mUserID;
        
        public static Smark.Data.FieldInfo userID = new Smark.Data.FieldInfo("tblUser", "UserID");
        
        private string mUserName;
        
        public static Smark.Data.FieldInfo userName = new Smark.Data.FieldInfo("tblUser", "UserName");
        
        private string mPassword;
        
        public static Smark.Data.FieldInfo password = new Smark.Data.FieldInfo("tblUser", "Password");
        
        private string mRealName;
        
        public static Smark.Data.FieldInfo realName = new Smark.Data.FieldInfo("tblUser", "RealName");
        
        private string mEmail;
        
        public static Smark.Data.FieldInfo email = new Smark.Data.FieldInfo("tblUser", "Email");
        
        private string mMobilePhone;
        
        public static Smark.Data.FieldInfo mobilePhone = new Smark.Data.FieldInfo("tblUser", "MobilePhone");
        
        [ID()]
        public virtual Guid UserID {
            get {
                return this.mUserID;
            }
            set {
                this.mUserID = value;
                this.EntityState.FieldChange("UserID");
            }
        }
        
        [Column()]
        public virtual string UserName {
            get {
                return this.mUserName;
            }
            set {
                this.mUserName = value;
                this.EntityState.FieldChange("UserName");
            }
        }
        
        [Column()]
        public virtual string Password {
            get {
                return this.mPassword;
            }
            set {
                this.mPassword = value;
                this.EntityState.FieldChange("Password");
            }
        }
        
        [Column()]
        public virtual string RealName {
            get {
                return this.mRealName;
            }
            set {
                this.mRealName = value;
                this.EntityState.FieldChange("RealName");
            }
        }
        
        [Column()]
        public virtual string Email {
            get {
                return this.mEmail;
            }
            set {
                this.mEmail = value;
                this.EntityState.FieldChange("Email");
            }
        }
        
        [Column()]
        public virtual string MobilePhone {
            get {
                return this.mMobilePhone;
            }
            set {
                this.mMobilePhone = value;
                this.EntityState.FieldChange("MobilePhone");
            }
        }
    }
}