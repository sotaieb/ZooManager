# Zoo Manager

## Building 
- Visual Studio 2019
- .Net Core 3.1
- DI framework: Autofac 6.1.0
- Test frameworks: NUnit, Moq, Autofixture


## Projects

- St.Zoo.Data (.Net Standard 2.0)
The data layer => Retrieves and Parses file stores.
- St.Zoo.Business (.Net Standard 2.0)
The business layer => Gets total food amount by day.
- St.Zoo.Models(.Net Standard 2.0)
The business models.
- St.Zoo.Console (.Net Core 3.1)
The demo project.

## Test Projects
- St.Zoo.Business.Tests
- St.Zoo.Data.Tests

=> There is only unit tests (No integration/functional tests).