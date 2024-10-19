# CRUD Code Test 


Create a Clean Architecture CRUD application with .NET Core 8.0 & CQRS Pattern & Blazor & Unit Tests & Bdd & TDD & DDD that implements the below model:
```
Customer {
	FirstName
	LastName
	DateOfBirth
	PhoneNumber
	Email
	BankAccountNumber
}
```
## Practices and patterns:

- Unit Testing
- [TDD](https://docs.microsoft.com/en-us/visualstudio/test/quick-start-test-driven-development-with-test-explorer?view=vs-2022)
- [BDD](https://en.wikipedia.org/wiki/Behavior-driven_development)
- [DDD](https://en.wikipedia.org/wiki/Domain-driven_design)
- [Clean architecture](https://github.com/jasontaylordev/CleanArchitecture)
- [CQRS](https://en.wikipedia.org/wiki/Command%E2%80%93query_separation#Command_query_responsibility_separation) pattern ([Event sourcing](https://en.wikipedia.org/wiki/Domain-driven_design#Event_sourcing)).
- Blazor Web.
- Docker-compose project that loads database service automatically which `docker-compose up`

### Validations

- During Create; validate the phone number to be a valid *mobile* number only (Please use [Google LibPhoneNumber](https://github.com/google/libphonenumber) to validate number at the backend).

- A Valid email and a valid bank account number must be checked before submitting the form.

- Customers must be unique in the database: By `Firstname`, `Lastname`, and `DateOfBirth`.

- Email must be unique in the database.

### Storage

- Store the phone number in a database with minimized space storage (choose `varchar`/`string`, or `ulong` whichever store less space).


### Run with Docker

- Install docker and run "docker-compose up" and open "http://localhost:5000/"

