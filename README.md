# AttendanceTracker

ASP.Net Core Api for tracking Students/Instructors, Courses Attended/Instructed, and Attendance Occurences for scheduled courses.

This is an Application built using Clean Architecture and the Mediator Pattern. This application is also well tested with both Integration and Unit testing.

Created by Daniel Aguirre - [Let's Connect On LinkedIn](https://www.linkedin.com/in/daniel-aguirre-/)

## Table Of Contents

- [Overview](https://github.com/uhyeay2/AttendanceTracker/blob/main/README.md#overview)
  - [What Does This Application Allow A User To Do?](https://github.com/uhyeay2/AttendanceTracker/blob/main/README.md#what-does-this-application-allow-a-user-to-do)
  - [What Was Used To Build This Application?](https://github.com/uhyeay2/AttendanceTracker/blob/main/README.md#what-was-used-to-build-this-application)
  - [How Is This Application Structured?](https://github.com/uhyeay2/AttendanceTracker/blob/main/README.md#how-is-this-application-structured)
- [Projects](https://github.com/uhyeay2/AttendanceTracker/blob/main/README.md#projects)
  - [AttendanceTracker.Domain](https://github.com/uhyeay2/AttendanceTracker/blob/main/README.md#attendancetrackerdomain)
  - [AttendanceTracker.Database](https://github.com/uhyeay2/AttendanceTracker/blob/main/README.md#attendancetrackerdatabase)
  - [AttendanceTracker.Data](https://github.com/uhyeay2/AttendanceTracker/blob/main/README.md#attendancetrackerdata)
  - [AttendanceTracker.Application](https://github.com/uhyeay2/AttendanceTracker/blob/main/README.md#attendancetrackerapplication)
  - [AttendanceTracker.Api](https://github.com/uhyeay2/AttendanceTracker/blob/main/README.md#attendancetrackerapi)
- [Features](https://github.com/uhyeay2/AttendanceTracker/blob/main/README.md#features)
  - [Subject](https://github.com/uhyeay2/AttendanceTracker/blob/main/README.md#subject)
  - [Course](https://github.com/uhyeay2/AttendanceTracker/blob/main/README.md#course)
  - [Student](https://github.com/uhyeay2/AttendanceTracker/blob/main/README.md#student)
  - [Instructor](https://github.com/uhyeay2/AttendanceTracker/blob/main/README.md#instructor)
  - [CourseScheduled](https://github.com/uhyeay2/AttendanceTracker/blob/main/README.md#coursescheduled)
- [Testing](https://github.com/uhyeay2/AttendanceTracker/blob/main/README.md#testing)

## Overview

This Application is an Asp.Net Core Api built using Clean Architecture. The primary purpose for this application is to track Attendance Occurences for Students/Instructors in Scheduled Courses. To support this functionality, the application also allows a user to create records for Students/Instructors, Subjects/Courses, and of course the data needed to track Attendance in any Courses Scheduled to be taught by an instructor to attended by a Student.

#### [Return To Table Of Contents](https://github.com/uhyeay2/AttendanceTracker/blob/main/README.md#table-of-contents)

### What Does This Application Allow A User To Do?

As mentioned previously, the primary functionality for this application is to track Student/Instructor Attendance Occurences. However, there are several other features exposed by this Api. Here's a brief list of what you can do with the Api:

- Subjects: Create/Read/Delete
- Courses: Create/Read/Update/Delete/IsExistingByCode
- Students: Create/Read/Update/Delete
- Instructors: Create/Read/Update/Delete
- CoursesScheduled: Create/Read/Delete

#### [Return To Table Of Contents](https://github.com/uhyeay2/AttendanceTracker/blob/main/README.md#table-of-contents)

### What Was Used To Build This Application?

This Application was built using the following frameworks/nuget packages:

- Api Framework: Asp.Net Core Api (.Net 6)
- Microsoft.Extensions.DependencyInjection.Abstraction
  - Dependency In AttendanceTracker.Data
  - Dependency In AttendanceTracker.Application 
  - Used to encapsulate Dependency Injection 
- System.Data.SqlClient
  - Dependency In AttendanceTracker.Data
  - Used to create Sql Connections
- Dapper (ORM)
  - Dependency In AttendanceTracker.Data
  - Utilized for sending Sql Transactions to Database
- SwashBuckle (Swagger)
  - Dependency In AttendanceTracker.Api 
  - This comes out the box with Asp.Net Core Api project
- xUnit
  - Dependency In All Test Projects 
  - Test Projects for this application are created using xUnit
- Moq
  - Dependency In AttendenceTracker.Application.Tests 
  - Integration Tests are set up by Mocking Dependencies using the popular Moq Framework
- Genfu 
  - Dependency In AttendenceTracker.Application.Tests
  - This tool is helpful for Generating Fake Data to use in Unit Tests.

#### [Return To Table Of Contents](https://github.com/uhyeay2/AttendanceTracker/blob/main/README.md#table-of-contents)

### How Is This Application Structured

This Application is structured following the Clean Architecture (or sometimes referred to as Ports and Adapters) Pattern. The Api also utilizes the Mediator pattern to separate the api from any of the logic on how requests are handled. This means that for each endpoint there is a Request object, which is tied to a Handler object. Each Handler defines how that request is handled. Below is a breakdown of the projects (excluding test projects) in this application and how they fit into the structure.

- AttendanceTracker.Domain
  - Class Library (.Net 6) 
  - Models, Enums, ExtensionMethods, Exceptions, Validation, etc. are stored in this project.
  - This is the core of the application. This project does not depend on any other projects.
- AttendanceTracker.Database
  - Sql Server Database Project (2019)
  - This holds the Database Schema that this Application depends on.
  - You can publish this Database Project to your own Database to create the necessary tables to run this Application.
- AttendanceTracker.Data
  - Class Library (.Net 6)
  - This Class Library (or Port) encapsulates the Read/Write Access to the Database.
  - Calls to Dapper ORM Dependency are abstracted through [DataAccess.cs](https://github.com/uhyeay2/AttendanceTracker/blob/main/AttendanceTracker.Data/Implementation/DataAccess.cs) class.
    - This Abstracts All SQL Transactions To ExecuteAsync(), FetchAsync(), & FetchListAsync() Methods.
  - DataRequestObjects and DataTransferObjects are stored in this project.
  - SqlGeneration is used to quickly create basic queries/commands.
    - SqlGeneration Methods Are Unit Tested.
  - The AttendanceTracker.Data project depends on AttendanceTracker.Domain
- AttendanceTracker.Application
  - Class Library (.Net 6)
  - This Class Library (or Port) encapsulates the logic on how Requests are Handled.
    - This is where the Mediator Pattern is implemented.
  - This is where Requests/Handlers are stored that the Api would send requests for.
    - RequestObjects are public and stored in the same file as Handlers.
    - Handlers are internal so the impelmentation is encapsulated, and only the requests are exposed.
    - Handlers define the logic on how a request is handled.
  - The AttendanceTracker.Application project depends on AttendanceTracker.Domain and AttendanceTracker.Data
- AttendanceTracker.Api
  - Asp.Net Core Api (.Net 6)
  - This Api exposes functionality for the Applications different features.
  - Implements Global ExceptionHandling Middleware
  - The AttendanceTracker.Api project depends on AttendanceTracker.Domain and AttendanceTracker.Application
        
#### [Return To Table Of Contents](https://github.com/uhyeay2/AttendanceTracker/blob/main/README.md#table-of-contents)

## Projects

With this application following Clean Architecture (or sometimes referred to as Ports And Adapters), each project would represent a different Port for this Application. The Domain is the core of the Application, so it has no dependencies on any other projects. Each of the other Projects/Ports would encapsulate functionality to a different section of the Application. For example, all interactions with the Database are encapsulated in the AttendanceTracker.Data project. In the below sections we will discuss each projects responsibility and any notes to highlight from that project.

#### [Return To Table Of Contents](https://github.com/uhyeay2/AttendanceTracker/blob/main/README.md#table-of-contents)

### AttendanceTracker.Domain

The Domain, as mentioned previously, is the core of the application. This project does not depend on any other projects, and most other projects actually depend on this one. Below is some more information regarding what can be found in the Domain project.

- Constants
  - Constant Values related to Subjects, Courses, Students, and Instructors.
- Enums
  - At this time the only Enum is AttendanceOccurenceTypes, such as Absent, LateArrival, or EarlyLeave.
- Exceptions
  - Custom Exception classes created for AlreadyExists, DoesNotExist, ExpectationFailed, and ValidationFailed scenarios.
- Extensions
  - Extension Methods are stored in this namespace to expose common functionality to any dependencies that may need it.
- Factories
  - At this time the only Factory stored here is the RandomCharacterFactory which implements IRandomCharacterFactory
- Interfaces
  - This namespace would house interface or contracts essential across the application.
  - At this time, IValidatable and IRandomCharacterFactory are the only Interfaces stored in this project.
- Models
  - These Models would be the Domain Object (Not Database Copy) of objects required for this application.
    - For Example, the Course_DTO would expose the Id, but the Course (Domain Object) would not.
    - Domain Objects may have other Domain (Complex) Objects as properties, whereas DTO's in the Data Layer would have primitive values.
- Policy
  - This namespace defines business Logic/Rules
  - Validation is defined in this area of the Application.
    - Validate{DataType} Classes have Extension Methods related to Validating Parameters of the specified Type.
    - Validation.cs provides helpers for Initializing/Checking List of ValidationFailureMessages (strings)
    - ValidationFailureMessage.cs provides helpers for generating messages regarding reason for Validation Failures.

#### [Return To Table Of Contents](https://github.com/uhyeay2/AttendanceTracker/blob/main/README.md#table-of-contents)

### AttendanceTracker.Database

The Sql Server Database Project is really cool because it not only allows you to track source control for changes made to a database, but it also allows you to publish changes for your Database. So for example, whenever I create a new Table in my Database, I will actually first create the table in my Database project in Visual Studios, and then I will publish that Database Change to Sql Server through Visual Studios. Below is more information on this project:

- dbo (This is the source control for the Database Schema)
  - Tables
    - Create Table Statements are stored here.
    - When you publish the Database through Visual Studios it will use these .sql files.
- PostDeploymentScripts
  - Not Yet Developed - In upcoming work this section will include Scripts for Seeding Data into the Database.

#### [Return To Table Of Contents](https://github.com/uhyeay2/AttendanceTracker/blob/main/README.md#table-of-contents)

### AttendanceTracker.Data

The Data Project encapsulates the Sql Transactions sent to the Database. This is where the Application has a dependency on Dapper (ORM) for sending requests to the Database. DataRequests (Queries/Commands to send to the Database) are defined as IDataRequests. Meanwhile, data fetched from the Database are defined as DataTransferObjects. This Class Library is where the  [DataAccess.cs](https://github.com/uhyeay2/AttendanceTracker/blob/main/AttendanceTracker.Data/Implementation/DataAccess.cs) class is implemented. This class will handle any IDataRequest.

- Abstraction
  - BaseRequests
    - This is where Abstract classes are stored for common Request Objects, such as Guid_DataRequest or Id_DataRequest.
  - Interfaces
    - IDataAccess
      - This is the Interface abstracting the calls to Dapper.
      - ExecuteAsync() - This method will take in an IDataRequest and return an int representing the number of rows affected by the command.
      - FetchAsync() - This method will take in an IDataRequest < TResponse > and will return a QueryFirstOrDefaultAsync() for the TResponse defined in the IDataRequest.
      - FetchListAsync() - This method will take in an IDataRequest < TResponse> and will QueryAsync() an IEnumerable < TResponse > for the TResponse defined in the IDataRequest.
    - IDataRequest
      - This interface defines that any IDataRequest class must be able to GetSql() and GetParameters() to execute a Sql Query/Command.
      - There is an IDataRequest \<TResponse> which allows an IDataRequest Object to define what type of DTO it would Fetch/FetchList of.      
    - IDbConnectionFactory
      - This interface allows us to abstract out the implementation for how a new SqlConnection is created for each Sql Query/Command.
  - DataRequestObjects
    - This is where classes that implement the IDataRequest interface reside.
    - Requests are split into seperate features, IE: StudentRequest, InstructorRequests, CourseRequests.
    - Every IDataRequest will define methods to GetSql() and GetParameters()
  - DataTransferObjects
    - This is where classes that represent the data fetched from the database reside.
    - All IDataRequest \<TResponse> objects will define a TResponse from the DataTransferObjects namespace.
      - These DataTransferObjects (DTO's) are an exact match to the query that fetches them.
  - Implementation
    - DataAccess
      - This is the only reference to Dapper in the entire project. Every Sql Transaction goes through this class.
      - Breaks down all Sql Transactions into: ExecuteAsync(), FetchAsync(), FetchListAsync().
      - Depends on IDbConnectionFactory to instantiate IDbConnections to the Database at runtime.
     - DbConnectionFactory
       - This class is responsible for creating a new SqlConnection() at runtime.
       - Depends on Database ConnectionString that is stored as a private member to use at runtime.
     - DependencyInjection
       - This Extension Class injects dependencies to an IServiceCollection using Microsoft.Extensions.DependencyInjection.Abstraction
       - IDbConnectionFactory Added as Singleton using DbConnectionFactory
       - IDataAccess added as Scoped using DataAccess
  - SqlGeneration
    - Delete - Sql Generation For deleting from a table with a where condition.
    - Insert - Sql Generation For Insert Commands
      - IntoTable() - Pass in the TableName and (ColumnName, ValueName)[] Array to generate an Insert Statement.
        - Overload for only passing in only ColumnNames when ParameterNames match ColumnNames.
      - SelectIntoTable() - SqlGeneration Similar to IntoTable() but allows you to select values from another table for insert transaction.
    - Select - Sql Generation for Select Queries
      - FromTable() - Define Table to select from, optionally define columns, and optionally define where statement. Defaults to use WITH(NOLOCK)
      - Exists() - Define Table and where statement, optionally define columns and defaults to use WITH(NOLOCK)
    - Update - Sql Generation For Update Commands
      - CoalesceTable() - Define Table and Where statement with (ColumnName, ValueName)[] Array of items to Coalesce.
        - Overload for passing in only ColumnNames when ParameterNames match ColumnNames
    - TableNames (Constants)
      - This is a static class to hold constants representing the different Table names.

#### [Return To Table Of Contents](https://github.com/uhyeay2/AttendanceTracker/blob/main/README.md#table-of-contents)

### AttendanceTracker.Application

The Application Project encapsulates the logic on how requests are handled. This is where I implemented the Mediator Pattern. For those that are familiar with MediatR (Nuget Package), you may notice some similarities with the IRequest, IHandler, and IOrchestrator interfaces. The IOrchestrator will receive an IRequest, and use an IHandlerFactory to instantiate the appropriate Handler at runtime. There is also RequestValidation that is automatically processed for any IRequest object that also implements the AttendenaceTracker.Domain.IValidatabale interface.

- Abstraction
  - BaseHandlers
    - This is where abstract classes are stored for consistently used dependencies, such as the DataHandler which depends on IDataAccess to interact with the Database.
  - BaseRequests
    - This is another place where abstract classes reside. An example would be RequiredGuidRequest which is an IRequest that has a 'Guid' Property expected in the body.
      - These 'Required' BaseRequests help to reduce repeated code on Request Validation Rules.
  - Interfaces
    - IHandler
      - IHandler\<TRequest> and IHandler\<TRequest, TResponse> interfaces define contracts for handling synchronous requests.
      - ITaskHandler\<TRequest> and ITaskHandler\<TRequest, TResponse> interfaces are for asynchronous requests.
    - IHandlerFactory
      - This interface defines the method called for instantiating a Handler at runtime.
    - IOrchestrator
      - This is the interface that projects which depend on the AttendanceTracker.Application will use.
      - Asynchronous requests will either call GetResponseAsync() or ExecuteRequestAsync()
      - Synchronous requests will use GetResponse() or ExecuteRequest()
      - Classes depending on this interface will define the TRequest and TResponse when making a call, but the Handler will be hidden from the caller.
    - IRequest
      - Each Handler will be tied to a class that implements the IRequest interface.
      - Handlers that return an object will implement the IRequest\<TResponse> interface.
 - Implementation
   - DependencyInjection
     - This Extension Class injects dependencies to an IServiceCollection using Microsoft.Extensions.DependencyInjection.Abstraction
     - Will use Reflection once to get a List\<Type> of all IHandlers to add to the ServiceCollection as a Singleton.
     - IHandlerFactory added as Singleton using HandlerFactory
     - IOrhcestrator added as Singleton using Orchestrator
     - IRandomCharacterFactory added as Singleton from RandomCharacterFactory
   - HandlerFactory
     - Helper Method GetHandler\<TRequest, THandler>() returns the first Type that implements the IHandler\<TRequest> or IHandler\<TRequest, TResponse> passed in.
       - Also works for the ITaskHandler\<TRequest> and ITaskHandler\<TRequest, TResponse>
     - Helper method for Instantiate\<TRequest, THandler> which will create an instance of the THandler requested using the IServiceProvider built from the applications IServiceCollection.
     - This class depends on the List\<Type> that's generated using Reflection at Startup, and the Microsoft.Extensions.DependencyInjection.ActivatorUtiltiies to instantiate (handler) objects at runtime.
   - Orchestrator
     - This class implements the IOrchestrator interface that IRequest's would be sent to (Similar to the IMediator interface from MedaitR)
     - Request Validation happens here before a Handler is instantiated for a request.
     - This class depends on the IHandlerFactory to instantiate a handler when a request is received.
 - RequestHandlers
   - This is where classes that implement the IHandler interfaces reside.
   - Handlers are broken down into features (such as Subject, Course, Student).
   - Each Handler.cs file will also contain the Request class that is tied to that handler.
     - The Request objects are public, but the Handlers are always internal.
       - The (Types of) Handlers are added to the IServiceCollection through the DependencyInjection, so they do not need to be exposed outside the assembly.

#### [Return To Table Of Contents](https://github.com/uhyeay2/AttendanceTracker/blob/main/README.md#table-of-contents)

### AttendanceTracker.Api

The Api Application is the outermost layer of the application. This is what exposes the functionality of the application to the user. Although the core purpose of this application is to track Student/Instructor Attendance Occurences, there are several other features exposed through the api. This section will review the endpoints exposed through the Api, and the Middleware that was integrated into the Application.

- Middleware
  - ExceptionHandlingMiddleware
    - This Middleware will wrap requests to globally catch any exceptions throw in the application
      - This is used to ensure the correct statuse codes are sent and appropriate information is attached to the content of the Api response.
 - Controllers
   - CourseController
     - 'InsertCourse/'
       - Generates a Unique CourseCode using the SubjectCode provided, and inserts a record into database. Returns the Course (Domain Object) inserted.
     - 'DeleteCourse/'
       - Delete the Instructor in database with the InstructorCode matching the one provided.
     - 'GetCourseByCourseCode/'
       - Fetch a Course using CourseCode and transform the DTO into the Course Domain Object.
     - 'UpdateCourse/'
       - Update the Course with the CourseCode provided using a Coalesce on the table for any other fields provided.
   - CourseScheduledController
     - 'InsertCourseScheduled/'
       - Insert a CourseScheduled that references the Instructor and Courses found using the InstructorCode and CourseCode provided. Returns a CourseScheduled Domain Object.
     - 'DeleteCourseScheduled/'
       - Delete the CourseScheduled in database with a Guid matching the one provided.
     - 'GetCourseScheduledByGuid/'
       - Fetch a CourseScheduled with the Guid provided. Return as a DomainObject with the Instructor and Course related to this Scheduled Course as properties.
   - StudentController
     - 'InsertStudent/' 
       - Generates a Unique StudentCode and inserts a record into database. Returns the Student Domain Object for the record inserted.
     - 'DeleteStudent/' 
       - Delete the student in database with StudentCode matching the one provided.
     - 'GetStudentByStudentCode/' 
       - Fetch a Student from Database using StudentCode and translates that DTO into the Student Domain Object.
     - 'GetStudentsByName/' 
       - Fetch an IEnumerable\<Student> using a LIKE query with the FirstName and/or LastName provided.
     - 'UpdateStudent/' 
       - Update the Student with the StudentCode provided using a Coalesce on the table for any other fields provided.
   - InstructorController
     - 'InsertInstructor/'
       - Generates a Unique InstructorCode and inserts a record into database. Returns the Instructor Domain Object for the record inserted.
     - 'DeleteInstructor/'
       - Delete the Instructor from Database using the InstructorCode matching the one provided.
     - 'GetInstructorByInstructorCode/'
       - Fetch an Instructor from Database using the InstructorCode provided and transform the DTO into an Instructor Domain Object.
     - 'UpdateInstructor/'
       - Update the Instructor with the InstructorCode provided using a Coalesce on the table for any other fields provided.
   - SubjectController
     - 'InsertSubject/'
       - Insert a new Subject into the database and return the Domain Object for the record inserted.
     - 'DeleteSubject/'
       - Delete a Subject from the database using the SubjectCode provided.
     - 'GetSubjectBySubjectCode/'
       - Fetch a Subject from the database using the SubjectCode provided and translate it into a Subject Domain Object.
     - 'IsSubjectCodeExisting/'
       - Return True/False depending on if the provided SubjectCode exists.
   
#### [Return To Table Of Contents](https://github.com/uhyeay2/AttendanceTracker/blob/main/README.md#table-of-contents)

## Features

The primary functionality for this application is to allow a user to track Students/Instructors attendance occurences in ScheduledCourses. However, there are several other features that this application exposes functionality for. This section will go into more detail on each feature added to the application.

#### [Return To Table Of Contents](https://github.com/uhyeay2/AttendanceTracker/blob/main/README.md#table-of-contents)

### Subject

A Subject groups common courses. When creating a Subject a unique SubjectCode must be provided, as well as the Subject's Name. Once the Subject is created, then Courses can be created using the SubjectCode. Other than creating Subjects, you can also Fetch them from the database and delete them using the SubjectCode.

#### [Return To Table Of Contents](https://github.com/uhyeay2/AttendanceTracker/blob/main/README.md#table-of-contents)

### Course

A Course can be defined as a Class that pertains to a Subject. For example, the 'Math' subject, could have multiple Courses such as 'Algebra 1', 'Geometry 1', etc. When a course is created, a SubjectCode is provided (along with a Name). A unique CourseCode is generated using the SubjectCode and a short series of random numbers. Once the Course is created, then an Instructor can create a CourseScheduled that they would teach. Other than creating Courses, you can also Fetch, Update, and Delete them using the CourseCode as an identifier.

#### [Return To Table Of Contents](https://github.com/uhyeay2/AttendanceTracker/blob/main/README.md#table-of-contents)

### Student

A Student is someone who would attend a CoursesScheduled to be taught by an Instructor. When a Student is created in the application, a random unique StudentCode will be generated that is a fixed size of random letters then numbers. Students can be created, fetched, updated, and deleted. Most requests use the StudentCode, but you can also search for students using their First and/or Last names.

#### [Return To Table Of Contents](https://github.com/uhyeay2/AttendanceTracker/blob/main/README.md#table-of-contents)

### Instructor

An Instructor would be an individual who teaches a course. When an Instructor is inserted a random unique InstructorCode is generated using their last name and a fixed length of random numbers. After the instructor has been created you can use their InstructorCode to insert a CourseScheduled that they would teach. Instructors can also be fetched, updated, and deleted.

#### [Return To Table Of Contents](https://github.com/uhyeay2/AttendanceTracker/blob/main/README.md#table-of-contents)

### CourseScheduled

A CourseScheduled can be defined as a Course that an Instructor will teach within a specified date range. When a CourseScheduled is inserted, a CourseCode and InstructorCode are provided to identify who will be teaching what course, as well as the StartTime/EndTime. You can also fetch and delete CourseScheduled records.

#### [Return To Table Of Contents](https://github.com/uhyeay2/AttendanceTracker/blob/main/README.md#table-of-contents)

## Testing

Testing is an important aspect of every application. This application ensures stability using both Integration and Unit tests with the popular framework xUnit. Each test project is responsible for testing a separate Port (Class Library) for the application. In this section we will review the test coverage for this project.

#### [Return To Table Of Contents](https://github.com/uhyeay2/AttendanceTracker/blob/main/README.md#table-of-contents)

### Data Tests (Database Intergation Testing)

Data Tests are integrated with the Sql Server Database. This can often times be challenging, however this Test Project makes use of a DataSeeder that will not only insert new records and fetch them to be used in a test, but will also set queue the record to be deleted after the test runs. This helped a lot with reducing repeated code related to setting up a record to test with.

This xUnit Test project is broken up into the following:

- DataRequestTests
  - This is where tests can be found for Data Transactions (ExecuteAsync(), FetchAsync(), FetchListAsync() from IDataAccess).
  - RequestTests are split up into a separate namespace for each Feature, and one test Class per DataTransaction.
  - Each test here will inherit from the base class DataTest.cs that can be found in the TestHelpers namespace.
- SqlGenerationTests
  - This is where the tests for SqlGeneration reside.
  - There is one test class per SqlGeneration Class.
- TestHelpers
  - DataSeeder
    - This class has a helper which takes in three IDataRequests (Insert, Fetch, and Delete). This is used to Seed any records necessary, fetch them to be used in the test, and queue for deletion.
    - There is a method for PurgingSeededRecords which will delete all of the IDataRequests that have been queued for deletion.
    - A method exists for each table that can be inserted.
  - DataTest
    - Each DataRequest Test will implement this abstract base class.
    - This class provides the IDataAccess that will be needed to process each IDataRequest that is being tested.
    - The DataSeeder is kept as a property of this abstract class so that it is available in all Data tests.

#### [Return To Table Of Contents](https://github.com/uhyeay2/AttendanceTracker/blob/main/README.md#table-of-contents)

### Application Tests (Unit Testing w/ Moq)

True Unit Testing will Mock Dependencies to ensure that a 'System Under Test' (SUT) is not testing any of its dependencies functionality, but instead only testing its own logic. The Tests for the Application layer are set up to Mock dependencies using the popular Moq framework. 

#### [Return To Table Of Contents](https://github.com/uhyeay2/AttendanceTracker/blob/main/README.md#table-of-contents)

### Api Tests (Integration Smoke Testing)

Api Integration/Smoke Testing To Come....

#### [Return To Table Of Contents](https://github.com/uhyeay2/AttendanceTracker/blob/main/README.md#table-of-contents)
