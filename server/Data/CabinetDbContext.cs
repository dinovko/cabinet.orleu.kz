using Microsoft.EntityFrameworkCore;
using server.cabinet.orleu.kz.Models;

namespace server.cabinet.orleu.kz.Data
{
    public class CabinetDbContext : DbContext
    {
        public CabinetDbContext(DbContextOptions<CabinetDbContext> o) : base(o)
        {

        }

        #region Справочники ЕБДС
        public DbSet<refEmpdepartment> refEmpdepartment { get; set; }
        public DbSet<refEmpposition> refEmpposition { get; set; }
        public DbSet<refKato> refKato { get; set; }
        public DbSet<refNationality> refNationality { get; set; }
        public DbSet<refNobdarea> refNobdarea { get; set; }
        public DbSet<refNobdplace> refNobdplace { get; set; }
        public DbSet<refNobdposition> refNobdposition { get; set; }
        public DbSet<refNobdqualcategory> refNobdqualcategory { get; set; }
        public DbSet<refNobdschool> refNobdschool { get; set; }
        public DbSet<refNobdsciencedegree> refNobdsciencedegree { get; set; }
        public DbSet<refNobdsubject> refNobdsubject { get; set; }
        public DbSet<refOrleubranch> refOrleubranch { get; set; }
        #endregion

    }
}
