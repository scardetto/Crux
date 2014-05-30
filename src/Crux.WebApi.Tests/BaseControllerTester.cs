using System;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using Crux.StructureMap;
using Crux.WebApi.Activators;
using Microsoft.Practices.ServiceLocation;
using NUnit.Framework;
using StructureMap;
using StructureMap.Graph;

namespace Crux.WebApi.Tests
{
    public abstract class BaseControllerTester<T> where T : ApiController
    {
        private HttpServer _server;

        protected BaseControllerTester()
        {
            InitializeContainer();
            InitializeServiceLocator();
        }

        [SetUp]
        protected virtual void TestSetup()
        {
            InitializeContainer();
            _server = new HttpServer(BuildHttpConfiguration());
        }

        protected HttpClient GetClient()
        {
            return new HttpClient(_server) { BaseAddress = new Uri("http://localhost/") };
        }

        private static HttpConfiguration BuildHttpConfiguration()
        {
            var config = new HttpConfiguration
            {
                IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always
            };

            config.Services.Replace(typeof(IHttpControllerActivator), new ControllerActivator());
            config.MapHttpAttributeRoutes();

            return config;
        }

        private static void InitializeContainer()
        {
            ObjectFactory.Initialize(x =>
            {
                x.For<IServiceLocator>().Use<StructureMapServiceLocator>();
                x.Scan(scan =>
                {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                });
            });
        }

        private static void InitializeServiceLocator()
        {
            ServiceLocator.SetLocatorProvider(ObjectFactory.GetInstance<IServiceLocator>);
        }
    }
}
