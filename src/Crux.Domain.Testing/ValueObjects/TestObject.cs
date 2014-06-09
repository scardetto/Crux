using Crux.Domain.ValueObjects;

namespace Crux.Domain.Testing.ValueObjects
{
    public class TestObject : INullable
    {
        // I'm the real object
        public virtual bool IsNull
        {
            get { return false; }
        }
    }

    public class NullTestObject : TestObject
    {
        // I'm the null version of the object
        private static readonly NullTestObject INSTANCE = new NullTestObject();

        public static NullTestObject Instance
        {
            get { return INSTANCE; }
        }

        public override bool IsNull
        {
            get { return true; }
        }
    }
}