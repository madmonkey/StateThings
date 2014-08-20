using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using StateInterface.Designer.Model.Helper;

namespace StateInterface.Designer.Model
{
    [Serializable]
    public class Criteria : EntityBase, IDataErrorInfo
    {
        private string _criteriaNamePropertyName;

        public Criteria()
        {
            CriteriaNodes = new List<CriteriaNode>();
            QAActions = new List<QAAction>();

            _criteriaNamePropertyName = PropertyHelper.GetPropertyName((Criteria item) => item.CriteriaName);
        }
        public Criteria(Criteria sourceCriteria, Transaction newTransaction, IEnumerable<FormField> newFormFields)
            :this()
        {
            //Id - DB handles
            Transaction = newTransaction;
            CriteriaName = sourceCriteria.CriteriaName;
            Sequence = sourceCriteria.Sequence;
            Description = sourceCriteria.Description;

            foreach (var sourceCriteriaNode in sourceCriteria.CriteriaNodes)
            {
                CriteriaNodes.Add(new CriteriaNode(sourceCriteriaNode, newFormFields));
            }

            //Test Cases - don't copy
            //QA Actions - don't copy 
            //todo: do we need to copy QA Actions

        }
        public virtual int Id { get; set; }
        public virtual Transaction Transaction { get; set; }
        public virtual string CriteriaName { get; set; }
        public virtual int Sequence { get; set; }
        public virtual string Description { get; set; }
        public virtual IList<CriteriaNode> CriteriaNodes { get; set; }

