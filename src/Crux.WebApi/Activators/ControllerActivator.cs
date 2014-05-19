using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using Microsoft.Practices.ServiceLocation;

namespace Crux.WebApi.Activators
{
    public class ControllerActivator : IHttpControllerActivator
    {
        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            return ServiceLocator.Current.GetInstance(controllerType) as IHttpController;
        }
    }
}
