# EntityFrameworkCore.Repository
A generic repository pattern for entity framework core

Entity Type:

    public class Person : Entity<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

Application Database:

    public class ApplicationDataContext : DbContext, IDataContext
    {
        public DbSet<Person> People { get; set; }

        public void Seed()
        {
            SeedPeople();
        }

        public IDataContext CreateInstance()
        {
            return new ApplicationDataContext();
        }

        private void SeedPeople()
        {
            People.Add(new Person());

            SaveChanges();
        }
    }

Service:

    public class PersonService
    {
        private readonly IRepository<Person, int> _repository;

        public PersonService(IRepository<Person, int> repository)
        {
            _repository = repository;
        }
    }
