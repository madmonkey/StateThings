using StateInterface.Designer.Model;
using StateInterface.Designer.Model.Projections;
using StateInterface.Designer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StateInterface.Designer;

namespace Designer.Tasks
{
    public class DesignerTasks : IDesignerTasks
    {
        private readonly IDesignerRepository _repository;
        public DesignerTasks(IDesignerRepository repository)
        {
            _repository = repository;
        }
        public IEnumerable<Role> GetRoles()
        {
            return _repository.GetAll<Role>();
        }
        public User GetUser(string userName)
        {
            var user = _repository.GetAll<User>().FirstOrDefault(x => x.LoginName == userName);

            //TODO: Consider refactoring how we associate records center to a user for the first time.
            if (user != null)
            {
                if (user.CurrentRecordsCenter == null)
                {
                    user.CurrentRecordsCenter = _repository.GetAll<RecordsCenter>().OrderBy(x => x.Name).First();
                    _repository.Save(user);
                }
            }
            return user;
        }
        public IEnumerable<RecordsCenter> GetRecordsCenters(TaskParameter taskParameter)
        {
            if (!string.IsNullOrWhiteSpace(taskParameter.CurrentUser))
            {
                var user = _repository.GetUser(taskParameter.CurrentUser);
                if(user != null)
                {
                    return _repository.GetAll<RecordsCenter>().OrderBy(x => x.Name);
                }
                throw new SecurityAccessDeniedException();
            }
            throw new SecurityAccessDeniedException();
        }
        public RecordsCenter GetRecordsCenterById(int id)
        {
            return _repository.GetById<RecordsCenter>(id);
        }
        public RecordsCenter GetRecordsCenterByName(string recordsCenterName)
        {
            return _repository.GetRecordsCenterByName(recordsCenterName);
        }
        public IEnumerable<Category> GetCategories()
        {
            return _repository.GetAll<Category>();
        }
        public IEnumerable<RequestForm> GetForms(int recordsCenterId)
        {
            return _repository.GetAll<RequestForm>().Where(x => x.RecordsCenter.Id == recordsCenterId).OrderBy(x => x.FormId);
        }
        public IEnumerable<RequestForm> GetForms(int recordsCenterId, int categoryId)
        {
            return _repository.GetAll<RequestForm>().Where(x => x.RecordsCenter.Id == recordsCenterId && x.Categories.Any(y => y.Id == categoryId)).OrderBy(x => x.FormId);
        }
        public IEnumerable<RequestForm> GetForms(string recordsCenterName, int categoryId)
        {
            return _repository.GetAll<RequestForm>().Where(x => x.RecordsCenter.Name.Equals(recordsCenterName) && x.Categories.Any(y => y.Id == categoryId)).OrderBy(x => x.FormId);
        }
        public TestCase UpdateTestCase(int criteriaId, string testCaseId, DateTime occurred, string note, string user, bool hasPassed)
        {
            var criteria = _repository.GetById<Criteria>(criteriaId);
            var qaActionTypes = _repository.GetAll<QAActionType>();

            var qaAction = new QAAction()
            {
                Criteria = criteria,
                HasPassed = hasPassed,
                Note = note,
                OccurredAt = occurred,
                TestCaseId = testCaseId,
                ByUser = user,
            };

            var qaState = criteria.GetTestCaseQAState(testCaseId);

            if (qaState.QAStage == QAStage.UnitTest)
            {
                qaAction.QAActionType = qaActionTypes.FirstOrDefault(x => x.ActionName.Equals(QAActionType.UnitTest));
            }
            else if (qaState.QAStage == QAStage.Verify)
            {
                qaAction.QAActionType = qaActionTypes.FirstOrDefault(x => x.ActionName.Equals(QAActionType.Verify));
            }
            else if (qaState.QAStage == QAStage.Certify)
            {
                qaAction.QAActionType = qaActionTypes.FirstOrDefault(x => x.ActionName.Equals(QAActionType.Certify));
            }

            criteria.QAActions.Add(qaAction);
            _repository.Save(criteria);

            criteria.Transaction.RequestForm.GenerateTestCases();

            return criteria.Transaction.RequestForm.TestCases.LastOrDefault(x => x.TestCaseId.Equals(testCaseId));
        }
        public TestCase ResetTestCase(int criteriaId, string testCaseId, DateTime occurred, string note, string user)
        {
            var criteria = _repository.GetById<Criteria>(criteriaId);
            var qaActionTypes = _repository.GetAll<QAActionType>();

            var qaAction = new QAAction()
        {
            Criteria = criteria,
            Note = note,
            OccurredAt = occurred,
            ByUser = user,
            HasPassed = null,
            TestCaseId = testCaseId,
            QAActionType = qaActionTypes.FirstOrDefault(x => x.ActionName.Equals(QAActionType.Reset))
        };

            criteria.QAActions.Add(qaAction);
            _repository.Save(criteria);

            criteria.Transaction.RequestForm.GenerateTestCases();
            return criteria.Transaction.RequestForm.TestCases.LastOrDefault(x => x.TestCaseId.Equals(testCaseId));
        }
        public RequestForm GetForm(int recordsCenterId, string formId)
        {
            return _repository.GetForm(formId, _repository.GetById<RecordsCenter>(recordsCenterId));
        }
        public IEnumerable<Field> GetFieldCatalogItems(string recordsCenterName)
        {
            return _repository.GetAll<Field>()
                .Where(x => x.RecordsCenter.Name == recordsCenterName)
                .OrderBy(x => x.TagName);
        }
        public Field GetField(string recordsCenterName, string tagName)
        {
            return _repository.GetAll<Field>()
                .SingleOrDefault(x => x.RecordsCenter.Name == recordsCenterName && x.TagName == tagName);
        }
        public IEnumerable<RequestFormProjection> GetFormProjectionsUsingField(Field field)
        {
            return _repository.GetFormProjectionsUsingField(field);
        }
        public OptionList GetList(int recordsCenterId, string listName)
        {
            return _repository.GetList(listName, _repository.GetById<RecordsCenter>(recordsCenterId));
        }
        public IEnumerable<FormFieldProjection> GetFormFieldProjectionsUsingOptionList(OptionList list)
        {
            return _repository.GetFormFieldProjectionsUsingOptionList(list);
        }
        public IEnumerable<RequestFormDetailProjection> GetRecordsCenterAcceptanceStatus(int recordsCenterId)
        {
            return _repository.GetRecordsCenterAcceptanceStatus(_repository.GetById<RecordsCenter>(recordsCenterId));
        }
        public IEnumerable<RequestFormProjection> GetFormProjections(TaskParameter<RecordsCenterId> taskParameter)
        {
            return _repository.GetFormProjectionsForRecordsCenter(GetRecordsCenters(taskParameter).FirstOrDefault(x => x.Id == taskParameter.Parameters.Id));
        }
        public IEnumerable<RequestForm> GetFormsByApplication(int recordsCenterId, int applicationId)
        {
            return _repository.GetAll<RequestForm>().Where(requestForm => requestForm.RecordsCenter.Id == recordsCenterId
                && requestForm.Applications.Any(y => y.Id == applicationId));
        }
        public IEnumerable<Application> GetApplications()
        {
            return _repository.GetAll<Application>();
        }
        public ApplicationFormProjection GetFormApplicationAssociations(int recordsCenterId, string formId)
        {
            return _repository.GetFormApplicationAssociations(_repository.GetById<RecordsCenter>(recordsCenterId), formId);
        }
        public ApplicationFormProjection UpdateFormApplicationAssociations(ApplicationFormProjection applicationFormProjection)
        {
            _repository.Save<ApplicationFormProjection>(applicationFormProjection);
            return _repository.GetById<ApplicationFormProjection>(applicationFormProjection.Id);
        }
        public RequestForm UpdateRequestForm(RequestForm requestForm)
        {
            _repository.Save(requestForm);
            return _repository.GetById<RequestForm>(requestForm.Id);

        }
        public StatisticsRecordsCenter GetStatisticsForRecordsCenter(string recordsCenterName)
        {
            RecordsCenter recordsCenter = GetRecordsCenterByName(recordsCenterName);
            StatisticsRecordsCenter statisticsRecordsCenter = new StatisticsRecordsCenter(recordsCenter);

            var applications = GetApplications().OrderBy(x => x.Name);
            var forms = GetForms(recordsCenter.Id).OrderBy(x => x.FormId);

            string unassociatedString = "Unassociated";
            string uncategorizedString = "Uncategorized";

            foreach (var form in forms)
            {
                if (form.Applications.Any())
                {
                    foreach (var application in form.Applications)
                    {
                        var recordsCenterApplication = statisticsRecordsCenter.Applications.FirstOrDefault(x => x.Name == application.Name);

                        if (recordsCenterApplication == null)
                        {
                            recordsCenterApplication = new StatisticsApplication(application.Name);
                            statisticsRecordsCenter.Applications.Add(recordsCenterApplication);
                        }

                        if (form.Categories.Any())
                        {
                            foreach (var category in form.Categories)
                            {
                                var recordsCenterApplicationCategory = recordsCenterApplication.Categories.FirstOrDefault(x => x.Name == category.Name);

                                if (recordsCenterApplicationCategory == null)
                                {
                                    recordsCenterApplicationCategory = new StatisticsCategory(category.Name);
                                    recordsCenterApplication.Categories.Add(recordsCenterApplicationCategory);
                                }
                                recordsCenterApplicationCategory.Forms.Add(form);
                            }
                        }
                        else
                        {
                            var recordsCenterApplicationCategory = recordsCenterApplication.Categories.FirstOrDefault(x => x.Name == uncategorizedString);
                            if (recordsCenterApplicationCategory == null)
                            {
                                recordsCenterApplicationCategory = new StatisticsCategory(uncategorizedString);
                                recordsCenterApplication.Categories.Add(recordsCenterApplicationCategory);
                            }
                            recordsCenterApplicationCategory.Forms.Add(form);
                        }
                    }
                }
                else
                {
                    var recordsCenterApplication = statisticsRecordsCenter.Applications.FirstOrDefault(x => x.Name == unassociatedString);

                    if (recordsCenterApplication == null)
                    {
                        recordsCenterApplication = new StatisticsApplication(unassociatedString);
                        statisticsRecordsCenter.Applications.Add(recordsCenterApplication);
                    }

                    if (form.Categories.Any())
                    {
                        foreach (var category in form.Categories)
                        {
                            var recordsCenterApplicationCategory = recordsCenterApplication.Categories.FirstOrDefault(x => x.Name == category.Name);

                            if (recordsCenterApplicationCategory == null)
                            {
                                recordsCenterApplicationCategory = new StatisticsCategory(category.Name);
                                recordsCenterApplication.Categories.Add(recordsCenterApplicationCategory);
                            }
                            recordsCenterApplicationCategory.Forms.Add(form);
                        }
                    }
                    else
                    {
                        var recordsCenterApplicationCategory = recordsCenterApplication.Categories.FirstOrDefault(x => x.Name == uncategorizedString);
                        if (recordsCenterApplicationCategory == null)
                        {
                            recordsCenterApplicationCategory = new StatisticsCategory(uncategorizedString);
                            recordsCenterApplication.Categories.Add(recordsCenterApplicationCategory);
                        }
                        recordsCenterApplicationCategory.Forms.Add(form);
                    }
                }
            }

            foreach (var application in statisticsRecordsCenter.Applications)
            {
                foreach (var category in application.Categories)
                {
                    category.CalculateQaStatistics(applications.FirstOrDefault(x => x.Name == application.Name));
                }
                application.CalculateQaStatistics();
            }

            statisticsRecordsCenter.CalculateQaStatistics();

            return statisticsRecordsCenter;
        }
        public IEnumerable<TestCase> GetOpenIssues(string recordsCenterName)
        {
            List<TestCase> failedTestCases = new List<TestCase>();
            var recordsCenter = GetRecordsCenterByName(recordsCenterName);

            //TODO: Determine how to use GetFailedTestCases(application) method
            failedTestCases.AddRange(recordsCenter.GetFailedTestCases());

            return failedTestCases;
        }

