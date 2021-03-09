# Intent

This code is in response to https://github.com/idsogeti/tech-assessment

# What to expect

- This Web API application was written in C# using the ASP.NET 5.0 Framework.
- The application uses Swagger docs which can be accessed by running the application locally.
- OOP standards and SOLID principles
  - Abstraction:
    - The Order class extends the BaseModel class. The BaseModel class gives some standard Audit fields that would likely be implemented in all future data object.
  - Encapsulation:
    - The OrderStore keeps the actual list of Orders private but the list can be accessed by methods within the class (AddOrder, GetOrder, GetOrders.)
  - Inheritance:
    - The OrderService and OrderStore inherit from IOrderService and IOrderStore respectively. Aside from allowing us to implement multiple inheritances, this allows for dependency injection and mocking (in unit tests)
  - Polymorphism:
    - There are examples of static polymorphism in the DTOs and Models by implementing multiple constructors.
  - Single Responsibility Principle:
    - The logic is separated into 3 reasonable chunks. The controller communicates with the client. The service handles data manipulation. The store handles storing and retrieving the data.
  - Open-Closed Principle:
    - By using Interfaces for the Service- and Store- level classes & using dependency injection, we have left them open for extension but closed for modification. New implementations of those interfaces could be utilized to make slight modifications to the functionality.
  - Dependency-Inversion Principle:
    - By accepting any implentation of IOrderService interface in the OrderController constructor, we could easily swap out the OrderService for another implenation without breaking things.
- Dependency Injection
  - The OrderService is injected as a Scoped service. This is so that the service is unique to lifespan of each individual web request.
  - The OrderStore is injected as a Singleton service. This is so that the list of Orders can be stored in memory without need for a database. If this were a real-world application, this Store would reach out to an external database.
- Unit Tests
  - All unit tests were written using X-Code.
  - Moq was used to mock services called by the class being tested.
  - The controller, service, store, DTOs, and models have 100% code coverage.
  - While 100% code coverage does not ensure perfect code, it's a good place to start. We all know that QAs are a special breed and have an amazing knack for breaking things that the Devs think are unbreakable.
- Gold Plating
  - I tried to avoid it, but I did want to add pagination and search ability to the GetOrders API. While it wasn't called for, it's such a standard request, that I figured I should add it.
  - I added StyleCop and thus implemented a lot of XML documentaion. It was probably way overkill for this task, but heavy documentation is something that I try to implement in every project.
