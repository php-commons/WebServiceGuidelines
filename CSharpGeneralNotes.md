Introduction
============

This document is to introduce those newer to the .Net Platform to some of the
paradigms used in this example. This should be useful to developers overall of
any level as well. We briefly touch on some common areas and how they are
approached in this sample.

Code Style
==========

Code should follow common naming conventions for .Net Code. In this example, we
adopt the common practice of using underscores as prefix where appropriate for
private fields. If one has ReSharper installed, it will suggest naming
convention fixes when they occur. Overall, the consistent use of casing is the
most important. For those used to Java, this means adopting Pascal Casing for
public properties and methods. One clear sign that one is new to .Net is the use
of Camel Casing.

Also, the use of XML comments is encouraged for all classes and methods. There
are tools that can be used to generate API documentation from such comments, but
even without such tools, their overall structure is very useful. Of course, the
appropriate use of comments is encouraged throughout.

Code Contracts and Precondition Checking
========================================

One of the most common source of errors comes from code that does not explicitly
state and check for what must be true before a method (or constructor) can be
called. This is so important that we use both Code Contracts to state these
preconditions as well as the use of if/throw blocks at the start of each method
and constructor.

Null Checking
-------------

One of the most common oversights in code is not checking for null parameters
when a null value is not acceptable. This is so much of an issue that C\# 8.0
will introduce a whole new set of tools and operators around this. For now, one
should adopt the following pattern for all non-null arguments:

    public void SampleMethod(object aParameter)
    {
        Contract.Requires(aParameter != null);
        if (aParameter == null)
            throw new ArgumentNullException(nameof(aParameter));
    }

Range Checking 
---------------

In other cases, one needs to check if a parameter is not null and/or is in a
certain range. The most common example of this is strings that can’t be null or
empty. For these arguments, use the following pattern.

    public void SampleMethod(string aParameter)
    {
        Contract.Requires(!String.IsNullOrEmpty(aParameter));
        if (String.IsNullOrEmpty(aParameter))
            throw new ArgumentOutOfRangeException(nameof(aParameter));
    }

Note the use of ArgumentOutOfRangeException. The very detailed developer may
separate the null check from the range check. In practice, this can complicate
exception handling by having to handle two distinct exceptions. But for a highly
practiced team, this level of precision is acceptable.

The use of the Contract static methods for structured documentation followed by if
and throw block is so common, we call it the CIF pattern. 

Constructors and Anemic Objects
-------------------------------

Another common source of errors is creating objects that assume that all values
for it’s properties and fields are valid and does not enforce constraints on
those values. For example, take a class that captures a person’s full name.

    public class FullName
    {
        public string FirstName {get; set;}
        public string MiddleName {get; set;}
        public string LastName {get; set;}
    }

Note, while simple, this object does no checking on the values. So, one can make
a FullName object with all the properties being empty or null. Such objects are
called anemic objects Anemic objects have a role (e.g. DTOs, ViewModels), but in
this case (and many others) this is clearly not acceptable. A better version is
presented below:

    public class FullName
    {
        public FullName(string firstName, string lastName) : this(firstName, "", lastName)
        {
        }

        public FullName(string firstName, string middleName, string lastName)
        {
            Contract.Requires(!String.IsNullOrEmpty(firstName));
            Contract.Requires(middleName != null);
            Contract.Requires(!String.IsNullOrEmpty(lastName));
            if (String.IsNullOrEmpty(firstName))
                throw new ArgumentOutOfRangeException(nameof(firstName));
            if (middleName == null)
                throw new ArgumentNullException(nameof(middleName));
            if (String.IsNullOrEmpty(lastName))
                throw new ArgumentOutOfRangeException(nameof(lastName))

                _firstName = firstName;
                _middleName = "";
                _lastName = lastName;
        }

        private string _firstName;
        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                    throw new ArgumentOutOfRangeException();
                _firstName = value;
            }
        }

        private string _middleName;
        public string MiddleName
        {
            get
            {
                return _middleName;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                _middleName = value;
            }
        }

        private string _lastName;
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                    throw new ArgumentOutOfRangeException();

                _lastName = value;
            }
        }
    }

