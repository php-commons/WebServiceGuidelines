# Introduction

This document serves as a guideline to how to develop RESTful web services at Providence Health Plan using C# and the .Net Core framework. Much of the information here applies to web applications as well. The goal of this document, alongside example code, is to provide developers with context to use in creating effective web services. This document is also focused on a microservice based approach, in which web services are not nearly as monolithic as older web services and interact in different ways from more monolithic web based systems. 

C# is one of the preferred platforms for web services in PHP, but not the only one. Guidance for other platforms (Mulesoft, NodeJS) are discussed in separate documents.  

Throughout this document, certain things are noted to be forbidden or not to be used. Of course, the real world requires that there are no absolutes; but the reader must deeply consider the reasons for these prohibitions before breaking them.

# The C# platform 

C# is a preferred platform as it is used frequently here at PHP for projects and with the advent of .Net Core, it is a powerful, cross platform tool for software development. More specifically, it has excellent frameworks for creating web services and applications using modern best practices.

The .Net Core Framework is also chosen as it is the future of .Net and provides the widest possible target for runtime environments. Of course, certain web services may need to use libraries that only run in the full .Net Framework. Even in this case, libraries in .Net Core are still to be used whenever possible. 

## .Net Core and the Full .Net Framework

Developers familiar with .Net Framework can be excused for being confused about how the .Net Framework has changed with .Net Core. While things have improved, certain misunderstandings still can happen. 

The most common misconception is the incorrect assumption that .Net Core libraries cannot be used with the .Net Framework. For older versions of the .Net framework (v4.0 and older) this can be true. But, right now, all .Net Core 2.0 libraries can run on .Net Framework version 4.6.1 (and later) as well as .Net Core version 2.0. So, if a developer is developing a solution that requires a library that is .Net Framework only (no .Net Core version exists), other .Net Core libraries can still be used. 

## .Net Core and Cross Platform Support

.Net Core is no longer a Windows specific product; it is available for Windows and Linux. This means that certain technologies will never be part of .Net Core as they specifically target Windows (i.e desktop application libraries). In the case of RESTFul web services, .Net Core and cross platform is clearly where the focus is. 

.Net Core is built on a foundation of open source. The core tools, platform, compilers and libraries are all open source and available on GitHub. This means that developers that aren't familiar with GitHub and Git may need to become more familiar with it.  

## .Net Core and NuGet

.Net Core and the .Net Framework has a mature package management tool in the form of NuGet. The core NuGet repository hosts most significant .Net Libraries and is integrated directly into Visual Studio. Tools for creating and publishing NuGet packages are also available. 

Also, creating custom NuGet feeds (a collection of packages) is as easy as providing a network share: No specific server is required. This makes sharing internal packages for reuse very simple. 

# Commonly Used Libraries

This section covers a small set of libraries that are likely to be used by web service projects. As expected, these libraries are available and updated via NuGet.

## ASP .Net Core

All web services should be based on ASP .Net Core. ASP .Net Core provides a unified programming model for web applications and web services. Also, ASP .Net Core allows for self hosting via Kestrel, which works well for hosting with in a container, Service Fabric, Azure App Services or AWS Elastic Beanstalk. IIS support is also available. 

Self hosting is a key requirement. Services should not rely on IIS. Even though ASP .Net Core servers can be hosted in IIS, this is not to be used in production unless absolutely required. Container based hosting environments or self hosting is the preferred and better option. 

ASP .Net Core is significantly different from older versions of ASP .Net MVC and ASP .Net WebApi. Not only did it merge these two libraries (which had significant overlap), it changed the overall model of applications are setup. Older versions of ASP .Net MVC and WebAPI had ties to IIS concepts and some parts of the ASP .Net Web Forms framework. ASP .Net Core completely changes these fundamental concepts, and uses a model that is completely independent of any hosting environment or older legacy frameworks and embraces modern web service practices. 

The self hosting nature of ASP .Net Core also makes it easier to set up a development environment. IIS is no longer a requirement. Also, Visual Studio 2017 provides excellent Docker integration, making it easy to build and run web services in containers.  

## Entity Framework Core

