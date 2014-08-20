using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using StateInterface.Designer.Model;
using FluentNHibernate.Conventions;

namespace StateInterface.Designer.Repository.Mappings
{
    public class PermissionMap : ClassMap<Permission>
    {
        public PermissionMap()
        {
            Id(x => x.Id);
            Map(x => x.PermissionName);
            Map(x => x.Description);
            HasManyToMany(x => x.Users)
                .AsBag()
                .Inverse()
                .Table("UserPermission")
                .Cascade.All();
        }
    }
}
