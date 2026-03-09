# eComAPI --- Event‑Driven E‑Commerce API with CQRS

A modern **E‑Commerce backend API** demonstrating **CQRS (Command Query
Responsibility Segregation)** and **Event‑Driven Architecture** built
with **.NET / ASP.NET Core**.

This project showcases how to design scalable backend systems by
separating **read and write workloads**, emitting **domain events**, and
projecting optimized **read models**.

------------------------------------------------------------------------

# Key Features

-   CQRS architecture
-   Event‑driven design
-   Separate read and write databases
-   Event projections
-   Clean architecture principles
-   RESTful API
-   Entity Framework migrations
-   Lightweight SQLite database

------------------------------------------------------------------------

# System Architecture

Below is the high‑level architecture of the system.

``` mermaid
flowchart LR
    Client[Client Application]
    API[ASP.NET Core API]
    Commands[Command Handlers]
    Queries[Query Handlers]
    WriteDB[(Write Database)]
    EventBus[(Event Dispatcher)]
    Projections[Projection Handlers]
    ReadDB[(Read Database)]

    Client --> API
    API --> Commands
    API --> Queries

    Commands --> WriteDB
    Commands --> EventBus

    EventBus --> Projections
    Projections --> ReadDB

    Queries --> ReadDB
```

### Architecture Highlights

-   **Commands** modify system state
-   **Events** are emitted after successful writes
-   **Projections** update read models
-   **Queries** read optimized data models

This separation improves:

-   scalability
-   performance
-   maintainability

------------------------------------------------------------------------

# CQRS Request Flow (Sequence Diagram)

This diagram shows how a typical command flows through the system.

``` mermaid
sequenceDiagram
    participant Client
    participant API
    participant CommandHandler
    participant WriteDB
    participant Event
    participant Projection
    participant ReadDB

    Client->>API: Create Product Request
    API->>CommandHandler: Execute Command
    CommandHandler->>WriteDB: Save Product
    WriteDB-->>CommandHandler: Success
    CommandHandler->>Event: Publish ProductCreatedEvent
    Event->>Projection: Trigger Projection
    Projection->>ReadDB: Update Read Model
    Client->>API: Query Products
    API->>ReadDB: Fetch Data
    ReadDB-->>Client: Return Product List
```

------------------------------------------------------------------------

# Event Lifecycle

The lifecycle of a domain event in the system.

``` mermaid
flowchart TD
    Command[Command Received]
    Handler[Command Handler]
    WriteDB[(Write Database)]
    Event[Domain Event Created]
    Dispatcher[Event Dispatcher]
    Projection[Projection Handler]
    ReadDB[(Read Database)]
    Query[Query Request]

    Command --> Handler
    Handler --> WriteDB
    Handler --> Event
    Event --> Dispatcher
    Dispatcher --> Projection
    Projection --> ReadDB
    Query --> ReadDB
```

### Event Flow Explained

1.  A **command** changes application state.
2.  The **write database** is updated.
3.  A **domain event** is created.
4.  The **event dispatcher** publishes the event.
5.  **Projections** update read models.
6.  Clients query the **read database**.

------------------------------------------------------------------------

# Project Structure

    eComAPI
    │
    ├── Commands
    ├── Queries
    ├── Events
    ├── Handlers
    ├── Projections
    ├── Models
    ├── DTOs
    ├── Data
    ├── Migrations
    │
    ├── Write.db
    ├── Read.db
    └── NoCQRS.db

------------------------------------------------------------------------

# Technology Stack

## Backend

-   C#
-   .NET
-   ASP.NET Core

## Architecture

-   CQRS
-   Event‑Driven Architecture
-   Clean Architecture

## Data

-   SQLite
-   Entity Framework Core

------------------------------------------------------------------------

# Running the Application

## Clone the repository

``` bash
git clone https://github.com/yourusername/eComAPI.git
cd eComAPI
```

## Run the API

``` bash
dotnet run
```

------------------------------------------------------------------------

# Learning Goals

This project demonstrates:

-   CQRS implementation in .NET
-   Event‑driven design
-   Read/write model separation
-   Event projections
-   scalable backend architecture patterns

------------------------------------------------------------------------

# Possible Improvements

Future improvements may include:

-   Docker containerization
-   Kafka or RabbitMQ event streaming
-   Redis distributed caching
-   Kubernetes deployment
-   distributed tracing
-   observability stack
