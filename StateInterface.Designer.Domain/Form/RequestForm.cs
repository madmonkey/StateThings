using System.Collections.Generic;
using System.Xml.Serialization;
using System.ComponentModel;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using StateInterface.Designer.Model.Helper;
using System.Text.RegularExpressions;

namespace StateInterface.Designer.Model
{
    public enum SubmissionMode
    {
        FirstValidatedTransaction = 0,
        AllValidatedTransactions = 1
    }

    [XmlRoot]
    [Serializable]
    public class RequestForm : EntityBase, IDataErrorInfo
    {
        private string _formIdPropertyName;
        private string _versionPropertyName;
        private string _titlePropertyName;

        public virtual int Id { get; set; }
        public virtual DateTime Created { get; set; }
        public virtual DateTime? Updated { get; set; }
        public virtual string Version { get; set; }
        public virtual string FormId { get; set; }
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual IList<FieldElement> FieldElements { get; set; }
        public virtual RecordsCenter RecordsCenter { get; set; }
        public virtual IList<RequestFormCategory> RequestFormCategories { get; set; }
        public virtual IList<Application> Applications { get; set; }
        public virtual IList<FormField> FormFields { get; set; }
        public virtual IList<LabelElement> LabelElements { get; set; }
        public virtual int TableRowCount { get; set; }
        public virtual int TableColumnCount { get; set; }
        public virtual IList<Transaction> Transactions { get; set; }
        public virtual bool IncludeFieldPrefixAndSuffix { get; set; }
        public virtual SubmissionMode SubmissionMode { get; set; }
        public virtual List<TestCase> TestCases { get; set; }
        public RequestForm()
        {
            Created = DateTime.UtcNow;
            LabelElements = new List<LabelElement>();
            FieldElements = new List<FieldElement>();
            Transactions = new List<Transaction>();
            FormFields = new List<FormField>();
            RequestFormCategories = new List<RequestFormCategory>();
            Applications = new List<Application>();
            TestCases = new List<TestCase>();

            TableRowCount = 0;
            TableColumnCount = 0;
            IncludeFieldPrefixAndSuffix = true;
            Version = "1";

            // Grab string property names for use with IDataErrorInfo
            _formIdPropertyName = PropertyHelper.GetPropertyName((RequestForm item) => item.FormId);
            _versionPropertyName = PropertyHelper.GetPropertyName((RequestForm item) => item.Version);
            _titlePropertyName = PropertyHelper.GetPropertyName((RequestForm item) => item.Title);
        }
        public RequestForm(RecordsCenter recordsCenter)
            : this()
        {
            RecordsCenter = recordsCenter;
        }
        public RequestForm(RequestForm sourceRequestForm)
            : this()
        {
            //Id - DB
            //Created - default constructor
            //Updated - elsewhere
            Version = sourceRequestForm.Version;
            FormId = sourceRequestForm.FormId;
            Title = sourceRequestForm.Title;
            Description = sourceRequestForm.Description;

            foreach (var sourceFormField in sourceRequestForm.FormFields)
            {
                FormFields.Add(new FormField(sourceFormField, this));
            }

            foreach (var sourceFieldElement in sourceRequestForm.FieldElements)
            {
                var newFormFieldForThisFieldElement = this.FormFields.Where(x => x.Field.TagName == sourceFieldElement.FormField.Field.TagName).FirstOrDefault();

                FieldElements.Add(new FieldElement(sourceFieldElement, newFormFieldForThisFieldElement));
            }

            RecordsCenter = sourceRequestForm.RecordsCenter;

            //todo: RequestFormCategory should be elimated and handled with the same structure as Applications
            foreach (var sourceRequestFormCategories in sourceRequestForm.RequestFormCategories)
            {
                RequestFormCategories.Add(new RequestFormCategory(this, sourceRequestFormCategories.Category));
            }

            foreach (var sourceApplication in sourceRequestForm.Applications)
            {
                Applications.Add(sourceApplication);
            }

            foreach (var sourceLabelElement in sourceRequestForm.LabelElements)
            {
                LabelElements.Add(new LabelElement(sourceLabelElement));
            }

            TableColumnCount = sourceRequestForm.TableColumnCount;
            TableRowCount = sourceRequestForm.TableRowCount;

            foreach (var sourceTransaction in sourceRequestForm.Transactions)
            {
                Transactions.Add(new Transaction(sourceTransaction, this));
            }

            IncludeFieldPrefixAndSuffix = sourceRequestForm.IncludeFieldPrefixAndSuffix;
            SubmissionMode = sourceRequestForm.SubmissionMode;
            //Test Cases - should not be copied

            //for each FormField that has a Parent, update the Parent to reference the equivalent new FormField instance
            foreach (var formField in FormFields.Where(x => x.ParentFormField != null))
            {
                formField.ParentFormField = FormFields.FirstOrDefault(x => x.Field == formField.ParentFormField.Field);
            }
        }
        public static List<string> SubmissionModes { get { return _submissionModes; } }
        public virtual QAStatistics GetQAStatistics(Application application)
        {
            if (TestCases.Any() == false)
            {
                GenerateTestCases();
            };

            var testCases = TestCases.Where(x => x.Application.Id == application.Id);

            return getQaStatistics(testCases);

        }
        public virtual QAStatistics GetQAStatistics()
        {
            if (TestCases.Any() == false)
            {
                GenerateTestCases();
            };

            return getQaStatistics(TestCases);
        }
        public virtual StatisticsDetails GetQaStatisticsDetails()
        {
            var statisticsDetails = new StatisticsDetails();

            if (TestCases.Any() == false)
            {
                GenerateTestCases();
            };

            statisticsDetails.TotalTestCases += TestCases.Count();

            foreach (var testCase in TestCases)
            {
                var result = testCase.Criteria.QAActions.Where(x => x.Criteria.Id == testCase.Criteria.Id
                        && x.TestCaseId.Equals(testCase.TestCaseId)
                        && x.HasPassed == true).ToList();
                foreach (var activity in result)
                {
                    statisticsDetails.Activities.Add(activity);
                }

                var qaAction = testCase.Criteria.QAActions.OrderBy(x => x.OccurredAt)
                    .LastOrDefault(x => x.Criteria.Id == testCase.Criteria.Id && x.TestCaseId.Equals(testCase.TestCaseId));

                if (qaAction != null)
                {
                    if (qaAction.QAActionType.ActionName.Equals(QAActionType.Certify))
                    {
                        if (qaAction.HasPassed == true)
                        {
                            statisticsDetails.CountCurrentCertifyPassed += 1;
                        }
                        else
                        {
                            statisticsDetails.CountCurrentCertifyFailed += 1;
                        }
                        statisticsDetails.CountCurrentVerifyPassed += 1;
                        statisticsDetails.CountCurrentUnitTestPassed += 1;
                    }
                    else if (qaAction.QAActionType.ActionName.Equals(QAActionType.Verify))
                    {
                        if (qaAction.HasPassed == true)
                        {
                            statisticsDetails.CountCurrentVerifyPassed += 1;
                        }
                        else
                        {
                            statisticsDetails.CountCurrentVerifyFailed += 1;
                        }
                        statisticsDetails.CountCurrentUnitTestPassed += 1;
                    }
                    else if (qaAction.QAActionType.ActionName.Equals(QAActionType.UnitTest))
                    {
                        if (qaAction.HasPassed == true)
                        {
                            statisticsDetails.CountCurrentUnitTestPassed += 1;
                        }
                        else
                        {
                            statisticsDetails.CountCurrentUnitTestFailed += 1;
                        }
                    }
                }
            }

            statisticsDetails.CalculateData();
            return statisticsDetails;
        }
        public virtual StatisticsDetails GetQaStatisticsDetails(Application application)
        {
            if (TestCases.Any() == false)
            {
                GenerateTestCases();
            };
            var statisticsDetails = new StatisticsDetails();

            var applicationTestCases = TestCases.Where(x => x.Application.Name == application.Name).ToList();

            statisticsDetails.TotalTestCases += applicationTestCases.Count();

            foreach (var testCase in applicationTestCases)
            {
                var result = testCase.Criteria.QAActions.Where(x => x.Criteria.Id == testCase.Criteria.Id
                        && x.TestCaseId.Equals(testCase.TestCaseId)
                        && x.HasPassed == true).ToList();
                foreach (var activity in result)
                {
                    statisticsDetails.Activities.Add(activity);
                }

                var qaAction = testCase.Criteria.QAActions.OrderBy(x => x.OccurredAt)
                    .LastOrDefault(x => x.Criteria.Id == testCase.Criteria.Id && x.TestCaseId.Equals(testCase.TestCaseId));

                if (qaAction != null)
                {
                    if (qaAction.QAActionType.ActionName.Equals(QAActionType.Certify))
                    {
                        if (qaAction.HasPassed == true)
                        {
                            statisticsDetails.CountCurrentCertifyPassed += 1;
                        }
                        else
                        {
                            statisticsDetails.CountCurrentCertifyFailed += 1;
                        }
                        statisticsDetails.CountCurrentVerifyPassed += 1;
                        statisticsDetails.CountCurrentUnitTestPassed += 1;
                    }
                    else if (qaAction.QAActionType.ActionName.Equals(QAActionType.Verify))
                    {
                        if (qaAction.HasPassed == true)
                        {
                            statisticsDetails.CountCurrentVerifyPassed += 1;
                        }
                        else
                        {
                            statisticsDetails.CountCurrentVerifyFailed += 1;
                        }
                        statisticsDetails.CountCurrentUnitTestPassed += 1;
                    }
                    else if (qaAction.QAActionType.ActionName.Equals(QAActionType.UnitTest))
                    {
                        if (qaAction.HasPassed == true)
                        {
                            statisticsDetails.CountCurrentUnitTestPassed += 1;
                        }
                        else
                        {
                            statisticsDetails.CountCurrentUnitTestFailed += 1;
                        }
                    }
                }
            }

            statisticsDetails.CalculateData();
            return statisticsDetails;
        }
        public virtual void GenerateTestCases()
        {
            foreach (var application in Applications)
            {
                foreach (var transaction in Transactions)
                {
                    foreach (var criteria in transaction.Criterion)
                    {
                        var criteriaTestCases = criteria.GetTestCases(application);
                        TestCases.AddRange(criteriaTestCases);
                    }
                }
            }
        }
        public virtual IList<TestCase> GetFailedTestCases(Application application)
        {
            List<TestCase> failedTestCases = new List<TestCase>();
            foreach (var transaction in Transactions)
            {
                failedTestCases.AddRange(transaction.GetFailedTestCases(application));
            }

            return failedTestCases;
        }
        public override string ToString()
        {
            return string.Format("{0} ({1})", FormId, Title);
        }
        private static List<string> _submissionModes = new List<string>() 
        {   
            "First valid transaction", 
            "All valid transactions"
        };
        private QAStatistics getQaStatistics(IEnumerable<TestCase> testCases)
        {
            QAStatistics result = new QAStatistics();

            result.TotalTestCases += testCases.Count();

            foreach (var testCase in testCases)
            {
                var qaAction = testCase.Criteria.QAActions.OrderBy(x => x.OccurredAt)
                    .LastOrDefault(x => x.Criteria.Id == testCase.Criteria.Id && x.TestCaseId.Equals(testCase.TestCaseId));

                if (qaAction != null)
                {
                    if (qaAction.QAActionType.ActionName.Equals(QAActionType.Certify))
                    {
                        if (qaAction.HasPassed == true)
                        {
                            result.CountCurrentCertifyPassed += 1;
                        }
                        else
                        {
                            result.CountCurrentCertifyFailed += 1;
                        }
                        result.CountCurrentVerifyPassed += 1;
                        result.CountCurrentUnitTestPassed += 1;
                    }
                    else if (qaAction.QAActionType.ActionName.Equals(QAActionType.Verify))
                    {
                        if (qaAction.HasPassed == true)
                        {
                            result.CountCurrentVerifyPassed += 1;
                        }
                        else
                        {
                            result.CountCurrentVerifyFailed += 1;
                        }
                        result.CountCurrentUnitTestPassed += 1;
                    }
                    else if (qaAction.QAActionType.ActionName.Equals(QAActionType.UnitTest))
                    {
                        if (qaAction.HasPassed == true)
                        {
                            result.CountCurrentUnitTestPassed += 1;
                        }
                        else
                        {
                            result.CountCurrentUnitTestFailed += 1;
                        }
                    }
                }
            }

            return result;
        }

