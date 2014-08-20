
using System.Collections.Generic;
namespace StateInterface.Designer.Model
{
    public class Application : EntityBase
    {
        public Application()
        {}
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
    }
}
