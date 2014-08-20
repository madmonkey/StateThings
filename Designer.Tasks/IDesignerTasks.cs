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
        RecordsCenter GetRecordsCenterByName(string recordsCenterName);
        IEnumerable<RecordsCenter> GetRecordsCenters(TaskParameter taskParameter);
        RecordsCenter GetRecordsCenterById(int id);
        void SetRecordsCenterForUser(string userName, string recordsCenterName);
        IEnumerable<Role> GetRoles();
        User GetUser(string userName);
        IEnumerable<Category> GetCategories();
        IEnumerable<Application> GetApplications();
        IEnumerable<RequestForm> GetForms(int recordsCenterId);
        IEnumerable<RequestForm> GetForms(int recordsCenterId, int CategoryId);
        IEnumerable<RequestForm> GetForms(string recordsCenterName, int categoryId);
        IEnumerable<RequestForm> GetFormsByApplication(int recordsCenterId, int applicationId);
        IEnumerable<RequestFormProjection> GetFormProjections(TaskParameter<RecordsCenterId> taskParameter);
        TestCase UpdateTestCase(int criteriaId, string testCaseId, DateTime occurred, string note, string user, bool hasPassed);
        TestCase ResetTestCase(int criteriaId, string testCaseId, DateTime occurred, string note, string user);
        RequestForm GetForm(int recordsCenterId, string formId);
        IEnumerable<Field> GetFieldCatalogItems(string recordsCenterName);
        Field GetField(string recordsCenterName, string tagName);
        OptionList GetList(int recordsCenterId, string listName);
        IEnumerable<FormFieldProjection> GetFormFieldProjectionsUsingOptionList(OptionList list);
        IEnumerable<RequestFormProjection> GetFormProjectionsUsingField(Field field);
        IEnumerable<RequestFormDetailProjection> GetRecordsCenterAcceptanceStatus(int recordsCenterId);
        ApplicationFormProjection GetFormApplicationAssociations(int recordsCenterId, string formId);
        ApplicationFormProjection UpdateFormApplicationAssociations(ApplicationFormProjection applicationFormProjection);
        RequestForm UpdateRequestForm(RequestForm requestForm);
        StatisticsRecordsCenter GetStatisticsForRecordsCenter(string recordsCenterName);
        IEnumerable<TestCase> GetOpenIssues(string recordsCenterName);
        IEnumerable<ListProjection> GetListProjections(TaskParameter<RecordsCenterId> taskParameter);
        IEnumerable<TransactionSnippet> GetTransactionSnippets(int recordsCenterId);
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

    public class RecordsCenterId
    {
        public RecordsCenterId(int id)
        {
            this.Id = id;
        }
        public int Id { get; set; }
    }
    
}
