using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SHOPHVT.Data
{
    public static class ServiceFactory
    {
        private static Dictionary<Type, object> _services = new Dictionary<Type, object>();

        public static T GetService<T>()
        {
            return (T)_services[typeof(T)];
        }

        public static void RegisterService<T>(T service)
        {
            _services[typeof(T)] = service;
        }
    }
}