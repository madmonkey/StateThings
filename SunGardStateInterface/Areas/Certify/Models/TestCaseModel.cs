using System.Collections.Generic;
using System.Linq;
using StateInterface.Designer.Model;
using System;

namespace StateInterface.Areas.Certify.Models
{
    public class TestCaseModel
    {
        public string FormId { get; set; }
        public int CriteriaId { get; set; }
        public string TestCaseId { get; set; }
        public string EntryDate { get; set; }
        public string CurrentStage { get; set; }
        public bool? HasPassed { get; set; }
        public bool ShowPassCommand { get; set; }
        public bool ShowResetCommand { get; set; }
        public List<FieldValidationCriteriaModel> FieldCriteria { get; set; }
        public List<TestCaseHistoryModel> TestCaseHistoryList { get; set; }
        public string RequiredFields { get; set; }
        public string OptionalFields { get; set; }
        public TestCaseModel()
        {
            FieldCriteria = new List<FieldValidationCriteriaModel>();
            TestCaseHistoryList = new List<TestCaseHistoryModel>();
        }
        public TestCaseModel(TestCase testCase)
            : this()
        {
            FormId = testCase.Criteria.Transaction.RequestForm.FormId;
            CriteriaId = testCase.Criteria.Id;
            TestCaseId = testCase.TestCaseId;
            HasPassed = testCase.QaStatus.HasPassed;
            CurrentStage = mapQAStage(testCase.QaStatus);

            RequiredFields = string.Empty;
            OptionalFields = string.Empty;

            ShowPassCommand = testCase.CanExecutePassTestCase();
            ShowResetCommand = testCase.CanExecuteResetTestCase();

            foreach (var fieldCriteria in testCase.TestCaseCriteriaNodes)
            {
                var fieldValidationCriteria = new FieldValidationCriteriaModel()
                {
                    IsRequired = fieldCriteria.Condition != FieldCriteriaCondition.Optional,
                    FieldTagName = fieldCriteria.FormField.Field.TagName,
                    FieldToolTip = fieldCriteria.FormField.Field.ToolTip,
                    Value = fieldCriteria.CheckValue,
                    Condition = mapCondition(fieldCriteria.Condition)
                };

                FieldCriteria.Add(fieldValidationCriteria);
            }

            foreach (var qaAction in testCase.Criteria.QAActions.Where(x => x.TestCaseId.Equals(testCase.TestCaseId)).OrderByDescending(x => x.OccurredAt))
            {
                var testCaseHistoryItem = new TestCaseHistoryModel(qaAction);
                TestCaseHistoryList.Add(testCaseHistoryItem);
            }

            extractRequiredAndOptionalFields();
        }

        private void extractRequiredAndOptionalFields()
        {
            foreach (var fieldCriteria in FieldCriteria.Where(x => x.IsRequired))
            {
                string formattedCriteria = string.Format("{0} {1} {2}", fieldCriteria.FieldTagName, fieldCriteria.Condition, fieldCriteria.Value).Trim();

                RequiredFields = !String.IsNullOrWhiteSpace(RequiredFields)
                    ? string.Format("{0} &#8226; {1}", RequiredFields, formattedCriteria)
                    : formattedCriteria;
            }

            foreach (var fieldCriteria in FieldCriteria.Where(x => !x.IsRequired))
            {
                string formattedCriteria = string.Format("{0} {1} {2}", fieldCriteria.FieldTagName, fieldCriteria.Condition, fieldCriteria.Value).Trim();

                OptionalFields = String.IsNullOrWhiteSpace(RequiredFields) && String.IsNullOrWhiteSpace(OptionalFields)
                    ? formattedCriteria
                    : string.Format("{0} &#8226; {1}", OptionalFields, formattedCriteria);
            }
        }

        private static string getTestStatus(TestCase testCase)
        {
            string result = string.Empty;

            if (testCase.QaStatus.HasPassed.HasValue == true)
            {
                result = testCase.QaStatus.HasPassed.Value == true ? "Passed" : "Failed";
            }
            return result;
        }

        private string mapQAStage(QAStatus qaStatus)
        {
            string qaStageString = string.Empty;
            if (qaStatus.QAStage == QAStage.UnitTest)
            {
                qaStageString = "Unit Test";
            }
            else if (qaStatus.QAStage == QAStage.Verify)
            {
                qaStageString = "Verify";

            }
            else if (qaStatus.QAStage == QAStage.Certify)
            {
                qaStageString = "Certify";

            }

            return qaStageString;
        }

        private string mapCondition(FieldCriteriaCondition condition)
        {
            string result = string.Empty;

            if (condition == FieldCriteriaCondition.MustBeBlank)
            {
                result = "is blank";
            }
            else if (condition == FieldCriteriaCondition.MustEqual)
            {
                result = "=";
            }
            else if (condition == FieldCriteriaCondition.MustNotEqual)
            {
                result = "not =";
            }

            return result;
        }

    }
}