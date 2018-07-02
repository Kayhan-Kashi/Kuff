using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;

namespace Kuff.WebUI.Infrastructure
{
    public class UnityDependencyResolver : IDependencyResolver
    {
        private IUnityContainer unityContainer;

        public UnityDependencyResolver(IUnityContainer unityContainer)
        {
            this.unityContainer = unityContainer;
        }

        public object GetService(Type serviceType)
        {
            try
            {                 
                return unityContainer.Resolve(serviceType);
                
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return unityContainer.ResolveAll(serviceType);
            }
            catch
            {
                return new List<object>();
            }
        }
    }
}