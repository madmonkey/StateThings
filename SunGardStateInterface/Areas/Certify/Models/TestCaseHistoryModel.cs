using StateInterface.Designer.Model;
using StateInterface.Properties;

namespace StateInterface.Areas.Certify.Models
{
    public class TestCaseHistoryModel
    {
        public int CriteriaId { get; set; }
        public string TestCaseId { get; set; }
        public string OccurredAt { get; set; }
        public string QaAction { get; set; }
        public string PerformedBy { get; set; }
        public string Note { get; set; }
        public bool? HasPassed { get; set; }
        public TestCaseHistoryModel(QAAction qaAction)
        {
            CriteriaId = qaAction.Criteria.Id;
            TestCaseId = qaAction.TestCaseId;
            HasPassed = qaAction.HasPassed;
            OccurredAt = qaAction.OccurredAt.ToString(Resources.DayDateTimeFormat);
            PerformedBy = qaAction.ByUser;
            Note = qaAction.Note;

            if(qaAction.QAActionType.ActionName.Equals(QAActionType.UnitTest))
            {
                QaAction = "Unit Test";
            }
            else if(qaAction.QAActionType.ActionName.Equals(QAActionType.Verify))
            {
                QaAction = "Verify";
            }
            else if (qaAction.QAActionType.ActionName.Equals(QAActionType.Certify))
            {
                QaAction = "Certify";
            }
        }
    }
}