When accessing SQL Server databases (a very common task), Entity Framework Core should be preferred (see https://docs.microsoft.com/en-us/ef/efcore-and-ef6/features for a comparison). EF6 can be used if a specific feature is not in Entity Framework Core, but check back frequently to make sure that the features has not been added to a later version of EF Core. If it has, porting to the new framework is fairly easy. 

In some cases, EF Core may have too much overhead. When primary executing queries or simple updates, Dapper (https://github.com/StackExchange/Dapper) can be used as an alternative. It is very lightweight and easy to use. Lower level APIs for accessing databases (for example ADO .Net) are not to be used. Both Dapper and EF code is much easier to test and maintain. 

In any case, developers should not assume the presence of a existing database, but allow for using tools like SQLite and/or SQL Server LocalDB for local development and unit testing as much as possible. EF Core has specific tools to make these tasks easier. 

## JSON.Net

Web services use JSON extensively, and the JSON .Net library is to be used (in fact, many parts of ASP .Net Core require it). It is mentioned here because JSON.Net is not developed by Microsoft and some developers may be tempted to ignore libraries that aren't from Microsoft itself. But, using third party and/or open source libraries is a key part of .Net Core development and this is one of the most visible examples. 

## SimpleInjector

While ASP .Net Core (and by extension, .Net Core) has some DI support, it is not as fully featured as some other DI containers. SimpleInjector is recommended as the DI container, as it integrates with ASP .Net Core well and is well documented and supported (see https://github.com/simpleinjector/SimpleInjector). 

However, the proper use of Dependency Injection is the most important aspect of a code base. Also, it should be relatively easy to replace one DI library for another. 

# Key Architectural Principles

A proper web service is one in which the details of interacting with resources via HTTP (and JSON based data) is separated for the underlying business models and rules of the resource. 

Often, in examples for ASP .Net Core, a shortcut is taken in which those details are placed within the Asp .Net controllers themselves. This is expedient for demonstration purposes, but quickly is not viable for production. 

In a production quality service, the resource business models and rules (the set of objects that represents the resource from the viewpoint of the business) should be in a separate library from the web service. This means that business logic can be tested (via proper mocking and other unit testing practices) independently of being hosted in a web service. Of course, the parts that mediate between the web and the core domain need to be tested and as well as the services themselves. ASP .Net core provides specific support for unit testing framework to enable these scenarios. 

## SOLID Code

Developers should take the time to study and apply the SOLID principles to their code base. SOLID is an acronym that captures the following principles of good code:

- Single Responsibility Principle
- Open/Closed Principle 
- Liskov Substitution Principle
- Interface Segregation Principle
- Dependency Inversion Principle

There are excellent resources about these concepts on the web. 

## Resource Naming and Conventions

RESTful web services are oriented around resources. To be consistent, the plural form of a resource should be used. For example, for membership, the base URI of the resource should be /api/Members, not /api/Member. Also, by convention, every web service should start at /api. This allows for better hosting of other assets and is a common pattern for many restful APIs.

Often, the identification of the resource is key to the success of an overall web service. A service that has a lot of root resources is often a sign of poor design.

## Domain Driven Design

While there are many paths to implementing business models and rules, it is also the case that developers can fall into many traps along the way. Of course, no development project is easy, but in the case of web services, the basic principles of Domain Driven Design (DDD for short) should be considered by all developers. DDD introduces concepts like Entities, Value Object, Aggregate Roots, Services, Repositories to better create software. These set of objects are often referred to as a Domain model. These concepts mesh very well with web services.

DDD defines a way to organize classes into various categories that make for more flexible domain models. A basic understanding of these principles is important, as well as understanding antipatterns to avoid. As an example, the Anemic Object is often discussed as an antipattern, even though it's use is very common.

While numerous references exist, the book Domain Driven Design: Tackling Complexity in the Heart of Software can not be recommended more highly. It is not brief, but it is accessible to any developer with a basic set of object oriented programming skills. Other excellent references do exist as well.

Also, as services get more complex, implementing the Command Query Response Segregation (CQRS) pattern can be of great use (see https://docs.microsoft.com/en-us/azure/architecture/patterns/cqrs for more information). Additionally, some understanding of the Event Sourcing Pattern and Even Driven architecture is useful.

## Persistence Ignorance

Not only should the domain model be unaware of web service specific details, but it should be unaware of how data is persisted. In DDD driven design, this is achieved via the repository pattern. It is highly recommended that persistence specific code be place in a separate library from the domain, just as the web service specific code is. 

Additionally, a developer should not assume there is a simple mapping between your domain objects and a store. In many cases, they will need to define and use Data Transfer Objects to mediate between a store and domain objects. Again, in many demonstration projects, this assumption is made for the sake of simplicity. 

## Inversion of Control and Dependency Injection

While it is specified in the SOLID principles, it is worth calling out Dependency Injection more directly. Using ASP .Net Core and Domain models requires an understanding of the Inversion of Control and Dependency Injection patterns. The reader, for the sake of brevity, is encouraged to do their own research online. As noted above, SimpleInjector is the preferred DI library.

## Thin Controller

In ASP .Net Core, the controller classes should be thin. This means that they validate their input, delegate to the appropriate domain service, transform the result (if needed) and return the expected results. In many code examples, ASP .Net controllers talk directly to a database. While this is expedient, it is not scalable or as easily testable as a properly abstracted design. 

## Container Based Development

Where ever possible, Docker should be used to build and package the web services. Microsoft has specific support for Docker in Visual Studio and Docker can greatly simplify continuous integration and deployment 

TODO: Discussion over docker registries, semantic versioning and tagging. 
    
## Microservices and Service Code Size

Modern web services should strive to be strongly cohesive and serve a small set of business needs. While there are no hard and fast rules, a suggested size is a service that takes no more than 2 small teams (4-6 people per team) to maintain. Also, the size should be that a complete reimplementation of a service would take no more than two to three months by one or two teams. This also means that major new versions can be created and deployed at least four times a year.

This focus is to help balance reusability of services for multiple use cases versus creating monolithic systems.    

# Service Quality 

Some specific aspects of quality need to be addressed. Firstly, semantic versioning is to be used and applied to all services. While tracking build numbers is not required, the consistent and correct use of major, minor and patch numbering is vital to insuring a manageable web service ecosystem. 

## Unit Testing and Isolation

Web Services should be able to be checked out, developed and unit tested without complex external dependencies being required or available whenever possible. This means that external data sources may need to mocked (via mocking frameworks or test doubles). This work enforces an overall design that is flexible to change and not tightly coupled to an external data source or system. 

It should go without saying that a certain level of unit test coverage is required before any service is to be considered for staging and production deployment. Most certainly, no service without any real unit tests can not be accepted as release ready. While this means that extra time and care may be needed to creating a service, it is a required investment in overall quality. 

In the cases that external systems or software is required, the setup of those resources should be completely automated where possible. This aligns with the overall DevOps approaches that a healthy microservices based ecosystem requires. 

Note, this doesn't mean that integration testing with external services is not required, merely that a certain level of development should be possible without it. 

# Service Scalability

There is no simple solution to making web services scalable. However, certain concepts do apply and are briefly discussed.

## Stateless Services 

Wherever possible, services should not hold state between requests, but remain stateless. This allows for better scaling and load balancing. Of course, some cases require stateful services, but doing so requires care and thought.

## Asynchronous Event Based Communication

While it is easier to imagine (and implement) a system as a ordered set of synchronous calls between services, in some cases, this paradigm doesn't scale nor it is robust in the face of transient service failures.

Asynchronous event based communication (using an appropriate event bus like RabbitMO, Azure Service Bus, etc) is often better, despite its additional implementation complexity.

TODO: There is a lot of discussion of how to balance HTTP based rest calls versus message queue based communication. The trend seems to be moving towards using message queues for service to service communication, or in some cases (Azure Service Fabric is an example), specialized internal communication libraries are available.  

## Fail Fast

Modern hosting environments can detect service failures and restart new instances within milliseconds. This means that services need not press on and recover in the face of certain errors, but instead quickly exit. 

This is counterintutive, but in reality, it often more scalable for a service to fail fast then to try and capture and handle many kinds of errors and recover from them.

## Reactive Systems and The Reactive Manifesto

The Reactive Extensions library (RX .Net) and the patterns implemented in it can be invaluable. See http://www.reactivex.io for more.  

While the use of manifesto is a bit much, the document itself contains a lot of high level ideas that are useful to consider. The reader is directed to https://www.reactivemanifesto.org/ for an overview. 

# Security

While it is tempting to enforce HTTPS based communication everywhere, the issue of certificate management and complexity of development becomes very problematic. Also, it is not necessary, as many services should not be exposed directly to outside applications. External access should be mediated via an API gateway or HTTP reverse proxy. The API gateway or reverse proxy can take care of authentication, SSL termination and work with the firewall to enforce policies that apply to applications as a whole. 

## OAuth and JWT

When it is necessary to pass claims about the user (or context) of a web request, Json Web Token should be used as the standard. As keys can be properly protected and tokens signed, a properly formed JWT token can be accepted as is. There is also excellent cross platform support for JWT.

# Service Hosting 

TODO: Discuss various hosting options. 

# Summary

TBD