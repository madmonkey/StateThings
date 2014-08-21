using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StateInterface.Designer.Model.Projections;
using StateInterface.Designer.Model;

namespace Designer.Tasks
{
    public interface IDesignerTasks
    {
        RecordsCenter GetRecordsCenterByName(TaskParameter<RecordsCenterName> taskParameter);
        IEnumerable<RecordsCenter> GetRecordsCenters(TaskParameter taskParameter);
        void SetRecordsCenterForUser(TaskParameter<RecordsCenterName> taskParameter);
        IEnumerable<Role> GetRoles();
        User GetUser(TaskParameter taskParameter);
        IEnumerable<Category> GetCategories(TaskParameter taskParameter);
        IEnumerable<Application> GetApplications(TaskParameter taskParameter);
        IEnumerable<RequestForm> GetForms(TaskParameter<RecordsCenterId> taskParameter);
        IEnumerable<RequestForm> GetForms(TaskParameter<FormsCategoryByRecordsCenterName> taskParameter);
        IEnumerable<RequestFormProjection> GetFormProjections(TaskParameter<RecordsCenterId> taskParameter);
        TestCase UpdateTestCase(TaskParameter<CriteriaTestCasePassFail> taskParameter);
        TestCase ResetTestCase(TaskParameter<CriteriaTestCase> taskParameter);
        RequestForm GetForm(TaskParameter<FormById> taskParameter);
        IEnumerable<Field> GetFieldCatalogItems(TaskParameter<RecordsCenterName> taskParameter);
        Field GetField(TaskParameter<FieldByTag> taskParameter);
        OptionList GetList(TaskParameter<ListByName> taskParameter);
        IEnumerable<FormFieldProjection> GetFormFieldProjectionsUsingOptionList(TaskParameter<OptionList> taskParameters);
        IEnumerable<RequestFormProjection> GetFormProjectionsUsingField(TaskParameter<Field> taskParameters);
        IEnumerable<RequestFormDetailProjection> GetRecordsCenterAcceptanceStatus(TaskParameter<RecordsCenterId> taskParameter);
        RequestForm UpdateRequestForm(TaskParameter<RequestForm> taskParameter);
        StatisticsRecordsCenter GetStatisticsForRecordsCenter(TaskParameter<RecordsCenterName> taskParameter);
        IEnumerable<TestCase> GetOpenIssues(TaskParameter<RecordsCenterName> taskParameter);
        IEnumerable<ListProjection> GetListProjections(TaskParameter<RecordsCenterId> taskParameter);
        IEnumerable<TransactionSnippet> GetTransactionSnippets(TaskParameter<RecordsCenterId> taskParameter);
        TransactionSnippet GetTransactionSnippet(TaskParameter<SnippetFieldByToken> taskParameter);
        TransactionSnippet UpdateTransactionSnippet(TaskParameter<TransactionSnippet> taskParameter);
        TransactionSnippet UpdateTransactionSnippetField(TaskParameter<SnippetFieldDetail> taskParameter);
        TransactionSnippet DeleteTransactionSnippet(TaskParameter<Snippet> taskParameter);
        TransactionSnippet DeleteTransactionSnippetField(TaskParameter<SnippetField> taskParameter);
        TransactionSnippet GetTransactionSnippet(TaskParameter<Snippet> taskParameter);
    }

    public class RecordsCenterId : ById
    {
        public RecordsCenterId(int id) : base(id) { }
    }

    public class RecordsCenterName : ByKey
    {
        public RecordsCenterName(string name):base(name) { }
        public string Name { get { return this.Key; } }
    }

    public class FormsCategoryByRecordsCenterName : RecordsCenterName
    {
        public FormsCategoryByRecordsCenterName(string recordsCenterName, int categoryId) : base(recordsCenterName)
        {
            this.CategoryId = categoryId;
        }
        public int CategoryId { get; private set; }
    }

    public class Snippet : ById
    {
        public Snippet(int id) : base(id) { }
    }
    
    public class SnippetField : ById
    {
        public SnippetField(int parentId, int fieldId):base(parentId)
        {
            this.FieldId = fieldId;
        }
        public int FieldId { get; private set; }
    }

    public class SnippetFieldDetail : ById
    {
        public SnippetFieldDetail(int parentSnippet, TransactionSnippetField field)
            : base(parentSnippet)
        {
            this.SnippetField = field;
        }
        public TransactionSnippetField SnippetField { get; private set; }
    }

    public class SnippetFieldByToken : RecordsCenterId
    {
        public SnippetFieldByToken(int recordsCenterId, string tokenName): base(recordsCenterId)
        {
            this.TokenName = tokenName;
        }
        public string TokenName { get; private set; }
    }

    public class ListByName: RecordsCenterId
    {
        public ListByName(int recordsCenterId, string listName): base(recordsCenterId)
        {
            this.ListName = listName;
        }
        public string ListName { get; private set; }
    }

    public class FieldByTag : RecordsCenterName
    {
        public FieldByTag(string recordsCenterName, string tagName):base(recordsCenterName)
        {
            this.TagName = tagName;
        }
        public string TagName { get; private set; }
    }

    public class FormById : RecordsCenterId
    {
        public FormById(int recordsCenterId, string formId) : base(recordsCenterId)
        {
            this.FormId = formId;
        }
        public string FormId { get; private set; }
    }
    
    public class CriteriaTestCase : ById
    {
        public CriteriaTestCase(int criteriaId, string testCaseId, DateTime occurred, string note): base(criteriaId)
        {
            this.TestCaseId = testCaseId;
            this.Occurred = occurred;
            this.Note = note;
        }
        public string TestCaseId { get; private set; }
        public DateTime Occurred { get; private set; }

        public string Note { get; private set; }
    }

    public class CriteriaTestCasePassFail : CriteriaTestCase
    {
        public CriteriaTestCasePassFail(int criteriaId, string testCaseId, DateTime occurred, string note, bool hasPassed)
            : base(criteriaId, testCaseId,occurred,note)
        {
            this.HasPassed = hasPassed;
        }
        public bool HasPassed { get; private set; }
    }
    public abstract class ById
    {
        public ById(int id)
        {
            this.Id = id;
        }
        public int Id { get; private set; }
    }

    public abstract class ByKey
    {
        public ByKey(string key)
        {
            this.Key = key;
        }
        public string Key { get; private set; }
    }
}
