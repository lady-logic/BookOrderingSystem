# BookOrderingSystem

Eine verteilte Microservices-Demonstration mit .NET und RabbitMQ, die event-getriebene Architekturmuster demonstriert.

## Architektur

```
OrderService → BookOrderSubmitted → BillingService → PaymentProcessed → NotificationService
```

Das System besteht aus drei unabhängigen Services, die asynchron über RabbitMQ kommunizieren:

- **OrderService**: Veröffentlicht Bestellevents mit zufälligen Buchdaten
- **BillingService**: Verarbeitet Zahlungen und veröffentlicht Zahlungsbestätigungen
- **NotificationService**: Behandelt Zahlungsbestätigungen und Logging

## Technology Stack

- **.NET 8** - Laufzeitumgebung und Framework
- **MassTransit** - Message Bus Abstraktionsschicht
- **RabbitMQ** - Message Broker
- **Docker & Docker Compose** - Containerisierung und Orchestrierung

## Schnellstart

### Voraussetzungen
- Docker Desktop
- .NET 8 SDK (für lokale Entwicklung)

### Mit Docker Compose starten
```bash
git clone <repository-url>
cd BookOrderingSystem
docker-compose up --build
```

### Services
- **RabbitMQ Management UI**: http://localhost:15672 (guest/guest)
- **OrderService**: Generiert alle 5 Sekunden Bestellungen
- **BillingService**: Verarbeitet Zahlungen mit 2-Sekunden-Simulation
- **NotificationService**: Loggt erfolgreiche Zahlungen

## Projektstruktur

```
BookOrderingSystem/
├── Contracts/               # Geteilte Message-Contracts
│   ├── BookOrderSubmitted   # Bestell-Event Definition
│   └── PaymentProcessed     # Zahlungs-Event Definition
├── OrderService/            # Bestell-Publisher Service
├── BillingService/          # Zahlungsverarbeitungs-Service
├── NotificationService/     # Benachrichtigungs-Service
├── docker-compose.yml       # Multi-Container Orchestrierung
└── */Dockerfile            # Individuelle Service Container
```

## Demonstrierte Konzepte

- **Event-Driven Architecture**: Lose Kopplung durch Message Passing
- **Microservices Pattern**: Unabhängige, deploybare Services
- **Asynchrone Verarbeitung**: Non-blocking Kommunikation via Message Queues
- **Containerisierung**: Docker-basierte Deployment-Strategie
- **Message Contracts**: Typisierte Inter-Service-Kommunikation

## Entwicklung

### Lokale Entwicklung
Jeder Service kann unabhängig für die Entwicklung gestartet werden:

```bash
# Terminal 1 - RabbitMQ starten
docker run -d -p 5672:5672 -p 15672:15672 rabbitmq:3-management

# Terminal 2-4 - Services starten
cd OrderService && dotnet run
cd BillingService && dotnet run  
cd NotificationService && dotnet run
```

### Konfiguration
Services erkennen automatisch die Umgebung:
- **Lokal**: Verbindet zu `localhost:5672`
- **Docker**: Verbindet zu `rabbitmq:5672` via Container-Networking

## Message Flow

1. **OrderService** generiert `BookOrderSubmitted` Events
2. **BillingService** konsumiert Bestellungen, simuliert Zahlungsverarbeitung
3. **BillingService** veröffentlicht `PaymentProcessed` Events
4. **NotificationService** loggt Zahlungsbestätigungen

Die gesamte Kommunikation ist asynchron und ausfallsicher durch RabbitMQs Delivery-Garantien.
