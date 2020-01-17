using System;
using System.Reflection;

namespace SCP939Intercom
{
	public static class ReflectionHelpers
	{
		private const BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic
		                                   | BindingFlags.Static | BindingFlags.InvokeMethod;

		//Invokables
		internal static object GetInstanceField(Type type, object instance, string fieldName)
		{
			FieldInfo field = type.GetField(fieldName, flags);
			return field?.GetValue(instance);
		}

		internal static void SetInstanceField(Type type, object instance, string fieldName, object value)
		{
			FieldInfo field = type.GetField(fieldName, flags);
			field?.SetValue(instance, value);
		}

		internal static void InvokeInstanceMethod(Type type, object instance, string methodName, object[] param)
		{
			MethodInfo info = type.GetMethod(methodName, flags);
			info?.Invoke(instance, param);
		}
		
		
		//Instance Extensions
		public static object GetInstanceField(this object obj, string fieldName)
		{
			FieldInfo field = obj.GetType().GetField(fieldName, flags);
			return field?.GetValue(obj);
		}
		public static void SetInstanceField(this object obj, string fieldName, object value)
		{
			FieldInfo field = obj.GetType().GetField(fieldName, flags);
			field?.SetValue(obj, value);
		}
		public static void InvokeInstanceMethod(this object obj, string methodName, object[] param)
		{
			MethodInfo info = obj.GetType().GetMethod(methodName, flags);
			info?.Invoke(obj, param);
		}
		
		//Generic Extensions
		public static T GetInstanceField<T>(this object obj, string fieldName) => (T) obj?.GetType().GetField(fieldName, flags)?.GetValue(obj);
		
		public static void SetInstanceField<T>(this object obj, string fieldName, T value)
		{
			FieldInfo field = obj.GetType().GetField(fieldName, flags);
			field?.SetValue(obj, value);
		}
		
		public static T GetStaticField<T>(this Type type, string fieldName)
		{
			FieldInfo field = type.GetField(fieldName, flags);
			return (T)field?.GetValue(null);
		}
		
		//Static Extensions
		public static object GetStaticField(this Type type, string fieldName)
		{
			FieldInfo field = type.GetField(fieldName, flags);
			return field?.GetValue(null);
		}
		public static void SetStaticField(this Type type, string fieldName, object value)
		{
			FieldInfo field = type.GetField(fieldName, flags);
			field?.SetValue(null, value);
		}
		public static void InvokeInstanceMethod(this Type type, string methodName, object[] param)
		{
			MethodInfo info = type.GetMethod(methodName, flags);
			info?.Invoke(null, param);
		}
	}
}