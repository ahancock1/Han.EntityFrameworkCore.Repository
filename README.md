# EntityFrameworkCore.Repository
A generic repository pattern for entity framework core


Application Database:

    public class ApplicationDataContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
    }
    
Repository:

    public interface IPersonRepository
    {
        IEnumerable<Person> GetPersons(Func<Person, bool> predicate);
        
        bool CreatePerson(Person person);
        
        bool UpdatePerson(Person person);
        
        bool DeletePerson(Person person);
    }

    public class PersonRepository : Repository<ApplicationDataContext>, IPersonRepository
    {
        public IEnumerable<Person> GetPersons(Func<Person, bool> predicate)
        {
            return All(predicate);
        }

        public bool CreatePerson(Person person)
        {
            return Create(person);
        }

        public bool UpdatePerson(Person person)
        {
            return Update(person);
        }

        public bool DeletePerson(Person person)
        {
            return Delete(person);
        }
    }
    
Service:

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
            return _repository.GetPersons(p => p.LastName.Equals(lastname));
        }
    }
