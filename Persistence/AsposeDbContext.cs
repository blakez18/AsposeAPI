using Microsoft.EntityFrameworkCore;
using Candidates.Models;
using Positions.Models;
using Companys.Models;
namespace AsposeDB.persistence
{
    public class CandidateDbContext : DbContext
    {
        public CandidateDbContext(DbContextOptions<CandidateDbContext> options) : base(options) {}    
        public DbSet<Candidate> Candidates {get; set;}    
    }

    public class CompanyDbContext : DbContext
    {
        public CompanyDbContext(DbContextOptions<CompanyDbContext> options) : base(options) {}
        public DbSet<Company> Companies {get; set;}
    }

    public class PositionDbContext : DbContext
    {
        public PositionDbContext(DbContextOptions<PositionDbContext> options) : base(options) {}
        public DbSet<Position> Position {get;set;}
    }
}