using System;
using NServiceBus;

namespace Crux.NServiceBus.Extensions
{
    public static class ConfigExtensions
    {
        public static Configure DefaultMessageNamingConventions(this Configure configure)
        {
            configure
                .DefiningCommandsAs(IsMessageTypeEndingWith("Command"))
                .DefiningEventsAs(IsMessageTypeEndingWith("Event"))
                .DefiningMessagesAs(IsMessageTypeEndingWith("Message"))
                ;

            return configure;
        }

        private static Func<Type, bool> IsMessageTypeEndingWith(string name)
        {
            return t => t.Namespace != null
                        && t.Namespace.Contains("Service.Messages.")
                        && t.Name.EndsWith(name);
        }
    }
}
