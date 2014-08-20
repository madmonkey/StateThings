using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using StateInterface.Designer.Model;
using FluentNHibernate.Conventions;

namespace StateInterface.Designer.Repository.Mappings
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Id(x => x.Id);
            Map(x => x.LoginName);
            Map(x => x.Name);
            References(x => x.CurrentRecordsCenter);

            HasManyToMany(x => x.Permissions)
                 .AsBag()
                 .Table("UserPermission")
                 .Cascade.All();

            HasManyToMany(x => x.Roles)
                 .AsBag()
                 .Table("UserRole")
                 .Cascade.All();
        }
    }
}
