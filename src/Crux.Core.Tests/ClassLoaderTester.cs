using System;
using System.Reflection;
using FluentAssertions;
using NUnit.Framework;

namespace Crux.Core.Tests
{
    [TestFixture]
    public class ClassLoaderTester
    {
        [Test]
        public void should_instantiate_class_by_name()
        {
            ClassLoader.New("Crux.Core.Tests.ClassLoaderTestClass, Crux.Core.Tests")
                .Should().BeOfType<ClassLoaderTestClass>();
        }

        [Test]
        public void should_throw_if_type_not_found()
        {
            Action action = () => ClassLoader.New("InvalidType");
            action.ShouldThrow<ActivationException>().WithMessage("Could not find type *");
        }

        [Test]
        public void should_lookup_types_from_calling_assembly()
        {
            ClassLoader.New("Crux.Core.Tests.ClassLoaderTestClass")
                .Should().BeOfType<ClassLoaderTestClass>();            
        }

        [Test]
        public void should_lookup_types_from_given_assembly()
        {
            ClassLoader.New<IClassLoaderTestClass>(Assembly.GetExecutingAssembly(), "Crux.Core.Tests.ClassLoaderTestClass")
                .Should().BeOfType<ClassLoaderTestClass>();
        }

        [Test]
        public void should_lookup_types_from_given_assembly_path()
        {
            ClassLoader.New<IClassLoaderTestClass>(Assembly.GetExecutingAssembly().Location, "Crux.Core.Tests.ClassLoaderTestClass")
                .Should().BeOfType<ClassLoaderTestClass>();
        }

        [Test]
        public void should_validate_class_is_assignable()
        {
            Action action = () => ClassLoader.New<ClassLoaderTestClass>("Crux.Core.Tests.ClassLoaderTester, Crux.Core.Tests");
            action.ShouldThrow<ActivationException>().WithMessage("The type * cannot be assigned to type *");
        }
    }

    public interface IClassLoaderTestClass { }
    
    public class ClassLoaderTestClass : IClassLoaderTestClass { }
    
    public class ClassLoaderTestClassDerivedType : ClassLoaderTestClass { }
}
