using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Collections.Generic;

namespace CN100.EnterprisePlatform.ORM
{
    public delegate object GetHandler(object source);
    public delegate void SetHandler(object source, object value);
    public delegate object InstantiateObjectHandler();

    sealed class DynamicMethodCompiler
    {
        private DynamicMethodCompiler() { }

        internal static InstantiateObjectHandler CreateInstantiateObjectHandler(Type type)
        {
            ConstructorInfo constructorInfo = type.GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[0], null);
            if (constructorInfo == null)
            {
                throw new ApplicationException(string.Format("The type {0} must declare an empty constructor (the constructor may be private, internal, protected, protected internal, or public).", type));
            }

            DynamicMethod dynamicMethod = new DynamicMethod("InstantiateObject", MethodAttributes.Static | MethodAttributes.Public, CallingConventions.Standard, typeof(object), null, type, true);
            ILGenerator generator = dynamicMethod.GetILGenerator();
            generator.Emit(OpCodes.Newobj, constructorInfo);
            generator.Emit(OpCodes.Ret);
            return (InstantiateObjectHandler)dynamicMethod.CreateDelegate(typeof(InstantiateObjectHandler));
        }
        
        internal static GetHandler CreateGetHandler(Type type, PropertyInfo propertyInfo)
        {
            MethodInfo getMethodInfo = propertyInfo.GetGetMethod(true);
            DynamicMethod dynamicGet = CreateGetDynamicMethod(type);
            ILGenerator getGenerator = dynamicGet.GetILGenerator();

            getGenerator.Emit(OpCodes.Ldarg_0);
            getGenerator.Emit(OpCodes.Call, getMethodInfo);
            BoxIfNeeded(getMethodInfo.ReturnType, getGenerator);
            getGenerator.Emit(OpCodes.Ret);

            return (GetHandler)dynamicGet.CreateDelegate(typeof(GetHandler));
        }

        internal static GetHandler CreateGetHandler(Type type, FieldInfo fieldInfo)
        {
            DynamicMethod dynamicGet = CreateGetDynamicMethod(type);
            ILGenerator getGenerator = dynamicGet.GetILGenerator();

            getGenerator.Emit(OpCodes.Ldarg_0);
            getGenerator.Emit(OpCodes.Ldfld, fieldInfo);
            BoxIfNeeded(fieldInfo.FieldType, getGenerator);
            getGenerator.Emit(OpCodes.Ret);

            return (GetHandler)dynamicGet.CreateDelegate(typeof(GetHandler));
        }

        internal static SetHandler CreateSetHandler(Type type, PropertyInfo propertyInfo)
        {
            MethodInfo setMethodInfo = propertyInfo.GetSetMethod(true);
            DynamicMethod dynamicSet = CreateSetDynamicMethod(type);
            ILGenerator setGenerator = dynamicSet.GetILGenerator();

            setGenerator.Emit(OpCodes.Ldarg_0);
            setGenerator.Emit(OpCodes.Ldarg_1);
            UnboxIfNeeded(setMethodInfo.GetParameters()[0].ParameterType, setGenerator);
            setGenerator.Emit(OpCodes.Call, setMethodInfo);
            setGenerator.Emit(OpCodes.Ret);

            return (SetHandler)dynamicSet.CreateDelegate(typeof(SetHandler));
        }

        internal static SetHandler CreateSetHandler(Type type, FieldInfo fieldInfo)
        {
            DynamicMethod dynamicSet = CreateSetDynamicMethod(type);
            ILGenerator setGenerator = dynamicSet.GetILGenerator();

            setGenerator.Emit(OpCodes.Ldarg_0);
            setGenerator.Emit(OpCodes.Ldarg_1);
            UnboxIfNeeded(fieldInfo.FieldType, setGenerator);
            setGenerator.Emit(OpCodes.Stfld, fieldInfo);
            setGenerator.Emit(OpCodes.Ret);

            return (SetHandler)dynamicSet.CreateDelegate(typeof(SetHandler));
        }

        private static DynamicMethod CreateGetDynamicMethod(Type type)
        {
            return new DynamicMethod("DynamicGet", typeof(object), new Type[] { typeof(object) }, type, true);
        }

        private static DynamicMethod CreateSetDynamicMethod(Type type)
        {
            return new DynamicMethod("DynamicSet", typeof(void), new Type[] { typeof(object), typeof(object) }, type, true);
        }
        
        private static void BoxIfNeeded(Type type, ILGenerator generator)
        {
            if (type.IsValueType)
            {
                generator.Emit(OpCodes.Box, type);
            }
        }

        private static void UnboxIfNeeded(Type type, ILGenerator generator)
        {
            if (type.IsValueType)
            {
                generator.Emit(OpCodes.Unbox_Any, type);
            }
        }

        private static Dictionary<int, Delegate> _propertyGetDelegates = new Dictionary<int, Delegate>();

        public static GetHandler
            GetCachedGetPropertyHandlerDelegate(Type type, PropertyInfo propertyInfo)
        {
            int intCachedKey = propertyInfo.GetHashCode();// type.ToString() + "|P|" + propertyInfo.Name;
            if (_propertyGetDelegates.ContainsKey(intCachedKey))
                return (GetHandler)_propertyGetDelegates[intCachedKey];

            GetHandler returnValue = CreateGetHandler(type, propertyInfo);
            lock (_propertyGetDelegates)
            {
                _propertyGetDelegates[intCachedKey] = returnValue;
            }
            return returnValue;
        }

        private static Dictionary<int, Delegate> _propertySetDelegates = new Dictionary<int, Delegate>();

        public static SetHandler
            GetCachedSetPropertyHandlerDelegate(Type type, PropertyInfo propertyInfo)
        {
            int intCachedKey = propertyInfo.GetHashCode();//type.ToString() + "|P|" + propertyInfo.Name;
            if (_propertySetDelegates.ContainsKey(intCachedKey))
                return (SetHandler)_propertySetDelegates[intCachedKey];

            SetHandler returnValue = CreateSetHandler(type, propertyInfo);
            lock (_propertySetDelegates)
            {
                _propertySetDelegates[intCachedKey] = returnValue;
            }
            return returnValue;
        }

        private static Dictionary<int, Delegate> _fieldGetDelegates = new Dictionary<int, Delegate>();

        public static GetHandler
            GetCachedGetFieldHandlerDelegate(Type type, FieldInfo fieldInfo)
        {
            int intCachedKey = fieldInfo.GetHashCode();//type.ToString() + "|F|" + fieldInfo.Name;
            if (_fieldGetDelegates.ContainsKey(intCachedKey))
                return (GetHandler)_fieldGetDelegates[intCachedKey];

            GetHandler returnValue = CreateGetHandler(type, fieldInfo);
            lock (_fieldGetDelegates)
            {
                _fieldGetDelegates[intCachedKey] = returnValue;
            }
            return returnValue;
        }

        private static Dictionary<int, Delegate> _fieldSetDelegates = new Dictionary<int, Delegate>();

        public static SetHandler
            GetCachedSetFieldHandlerDelegate(Type type, FieldInfo fieldInfo)
        {
            int intCachedKey = fieldInfo.GetHashCode();//type.ToString() + "|F|" + fieldInfo.Name;
            if (_fieldSetDelegates.ContainsKey(intCachedKey))
                return (SetHandler)_fieldSetDelegates[intCachedKey];

            SetHandler returnValue = CreateSetHandler(type, fieldInfo);
            lock (_fieldSetDelegates)
            {
                _fieldSetDelegates[intCachedKey] = returnValue;
            }
            return returnValue;
        }

    }
}