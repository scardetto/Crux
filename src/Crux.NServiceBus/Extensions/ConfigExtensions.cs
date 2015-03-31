using System;
using NServiceBus;

namespace Crux.NServiceBus.Extensions
{
    public static class ConfigExtensions
    {
        public static BusConfiguration DefaultMessageNamingConventions(this BusConfiguration config)
        {
            config.Conventions()
                .DefiningCommandsAs(IsMessageTypeEndingWith("Command"))
                .DefiningEventsAs(IsMessageTypeEndingWith("Event"))
                .DefiningMessagesAs(IsMessageTypeEndingWith("Message"));

            return config;
        }

        private static Func<Type, bool> IsMessageTypeEndingWith(string name)
        {
            return t => t.Namespace != null
                        && t.Namespace.Contains(".Messages.")
                        && t.Name.EndsWith(name);
        }
    }
}
