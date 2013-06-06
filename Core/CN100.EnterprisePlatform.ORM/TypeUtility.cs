using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;

namespace CN100.EnterprisePlatform.ORM
{
    public class TypeUtility<ObjectType>
    {
        public delegate MemberType MemberGetDelegate<MemberType>(ObjectType obj);

        public static MemberGetDelegate<MemberType>
            GetMemberGetDelegate<MemberType>(string memberName)
        {
            Type objectType = typeof(ObjectType);

            PropertyInfo pi = objectType.GetProperty(memberName);
            FieldInfo fi = objectType.GetField(memberName);
            if (pi != null)
            {
                MethodInfo mi = pi.GetGetMethod();
                if (mi != null)
                {
                    return (MemberGetDelegate<MemberType>)
                        Delegate.CreateDelegate(typeof(MemberGetDelegate<MemberType>), mi);
                }
                else
                {
                    throw new Exception(String.Format(
                        "Property: '{0}' of Type: '{1}' does not have a Public Get accessor",
                        memberName, objectType.Name));
                }
            }
            else if (fi != null)
            {
                DynamicMethod dm = new DynamicMethod("Get" + memberName,
                    typeof(MemberType), new Type[] { objectType }, objectType);
                ILGenerator il = dm.GetILGenerator();
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Ldfld, fi);
                il.Emit(OpCodes.Ret);

                return (MemberGetDelegate<MemberType>)
                    dm.CreateDelegate(typeof(MemberGetDelegate<MemberType>));
            }
            else
            {
                throw new Exception(String.Format(
                    "Member: '{0}' is not a Public Property or Field of Type: '{1}'",
                    memberName, objectType.Name));
            }
        }

        public delegate void MemberSetDelegate<MemberType>(ObjectType obj, MemberType val);

        public static MemberSetDelegate<MemberType>
            GetMemberSetDelegate<MemberType>(string memberName)
        {
            Type objectType = typeof(ObjectType);

            PropertyInfo pi = objectType.GetProperty(memberName);

            if (pi != null)
            {
                MethodInfo mi = pi.GetSetMethod();
                if (mi != null)
                {
                    Type memberType = typeof(MemberType);

                    DynamicMethod method = new DynamicMethod("Set"+memberName, null, new Type[] { objectType, memberType }, objectType.Module);
                    var il = method.GetILGenerator();
                    var local = il.DeclareLocal(memberType, true);
                    il.Emit(OpCodes.Ldarg_1);
                    if (typeof(MemberType).IsValueType)
                    {
                        il.Emit(OpCodes.Unbox_Any, memberType);
                    }
                    else
                    {
                        il.Emit(OpCodes.Castclass, memberType);
                    }
                    il.Emit(OpCodes.Stloc, local);
                    il.Emit(OpCodes.Ldarg_0);
                    il.Emit(OpCodes.Ldloc, local);
                    il.EmitCall(OpCodes.Callvirt, mi, null);
                    il.Emit(OpCodes.Ret);

                    return (MemberSetDelegate<MemberType>)
                    Delegate.CreateDelegate(typeof(MemberSetDelegate<MemberType>), mi);
                }
                else
                {
                    throw new Exception(String.Format(
                    "Member: '{0}' is not a Public Property or Field of Type: '{1}'",
                    memberName, objectType.Name));
                }
            }
            else
            {
                throw new Exception(String.Format(
                    "Member: '{0}' is not a Public Property or Field of Type: '{1}'",
                    memberName, objectType.Name));
            }
        }

        private static Dictionary<string, Delegate> _memberGetDelegates = new Dictionary<string, Delegate>();

        public static MemberGetDelegate<MemberType>
            GetCachedMemberGetDelegate<MemberType>(string memberName)
        {
            if (_memberGetDelegates.ContainsKey(memberName))
                return (MemberGetDelegate<MemberType>)_memberGetDelegates[memberName];

            MemberGetDelegate<MemberType> returnValue = GetMemberGetDelegate<MemberType>(memberName);
            lock (_memberGetDelegates)
            {
                _memberGetDelegates[memberName] = returnValue;
            }
            return returnValue;
        }

        private static Dictionary<string, Delegate> _memberSetDelegates = new Dictionary<string, Delegate>();

        public static MemberSetDelegate<MemberType>
            GetCachedMemberSetDelegate<MemberType>(string memberName)
        {
            if (_memberSetDelegates.ContainsKey(memberName))
                return (MemberSetDelegate<MemberType>)_memberSetDelegates[memberName];

            MemberSetDelegate<MemberType> returnValue = GetMemberSetDelegate<MemberType>(memberName);
            lock (_memberSetDelegates)
            {
                _memberSetDelegates[memberName] = returnValue;
            }
            return returnValue;
        }

    }
}
