# AttendanceTracker

ASP.Net Core Api for tracking Students/Instructors, Courses Attended/Instructed, and Attendance Occurences for Students/Instructors.

## Contents

* [Overview](https://github.com/uhyeay2/AttendanceTracker/blob/main/README.md#overview)
* [Projects](https://github.com/uhyeay2/AttendanceTracker/blob/main/README.md#projects)
* [Features](https://github.com/uhyeay2/AttendanceTracker/blob/main/README.md#features)
* [Testing](https://github.com/uhyeay2/AttendanceTracker/blob/main/README.md#testing)

## Overview

This Application is an Asp.Net Core Api built using Clean Architecture. The primary purpose for this application is to track Attendance Occurences for Students/Instructors in Scheduled Courses. To support this functionality, the application also allows a user to create records for Students/Instructors, Subjects/Courses, and of course the data needed to track Attendance in any Courses Scheduled to be taught by an instructor to attended by a Student.

### What Does This Application Allow A User To Do?

As mentioned previously, the primary functionality for this application is to tracker Student/Instructor Attendance Occurences. However, there are several other features exposed by this Api. Here's a brief list of what you can do with the Api:

- Students: Create/Read/Update/Delete
- Instructors: Create/Read/Update/Delete
- Subjects: Create/Read/Delete
- Courses: Create/Read/Update/Delete/IsExistingByCode
- CoursesScheduled: Create/Read/Delete

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

### How Is This Application Structured

This Application is structured following the Clean Architecture (or sometimes referred to as Ports and Adapters) Pattern. The Api also utilizes the Mediator pattern to separate the api from any of the logic on how requests are handled. This means that for each endpoint there is a Request object, which is tied to a Handler object. Each Handler defines how that request is handled. Below is a breakdown of the projects (excluded test projects) in this application and how they fit into the structure.

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
        

#### [Return To Contents](https://github.com/uhyeay2/AttendanceTracker/blob/main/README.md#contents)

## Projects

With this application following Clean Architecture (or sometimes referred to as Ports And Adapters), each project would represent a different Port for this Application. The Domain is the core of the Application, so it has no dependencies on any other projects. Each of the other Projects/Ports would encapsulate functionality to a different section of the Application. For example, all interactions with the Database are encapsulated in the AttendanceTracker.Data project. In the below sections we will discuss each projects responsibility and any notes to highlight from that project.

### AttendanceTracker.Domain

### AttendanceTracker.Database

### AttendanceTracker.Data

### AttendanceTracker.Application

### AttendanceTracker.Api

#### [Return To Contents](https://github.com/uhyeay2/AttendanceTracker/blob/main/README.md#contents)

## Features

Features are an important...

### Subject

Create, Read, and Delete Functionality....

### Course

Create, Read, Update, Delete...

### Student

Create, Read, Update, Delete...

### Instructor

Create, Read, Update, Delete....

### CourseScheduled

Create, Read, Delete....

#### [Return To Contents](https://github.com/uhyeay2/AttendanceTracker/blob/main/README.md#contents)

## TestingStrategies

Testing is an important aspect of every application. This application ensures stability using....

### Data Tests (Database Intergation Testing)

Data Tests are integrated with the Sql Server Database. This can often times be challenging, however....

### Application Tests (Unit Testing w/ Moq)

True Unit Testing will Mock Dependencies to ensure that a 'System Under Test' (SUT) is not testing any of its dependencies functionality, but instead only testing its own logic. The Tests for the Application layer are set up to Mock dependencies using the Moq framework.

### Api Tests (Integration Smoke Testing)

Smoke Testing To Come....

#### [Return To Contents](https://github.com/uhyeay2/AttendanceTracker/blob/main/README.md#contents)
