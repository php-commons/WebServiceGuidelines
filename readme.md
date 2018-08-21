# Introduction 

This project is a conceptual web service that represents a bare-bones banking account and some simple transactions. If you haven't, read the WebServiceGuidelines and CSharpGeneralNotes documents before moving on. 

NOTE: This code base is incomplete, there are exercises for the reader to work on to better understand the code. And bugs may be present that need to be fixed.

We don't address certain issues in terms of authentication and tracking identity. The assumption is that this service is part of a microservices environment and appropriate reverse proxies and API gateways are used. 

The exercises are as follows:

0. The solution uses Visual Studio 2017, so you will need that or the .Net Core SDK 2.0 and the command line tools. Visual Studio Code is also recommended. Visual Studio 2017 is available via MSDN. 

1. In the commands part of the domain library, there are four commands. They are inconsistent with each other. What is the inconsistency? Make all the commands consistent. 

2. There is a test project, but there are no tests. This needs to be fixed. Add some basic unit tests for the domain and persistence libraries. As a bonus, add unit tests for the web service project. Hint: This project uses xUnit.

3. Not all the classes use the Contract If Throw (CIF) pattern consistently. Fix that so all the classes are consistent. 

4. Update the AccountController class to allow accounts to be created via the web service. (Hint, think about REST and what HTTP verb is used, what JSON is sent, and so on).

5. Add a command to process a transaction that actually updates the account balance, Change the domain objects, repositories and services as you see fit and expose it via the web service. Hint: You may have to revisit the Transaction entity. 

6. If you haven't, update your unit tests. Also, make a note to always keep your unit tests and changes in sync. Also, make sure you are still using 

This should give an good start on using ASP .Net Core and basic Domain Driven Design concepts. For those that are motivated, the following are bonus exercises.

1. The persistence layer is all in memory. Replace it with a database-backed store using Entity Framework Core. 

2. If you didn't already, have the transactions stored in a completely separate database from the accounts. 

3. Using your free Azure account via MSDN, create a storage account and use blob or table storage for the transactions. Note, transactions should have no meaningful information. No PHI, nor anything that even looks like PHI. Bonus, deal with transient network failures in your code. 

4. Update the service to use ASP .Net Core middleware that checks for a valid JWT in the Authentication header. You can do this by plugging into the Authentication pipeline or by creating completely custom middleware.