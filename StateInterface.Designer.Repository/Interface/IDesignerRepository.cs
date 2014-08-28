using StateInterface.Designer.Model;
using StateInterface.Designer.Model.Projections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StateInterface.Designer.Repository
{
    public interface IDesignerRepository
    {
        T GetById <T>(int id);
        IQueryable<T> GetAll<T>();
        void Save<T>(T entity);
        void Remove<T>(T entity);
        RecordsCenter GetRecordsCenterByName(string recordsCenterName);
        Field FindFieldByTag(string tagName, RecordsCenter recordsCenter);
        IQueryable<Field> GetAllFields();
        IQueryable<Field> GetFieldsForRecordsCenter(RecordsCenter recordsCenter);
        IEnumerable<FieldProjection> GetFieldProjectionsForRecordsCenter(RecordsCenter recordsCenter);
        IList<RequestFormProjection> GetFormProjectionsUsingField(Field field);
        Field GetField(int fieldId);
        User GetUser(string userName);
        IList<Field> FindFields(List<string> searchTerms, RecordsCenter recordsCenter);
        RequestForm GetForm(string formId, RecordsCenter recordCenter);
        RequestFormProjection GetFormProjectionForRecordsCenter(string formId, RecordsCenter recordsCenter);
        OptionList GetList(string listName, RecordsCenter recordCenter);
        RequestForm GetStatelessFormById(int id, RecordsCenter recordsCenter);
        IEnumerable<RequestFormProjection> GetFormProjectionsForRecordsCenter(RecordsCenter recordsCenter);        
        IQueryable<RequestForm> GetFormsForRecordsCenter(RecordsCenter recordsCenter);
        IQueryable<string> FindFormIdsLike(string formIdPattern);
        IQueryable<string> FindFormIdsLike(string formIdPattern, RecordsCenter recordsCenter);
        IList<RequestFormProjection> FindRequestForms(List<string> searchTerms, RecordsCenter recordsCenter);
        Header FindByHeaderName(string headerName, RecordsCenter recordCenter);
        IQueryable<Header> GetHeadersForRecordsCenter(RecordsCenter recordsCenter);
        IQueryable<Header> GetHeadersUsingField(Field field, RecordsCenter recordsCenter);
        IList<Header> FindHeaders(List<string> searchTerms, RecordsCenter recordsCenter);
        IList<RequestFormProjection> GetFormsUsingHeader(Header header);
        IList<OptionListProjection> GetOptionListProjectionsForRecordsCenter(RecordsCenter recordsCenter);
        IQueryable<OptionList> GetOptionListsForRecordsCenter(RecordsCenter recordsCenter);
        IList<FormFieldProjection> GetFormFieldProjectionsUsingOptionList(OptionList list);
        IEnumerable<ListProjection> GetListProjectionsForRecordsCenter(RecordsCenter recordsCenter);
        OptionList FindOptionListByName(string listName, RecordsCenter recordsCenter);
        OptionList GetStatelessOptionListById(int id, RecordsCenter recordsCenter);
        IList<OptionList> FindOptionLists(List<string> searchTerms, RecordsCenter recordsCenter);
        RecordsCenter FindByName(string name);
        IEnumerable<RequestFormDetailProjection> GetRecordsCenterAcceptanceStatus(RecordsCenter recordsCenter);
        ApplicationFormProjection GetFormApplicationAssociations(RecordsCenter recordsCenter, string formId);
        ApplicationFormProjection UpdateFormApplicationAssociations(ApplicationFormProjection applicationFormProjection);
        TransactionSnippet UpdateTransactionSnippetField(int parentSnippetId, TransactionSnippetField transactionSnippetField);
        TransactionSnippet GetTransactionSnippet(string recordsCenter, string tokenName);
    }
}
