using System.ComponentModel;
using System.Collections.Generic;
using System;
using System.Linq;

namespace StateInterface.Designer.Model
{
    [Serializable]
    public class RecordsCenter : EntityBase, IDataErrorInfo
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual IList<RequestForm> RequestForms { get; set; }
        public virtual IList<OptionList> OptionLists { get; set; }
        public virtual IList<Field> Fields { get; set; }
        public virtual IList<Header> Headers { get; set; }
        public virtual IList<TransactionSnippet> TransactionSnippets { get; set; }

        public virtual IList<TestCase> GetFailedTestCases()
        {
            List<TestCase> failedTestCases = new List<TestCase>();
            foreach (var form in RequestForms)
            {
                if (!form.TestCases.Any())
                {
                    form.GenerateTestCases();
                }

                failedTestCases.AddRange(form.TestCases.Where(x => x.QaStatus.HasPassed.HasValue && !x.QaStatus.HasPassed.Value));
            }

            return failedTestCases;
        }

        public virtual IList<TestCase> GetFailedTestCases(Application application)
        {
            List<TestCase> failedTestCases = new List<TestCase>();
            foreach (var form in RequestForms)
            {
                if (!form.TestCases.Any())
                {
                    form.GenerateTestCases();
                }

                failedTestCases.AddRange(form.GetFailedTestCases(application));
            }

            return failedTestCases;
        }

        public RecordsCenter()
        {
            Id = 0;
            Name = String.Empty;
            Description = String.Empty;
            RequestForms = new List<RequestForm>();
            OptionLists = new List<OptionList>();
            Fields = new List<Field>();
            Headers = new List<Header>();
            TransactionSnippets = new List<TransactionSnippet>();
        }

        #region IDataErrorInfo Members

        public virtual bool IsValid()
        {
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
                return null;
            }
        }

        #endregion
    }
}
