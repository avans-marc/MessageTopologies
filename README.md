# .NET Messaging Sample with RabbitMQ & MassTransit

This repository contains .NET 9 Worker Service projects demonstrating messaging patterns using RabbitMQ. Before running any of the projects, you need a RabbitMQ broker running locally in a Docker container.

## Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Docker](https://www.docker.com/products/docker-desktop/)

## Start RabbitMQ with Management Plugin

To start a RabbitMQ container with the management UI, run the following command:

`docker run -d --name rabbitmq  -p 5672:5672 -p 15672:15672  --mount source=rabbitmqdata,target=/var/lib/rabbitmq -e RABBITMQ_DEFAULT_USER=guest -e RABBITMQ_DEFAULT_PASS=guest rabbitmq:management`


- **5672**: RabbitMQ broker port (used by applications)
- **15672**: RabbitMQ management portal

### Access the Management Portal

Once the container is running, access the RabbitMQ management UI at:

[http://localhost:15672](http://localhost:15672)

- **Username:** `guest`
- **Password:** `guest`

## Running the Projects

1. Start the RabbitMQ container as described above.
2. Open the desired project (e.g., `FanOut` or `Point2Point`) in Visual Studio 2022.
3. Build and run the project.

## About the Point-to-Point Topology

The **Point-to-Point** messaging pattern is a type of queue-based topology. In this pattern:

- Messages are sent to a queue, and each message is consumed by only one consumer.
- Multiple consumers can listen to the same queue, but each message is delivered to only one of them (competing consumers).
- This is useful for load balancing work across multiple consumers, ensuring that each task is processed only once.

In this sample, the `Point2Point` project demonstrates how to implement the point-to-point pattern using RabbitMQ and MassTransit. This approach is ideal for scenarios where you want to distribute tasks among several workers, rather than broadcasting to all subscribers.

## About the FanOut Topology

The **FanOut** messaging pattern is a type of publish/subscribe topology. In this pattern:

- A message published to a *fanout exchange* is delivered to all queues bound to that exchange.
- All subscribers receive a copy of every message, regardless of routing keys.
- This is useful for broadcasting messages to multiple consumers simultaneously.

In this sample, the `FanOut` project demonstrates how to implement the publish/subscribe pattern using RabbitMQ and MassTransit. 
MassTransit abstracts the underlying infrastructure (RabbitMQ) away for a more friendlier and message-bus-technology-agnostic experience and avoids a lot of boilerplating.

---
