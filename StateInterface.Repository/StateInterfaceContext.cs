using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StateInterface.Model;
using StateInterface.Repository.Configuration;

namespace StateInterface.Repository
{
    public class StateInterfaceContext : DbContext
    {
        public DbSet<RecordsCenter> RecordsCenters { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<MessageVariation> MessageVariations { get; set; }

        public StateInterfaceContext()
            : base("Name=StateInterfaceContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new CategoryConfiguration());
            modelBuilder.Configurations.Add(new RecordsCenterConfiguration());
            modelBuilder.Configurations.Add(new MessageConfiguration());
            modelBuilder.Configurations.Add(new MessageVariationConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
