﻿// © 2011 IDesign Inc. All rights reserved 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.Diagnostics;
using System.ServiceModel.Description;
using Microsoft.ServiceBus;
using ServiceModelEx.Data;

namespace ServiceModelEx.ServiceBus.Hosts
{
   public class QueuedServiceBusHost : DiscoverableServiceHost 
   {
      protected internal bool RequiresSession
      {get;set;}

      protected TokenProvider QueueCredentials
      {
         get
         {
            IServiceBusProperties properties = this as IServiceBusProperties;
            return properties.Credential.TokenProvider;
         }
      }

      public QueuedServiceBusHost(Type serviceType,bool requiresSession = false,params Uri[] baseAddresses) : base(serviceType,baseAddresses)
      {
         Construct(requiresSession);
      }
      public QueuedServiceBusHost(object singleton,bool requiresSession = false,params Uri[] baseAddresses) : base(singleton,baseAddresses)
      {
         Construct(requiresSession);
      }
      protected override void OnClosed()
      {
         PurgeQueues();
         base.OnClosed();
      }

      protected override void OnOpening()
      {
         this.AddGenericResolver();

         IServiceBusProperties properties = this as IServiceBusProperties;
         foreach(ServiceEndpoint endpoint in Description.Endpoints)
         {
            Tuple<Uri,string> tuple = ServiceBusHelper.ParseUri(endpoint.Address.Uri);
            ServiceBusHelper.VerifyQueue(tuple.Item1,tuple.Item2,properties.Credential.TokenProvider,RequiresSession);
         } 
         base.OnOpening();
      }
      void Construct(bool requiresSession)
      {
         RequiresSession = requiresSession;
      }
      [Conditional("DEBUG")]
      void PurgeQueues()
      {
         IServiceBusProperties properties = this as IServiceBusProperties;

         foreach(Uri queueAddress in properties.Addresses)
         {
            try
            {
               Uri baseAddress  = ServiceBusHelper.ParseUri(queueAddress).Item1;
               string queueName = ServiceBusHelper.ParseUri(queueAddress).Item2;
               ServiceBusHelper.PurgeQueue(baseAddress,queueName,properties.Credential.TokenProvider);
            }
            catch
            {}
         }
      }
   }
}