using Microsoft.EntityFrameworkCore;
using Candidates.Models;

namespace AsposeDB.persistence
{
    public class CandidateDbContext : DbContext
    {
        public CandidateDbContext(DbContextOptions<CandidateDbContext> options) : base(options) {}    
        public DbSet<Candidate> Candidates {get; set;}    
    }

    public class CompanyDbContext : DbContext
    {
        
    }
}