# Introduction

This document serves as a guideline to how to develop RESTful web services at Providence Health Plan. Much of the information here applies to web applications as well. The goal of this document, alongside example code, is to provide developers with context to use in creating effective web services. This document is also focused on a microservice based approach, in which web services are not nearly as monolithic as older web services and interact in different ways from more monolithic web based systems. 

Throughout this document, certain things are noted to be forbidden or not to be used. Of course, the real world requires that there are no absolutes; but the reader must deeply consider the reasons for these prohibitions before breaking them.

# Recommended Platform 

The recommended platform is C# and .Net Core Framework (version 2.0 or greater). When there is a significant benefit, Javascript and NodeJS can be used instead of C# and .Net Core. A separate document will discuss best practices for JavaScript and NodeJS. The rest of this document pertains to C# and the .Net Core Framework.

C# is chosen as it is used most frequently here at PHP for projects, and existing products have been developed using the .Net Framework, and with the advent of .Net Core, it is a powerful, cross platform tool for software development. 

The .Net Core Framework is chosen as it is the future of .Net and provides the widest possible target for runtime environments. Of course, certain web services may need to use libraries that only run in the full .Net Framework, but the libraries in .Net Core are still to be used whenever possible. 

Developers familiar with .Net Framework can be excused for being confused about how the .Net Framework has changed with .Net Core. While things have improved, certain misunderstanding still can happen. 

As an example, consider the incorrect assumption that .Net Core libraries cannot be used with the .Net Framework. For older versions of the .Net framework (v4.0 and older) this can be true. But, right now, all .Net Core 2.0 libraries can run on .Net Framework version 4.6.1 (and later) as well as .Net Core version 2.0. 
So, if a developer is developing a solution that requires a library that is .Net Framework only (no .Net Core version exists), other .Net Core libraries can still be used. 

.Net Core is no longer a Windows specific product; it is available for Windows and Linux. This means that certain technologies will never be part of .Net Core as they specifically target Windows (i.e desktop application libraries). In the case of web services, .Net Core is clearly where the focus is. 

.Net Core is built on a foundation of open source. The core tools, platform, compilers and libraries are all open source and available on GitHub. This means that developers that aren't familiar with commonly used open source tooling will need to become more familiar with it.  

# Commonly Used Libraries

This section covers a small set of libraries that are likely to be used by web service projects. As expected, these libraries are available and updated via NuGet.

## ASP .Net Core

All web services should be based on ASP .Net Core. ASP .Net Core provides a unified programming model for web application and web services. 
Also, ASP .Net Core allows for self hosting via Kestrel.

Self hosting is a key requirement. Services should not rely on IIS. Even though ASP .Net Core servers can be hosted in IIS, this is not to be used. Container based hosting environments or self hosting is the preferred and better option. IIS can still be used to serve static files or front-facing web applications that use web services.

ASP .Net Core is significantly different from older versions of ASP .Net MVC and ASP .Net WebApi. Not only did it merge these two libraries (which had significant overlap), it changed the overall model of applications are setup. Older versions of ASP .Net MVC and WebAPI had ties to IIS concepts and some parts of the ASP .Net Web Forms framework. ASP .Net Core completely changes these concepts, and uses a model that is completely independent of any hosting environment or older legacy frameworks. 

For example, older .Net web services where often hosted on IIS. While self hosting was possible, it wasn't commonly done as developers were comfortable with an IIS based hosting experience (via IIS on a local box or IIS Express). 

ASP .Net Core breaks with that tradition. Out of the box, a ASP .Net Core web service is a console application running in the .Net Core environment. This speaks to the cross-platform nature of .Net Core. A .Net Core web service can run on Linux or Windows. 

## Entity Framework Core

