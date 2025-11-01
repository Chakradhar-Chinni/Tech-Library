Unit Testing > Integration Testing

testing private methods, internal classes

logical phase: 
AAA - 
  Arrange: set up things, create test data/inputs
  Act: call methods, execute code
  Assert: check results, pass / fail


Production code project: contains actual application logic
Test Project: Contains test classes and methods
Test project code reference production code project to access its classes and methods
XUnit.net library is added to the test project via NuGet
Tests are executed using Test Runner which discover test methods, executes them, reports results
common test runners: VS TEst explorer, .net core cli, 3rd parties like ReSharper

Writing Tests in xUnit.net

Test Class Naming Convention: Use descriptive names ending with Should, e.g., PlayerCharacterShould.
Test Methods:

Represent individual tests.
Named to describe behavior, e.g., IncreaseHealthAfterSleeping.
Decorated with [Fact] attribute to mark them as test methods.