        public IEnumerable<ListProjection> GetListProjections(TaskParameter<RecordsCenterId> taskParameter)
        {
            return _repository.GetListProjectionsForRecordsCenter(GetRecordsCenters(taskParameter).FirstOrDefault(x => x.Id == taskParameter.Parameters.Id));
        }
        public IEnumerable<TransactionSnippet> GetTransactionSnippets(int recordsCenterId)
        {
            return _repository.GetAll<TransactionSnippet>().Where(x => x.RecordsCenter.Id == recordsCenterId).OrderBy(x => x.TokenName);
        }
        public TransactionSnippet GetTransactionSnippet(int recordsCenterId, string tokenName)
        {
            return _repository.GetAll<TransactionSnippet>().FirstOrDefault(x => x.RecordsCenter.Id == recordsCenterId && x.TokenName == tokenName);
        }
        public IEnumerable<TransactionSnippetField> GetTransactionSnippetFields(RecordsCenter recordCenter, string tokenName)
        {
            var transaction = _repository.GetAll<TransactionSnippet>().FirstOrDefault(x => x.TokenName == tokenName);
            if (transaction != null)
            {
                return transaction.TransactionSnippetFields.ToArray().OrderBy(x => x.TagName);
            }
            return null;
        }
        public TransactionSnippet CreateTransactionSnippet(RecordsCenter recordsCenter, string name, string description)
        {
            return UpdateTransactionSnippet(new TransactionSnippet() { RecordsCenter = recordsCenter, TokenName = name, Description = description });
        }
        public TransactionSnippet UpdateTransactionSnippet(TransactionSnippet transactionsnippet)
        {
            transactionsnippet.Updated = DateTime.UtcNow;
            _repository.Save<TransactionSnippet>(transactionsnippet);
            return transactionsnippet;
        }
        public TransactionSnippet CreateTransactionSnippetField(int parentSnippetId, string tagName, int length)
        {
            return UpdateTransactionSnippetField(parentSnippetId, new TransactionSnippetField() { TagName = tagName, Length = length });
        }
        public TransactionSnippet UpdateTransactionSnippetField(int parentSnippetId, TransactionSnippetField transactionSnippetField)
        {
            var snippet = _repository.GetById<TransactionSnippet>(parentSnippetId);
            if (snippet != null)
            {
                transactionSnippetField.IsValid();
                if (transactionSnippetField.Id == 0 && snippet.TransactionSnippetFields.Any(x => string.Compare(x.TagName, transactionSnippetField.TagName, StringComparison.InvariantCultureIgnoreCase) == 0))
                {
                    throw new ArgumentException("Key already exists"); //Key already exists
                }
                return _repository.UpdateTransactionSnippetField(parentSnippetId, transactionSnippetField);
            }
            throw new KeyNotFoundException();
        }

