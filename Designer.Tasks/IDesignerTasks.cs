using StateInterface.Designer.Model;
using StateInterface.Designer.Model.Projections;
using System;
using System.Collections.Generic;

namespace Designer.Tasks
{
    public interface IDesignerTasks
    {
        RecordsCenter GetRecordsCenterByName(string currentUser, string recordsCenterName);
        IEnumerable<RecordsCenter> GetRecordsCenters(string currentUser);
        void SetRecordsCenterForUser(string currentUser, string recordsCenterName);
        IEnumerable<Role> GetRoles();
        User GetUser(string userName);
        IEnumerable<Category> GetCategories(string currentUser);
        IEnumerable<Application> GetApplications(string currentUser);
        IEnumerable<RequestForm> GetForms(string currentUser, int recordsCenterId);
        IEnumerable<RequestForm> GetForms(string currentUser, int recordsCenterId, int CategoryId);
        IEnumerable<RequestForm> GetForms(string currentUser, string recordsCenterName, int categoryId);
        IEnumerable<RequestFormProjection> GetFormProjections(string currentUser, int recordsCenterId);
        TestCase UpdateTestCase(string currentUser, int criteriaId, string testCaseId, DateTime occurred, string note, bool hasPassed);
        TestCase ResetTestCase(string currentUser, int criteriaId, string testCaseId, DateTime occurred, string note);
        RequestForm GetForm(string currentUser, int recordsCenterId, string formId);
        RequestForm GetForm(string currentUser, string recordsCenterName, string formId);
        IEnumerable<Field> GetFieldCatalogItems(string currentUser, string recordsCenterName);
        Field GetField(string currentUser, string recordsCenterName, string tagName);
        OptionList GetList(string currentUser, int recordsCenterId, string listName);
        IEnumerable<FormFieldProjection> GetFormFieldProjectionsUsingOptionList(string currentUser, OptionList list);
        IEnumerable<RequestFormProjection> GetFormProjectionsUsingField(string currentUser, Field field);
        IEnumerable<RequestFormDetailProjection> GetRecordsCenterAcceptanceStatus(string currentUser, int recordsCenterId);
        RequestForm UpdateRequestFormApplications(string currentUser, string recordsCenterName, string formId, IEnumerable<int> selectedApplicationIds);
        RequestForm UpdateRequestFormCategrories(string currentUser, string recordsCenterName, string formId, IEnumerable<int> selectedCategoryIds);
        StatisticsRecordsCenter GetStatisticsForRecordsCenter(string currentUser, string recordsCenterName);
        IEnumerable<TestCase> GetOpenIssues(string currentUser, string recordsCenterName);
        IEnumerable<ListProjection> GetListProjections(string currentUser, int recordsCenterId);
        IEnumerable<TransactionSnippet> GetTransactionSnippets(string currentUser, int recordsCenterId);
        TransactionSnippet GetTransactionSnippet(string currentUser, int recordsCenterId, string tokenName);
        TransactionSnippet UpdateTransactionSnippet(string currentUser, TransactionSnippet transactionsnippet);
        TransactionSnippet UpdateTransactionSnippetField(string currentUser, int parentSnippetId, TransactionSnippetField transactionSnippetField);
        TransactionSnippet DeleteTransactionSnippet(string currentUser, int snippetId);
        TransactionSnippet DeleteTransactionSnippetField(string currentUser, int parentSnippetId, TransactionSnippetField transactionSnippetField);
        TransactionSnippet DeleteTransactionSnippetField(string currentUser, int parentSnippetId, int transactionSnippetFieldId);
        TransactionSnippet GetTransactionSnippet(string currentUser, int snippetId);
    }
}
