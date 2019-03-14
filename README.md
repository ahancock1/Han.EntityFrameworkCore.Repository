# Han.EntityFrameworkCore.Repository

	A generic repository pattern for entity framework core

## Installation

    Install-Package Han.EntityFrameworkCore.Repository

## Changes

### Version 1.2

	- Changed includes to support EntityFrameworks 'ThenInclude' and 'Include' eager loading. This requires a change to the 'All' for repository.
	
		'
            return All(
				predicate: predicate,
				includes: s => s.Include(i => i.Teachers).ThenInclude(i => i.Qualifications));
		'
		
	- Updated to latest version of EntityFrameworkCore

## Usage

### Application Database:

    public class ApplicationDataContext : DbContext
    {
        public DbSet<School> Students { get; set; }

		public DbSet<Teacher> Teachers { get; set; }

		public DbSet<Qualification> Qualification { get; set; }
    }

	public class ApplicationRepository<T> : Repository<ApplicationDataContext, T>
		where T : class
	{
        private readonly string _connection;

        protected ApplicationRepository(string connection)
        {
            _connection = connection;
        }

        protected override ApplicationDataContext GetDataContext()
        {
            var options = new DbContextOptionsBuilder()
                .UseSqlServer(_connection)
                .Options;

            return new ApplicationDataContext(options);
        }
	}
    
### Repository:

    public interface ISchoolRepository
    {
        IEnumerable<School> GetSchools(Func<School, bool> predicate);
        
        bool CreateSchool(School School);
        
        bool UpdateSchool(School School);
        
        bool DeleteSchool(School School);
    }

    public class SchoolRepository : ApplicationRepository<ApplicationDataContext>, ISchoolRepository
    {
        public ProductsRepository(string connection) 
            : base(connection)
        { }

        public IEnumerable<School> GetSchools(Func<School, bool> predicate)
        {
            return All(
				predicate: predicate,
				includes: s => s.Include(i => i.Teachers).ThenInclude(i => i.Qualifications));
        }

        public bool CreateSchool(School School)
        {
            return Create(School);
        }

        public bool UpdateSchool(School School)
        {
            return Update(School);
        }

        public bool DeleteSchool(School School)
        {
            return Delete(School);
        }
    }
    
### Service:

    public interface ISchoolService
    {
        IEnumerable<School> GetSchoolByPostcode(string postcode);
    }

    public class SchoolService : ISchoolService
    {
        private readonly ISchoolRepository _repository;

        public SchoolService(ISchoolRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<School> GetSchoolByPostcode(string lastname)
        {
            return _repository.GetSchools(p => 
				p.Postcode.Equals(lastname, StringComparison.OrdinalIgnoreCase));
        }
    }
