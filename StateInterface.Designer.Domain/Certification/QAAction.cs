
using System;
using System.Collections.Generic;
namespace StateInterface.Designer.Model
{
    public class QAAction : EntityBase
    {
        public virtual int Id { get; set; }
        public virtual QAActionType QAActionType { get; set; }
        public virtual DateTime OccurredAt { get; set; }
        public virtual Criteria Criteria { get; set; }
        public virtual string ByUser { get; set; }
        public virtual string TestCaseId { get; set; }
        public virtual string Note { get; set; }
        public virtual bool? HasPassed { get; set; }
        public QAAction()
        {
        }
    }
}
