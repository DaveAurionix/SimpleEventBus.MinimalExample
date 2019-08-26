# SimpleEventBus

## Overview

This is the simplest possible example for a publisher and a subscriber using SimpleEventBus and Azure Service Bus.  The publisher publishes an event message onto Azure Service Bus, the subscriber receives it.

## Getting started

This project uses a Visual Studio 2019 solution.

Both the Publisher and Subscriber projects need a connection string to the same Azure Service Bus instance. You'll need to create an Azure Service Bus instance for development (e.g. in Azure Portal) before the solution will run.  Don't create a Topic or Subscription - these will be created automatically in a moment.

After creating the Bus, create a new appsettings.Development.json file alongside appsettings.json with a connection string in pointing to the Azure Service Bus instance that you have created.  Example file contents:

```json
"AzureServiceBusTransport": {
    "ConnectionStrings": [
      "Endpoint=sb://<namespace>.servicebus.windows.net/;SharedAccessKeyName=<keyname>;SharedAccessKey=<key>"
    ],
    "TopicName": "{MachineName}-Test",
    "EnablePartitioning": false
```

Copy the actual connection string from Azure Portal for your Azure Service Bus instance.  You'll want to generate a key with Manage, Send and Listen permissions.  **Do not check that connection string into source control**.

Finally, start both the Publisher and Subscriber projects running.  The Topic and Subscription should be created on the Bus.

## Future direction

We intend to update this project to use the new .NET Core 3 console builder pattern after release.

## How it works

### Publisher

TODO Mention console app but in the real world it could be a Web API project, a Web Site, or another Subscriber

### Subscriber

TODO Mention background process

### Contract

TODO Mention events - language is something that has happened (i.e. in the past).  One publisher (one service makes the decision) but many subscribers.
TODO Mention that in the real world, pub and sub may be in different solutions or areas of the codebase
TODO Mention that pub and sub may reference different copies of the contract, compatibility over the wire is what matters.

## References

See the [Readme in the main SimpleEventBus repository](https://github.com/GivePenny/SimpleEventBus/blob/master/Readme.md) for a list of other examples.