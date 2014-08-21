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
        RecordsCenter GetRecordsCenterById(TaskParameter<RecordsCenterId> taskParameter);
        void SetRecordsCenterForUser(TaskParameter<RecordsCenterName> taskParameter);
        IEnumerable<Role> GetRoles(TaskParameter taskParameter);
        User GetUser(TaskParameter<UserByName> taskParameter);
        User GetUser(TaskParameter taskParameter);
        IEnumerable<Category> GetCategories(TaskParameter taskParameter);
        IEnumerable<Application> GetApplications(TaskParameter taskParameter);
        IEnumerable<RequestForm> GetForms(TaskParameter<RecordsCenterId> taskParameter);
        IEnumerable<RequestForm> GetForms(int recordsCenterId, int CategoryId);
        IEnumerable<RequestForm> GetForms(string recordsCenterName, int categoryId);
        IEnumerable<RequestForm> GetFormsByApplication(int recordsCenterId, int applicationId);
        IEnumerable<RequestFormProjection> GetFormProjections(TaskParameter<RecordsCenterId> taskParameter);
        TestCase UpdateTestCase(int criteriaId, string testCaseId, DateTime occurred, string note, string user, bool hasPassed);
        TestCase ResetTestCase(int criteriaId, string testCaseId, DateTime occurred, string note, string user);
        RequestForm GetForm(int recordsCenterId, string formId);
        IEnumerable<Field> GetFieldCatalogItems(TaskParameter<RecordsCenterName> taskParameter);
        Field GetField(string recordsCenterName, string tagName);
        OptionList GetList(int recordsCenterId, string listName);
        IEnumerable<FormFieldProjection> GetFormFieldProjectionsUsingOptionList(OptionList list);
        IEnumerable<RequestFormProjection> GetFormProjectionsUsingField(Field field);
        IEnumerable<RequestFormDetailProjection> GetRecordsCenterAcceptanceStatus(TaskParameter<RecordsCenterId> taskParameter);
        ApplicationFormProjection GetFormApplicationAssociations(int recordsCenterId, string formId);
        ApplicationFormProjection UpdateFormApplicationAssociations(ApplicationFormProjection applicationFormProjection);
        RequestForm UpdateRequestForm(RequestForm requestForm);
        StatisticsRecordsCenter GetStatisticsForRecordsCenter(TaskParameter<RecordsCenterName> taskParameter);
        IEnumerable<TestCase> GetOpenIssues(TaskParameter<RecordsCenterName> taskParameter);
        IEnumerable<ListProjection> GetListProjections(TaskParameter<RecordsCenterId> taskParameter);
        IEnumerable<TransactionSnippet> GetTransactionSnippets(TaskParameter<RecordsCenterId> taskParameter);
        TransactionSnippet GetTransactionSnippet(int recordsCenterId, string tokenName);
        TransactionSnippet CreateTransactionSnippet(RecordsCenter recordsCenter, string name, string description);
        TransactionSnippet UpdateTransactionSnippet(TransactionSnippet transactionsnippet);
        TransactionSnippet CreateTransactionSnippetField(int parentSnippetId, string tagName, int length);
        TransactionSnippet UpdateTransactionSnippetField(int parentSnippetId, TransactionSnippetField transactionSnippetField);
        TransactionSnippet DeleteTransactionSnippet(int snippetId);
        TransactionSnippet DeleteTransactionSnippetField(int parentSnippetId, TransactionSnippetField transactionSnippetField);
        TransactionSnippet DeleteTransactionSnippetField(int parentSnippetId, int transactionSnippetFieldId);
        TransactionSnippet GetTransactionSnippet(int snippetId);
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

    public class UserByName : ByKey
    {
        public UserByName(string name) : base(name) { }
        public string UserName { get { return this.Key; } }
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
