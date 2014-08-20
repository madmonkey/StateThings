using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using StateInterface.Designer.Model;
using FluentNHibernate.Conventions;

namespace StateInterface.Designer.Repository.Mappings
{
    public class QAActionTypeMap : ClassMap<QAActionType>
    {
        public QAActionTypeMap()
        {
            Id(x => x.Id);
            Map(x => x.ActionName);
            Map(x => x.Description);
        }
    }
}