When accessing SQL Server databases (a very common task), EntityFramework Core should be used unless absolutely not needed (see https://docs.microsoft.com/en-us/ef/efcore-and-ef6/features for a comparison). For certain use cases where a lighter weight solution has a noticeable advantage, Dapper (https://github.com/StackExchange/Dapper) can be used as an alternative. Lower level APIs for accessing databases (for example ADO .Net) are not to be used. 

## JSON.Net

Web services use JSON extensively, and the JSON .Net library is to be used (in fact, many parts of ASP .Net Core require it). It is mentioned here because JSON.Net is not developed by Microsoft and certain developers often ignore libraries that aren't from Microsoft itself. But, using third party and/or open source libraries is a key part of .Net Core development.

## SimpleInjector

While ASP .Net Core (and by extension, .Net Core) has some DI support, it is not as fully featured as some other DI containers. SimpleInjector is recommended as the DI container, as it integrates with ASP .Net Core well and is well documented and supported (see https://github.com/simpleinjector/SimpleInjector).

# Key Architectural Principles

A proper web service is one in which the details of interacting with resources via HTTP (and JSON based data) is separated for the underlying business models and rules of the resource. 

Often, in examples for ASP .Net Core, a shortcut is taken in which those details are placed within the Asp .Net controllers themselves. This is expedient for demonstration purposes, but quickly is not viable for production. 

In a production quality service, the resource business models and rules (the set of objects that represents the resource from the viewpoint of the business) should be in a separate library from the web service itself. 

This means that business logic can be tested (via proper mocking and other unit testing practices) independently of being hosted in a web service. Of course, the parts that mediate between the web and the core domain need to be tested, and ASP .Net Core web services can be easily unit tested. 

## Domain Driven Design

While there are many paths to implementing business models and rules, it is also the case that developers can fall into many traps along the way. Of course, no development project is easy, but in the case of web services, the basic principles of Domain Driven Design (DDD for short) should be considered by all developers. DDD introduces concepts like Entities, Value Object, Aggregate Roots, Services, Repositories to implement business concepts. These set of objects are often referred to as a Domain model. These concepts mesh very well with web services.

While numerous references exist, the book Domain Driven Design: Tackling Complexity in the Heart of Software can not be recommended more highly. It is not brief, but it is accessible to any developer with a basic set of object oriented programming skills. Other excellent references do exist as well.

DDD defines a way to organize classes into various categories that make for more flexible domain models. A basic understanding of these principles is important, as well as understanding antipatterns to avoid. As an example, the Anemic Object is often discussed as an antipattern, even though it's use is very common.

Also, as services get more complex, implementing the Command Query Response Segregation (CQRS) pattern can be of great use (see https://docs.microsoft.com/en-us/azure/architecture/patterns/cqrs for more information). Additionally, some understanding of the Event Sourcing Pattern and Even Driven architecture is useful.

## Persistence Ignorance

Not only should the domain model be unaware of web service specific details, but it should be unaware of how data is persisted. In DDD driven design, this is achieved via the repository pattern. It is highly recommended that persistence specific code be place in a separate library from the domain, just as the web service specific code is. 

Additionally, a developer should not assume there is a simple mapping between your domain objects and a store. In many cases, they will need to define and use Data Transfer Objects to mediate between a store and domain objects. Again, in many demonstration projects, this assumption is made for the sake of simplicity. 

## Inversion of Control and Dependency Injection

Using ASP .Net Core and Domain models requires an understanding of the Inversion of Control and Dependency Injection patterns. The reader, for the sake of brevity, is encouraged to do their own research online. As noted above, SimpleInjector is the preferred DI library.

# Container Based Development

To be added when more information is available. Briefly, all services should be tested and maintained via Docker.

Points of discussion:
    
* Dockerfile bases
* Private Docker hub
* Hosting environment

# Microservices and Service Code Size

Modern web services should strive to be tightly coupled and serve a small set of business needs. While there are no hard and fast rules, a suggested size is a service that takes no more than 2 small teams (4-6 people per team) to maintain. Also, the size should be that complete re-implementation of a service would take no more than two to three months by those teams. This also means that major new versions can be created and deployed at least four times a year.

This tight focus on code size is to focus development of a loosely coupled set of services where each service has high cohesion. Web services can enforce lower levels of coupling than object oriented code can, and good web services take advantage of that.   

# Service Scalability

There is no simple solution to making service scalable. However, certain concepts do apply and are briefly discussed.

## Stateless services 

Wherever possible, services should not hold state between requests, but remain stateless. This allows for better scaling and load balancing. Of course, some cases require stateful services, but those cases need to be managed carefully. 

## Asynchronous Event Based Communication

While it is easier to imagine (and implement) a system as a ordered set of synchronous calls, this paradigm doesn't scale, nor it is robust in the face of transient service failures. Asynchronous event based communication (using an appropriate event bus like RabbitMO, Azure Service Bus, etc) is much better, despite its additional implementation complexity. 

To be added: There is a lot of discussion of how to balance HTTP based rest calls versus message queue based communication. The trend seems to be moving towards using message queues for service to service communication, or in some cases (Azure Service Fabric is an example), specialized internal communication libraries are available.  

## Fail Fast

Modern hosting environments can detect service failure and restart new instances within milliseconds. This means that services need not press on and recover in the face of certain errors, but to quickly exit. This is counterintutive, but in reality, it often more scalable for a service to fail and fail over to occur than to try and capture and handle many kinds of errors. 

## The Reactive Manifesto

While the use of manifesto is a bit much, the document itself contains a lot of high level ideas that are useful to consider. The reader is directed to https://www.reactivemanifesto.org/ for an overview. 

Also, the Reactive Extensions library (RX .Net) and the patterns implemented in it can be invaluable. See http://www.reactivex.io for more.  

# Security

While it is tempting to enforce HTTPS based communication everywhere, the issue of certificate management and complexity of development becomes very problematic. Also, it is not necessary, as services should not be exposed directly to outside applications. External access should be mediated via an API gateway and direct access blocked via a firewall. The API gateway takes care of authentication (via OAuth) and works with the firewall to isolate services from the outside world. 

# Service Hosting 

This will likely be about MuleSoft. 

# Summary

TBD