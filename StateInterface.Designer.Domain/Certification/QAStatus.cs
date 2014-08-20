
using System;
using System.Collections.Generic;
namespace StateInterface.Designer.Model
{
    public enum QAStage
    {
        UnitTest,
        Verify,
        Certify,
    }
    public class QAStatus : EntityBase
    {
        public QAStage QAStage { get; set; }
        public bool? HasPassed { get; set; }

        public QAStatus()
        {

        }
    }
}
