// © 2011 IDesign Inc. All rights reserved 
//Questions? Comments? go to 
//http://www.idesign.net


using System;
using System.Diagnostics;
using System.ServiceModel;

namespace Proxy.ServiceBus
{
    static partial class ServiceBusHelper
    {
        internal static void VerifyOneWay(Type interfaceType)
        {
            Debug.Assert(interfaceType.IsInterface);

            var methods = interfaceType.GetMethods();
            foreach (var method in methods)
            {
                var attributes = method.GetCustomAttributes(typeof (OperationContractAttribute), true);
                Debug.Assert(attributes.Length == 1);

                var attribute = attributes[0] as OperationContractAttribute;
                // ReSharper disable once PossibleNullReferenceException
                if (attribute.IsOneWay)
                {
                    continue;
                }

                var message = "All operations on contract " + interfaceType +
                              " must be one-way, but operation " + method.Name +
                              " is not configured for one-way";
                throw new InvalidOperationException(message);
            }
        }
    }
}