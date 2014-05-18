using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.ServiceLocation;

namespace Crux.Domain.Events
{
    public static class DomainEvents
    {
        [ThreadStatic]
        private static List<Delegate> _actions;

        public static IServiceLocator ServiceLocator { get; set; }

        public static void Raise<T>(T args) where T : IDomainEvent
        {
            InvokeHandlers(args);

            InvokeActions(args);
        }

        private static void InvokeHandlers<T>(T args) where T : IDomainEvent
        {
            if (ServiceLocator == null) return;

            foreach (var handler in GetHandlers<T>()) {
                handler.Handle(args);
            }
        }

        private static IEnumerable<IHandle<T>> GetHandlers<T>() where T : IDomainEvent
        {
            return ServiceLocator.GetAllInstances<IHandle<T>>();
        }

        private static void InvokeActions<T>(T args) where T : IDomainEvent
        {
            if (_actions == null) return;

            foreach (var action in _actions.OfType<Action<T>>()) {
                action.Invoke(args);
            }
        }

        public static void Register<T>(Action<T> callback) where T : IDomainEvent
        {
            _actions = _actions ?? new List<Delegate>();
            _actions.Add(callback);
        }

        public static void ClearCallbacks()
        {
            _actions = null;
        }
    }
}
