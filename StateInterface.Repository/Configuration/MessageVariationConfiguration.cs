using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StateInterface.Model;

namespace StateInterface.Repository.Configuration
{
    public class MessageVariationConfiguration : EntityTypeConfiguration<MessageVariation>
    {
        public MessageVariationConfiguration()
        {
            this.HasKey(t => t.Id);
            this.Property(t => t.MessageText);
            this.Property(t => t.Description);
            this.Property(t => t.IsStandardSet);
            this.HasOptional(t => t.Message);
        }
    }
}
