using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StateInterface.Model;
using System.Data.Entity.ModelConfiguration;

namespace StateInterface.Repository.Configuration
{
    public class RecordsCenterConfiguration : EntityTypeConfiguration<RecordsCenter>
    {
        public RecordsCenterConfiguration()
        {
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name);
            this.Property(t => t.Code);
            this.Property(t => t.Description);
            this.HasMany(t => t.Categories);
        }
    }
}