        public TransactionSnippet DeleteTransactionSnippet(int snippetId)
        {
            var snippet = GetTransactionSnippet(snippetId);
            _repository.Remove<TransactionSnippet>(snippet);
            return snippet;
        }
        public TransactionSnippet DeleteTransactionSnippetField(int parentSnippetId, TransactionSnippetField transactionSnippetField)
        {
            return DeleteTransactionSnippetField(parentSnippetId, transactionSnippetField.Id);
        }
        public TransactionSnippet GetTransactionSnippet(int snippetId)
        {
            return _repository.GetById<TransactionSnippet>(snippetId);
        }
        public TransactionSnippet DeleteTransactionSnippetField(int parentSnippetId, int transactionSnippetFieldId)
        {
            var transactionSnippetField = _repository.GetById<TransactionSnippetField>(transactionSnippetFieldId);
            _repository.Remove(transactionSnippetField);
            var snippet = _repository.GetById<TransactionSnippet>(parentSnippetId);
            snippet.Updated = DateTime.UtcNow;
            _repository.Save(snippet);
            return snippet;
        }

        public void SetRecordsCenterForUser(string userName, string recordsCenterName)
        {
            var user = _repository.GetUser(userName);
            var recordsCenter = _repository.GetRecordsCenterByName(recordsCenterName);
            user.CurrentRecordsCenter = recordsCenter;
            _repository.Save(user);
        }
    }
}
