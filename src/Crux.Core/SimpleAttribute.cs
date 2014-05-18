using System;

namespace Crux.Core
{
    [AttributeUsage(AttributeTargets.Class)]
    public abstract class SimpleAttribute : Attribute
    {
        private readonly string _setting;

        protected SimpleAttribute(string setting)
        {
            _setting = setting;
        }

        public string Setting
        {
            get { return _setting; }
        }
    }

    public static class SimpleAttributeExtensions
    {
        public static string GetSimpleAttributeSetting<T>(this object decoratedObject) where T : SimpleAttribute
        {
            var type = decoratedObject.GetType();

            object[] attributes = type.GetCustomAttributes(typeof(T), true);
            SimpleAttribute att = null;

            if (attributes.Length > 0) {
                att = (SimpleAttribute)attributes[0];
            }

            return (att != null) ? att.Setting : null;
        }
    }
}