        #region IDataErrorInfo Members

        public virtual bool CanBuildForm()
        {
            bool allFieldsOnForm = true;

            foreach (var item in FormFields)
            {
                if (FieldElements.Any(x => x.FormField == item) == false)
                {
                    allFieldsOnForm = false;
                }
            }



            return IsValid && allFieldsOnForm;
        }

        public virtual bool IsValid
        {
            get
            {
                if (this[_formIdPropertyName] != null) { return false; }
                if (this[_titlePropertyName] != null) { return false; }
                if (this[_versionPropertyName] != null) { return false; }
                if (Transactions.Any<Transaction>(x => x.IsValid() == false)) { return false; }
                if (!areListsExpressedOnForm()) { return false; }
                if (!areListsMapped()) { return false; }

                return true;
            }
        }

        private bool areListsMapped()
        {
            List<FormField> mapping = new List<FormField>();

            foreach (var formField in FormFields.Where(x => x.OptionList != null && x.OptionListTier != x.OptionList.OptionListTiers.First()))
            {
                if (formField.ParentFormField == null || mapping.Contains(formField.ParentFormField))
                {
                    return false;
                }
                else
                {
                    mapping.Add(formField.ParentFormField);
                }
            }

            return true;
        }

        private bool areListsExpressedOnForm()
        {
            Dictionary<OptionList, Dictionary<OptionListTier, int>> optionListTiersFound = new Dictionary<OptionList, Dictionary<OptionListTier, int>>();

            foreach (var item in FormFields)
            {
                if (item.OptionList != null)
                {
                    if (!optionListTiersFound.ContainsKey(item.OptionList))
                    {
                        optionListTiersFound.Add(item.OptionList, new Dictionary<OptionListTier, int>());
                    }

                    if (!optionListTiersFound[item.OptionList].ContainsKey(item.OptionListTier))
                    {
                        optionListTiersFound[item.OptionList].Add(item.OptionListTier, 1);
                    }
                    else
                    {
                        optionListTiersFound[item.OptionList][item.OptionListTier]++;
                    }
                }
            }

            foreach (KeyValuePair<OptionList, Dictionary<OptionListTier, int>> pair in optionListTiersFound)
            {
                if (pair.Value.Count % pair.Key.OptionListTiers.Count != 0)
                {
                    return false;
                }
                else
                {
                    int count = -1;
                    foreach (var tier in pair.Value)
                    {
                        if (count == -1)
                        {
                            count = tier.Value;
                        }
                        else if (tier.Value != count)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        public virtual string Error
        {
            get { return null; }
        }

        public virtual string this[string columnName]
        {
            get
            {
                if (columnName == _formIdPropertyName)
                {
                    if (String.IsNullOrEmpty(FormId))
                    {
                        return "Form Id is required";
                    }

                    if (FormId.Contains(" "))
                    {
                        return "Form Id may not contain spaces";
                    }

                    if (!Regex.IsMatch(FormId, "^[a-zA-Z0-9]+$", RegexOptions.None))
                    {
                        return "Form Id can contain letters and numbers only";
                    }
                }
                else if (columnName == _titlePropertyName)
                {
                    if (String.IsNullOrEmpty(Title))
                    {
                        return "Form Title is required";
                    }
                }

                return null;
            }
        }

        #endregion
    }
}
