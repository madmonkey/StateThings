
using System;
using System.Collections.Generic;
namespace StateInterface.Designer.Model
{
    public class TestCase
    {
        public string TestCaseId { get; set; }
        public Criteria Criteria { get; set; }
        public Application Application { get; set; }
        public QAStatus QaStatus { get; set; }
        public bool IncludesRequiredFields { get; set; }
        public bool IncludesOptionalFields { get; set; }
        public IList<TestCaseCriteriaNode> TestCaseCriteriaNodes { get; set; }
        public TestCase()
        {
            TestCaseCriteriaNodes = new List<TestCaseCriteriaNode>();
        }

        public bool CanExecutePassTestCase()
        {
            return QaStatus.QAStage == QAStage.Certify
                && (QaStatus.HasPassed.HasValue && QaStatus.HasPassed.Value == true) ? false : true;
        }
        public bool CanExecuteResetTestCase()
        {
            return QaStatus.QAStage ==
                    QAStage.UnitTest && QaStatus.HasPassed.HasValue == false ? false : true;
        }
    }
}

