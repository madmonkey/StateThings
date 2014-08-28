using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StateInterface.Designer.Model;
using NHibernate;
using StateInterface.Designer.Model.Projections;
using NHibernate.Criterion;
using NHibernate.Transform;
using NHibernate.Linq;
using NHibernate.SqlCommand;

namespace StateInterface.Designer.Repository
{
    public class DesignerRepository : IDesignerRepository
    {
        private ISession _session;

        public ISession Session
        {
            get
            {
                return _session;
            }
        }
        public DesignerRepository()
        {
            _session = SessionProvider.OpenSession();
        }
        public T GetById<T>(int id)
        {
            using (var transaction = _session.BeginTransaction())
            {
                T result = _session.Get<T>(id);
                transaction.Commit();
                return result;
            }
        }
        public IQueryable<T> GetAll<T>()
        {
            using (var transaction = _session.BeginTransaction())
            {
                IQueryable<T> result = _session.Query<T>();
                transaction.Commit();
                return result;
            }
        }
        public void Save<T>(T item)
        {
            try
            {
                using (var transaction = _session.BeginTransaction())
                {
                    _session.SaveOrUpdate(item);
                    transaction.Commit();
                }
            }
            catch(ADOException ex)
            {
                throw;
            }
            catch(Exception exp)
            {
                throw;
            }
        }
        public void Remove<T>(T item)
        {
            using (var transaction = _session.BeginTransaction())
            {
                _session.Delete(item);
                transaction.Commit();
            }
        }
        public RecordsCenter GetRecordsCenterByName(string recordsCenterName)
        {
            using (var transaction = Session.BeginTransaction())
            {
                RecordsCenter recordsCenter = Session.Query<RecordsCenter>()
                    .FirstOrDefault(x => x.Name == recordsCenterName);

                transaction.Commit();
                return evaluteEntity<RecordsCenter>(recordsCenter);
            }
        }
        public Field FindFieldByTag(string tagName, RecordsCenter recordsCenter)
        {
            using (var transaction = Session.BeginTransaction())
            {
                Field field = Session.Query<Field>().FirstOrDefault(x => x.TagName == tagName
                    && x.RecordsCenter.Id == recordsCenter.Id);
                transaction.Commit();
                return field;
            }
        }
        public IQueryable<Field> GetAllFields()
        {
            IQueryable<Field> fields = Session.Query<Field>().OrderBy(x => x.TagName);
            return evaluteEntity<IQueryable<Field>>(fields);
        }
        public IQueryable<Field> GetFieldsForRecordsCenter(RecordsCenter recordsCenter)
        {
            using (var transaction = Session.BeginTransaction())
            {
                IQueryable<Field> fields = Session
                    .Query<Field>()
                    .Where(x => x.RecordsCenter.Name == recordsCenter.Name);
                transaction.Commit();

                return evaluteEntity<IQueryable<Field>>(fields);
            }
        }
        public IEnumerable<FieldProjection> GetFieldProjectionsForRecordsCenter(RecordsCenter recordsCenter)
        {
            using (var transaction = Session.BeginTransaction())
            {
                IEnumerable<FieldProjection> fields = Session.CreateCriteria<Field>("field")
                    .CreateAlias("field.RecordsCenter", "recordsCenter")
                    .Add(Expression.Eq("recordsCenter.Id", recordsCenter.Id))
                    .SetProjection(Projections.ProjectionList()
                        .Add(Projections.Property("field.Id"), "Id")
                        .Add(Projections.Property("field.TagName"), "TagName")
                        .Add(Projections.Property("field.ToolTip"), "ToolTip"))
                    .SetResultTransformer(Transformers.AliasToBean(typeof(FieldProjection)))
                    .List<FieldProjection>();
                transaction.Commit();

                return evaluteEntity<IEnumerable<FieldProjection>>(fields);
            }
        }
        public IList<RequestFormProjection> GetFormProjectionsUsingField(Field field)
        {
            using (var transaction = Session.BeginTransaction())
            {
                var forms = Session
                    .CreateCriteria<RequestForm>("form")
                    .CreateAlias("FormFields", "formField")
                    .CreateAlias("formField.Field", "fieldId")
                    .Add(Expression.Eq("fieldId.Id", field.Id))
                    .SetProjection(Projections.ProjectionList()
                        .Add(Projections.Property("form.Id"), "Id")
                        .Add(Projections.Property("form.FormId"), "FormId")
                        .Add(Projections.Property("form.Title"), "Title"))
                        .SetResultTransformer(Transformers.AliasToBean(typeof(RequestFormProjection)))
                    .List<RequestFormProjection>();

                transaction.Commit();
                return evaluteEntity<IList<RequestFormProjection>>(forms);
            }
        }
        public Field GetField(int fieldId)
        {
            var textField = GetById<Field>(fieldId);
            return evaluteEntity<Field>(textField);
        }
        public User GetUser(string userName)
        {
            var user = GetAll<User>().FirstOrDefault(x=> x.LoginName.Equals(userName));
            return evaluteEntity<User>(user);
        }
        public IList<Field> FindFields(List<string> searchTerms, RecordsCenter recordsCenter)
        {
            IList<Field> result = null;
            foreach (var searchTermItem in searchTerms)
            {
                using (var transaction = Session.BeginTransaction())
                {
                    IList<Field> searchTermResult = Session
                        .Query<Field>()
                        .Where(x => (x.TagName.Contains(searchTermItem) || x.ToolTip.Contains(searchTermItem))
                            && x.RecordsCenter.Id == recordsCenter.Id)
                        .ToList();

                    if (searchTermResult != null)
                    {
                        if (result == null)
                        {
                            result = new List<Field>();
                        }

                        foreach (var searchTermResultItem in searchTermResult)
                        {
                            if (!result.Contains(searchTermResultItem))
                            {
                                result.Add(searchTermResultItem);
                            }
                        }
                        transaction.Commit();
                    }
                }
            }
            return result;
        }
        public RequestForm GetForm(string formId, RecordsCenter recordCenter)
        {
            using (var transaction = Session.BeginTransaction())
            {
                RequestForm requestForm = Session.Query<RequestForm>()
                    .FirstOrDefault(x => x.RecordsCenter.Id == recordCenter.Id && x.FormId == formId);
                transaction.Commit();
                return evaluteEntity<RequestForm>(requestForm);
            }
        }
        public RequestFormProjection GetFormProjectionForRecordsCenter(string formId, RecordsCenter recordsCenter)
        {
            using (Session.BeginTransaction())
            {
                var requestFormProjection = Session.Query<RequestFormProjection>().FirstOrDefault(x => x.RecordsCenter.Id == recordsCenter.Id && x.FormId == formId);
                return evaluteEntity<RequestFormProjection>(requestFormProjection);
            }
        }
        public OptionList GetList(string listName, RecordsCenter recordCenter)
        {
            using (var transaction = Session.BeginTransaction())
            {
                OptionList optionList = Session.Query<OptionList>()
                    .FirstOrDefault(x => x.RecordsCenter.Id == recordCenter.Id && x.ListName == listName);
                transaction.Commit();
                return evaluteEntity<OptionList>(optionList);
            }
        }
        public RequestForm GetStatelessFormById(int id, RecordsCenter recordsCenter)
        {
            using (var transaction = Session.BeginTransaction())
            {
                var form = Session.Query<RequestForm>()
                     .Where(x => x.Id == id
                     && x.RecordsCenter.Id == recordsCenter.Id).FirstOrDefault();

                transaction.Commit();
                return evaluteEntity<RequestForm>(form);
            }
        }
        public IEnumerable<RequestFormProjection> GetFormProjectionsForRecordsCenter(RecordsCenter recordsCenter)
        {
            using (Session.BeginTransaction())
            {
                var requestFormProjections = Session.Query<RequestFormProjection>().Where(x => x.RecordsCenter.Id == recordsCenter.Id).OrderBy(x=> x.FormId);
                return evaluteEntity<IEnumerable<RequestFormProjection>>(requestFormProjections);
            }
        }
        public IQueryable<RequestForm> GetFormsForRecordsCenter(RecordsCenter recordsCenter)
        {
            using (var transaction = Session.BeginTransaction())
            {
                IQueryable<RequestForm> requestForms = Session
                    .Query<RequestForm>()
                    .Where(x => x.RecordsCenter.Name == recordsCenter.Name);
                transaction.Commit();
                return evaluteEntity<IQueryable<RequestForm>>(requestForms);
            }
        }
        public IQueryable<string> FindFormIdsLike(string formIdPattern)
        {
            IQueryable<string> forms;
            using (var transaction = Session.BeginTransaction())
            {
                forms = Session.Query<RequestForm>()
                    .Where(x => x.FormId.StartsWith(formIdPattern))
                    .Select(x => x.FormId);

                transaction.Commit();
                return forms;
            }
        }
        public IQueryable<string> FindFormIdsLike(string formIdPattern, RecordsCenter recordsCenter)
        {
            if (recordsCenter == null)
            {
                throw new ArgumentNullException("recordsCenter");
            }

            IQueryable<string> formIds;
            IQueryable<RequestForm> forms;
            using (var transaction = Session.BeginTransaction())
            {
                forms = Session.Query<RequestForm>()
                    .Where(x => x.FormId.StartsWith(formIdPattern));

                formIds = forms.Where(x => x.RecordsCenter.Id == recordsCenter.Id)
                    .Select(x => x.FormId);

                transaction.Commit();
                return formIds;
            }
        }
        public IList<RequestFormProjection> FindRequestForms(List<string> searchTerms, RecordsCenter recordsCenter)
        {
            IList<RequestFormProjection> result = null;
            using (var transaction = Session.BeginTransaction())
            {
                foreach (var searchTermItem in searchTerms)
                {
                    IList<RequestFormProjection> searchTermResult = Session
                    .CreateCriteria<RequestForm>("form")
                    .Add(Expression.Eq("form.RecordsCenter.Id", recordsCenter.Id) && (Expression.Like("form.FormId", "%" + searchTermItem + "%") || Expression.Like("form.Title", "%" + searchTermItem + "%")))
                    .SetProjection(Projections.ProjectionList()
                        .Add(Projections.Property("form.Id"), "Id")
                        .Add(Projections.Property("form.FormId"), "FormId")
                        .Add(Projections.Property("form.Title"), "Title"))
                        .SetResultTransformer(Transformers.AliasToBean(typeof(RequestFormProjection)))
                    .List<RequestFormProjection>();

                    if (searchTermResult != null)
                    {
                        if (result == null)
                        {
                            result = new List<RequestFormProjection>();
                        }

                        foreach (var searchTermResultItem in searchTermResult)
                        {
                            if (!result.Any(x => x.Id == searchTermResultItem.Id))
                            {
                                result.Add(searchTermResultItem);
                            }
                        }
                    }
                }

                transaction.Commit();

                return result;
            }
        }
        public Header FindByHeaderName(string headerName, RecordsCenter recordCenter)
        {
            using (var transaction = Session.BeginTransaction())
            {
                Header header = Session.Query<Header>()
                    .FirstOrDefault(x => x.HeaderName == headerName
                    && x.RecordsCenter.Id == recordCenter.Id);
                transaction.Commit();
                return header;
            }
        }
        public IQueryable<Header> GetHeadersForRecordsCenter(RecordsCenter recordsCenter)
        {
            using (var transaction = Session.BeginTransaction())
            {
                IQueryable<Header> headers = Session
                    .Query<Header>()
                    .Where(x => x.RecordsCenter.Name == recordsCenter.Name);
                transaction.Commit();
                return evaluteEntity<IQueryable<Header>>(headers);
            }
        }
        public IQueryable<Header> GetHeadersUsingField(Field field, RecordsCenter recordsCenter)
        {
            using (var transaction = Session.BeginTransaction())
            {
                var headers = Session
                    .Query<Header>()
                    .Where(x => x.RecordsCenter.Id == recordsCenter.Id
                        && (x.HeaderNodes.Where(y => (y as HeaderFieldNode).Field.Id == field.Id).Any()));

                transaction.Commit();
                return evaluteEntity<IQueryable<Header>>(headers);
            }
        }
        public IList<Header> FindHeaders(List<string> searchTerms, RecordsCenter recordsCenter)
        {
            IList<Header> result = null;
            using (var transaction = Session.BeginTransaction())
            {
                foreach (var searchTermItem in searchTerms)
                {
                    IList<Header> searchTermResult = Session
                        .Query<Header>()
                        .Where(x => x.RecordsCenter.Id == recordsCenter.Id
                            && (x.HeaderName.Contains(searchTermItem) || x.Description.Contains(searchTermItem)))
                        .ToList();

                    if (searchTermResult != null)
                    {
                        if (result == null)
                        {
                            result = new List<Header>();
                        }

                        foreach (var searchTermResultItem in searchTermResult)
                        {
                            result.Add(searchTermResultItem);
                        }
                    }
                }

                transaction.Commit();

                return result;
            }
        }
        public IList<RequestFormProjection> GetFormsUsingHeader(Header header)
        {
            var forms = Session
                .CreateCriteria<RequestForm>("form")
                .CreateAlias("Transactions", "transaction")
                .CreateAlias("transaction.Header", "header")
                .Add(Expression.Eq("header.Id", header.Id))
                .SetProjection(Projections.Distinct(Projections.ProjectionList()
                    .Add(Projections.Property("form.Id"), "Id")
                    .Add(Projections.Property("form.FormId"), "FormId")
                    .Add(Projections.Property("form.Title"), "Title")))
                    .SetResultTransformer(Transformers.AliasToBean(typeof(RequestFormProjection)))
                .List<RequestFormProjection>();
            return evaluteEntity<IList<RequestFormProjection>>(forms);
        }
        public IList<OptionListProjection> GetOptionListProjectionsForRecordsCenter(RecordsCenter recordsCenter)
        {
            using (var transaction = Session.BeginTransaction())
            {
                IList<OptionListProjection> optionLists = Session
                    .CreateCriteria<OptionList>("optionList")
                    .CreateAlias("RecordsCenter", "recordsCenter")
                    .Add(Expression.Eq("recordsCenter.Id", recordsCenter.Id))
                    .SetProjection(Projections.ProjectionList()
                        .Add(Projections.Property("optionList.ListName"), "ListName")
                        .Add(Projections.Property("optionList.Id"), "Id"))
                        .SetResultTransformer(Transformers.AliasToBean(typeof(OptionListProjection)))
                    .List<OptionListProjection>();
                transaction.Commit();
                return evaluteEntity<IList<OptionListProjection>>(optionLists);
            }
        }
        public IQueryable<OptionList> GetOptionListsForRecordsCenter(RecordsCenter recordsCenter)
        {
            using (var transaction = Session.BeginTransaction())
            {
                IQueryable<OptionList> optionLists = Session
                    .Query<OptionList>()
                    .Where(x => x.RecordsCenter.Id == recordsCenter.Id);
                transaction.Commit();
                return evaluteEntity<IQueryable<OptionList>>(optionLists);
            }
        }
        public IEnumerable<ListProjection> GetListProjectionsForRecordsCenter(RecordsCenter recordsCenter)
        {
            using (var transaction = Session.BeginTransaction())
            {
                var listProjections = Session.Query<ListProjection>().Where(x => x.RecordsCenter.Id == recordsCenter.Id).OrderBy(x => x.ListName);
                transaction.Commit();
                return evaluteEntity<IEnumerable<ListProjection>>(listProjections);
            }
        }
        public IList<FormFieldProjection> GetFormFieldProjectionsUsingOptionList(OptionList list)
        {
            using (var transaction = Session.BeginTransaction())
            {
                var formFields = Session
                    .CreateCriteria<FormField>("formField")
                    .CreateAlias("OptionList", "optionList")
                    .CreateAlias("Requestform", "requestForm")
                    .CreateAlias("Field", "field")
                    .Add(Expression.Eq("optionList.Id", list.Id))
                    .SetProjection(Projections.ProjectionList()
                        .Add(Projections.Property("requestForm.FormId"), "RequestFormId")
                        .Add(Projections.Property("field.TagName"), "FieldTagName"))
                        .SetResultTransformer(Transformers.AliasToBean(typeof(FormFieldProjection)))
                    .List<FormFieldProjection>();

                transaction.Commit();
                return evaluteEntity<IList<FormFieldProjection>>(formFields);
            }
        }
        public OptionList FindOptionListByName(string listName, RecordsCenter recordsCenter)
        {
            if (recordsCenter == null)
            {
                throw new ArgumentNullException("recordsCenter");
            }

            using (var transaction = Session.BeginTransaction())
            {
                var list = Session.Query<OptionList>()
                     .Where(x => x.ListName == listName
                     && x.RecordsCenter.Id == recordsCenter.Id).FirstOrDefault();

                transaction.Commit();
                return list;
            }
        }
        public OptionList GetStatelessOptionListById(int id, RecordsCenter recordsCenter)
        {
            if (recordsCenter == null)
            {
                throw new ArgumentNullException("recordsCenter");
            }
            using (var transaction = Session.BeginTransaction())
            {
                var list = Session.Query<OptionList>()
                    .Where(x => x.Id == id
                        && x.RecordsCenter.Id == recordsCenter.Id)
                    .Fetch(x => x.OptionListItems)
                    .ToList()
                    .FirstOrDefault();

                transaction.Commit();
                return evaluteEntity<OptionList>(list);
            }
        }
        public IList<OptionList> FindOptionLists(List<string> searchTerms, RecordsCenter recordsCenter)
        {
            IList<OptionList> result = null;
            using (var transaction = Session.BeginTransaction())
            {
                foreach (var searchTermItem in searchTerms)
                {
                    IList<OptionList> searchTermResult = Session
                        .Query<OptionList>()
                        .Where(x => x.RecordsCenter.Id == recordsCenter.Id
                            && (x.ListName.Contains(searchTermItem) || x.OptionListTiers.Any(y => y.Name.Contains(searchTermItem))))
                        .ToList();

                    if (searchTermResult != null)
                    {
                        if (result == null)
                        {
                            result = new List<OptionList>();
                        }

                        foreach (var searchTermResultItem in searchTermResult)
                        {
                            result.Add(searchTermResultItem);
                        }
                    }
                }

                transaction.Commit();

                return result;
            }
        }
        public RecordsCenter FindByName(string name)
        {
            using (var transaction = Session.BeginTransaction())
            {
                RecordsCenter recordsCenter = Session.Query<RecordsCenter>().FirstOrDefault(x => x.Name == name);
                transaction.Commit();
                return evaluteEntity<RecordsCenter>(recordsCenter);
            }
        }
        public IEnumerable<RequestFormDetailProjection> GetRecordsCenterAcceptanceStatus(RecordsCenter recordsCenter)
        {
            using (var transaction = Session.BeginTransaction())
            {
                var projections = Session.Query<RequestFormDetailProjection>().Where(x => x.RecordsCenter.Id == recordsCenter.Id).ToList();
                transaction.Commit();
                return evaluteEntity<IEnumerable<RequestFormDetailProjection>>(projections);
            }
        }
        public ApplicationFormProjection GetFormApplicationAssociations(RecordsCenter recordsCenter, string formId)
        {
            using (var transaction = Session.BeginTransaction())
            {
                var applicationProjection = Session.Query<ApplicationFormProjection>().Where(x => x.RecordsCenter.Id == recordsCenter.Id && x.FormId == formId).FirstOrDefault();
                transaction.Commit();
                return evaluteEntity<ApplicationFormProjection>(applicationProjection);
            }
        }
        public ApplicationFormProjection UpdateFormApplicationAssociations(ApplicationFormProjection applicationFormProjection)
        {
            
            Save<ApplicationFormProjection>(applicationFormProjection);
            return GetFormApplicationAssociations(applicationFormProjection.RecordsCenter, applicationFormProjection.FormId);
        }
        public TransactionSnippet UpdateTransactionSnippetField(int parentSnippetId, TransactionSnippetField transactionSnippetField)
        {
            TransactionSnippet currentSnippet;
            validateEntity<TransactionSnippetField>(transactionSnippetField);
            using (var transaction = Session.BeginTransaction())
            {
                currentSnippet = _session.Query<TransactionSnippet>().FirstOrDefault(x => x.Id == parentSnippetId);
                var updateItem = currentSnippet.TransactionSnippetFields.FirstOrDefault(x => x.Id == transactionSnippetField.Id);
                if (updateItem != null)
                    {
                        updateItem.DefaultValue = transactionSnippetField.DefaultValue;
                        updateItem.FormatMask = transactionSnippetField.FormatMask;
                        updateItem.Frequency = transactionSnippetField.Frequency;
                        updateItem.Length = transactionSnippetField.Length;
                        updateItem.MakeUpperCase = transactionSnippetField.MakeUpperCase;
                        updateItem.AcceptCarriageReturns = transactionSnippetField.AcceptCarriageReturns;
                        updateItem.PadCharacterDec = transactionSnippetField.PadCharacterDec;
                        updateItem.Prefix = transactionSnippetField.Prefix;
                        updateItem.Separator = transactionSnippetField.Separator;
                        updateItem.Suffix = transactionSnippetField.Suffix;
                        updateItem.TagName = transactionSnippetField.TagName;
                        updateItem.ToolTip = transactionSnippetField.ToolTip;
                        updateItem.TransformFormat = transactionSnippetField.TransformFormat;
                        updateItem.TrimInputToLength = transactionSnippetField.TrimInputToLength;
                    }
                else
                {
                    currentSnippet.TransactionSnippetFields.Add(transactionSnippetField);
                }
                currentSnippet.Updated = DateTime.UtcNow; // regardless, update the parent modified
                _session.SaveOrUpdate(currentSnippet);
                transaction.Commit();
                return currentSnippet;
            }
        }

