# Han.EntityFrameworkCore.Repository
A generic repository pattern for entity framework core that exposes CRUD functionality and async methods.

## Installation

    Install-Package Han.EntityFrameworkCore.Repository
    
## Documentation

### All
Filters the DbSet based on a predicate, sorts in ascending order, skips a number of entities and returns the specified number of entities. Includes specifies which related entities to include in the query results.

### AllAsync
Asynchronously filters the DbSet based on a predicate, sorts in ascending order, skips a number of entities and returns the specified number of entities. Includes specifies which related entities to include in the query results.

### Any
Determines whether any entities in the DbSet satisfy a condition.

### AnyAsync
Asynchronously determines whether any entities in the DbSet satisfy a condition.

### Create
Inserts the specified entities into the DbSet.
 
### CreateAsync
 Asynchronously inserts the specified entities into the DbSet.
 
### Delete 
Removes the specified entities from the DbSet.

### DeleteAsync
Asynchronously removes the specified entities from the DbSet.

### Get
Retrieves the first entity from the DbSet that satisfies the specified condition otherwise returns default value.
        
### GetAsync
Asynchronously retrieves the first entity from the DbSet that satisfies the specified condition otherwise returns default value
        
### Update
Updates the specified entities in the DbSet.

### UpdateAsync
Asynchronously updates the specified entities in the DbSet.

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
