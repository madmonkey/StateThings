using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StateInterface.Model;

namespace StateInterface.Repository.Configuration
{
    public class CategoryConfiguration : EntityTypeConfiguration<Category>
    {
        public CategoryConfiguration()
        {
            this.HasKey(t => t.Id);
            this.Property(t => t.Code);
            this.Property(t => t.Name);
            this.Property(t => t.Description);
            this.HasOptional(t => t.RecordsCenter);
            this.HasMany(t => t.Messages);
        }
    }
}
