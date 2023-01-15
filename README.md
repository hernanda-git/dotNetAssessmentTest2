# .NET Assessment Test 2 by AKASIA

## The Tasks:
- Create Simple In-Memory CRUD Employee Application (simple console / command apps)

Data:
EmployeeId, string
FullName, string
BIrthDate, Date

Requirement:
- Key functionality
- Adding new data (can be hardcoded to save development time)
- Display list of data
- Remove / Delete data, for example delete employee 1003 – Amir from list of employees

Program must have:
- Comment (short description to describe method)
- Variable
- Validation (null handling, date handling, duplicate data handling)
- Error handling (exception)

# Employee CRUD Application
A simple in-memory CRUD application for an Employee model.

## Features
- Create Employee
- Update Employee
- Delete Employee
- List Employee

## Usage
1. Clone the repository => git clone https://github.com/hernanda-git/dotNetAssessmentTest2
2. Open the project in Visual Studio 2022
3. Build and run the application
4. Select an option from the menu to perform the corresponding task

## Note
- This program uses an in-memory database for storing employee information. Any created or updated employees will not be persisted after the program is closed.
- This program runs on .NET 7 framework. Please ensure that you have the appropriate version of the framework installed before running the application.
- The BetterConsoleTables library by Douglas Gaskell is used to make the output more fancy and user-friendly.
