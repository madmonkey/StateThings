using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StateInterface.Model;

namespace StateInterface.Repository.Configuration
{
    public class MessageConfiguration : EntityTypeConfiguration<Message>
    {
        public MessageConfiguration()
        {
            this.HasKey(t => t.Id);
            this.Property(t => t.MessageKey);
            this.Property(t => t.Description);
            this.HasOptional(t => t.Category);
            this.HasMany(t => t.MessagesVariations);
        }
    }
}
