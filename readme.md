# Introduction 

This project is a conceptual web service that represents a bare-bones banking account and some simple transactions. This code base is incomplete, there are exercises for the reader to work on to better understand the code. And bugs may be present that need to be fixed.

We don't address certain issues in terms of authentication and tracking identity. Also, the assumption is that this service is part of a microservices environment and is not exposed directly to third parties, but is accessible only by hosted web applications. 

The exercises are as follows:

0. The solution uses Visual Studio 2017, so you will need that or the .Net Core SDK 2.0 and the command line tools. Visual Studio Code is also recommended. Visual Studio 2017 is available via MSDN. 

1. In the commands part of the domain library, there are four commands. They are inconsistent. What is the inconsistency? Make all the commands consistent. 

2. There is a test project, but there are no tests. This needs to be fixed. Add some basic unit tests for the domain and persistence libraries. As a bonus, add unit tests for the web service project. Hint: This project uses xUnit and you will need to check the references.

3. Update the AccountController class to allow accounts to be created via the web service. (Hint, think about REST and what HTTP verb is used, what JSON is sent, and so on).

4. Add a command to process a transaction.  Change the domain object, repositories and services as you see fit and expose it via the web service. Hint: You may have to revisit the Transaction entity. 

5. If you haven't, update your unit tests. Also, make a note to always keep your unit tests and changes in sync.

This should give an good start on using ASP .Net Core and basic Domain Driven Design concepts. For those that are motivated, the following are bonus exercises.

1. The persistence layer is all in memory. Replace it with a database-backed store using Entity Framework core. 

2. If you didn't already, have the transactions stored in a completely separate database from the accounts. 

3. Using your free Azure account via MSDN, create a storage account and use blob or table storage for the transactions. Note, transactions should have no meaningful information. No PHI, nor anything that even looks like PHI. Bonus, deal with transient network failures in your code. 