        public virtual IEnumerable<TestCase> GetTestCases(Application application)
        {
            List<TestCase> testsCases = new List<TestCase>();

            //Look for implicit test cases. Are there any fields that might be MKE selectors
            var transactionFieldNodes = Transaction.TxNodes.Where(x => x is TxFieldNode).Select(x => x as TxFieldNode);

            var mkefield = transactionFieldNodes
                .FirstOrDefault(x => x.FormField.Field.TagName.Contains("MKE")
                    && x.FormField.OptionList != null
                    && x.FormField.OptionList.ListName.Contains("MKE"));

            if (mkefield != null && CriteriaNodes.Any(x => x.FormField.Id == mkefield.FormField.Id 
                && (x.Condition == FieldCriteriaCondition.Required 
                || x.Condition == FieldCriteriaCondition.Optional
                || x.Condition == FieldCriteriaCondition.MustNotEqual)))
            {
                foreach (var mkeOptionListItem in mkefield.FormField.OptionList.OptionListItems)
                {
                    if(CriteriaNodes.Any(x => x.FormField.Id == mkefield.FormField.Id 
                        && x.Condition == FieldCriteriaCondition.MustNotEqual
                        && x.CheckValue.Equals(mkeOptionListItem.Code, StringComparison.CurrentCultureIgnoreCase)))
                    {
                        continue;
                    }

                    if (RequiredFieldNodes.Any())
                    {
                        var requiredFieldsTestCase = new TestCase()
                        {
                            Application = application,
                            Criteria = this,
                            IncludesRequiredFields = true,
                            TestCaseId = string.Format("{0}.1.{1}.{2}", application.Name, mkefield.FormField.Field.TagName, mkeOptionListItem.Code),
                        };

                        requiredFieldsTestCase.QaStatus = GetTestCaseQAState(requiredFieldsTestCase.TestCaseId);

                        foreach (var node in RequiredFieldNodes)
                        {
                            var testCaseCriteriaNode = new TestCaseCriteriaNode(node);
                            if (node.FormField.Field.TagName.Equals(mkefield.FormField.Field.TagName))
                            {
                                testCaseCriteriaNode.CheckValue = mkeOptionListItem.Code;
                                testCaseCriteriaNode.Condition = FieldCriteriaCondition.MustEqual;
                            }

                            requiredFieldsTestCase.TestCaseCriteriaNodes.Add(testCaseCriteriaNode);
                        }
                        testsCases.Add(requiredFieldsTestCase);
                    }

                    if (OptionalFieldNodesInAssociatedTransaction.Any())
                    {
                        var requiredAndOptionalFieldsTestCase = new TestCase()
                        {
                            Application = application,
                            Criteria = this,
                            IncludesRequiredFields = true,
                            IncludesOptionalFields = true,
                            TestCaseId = string.Format("{0}.2.{1}.{2}", application.Name, mkefield.FormField.Field.TagName, mkeOptionListItem.Code),
                        };

                        requiredAndOptionalFieldsTestCase.QaStatus = GetTestCaseQAState(requiredAndOptionalFieldsTestCase.TestCaseId);

                        foreach (var node in RequiredFieldNodes)
                        {
                            var testCaseCriteriaNode = new TestCaseCriteriaNode(node);
                            if (node.FormField.Field.TagName.Equals(mkefield.FormField.Field.TagName))
                            {
                                testCaseCriteriaNode.CheckValue = mkeOptionListItem.Code;
                                testCaseCriteriaNode.Condition = FieldCriteriaCondition.MustEqual;
                            }

                            requiredAndOptionalFieldsTestCase.TestCaseCriteriaNodes.Add(testCaseCriteriaNode);
                        }

                        foreach (var node in OptionalFieldNodesInAssociatedTransaction)
                        {
                            var testCaseCriteriaNode = new TestCaseCriteriaNode(node);
                            if (node.FormField.Field.TagName.Equals(mkefield.FormField.Field.TagName))
                            {
                                testCaseCriteriaNode.CheckValue = mkeOptionListItem.Code;
                                testCaseCriteriaNode.Condition = FieldCriteriaCondition.MustEqual;
                            }

                            requiredAndOptionalFieldsTestCase.TestCaseCriteriaNodes.Add(testCaseCriteriaNode);
                        }

                        testsCases.Add(requiredAndOptionalFieldsTestCase);
                    }
                }
            }
            // Create explicit test cases
            else
            {
                if (RequiredFieldNodes.Any())
                {
                    var requiredFieldsTestCase = new TestCase()
                    {
                        Application = application,
                        Criteria = this,
                        IncludesRequiredFields = true,
                        TestCaseId = string.Format("{0}.1", application.Name),
                    };

                    requiredFieldsTestCase.QaStatus = GetTestCaseQAState(requiredFieldsTestCase.TestCaseId);

                    foreach (var node in RequiredFieldNodes)
                    {
                        requiredFieldsTestCase.TestCaseCriteriaNodes.Add(new TestCaseCriteriaNode(node));
                    }
                    testsCases.Add(requiredFieldsTestCase);
                }

                if (OptionalFieldNodesInAssociatedTransaction.Any())
                {
                    var requiredAndOptionalFieldsTestCase = new TestCase()
                    {
                        Application = application,
                        Criteria = this,
                        IncludesRequiredFields = true,
                        IncludesOptionalFields = true,
                        TestCaseId = string.Format("{0}.2", application.Name),
                    };

                    requiredAndOptionalFieldsTestCase.QaStatus = GetTestCaseQAState(requiredAndOptionalFieldsTestCase.TestCaseId);

                    foreach (var node in RequiredFieldNodes)
                    {
                        requiredAndOptionalFieldsTestCase.TestCaseCriteriaNodes.Add(new TestCaseCriteriaNode(node));
                    }

                    foreach (var node in OptionalFieldNodesInAssociatedTransaction)
                    {
                        requiredAndOptionalFieldsTestCase.TestCaseCriteriaNodes.Add(new TestCaseCriteriaNode(node));
                    }

                    testsCases.Add(requiredAndOptionalFieldsTestCase);
                }
            }

            return testsCases;
        }
        public virtual IList<TestCase> GetFailedTestCases(Application application)
        {
            List<TestCase> failedTestCases = new List<TestCase>();
            var testCases = GetTestCases(application);

            foreach (var testCase in testCases.Where(x => x.QaStatus.HasPassed.HasValue && !x.QaStatus.HasPassed.Value))
            {
                failedTestCases.Add(testCase);
            }

            return failedTestCases;
        }
        public virtual IList<CriteriaNode> RequiredFieldNodes
        {
            get
            {
                return CriteriaNodes
                    .Where(x => x.Condition != FieldCriteriaCondition.Optional).ToList();
            }
        }
        public virtual IList<QAAction> QAActions { get; set; } //todo: move with other properites
        public virtual QAStatus GetTestCaseQAState(string testCaseId)
        {
            QAStatus qaStatus = new QAStatus()
                {
                    QAStage = QAStage.UnitTest
                };

            if (this.QAActions != null)
            {
                var lastAction = this.QAActions
                    .OrderBy(x => x.OccurredAt)
                    .LastOrDefault(x => x.TestCaseId.Equals(testCaseId));

                if (lastAction != null)
                {
                    if (lastAction.QAActionType.ActionName.Equals(QAActionType.Reset))
                    {
                        qaStatus.QAStage = QAStage.UnitTest;
                    }
                    else if (lastAction.QAActionType.ActionName.Equals(QAActionType.UnitTest))
                    {
                        qaStatus.QAStage = lastAction.HasPassed == true ? QAStage.Verify : QAStage.UnitTest;
                        if (lastAction.HasPassed == false)
                        {
                            qaStatus.HasPassed = lastAction.HasPassed;
                        }
                    }
                    else if (lastAction.QAActionType.ActionName.Equals(QAActionType.Verify))
                    {
                        qaStatus.QAStage = lastAction.HasPassed == true ? QAStage.Certify : QAStage.Verify;
                        if (lastAction.HasPassed == false)
                        {
                            qaStatus.HasPassed = lastAction.HasPassed;
                        }
                    }
                    else if (lastAction.QAActionType.ActionName.Equals(QAActionType.Certify))
                    {
                        qaStatus.QAStage = lastAction.HasPassed == true ? QAStage.Certify : QAStage.Certify;
                        qaStatus.HasPassed = lastAction.HasPassed;
                    }
                }

            }
            return qaStatus;
        }
        public virtual IList<CriteriaNode> OptionalFieldNodesInAssociatedTransaction
        {
            get
            {
                List<CriteriaNode> nodes = new List<CriteriaNode>();
                foreach (var node in CriteriaNodes.Where(x => x.Condition == FieldCriteriaCondition.Optional))
                {
                    if (this.Transaction.TxNodes.Any(x => x is TxFieldNode && (x as TxFieldNode).FormField.Id == node.FormField.Id))
                    {
                        nodes.Add(node);
                    }
                }

                return nodes;
            }
        }
        public virtual void RemoveCriteriaNodes(List<CriteriaNode> criteriaNodesToDelete)
        {
            foreach (var item in criteriaNodesToDelete)
            {
                CriteriaNodes.Remove(item);
            }
        }

        #region IDataErrorInfo Members

        public virtual bool IsValid()
        {
            if (this[_criteriaNamePropertyName] != null) { return false; }

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
                if (columnName == _criteriaNamePropertyName)
                {
                    if (String.IsNullOrEmpty(CriteriaName))
                    {
                        return "Criteria name is required";
                    }

                    if (CriteriaName.Length > 100)
                    {
                        return "Criteria name must be less than 100 characters";
                    }

                    // Make sure it's unique on the form
                    if (Transaction.RequestForm.Transactions.SelectMany(x => x.Criterion).Any(x => x.CriteriaName == CriteriaName
                        && x != this))
                    {
                        return "The Criteria Name is already used on this form";
                    }
                }

                return null;
            }
        }

        #endregion
    }
}
