using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using StateInterface.Designer.Model;
using FluentNHibernate.Conventions;

namespace StateInterface.Designer.Repository.Mappings
{
    public class QAActionMap : ClassMap<QAAction>
    {
        public QAActionMap()
        {
            Id(x => x.Id);
            Map(x => x.OccurredAt);
            Map(x => x.ByUser);
            Map(x => x.Note);
            Map(x => x.HasPassed);
            Map(x => x.TestCaseId);
            References(x => x.Criteria);
            References(x => x.QAActionType);
        }
    }
}
