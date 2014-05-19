using System.IO;
using System.Xml;
using log4net.Config;

namespace Crux.Logging
{
    /// <summary>
    /// Summary description for LoggingConfigurator.
    /// </summary>
    public class LoggingConfigurator
    {
        public void Configure()
        {
            XmlConfigurator.Configure();
        }

        public void ConfigureInternal()
        {
            XmlConfigurator.Configure();
        }

        public void Configure(string configFilePath)
        {
            Configure(new FileInfo(configFilePath));
        }

        public void Configure(FileInfo fileInfo)
        {
            XmlConfigurator.Configure(fileInfo);
        }

        public void Configure(Stream stream)
        {
            XmlConfigurator.Configure(stream);
        }

        private void Configure(XmlElement xmlElement)
        {
            XmlConfigurator.Configure(xmlElement);
        }

        public void ConfigureAndWatch(FileInfo fileInfo)
        {
            XmlConfigurator.ConfigureAndWatch(fileInfo);
        }
    }
}
