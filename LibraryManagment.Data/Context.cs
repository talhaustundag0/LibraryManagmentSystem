using LibraryManagment.Data.Migrations;
using LibraryManagment.Data.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagment.Data
{
    public class Context:DbContext
    {
        public Context():base("Context")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<Context, Configuration>("Context"));
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Books> Kitaplar { get; set; }
        public DbSet<GivenBooks> OduncKitaplar { get; set; }
        public DbSet<Members> Members { get; set; }
        public DbSet<Writer> Writers { get; set; }

    }
}