        private void validateEntity<T>(T entity)
        {
            IValidate canTestModel = entity as IValidate;
            if(canTestModel !=null)
            {
                try
                {
                    canTestModel.IsValid();
                }
                catch (Exception)
                {
                    throw new ValidationException();
                }
                
            }
        }
        private T evaluteEntity<T>(T entity)
        {
            if(entity !=null)
            {
                return entity;
            }
            throw new ObjectNotFoundException();
        }


        public TransactionSnippet GetTransactionSnippet(string recordsCenter, string tokenName)
        {
            using (var transaction = Session.BeginTransaction())
            {
                var snippet =_session.Query<TransactionSnippet>().FirstOrDefault(x => x.RecordsCenter.Name == recordsCenter && x.TokenName == tokenName);
                transaction.Commit();
                return snippet;
            }
            //if (transactionsnippet.Id == 0)
            //{
            //    if (_repository.GetAll<TransactionSnippet>()
            //        .Where(x => string.Compare(x.RecordsCenter.Name, transactionsnippet.RecordsCenter.Name, StringComparison.InvariantCultureIgnoreCase) == 0
            //            && string.Compare(x.TokenName, transactionsnippet.TokenName, StringComparison.InvariantCultureIgnoreCase) == 0)
            //        .FirstOrDefault() == null)
            //    {
            //        throw new DuplicateKeyException(string.Format("The TokenName value '{0}' already exists for this records center", transactionsnippet.TokenName));
            //    }
            //}
            //throw new NotImplementedException();
        }
    }
}
