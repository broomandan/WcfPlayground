# WcfPlayGround
This repository includes multiple Visual Studio solutions to experiment the normalized connectivity concept in WCF. 
The idea is to expose same business logic through different connectivity protocols (http, tcp, msmq, sb and etc ...) with minimum to none pluming code.
The emphasis is on Microsoft Azure Service Bus messaging technologies.

1. **WcfQueuedService** is a basic WCF service that is exposing htt, tcp and msmq endpoints

2. **ServiceBusQueues** solution is an implementation of Microsoft tutorial

  Reference
  https://msdn.microsoft.com/en-us/library/azure/hh367512.aspx

3. **WcfServiceWithServiceBusQueues** is implementing a simple WCF service with netMessaging binding which points to a Azure Service Bus queue, using IDesign Inc. ServiceModelEx

  **Instructions**:
  1. Open ServiceBusConfigurationItems.cs file and add you namespace name and shared secret
	  
    Example: for "sb://Robert-messaging.servicebus.windows.net"
    Set `Namespace ="Robert-messaging"`
  2. Queue name conventions:
  
    1. In Development environment generated queue name is "ServiceContractName-HostMachineName" 
    2. In Non-development envirnoments generated queue name is "ServiceContractName"

  References:
  
    1. ServiceModelEx by Idesign
    2. https://code.msdn.microsoft.com/windowsazure/Brokered-Messaging-WCF-db4262c2
