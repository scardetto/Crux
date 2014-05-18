using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Crux.Core.Extensions
{
    public static class ReflectionExtensions
    {
        public static T GetPrivatePropertyValue<T>(this object target, string propertyName)
        {
            return (T)GetPrivatePropertyInfo(target, propertyName).GetValue(target, null);
        }

        public static void SetPrivatePropertyValue<T>(this object obj, string propName, T val)
        {
            GetPrivatePropertyInfo(obj, propName).SetValue(obj, val);
        }

        public static PropertyInfo GetPrivatePropertyInfo(object target, string propertyName)
        {
            if (target == null) {
                throw new ArgumentNullException("target");
            }

            var propertyInfo = target.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            if (propertyInfo == null) {
                throw new ArgumentOutOfRangeException("propertyName", string.Format("Property {0} was not found in Type {1}", propertyName, target.GetType().FullName));
            }

            return propertyInfo;
        }

        public static T GetPrivateFieldValue<T>(this object obj, string propName)
        {
            return (T)GetPrivateFieldInfo(obj, propName).GetValue(obj);
        }

        public static void SetPrivateFieldValue<T>(this object obj, string propName, T val)
        {
            GetPrivateFieldInfo(obj, propName).SetValue(obj, val);
        }

        public static FieldInfo GetPrivateFieldInfo(object target, string propertyName)
        {
            if (target == null) {
                throw new ArgumentNullException("target");
            }

            var type = target.GetType();
            FieldInfo fieldInfo = null;

            while (fieldInfo == null && type != null) {
                fieldInfo = type.GetField(propertyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                type = type.BaseType;
            }

            if (fieldInfo == null) {
                throw new ArgumentOutOfRangeException("propertyName", string.Format("Field {0} was not found in Type {1}", propertyName, target.GetType().FullName));
            }

            return fieldInfo;
        }

        public static bool MeetsSpecialGenericConstraints(Type genericArgType, Type proposedSpecificType)
        {
            GenericParameterAttributes gpa = genericArgType.GenericParameterAttributes;
            GenericParameterAttributes constraints = gpa & GenericParameterAttributes.SpecialConstraintMask;

            // No constraints, away we go!
            if (constraints == GenericParameterAttributes.None) {
                return true;
            }

            // "class" constraint and this is a value type
            if ((constraints & GenericParameterAttributes.ReferenceTypeConstraint) != 0
                && proposedSpecificType.IsValueType) {
                return false;
            }

            // "struct" constraint and this is not a value type
            if ((constraints & GenericParameterAttributes.NotNullableValueTypeConstraint) != 0
                && !proposedSpecificType.IsValueType) {
                return false;
            }

            // "new()" constraint and this type has no default constructor
            if ((constraints & GenericParameterAttributes.DefaultConstructorConstraint) != 0
                && proposedSpecificType.GetConstructor(Type.EmptyTypes) == null) {
                return false;
            }

            return true;
        }

        public static PropertyInfo GetProperty<TModel>(Expression<Func<TModel, object>> expression)
        {
            var memberExpression = GetMemberExpression(expression);
            return (PropertyInfo)memberExpression.Member;
        }

        public static PropertyInfo GetProperty<TModel, T>(Expression<Func<TModel, T>> expression)
        {
            var memberExpression = GetMemberExpression(expression);
            return (PropertyInfo)memberExpression.Member;
        }

        public static PropertyInfo GetProperty(LambdaExpression expression)
        {
            var memberExpression = GetMemberExpression(expression, true);
            return (PropertyInfo)memberExpression.Member;
        }

        private static MemberExpression GetMemberExpression<TModel, T>(Expression<Func<TModel, T>> expression)
        {
            MemberExpression memberExpression = null;

            switch (expression.Body.NodeType) {
                case ExpressionType.Convert: {
                    var body = (UnaryExpression)expression.Body;
                    memberExpression = body.Operand as MemberExpression;
                }
                    break;
                case ExpressionType.MemberAccess:
                    memberExpression = expression.Body as MemberExpression;
                    break;
            }

            if (memberExpression == null) throw new ArgumentException("Not a member access", "expression");
            return memberExpression;
        }

        public static MemberExpression GetMemberExpression(this LambdaExpression expression, bool enforceMemberExpression)
        {
            MemberExpression memberExpression = null;

            switch (expression.Body.NodeType) {
                case ExpressionType.Convert: {
                    var body = (UnaryExpression)expression.Body;
                    memberExpression = body.Operand as MemberExpression;
                }
                    break;
                case ExpressionType.MemberAccess:
                    memberExpression = expression.Body as MemberExpression;
                    break;
            }

            if (enforceMemberExpression && memberExpression == null) throw new ArgumentException("Not a member access", "expression");
            return memberExpression;
        }

        public static bool IsMemberExpression<T>(Expression<Func<T, object>> expression)
        {
            return IsMemberExpression<T, object>(expression);
        }

        public static bool IsMemberExpression<T, U>(Expression<Func<T, U>> expression)
        {
            return GetMemberExpression(expression, false) != null;
        }

        public static MethodInfo GetMethod<T>(Expression<Func<T, object>> expression)
        {
            return new FindMethodVisitor(expression).Method;
        }

        public static MethodInfo GetMethod(Expression<Func<object>> expression)
        {
            return GetMethod<Func<object>>(expression);
        }

        public static MethodInfo GetMethod(Expression expression)
        {
            return new FindMethodVisitor(expression).Method;
        }

        public static MethodInfo GetMethod<TDelegate>(Expression<TDelegate> expression)
        {
            return new FindMethodVisitor(expression).Method;
        }

        public static MethodInfo GetMethod<T, U>(Expression<Func<T, U>> expression)
        {
            return new FindMethodVisitor(expression).Method;
        }

        public static MethodInfo GetMethod<T, U, V>(Expression<Func<T, U, V>> expression)
        {
            return new FindMethodVisitor(expression).Method;
        }

        public static MethodInfo GetMethod<T>(Expression<Action<T>> expression)
        {
            return new FindMethodVisitor(expression).Method;
        }
    }

    public class FindMethodVisitor : ExpressionVisitor
    {
        private MethodInfo _method;

        public FindMethodVisitor(Expression expression)
        {
            Visit(expression);
        }

        public MethodInfo Method
        {
            get { return _method; }
        }

        protected override Expression VisitMethodCall(MethodCallExpression m)
        {
            _method = m.Method;
            return m;
        }
    }
}