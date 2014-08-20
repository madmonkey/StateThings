
using System;
using System.Collections.Generic;
namespace StateInterface.Designer.Model
{
    public class QAActionType : EntityBase
    {
        public const string Reset = "Reset";
        public const string UnitTest = "UnitTest";
        public const string Verify = "Verify";
        public const string Certify = "Certify";

        public virtual int Id { get; set; }
        public virtual string ActionName { get; set; }
        public virtual string Description { get; set; }
        public QAActionType()
        {
        }
    }
}
