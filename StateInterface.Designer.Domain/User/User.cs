using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StateInterface.Designer.Model
{
    public class User : EntityBase
    {
        public virtual int Id {get; set;}
        public virtual string Name { get; set; }
        public virtual string LoginName { get; set; }
        public virtual IList<Permission> Permissions { get; set; }
        public virtual IList<Role> Roles { get; set; }
        public virtual RecordsCenter CurrentRecordsCenter { get; set; }
        public User()
        {
            Permissions = new List<Permission>();
            Roles = new List<Role>();
        }
        public virtual bool CanDesignManage { get { return Permissions.Any(x => (x.PermissionName.Equals(Permission.CanDesignManage, StringComparison.InvariantCultureIgnoreCase)));} }
    }
}
