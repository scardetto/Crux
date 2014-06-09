using System;
using Crux.Domain.Events;
using FluentAssertions;
using Microsoft.Practices.ServiceLocation;
using NUnit.Framework;
using Rhino.Mocks;

namespace Crux.Domain.Testing.Events
{
    [TestFixture]
    public class DomainEventsTester
    {
        [TearDown]
        public void Teardown()
        {
            DomainEvents.ClearCallbacks();
        }

        [Test]
        public void should_dispatch_events_to_actions()
        {
            bool wasCalled = false;
            var domainEvent = new TestEvent();

            Action<TestEvent> callback = e => {
                e.Should().Be(domainEvent);
                wasCalled = true;
            };

            DomainEvents.Register(callback);
            DomainEvents.Raise(domainEvent);

            wasCalled.Should().BeTrue();
        }

        [Test]
        public void should_dispatch_events_to_handlers()
        {
            var handler = new TestEventHandler();
            var domainEvent = new TestEvent();

            var serviceLocator = MockRepository.GenerateMock<IServiceLocator>();
            serviceLocator.Stub(l => l.GetAllInstances<IHandle<TestEvent>>()).Return(new[] { handler }).IgnoreArguments();

            DomainEvents.ServiceLocator = serviceLocator;
            DomainEvents.Raise(domainEvent);

            handler.WasCalled.Should().BeTrue();
            handler.Event.Should().Be(domainEvent);
        }

        private class TestEvent : IDomainEvent { }

        private class TestEventHandler : IHandle<TestEvent> 
        {
            public TestEvent Event { get; private set; }
            public bool WasCalled { get; private set; }

            public void Handle(TestEvent args)
            {
                Event = args;
                WasCalled = true;
            }
        }
    }
}
