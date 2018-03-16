# Han.EntityFrameworkCore.Repository
A generic repository pattern for entity framework core that exposes CRUD functionality and async methods.

## Exposes

All
AllAsync
Any
AnyAsync
Create
CreateAsync
Delete 
DeleteAsync
Get
GetAsync
Update
UpdateAsync

## Installation

    Install-Package Han.EntityFrameworkCore.Repository

## Usage

### Application Database:
```csharp
    public class ApplicationDataContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
    }
```
    
### Repository:
```csharp
    public interface IPersonRepository : IRepository<Person>
    {
    }

    public class PersonRepository 
        : Repository<ApplicationDataContext, Person>, IPersonRepository
    {
        public PersonRepository()
        {
            Seed();
        }

        private void Seed()
        {
            if (!Any())
            {
                Create(new Person());
            }
        }
    }
```
    
### Service:
```csharp
    public interface IPersonService
    {
        IEnumerable<Person> GetPersonsByLastName(string lastname);
    }

    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _repository;

        public PersonService(IPersonRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Person> GetPersonsByLastName(string lastname)
        {
            return _repository.All(p => p.LastName.Equals(lastname));
        }
    }
```
    
### Dependency Injection - IoC:
```csharp
    public class Startup
    {
        private void ConfigureRepositories(IServiceCollection services)
        {
            // Person
            services
                .AddSingleton<IPersonRepository, PersonRepository>()
                .AddSingleton<IPersonService, PersonService>();
        }
    }
```
