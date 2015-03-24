// © 2011 IDesign Inc. All rights reserved 
//Questions? Comments? go to 
//http://www.idesign.net

using System.ServiceModel;
using System.ServiceModel.Channels;

namespace ServiceModelEx.Errors.Logbook
{
   [ServiceContract]
   public interface ILogbookManager
   {
       [OperationContract(IsOneWay = true)]
       void LogEntry(LogbookEntryClient entry);
   }

   public class LogbookManagerClient : ClientBase<ILogbookManager>,ILogbookManager
   {
      public LogbookManagerClient()
      {}

      public LogbookManagerClient(string endpointConfigurationName) : base(endpointConfigurationName)
      {}

      public LogbookManagerClient(string endpointConfigurationName,string remoteAddress) : base(endpointConfigurationName,remoteAddress)
      {}

      public LogbookManagerClient(string endpointConfigurationName,EndpointAddress remoteAddress) : base(endpointConfigurationName,remoteAddress)
      {}

      public LogbookManagerClient(Binding binding,EndpointAddress remoteAddress) : base(binding,remoteAddress)
      {}

      public void LogEntry(LogbookEntryClient entry)
      {
         Channel.LogEntry(entry);
      }
   }
 }