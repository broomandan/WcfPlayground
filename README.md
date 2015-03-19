# WcfPlayGround
This repository includes multiple Visual Studio solutions to experiment the normalized connectivity concept in WCF. 
The idea is to expose same business logic through different connectivity protocols (http, tcp, msmq, sb and etc ...) with minimum to none pluming code.
The emphasis is on Microsoft Azure Service Bus messaging technologies.


- ServiceBusQueues solution is an implementation of Microsoft tutorial:
Reference
https://msdn.microsoft.com/en-us/library/azure/hh367512.aspx

- WcfServiceWithServiceBusQueues is implementing a simple WCF service with netMessaging binding which points to a Azure Service Bus queue
Reference
https://code.msdn.microsoft.com/windowsazure/Brokered-Messaging-WCF-db4262c2

- WcfQueuedService is a basic WCF service that is exposing htt, tcp and msmq endpoints