It is important to note that we do not have a default constructor. This is often
a key pattern that is neglected on objects: They don’t enforce prerequisites on
construction. In this example, if it does make sense to have a FullName object
with an null or empty FirstName and/or LastName, there is no reason to allow this
to occur.

### Preconditions and Custom Exception Classes

For custom exception classes, the rules around precondition checking and
constructors do not apply, the objects should be anemic. The reason for this is
that if an exception occurs while constructing an exception to be thrown, the
runtime often has no choice but to crash and exit completely.

Object Checking
---------------

It is not required to check that an object is valid. The assumption is that the
object itself should validate on construction and protect its state. However, it
may be the case that certain methods require extra restrictions on the object
state. An example using a Point class and a method that requires that the X and
Y properties be greater than zero. This is not common, but it is good to be
aware of.

Advanced Use of Contracts
-------------------------

Experienced teams can explore the use of Contracts to document postconditions
and invariants along with preconditions. While no static analysis or dynamic
rewriting tools exist to further support this, future tools may do so. Also, the
nature of the Contract class is that, by default, no code is emitted for the
calls, they serve as structured comments.

Unit Testing
============

Of course, all code should contain unit tests. More importantly, is should be
designed to make it as easy as possible to unit test without lots of external
dependencies using test doubles and mocks. This section touches on some on other
areas specific to this code.

Unit Tests and Code Contracts
-----------------------------

While it can be a bit tedious, one should write unit tests that make sure that
the prerequisites of methods are enforced. In the example of the FullName class,
this would mean tests that check that if the firstName parameter in the
constructor is empty, an ArgumentOutOfRangeException is thrown.

The experienced developer will note that this is a lot of testing for "obvious"
code. If one assumes that Unit Tests are merely about quality, this may seem
like too much overhead for the benefit. But, unit tests are another way of
documenting the behavior of a class. Without these tests, future developers
can’t tell if a prerequisite is really needed or is just out of date.

So, if a prerequisite no longer makes sense, the contract, the if/throw block
and the unit test all are removed. This is the signal that the class is working
as intended. Also, if any part of the contract, if/throw block and unit test is
missing, error on the side of making them complete rather than removing the
prerequisite.

More advanced teams can explore the use of automatic test generation and similar
tools to cut down on boilerplate and manual test writing.

Design Patterns and Functional Programming
==========================================

When writing object-oriented code, it is almost impossible to avoid some
discussion of design patterns. While a familiarity of with these patterns and
the concept of SOLID code is very useful, we have found that in C\#, patterns
are often overused and abused.

It is vital to note that the classic (Gang of Four) design patterns targeted
C++98 and ignored functional programming paradigms as they did not match the C++
language at that time. Further on, these patterns were adopted for Java and,
again, functional programming paradigms were ignored because of language
limitations.

However, C\# has excellent support for functional paradigms and using these
appropriately greatly reduces the need for using classic design patterns and the
additional classes they can lead to in many cases. Remember, design patterns are
not a goal into themselves, but a way to solve a problem when it is presented.

For example, it is not uncommon to see the following:

    public abstract class ProcessTemplateClass
    {
        public abstract bool ShouldProcess(ProcessItem processItem);
        public void Process()
        {
            // Code that calls should process...
        }
    }

    public class ProcessSimpleClass : ProcessTemplateClass
    {
        public override bool ShouldProcess()
        {
            // Code ...
        }
    }

This is a standard application of the template pattern that varies on one method, in 
this case the ShouldProcess abstract method. C\# completely eliminates the need for 
subclasses by providing a function parameter to concrete method, as follows:

    public class ProcessClass
    {
        public void Process(Func<ProcessItem, bool> shouldProcessFunction)
        {
            // Code that calls shouldProcessFunction
        }
    }

This is more concise and reduces the number of overall classes. Again, 
understanding the functional paradigm and how C\# supports it can
eliminate a lot of overhead.

Reactive Programming
--------------------

One very useful counterpart to functional programming is the Reactive
programming model (as provided by the System.Reactive library in C\#).
Appropriate use of these paradigms can greatly improve certain code bases.