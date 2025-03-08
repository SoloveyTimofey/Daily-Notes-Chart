## Project Description and its Purpose
This is pet project the purpose of which is to create time-based charts with notes. This can be useful for tracking and noting daily activity. 
I've came up with idea to create this app when i was reading the Why We Sleep book by Matthew Walker. I wanted to make my sleep notes on diagram and note my well-being with the help of app. Of course, when i will finish this app, it will be posiible not only to create sleep notes.

The second and more important purpose of this project is to practice my certain knowledge.

## Fancy Words about Technologies and Aprroaches that were taken in this project
> Clean Architecture, CQRS, DDD, Rich Domain Model, Result Pattern, Auto Mapper, MediatR, EF Core, ASP.NET Core Web API, JWT Bearer, Unit and Architecture Tests, Next.JS
- I tried to apply DDD and create server side architecture based on Clean Architecture. 
- As far as Clean Architecture is a domain centric architecture and i applied DDD, i also tried to use Rich Domain Model, so all business logic is located in Domain layer.
- Also my Core part of architecture is really abstracted away from concrete infrastructure details like Microsoft.AspNetCore.Identity and EF Core.
- What also interesting is that i applied the Result pattern instead of throwing exceptions in the parts where error is expected behavior of the program.

## Here are Sources of Ideas that isnpired me:

Result Pattern:
- [Get Rid of Exceptions in Your Code With the Result Pattern](https://www.youtube.com/watch?v=E3dU9Y1CsnI)
- [Exceptions Are Extremely Expensive… Do This Instead](https://www.youtube.com/watch?v=E3dU9Y1CsnI)

Rich Domain Model:
- [Is an Anemic Domain Model an Anti-Pattern?](https://www.youtube.com/watch?v=6gwIDiUk2h4)

Books:
- Clean Architecture by Robert Martin
- Domain-Driven Design: Tackling Complexity in the Heart of Software by Eric Evans
