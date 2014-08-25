using StateInterface.Designer;
using StateInterface.Designer.Model;
using StateInterface.Designer.Model.Projections;
using StateInterface.Designer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Designer.Tasks
{
    //TODO: Consider where-when-how to apply the Nacho-Cheese exception
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
        public User GetUser(string currentUser)
        {
            //var user = _repository.GetAll<User>().FirstOrDefault(x => x.LoginName == userName);
            var user = validateUserContext(currentUser);
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
        public IEnumerable<RecordsCenter> GetRecordsCenters(string currentUser)
        {
            var user = validateUserContext(currentUser);
            return _repository.GetAll<RecordsCenter>().OrderBy(x => x.Name);
        }
        public RecordsCenter GetRecordsCenterByName(string currentUser, string recordsCenterName)
        {
            var user = validateUserContext(currentUser);
            return _repository.GetRecordsCenterByName(recordsCenterName);
        }
        public IEnumerable<Category> GetCategories(string currentUser)
        {
            var user = validateUserContext(currentUser);
            return _repository.GetAll<Category>();
        }
        public IEnumerable<RequestForm> GetForms(string currentUser, int recordsCenterId)
        {
            var user = validateUserContext(currentUser);
            return _repository.GetAll<RequestForm>().Where(x => x.RecordsCenter.Id == recordsCenterId).OrderBy(x => x.FormId);
        }
        public IEnumerable<RequestForm> GetForms(string currentUser, int recordsCenterId, int categoryId)
        {
            var user = validateUserContext(currentUser);
            return _repository.GetAll<RequestForm>().Where(x => x.RecordsCenter.Id == recordsCenterId && x.Categories.Any(y => y.Id == categoryId)).OrderBy(x => x.FormId);
        }
        public IEnumerable<RequestForm> GetForms(string currentUser, string recordsCenterName, int categoryId)
        {
            var user = validateUserContext(currentUser);
            return _repository.GetAll<RequestForm>().Where(x => x.RecordsCenter.Name.Equals(recordsCenterName) && x.Categories.Any(y => y.Id == categoryId)).OrderBy(x => x.FormId);
        }
        public TestCase UpdateTestCase(string currentUser, int criteriaId, string testCaseId, DateTime occurred, string note, bool hasPassed)
        {
            var user = validateUserContext(currentUser);
            var criteria = _repository.GetById<Criteria>(criteriaId);
            var qaActionTypes = _repository.GetAll<QAActionType>();

            var qaAction = new QAAction()
            {
                Criteria = criteria,
                HasPassed = hasPassed,
                Note = note,
                OccurredAt = occurred,
                TestCaseId = testCaseId,
                ByUser = user.LoginName,
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
        public TestCase ResetTestCase(string currentUser, int criteriaId, string testCaseId, DateTime occurred, string note)
        {
            var user = validateUserContext(currentUser);
            var criteria = _repository.GetById<Criteria>(criteriaId);
            var qaActionTypes = _repository.GetAll<QAActionType>();

            var qaAction = new QAAction()
            {
                Criteria = criteria,
                Note = note,
                OccurredAt = occurred,
                ByUser = user.LoginName,
                HasPassed = null,
                TestCaseId = testCaseId,
                QAActionType = qaActionTypes.FirstOrDefault(x => x.ActionName.Equals(QAActionType.Reset))
            };

            criteria.QAActions.Add(qaAction);
            _repository.Save(criteria);

            criteria.Transaction.RequestForm.GenerateTestCases();
            return criteria.Transaction.RequestForm.TestCases.LastOrDefault(x => x.TestCaseId.Equals(testCaseId));
        }
        public RequestForm GetForm(string currentUser, int recordsCenterId, string formId)
        {
            var user = validateUserContext(currentUser);
            return _repository.GetForm(formId, _repository.GetById<RecordsCenter>(recordsCenterId));
        }
        public RequestForm GetForm(string currentUser, string recordsCenterName, string formId)
        {
            var user = validateUserContext(currentUser);

            return _repository.GetAll<RequestForm>()
                .FirstOrDefault(x=> x.RecordsCenter.Name.Equals(recordsCenterName) && x.FormId.Equals(formId));
        }
        public IEnumerable<Field> GetFieldCatalogItems(string currentUser, string recordsCenterName)
        {
            var user = validateUserContext(currentUser);
            return _repository.GetAll<Field>()
                .Where(x => x.RecordsCenter.Name == recordsCenterName)
                .OrderBy(x => x.TagName);
        }
        public Field GetField(string currentUser, string recordsCenterName, string tagName)
        {
            var user = validateUserContext(currentUser);
            return _repository.GetAll<Field>()
                .SingleOrDefault(x => x.RecordsCenter.Name == recordsCenterName && x.TagName == tagName);
        }
        public IEnumerable<RequestFormProjection> GetFormProjectionsUsingField(string currentUser, Field field)
        {
            var user = validateUserContext(currentUser);
            return _repository.GetFormProjectionsUsingField(field);
        }
        public OptionList GetList(string currentUser, int recordsCenterId, string listName)
        {
            var user = validateUserContext(currentUser);
            return _repository.GetList(listName, _repository.GetById<RecordsCenter>(recordsCenterId));
        }
        public IEnumerable<FormFieldProjection> GetFormFieldProjectionsUsingOptionList(string currentUser, OptionList list)
        {
            var user = validateUserContext(currentUser);
            return _repository.GetFormFieldProjectionsUsingOptionList(list);
        }
        public IEnumerable<RequestFormDetailProjection> GetRecordsCenterAcceptanceStatus(string currentUser, int recordsCenterId)
        {
            var user = validateUserContext(currentUser);
            return _repository.GetRecordsCenterAcceptanceStatus(_repository.GetById<RecordsCenter>(recordsCenterId));
        }
        public IEnumerable<RequestFormProjection> GetFormProjections(string currentUser, int recordsCenterId)
        {
            var user = validateUserContext(currentUser);
            return _repository.GetFormProjectionsForRecordsCenter(GetRecordsCenters(currentUser).FirstOrDefault(x => x.Id == recordsCenterId));
        }
        public IEnumerable<Application> GetApplications(string currentUser)
        {
            var user = validateUserContext(currentUser);
            return _repository.GetAll<Application>();
        }
        public RequestForm UpdateRequestFormApplications(string currentUser, string recordsCenterName, string formId, IEnumerable<int> selectedApplicationIds)
        {
            var user = validateUserContext(currentUser);
            var requestForm = GetForm(currentUser, recordsCenterName, formId);
            var applications = GetApplications(currentUser);

            requestForm.Applications.Clear();
            foreach (var selectedApplicationId in selectedApplicationIds)
            {
                var application = applications.FirstOrDefault(x => x.Id == selectedApplicationId);
                requestForm.Applications.Add(application);
            }

            _repository.Save(requestForm);

            return requestForm;
        }
        public RequestForm UpdateRequestFormCategrories(string currentUser, string recordsCenterName, string formId, IEnumerable<int> selectedCategoryIds)
        {
            var user = validateUserContext(currentUser);
            var requestForm = GetForm(currentUser, recordsCenterName, formId);
            var categories = GetCategories(currentUser);

            requestForm.Categories.Clear();
            foreach (var selectedCategoryId in selectedCategoryIds)
            {
                var application = categories.FirstOrDefault(x => x.Id == selectedCategoryId);
                requestForm.Categories.Add(application);
            }

            _repository.Save(requestForm);

            return requestForm;
        }
        public StatisticsRecordsCenter GetStatisticsForRecordsCenter(string currentUser, string recordsCenterName)
        {
            var user = validateUserContext(currentUser);
            RecordsCenter recordsCenter = GetRecordsCenterByName(currentUser, recordsCenterName);
            StatisticsRecordsCenter statisticsRecordsCenter = new StatisticsRecordsCenter(recordsCenter);

            var applications = GetApplications(currentUser).OrderBy(x => x.Name);
            var forms = GetForms(currentUser, recordsCenter.Id).OrderBy(x => x.FormId);

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
        public IEnumerable<TestCase> GetOpenIssues(string currentUser, string recordsCenterName)
        {
            var user = validateUserContext(currentUser);
            List<TestCase> failedTestCases = new List<TestCase>();
            var recordsCenter = GetRecordsCenterByName(currentUser, recordsCenterName);

            //TODO: Determine how to use GetFailedTestCases(application) method
            failedTestCases.AddRange(recordsCenter.GetFailedTestCases());

            return failedTestCases;
        }
        public IEnumerable<ListProjection> GetListProjections(string currentUser, int recordsCenterId)
        {
            var user = validateUserContext(currentUser);
            return _repository.GetListProjectionsForRecordsCenter(GetRecordsCenters(currentUser).FirstOrDefault(x => x.Id == recordsCenterId));
        }
        public IEnumerable<TransactionSnippet> GetTransactionSnippets(string currentUser, int recordsCenterId)
        {
            var user = validateUserContext(currentUser);
            return _repository.GetAll<TransactionSnippet>().Where(x => x.RecordsCenter.Id == recordsCenterId).OrderBy(x => x.TokenName);
        }
        public TransactionSnippet GetTransactionSnippet(string currentUser, int recordsCenterId, string tokenName)
        {
            var user = validateUserContext(currentUser);
            return _repository.GetAll<TransactionSnippet>().FirstOrDefault(x => x.RecordsCenter.Id == recordsCenterId && x.TokenName == tokenName);
        }
        public TransactionSnippet UpdateTransactionSnippet(string currentUser, TransactionSnippet transactionsnippet)
        {
            var user = validateUserContext(currentUser);
            transactionsnippet.Updated = DateTime.UtcNow;
            _repository.Save<TransactionSnippet>(transactionsnippet);
            return transactionsnippet;
        }
        public TransactionSnippet UpdateTransactionSnippetField(string currentUser, int parentSnippetId, TransactionSnippetField transactionSnippetField)
        {
            var user = validateUserContext(currentUser);
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
        public TransactionSnippet DeleteTransactionSnippet(string currentUser, int snippetId)
        {
            var user = validateUserContext(currentUser);
            var snippet = GetTransactionSnippet(currentUser, snippetId);
            _repository.Remove<TransactionSnippet>(snippet);
            return snippet;
        }
        public TransactionSnippet DeleteTransactionSnippetField(string currentUser, int parentSnippetId, TransactionSnippetField transactionSnippetField)
        {
            var user = validateUserContext(currentUser);
            return DeleteTransactionSnippetField(currentUser, parentSnippetId, transactionSnippetField.Id);
        }
        public TransactionSnippet GetTransactionSnippet(string currentUser, int snippetId)
        {
            var user = validateUserContext(currentUser);
            return _repository.GetById<TransactionSnippet>(snippetId);
        }
        public TransactionSnippet DeleteTransactionSnippetField(string currentUser, int parentSnippetId, int transactionSnippetFieldId)
        {
            var user = validateUserContext(currentUser);
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
        private User validateUserContext(string currentUser)
        {
            if (!string.IsNullOrWhiteSpace(currentUser))
            {
                var user = _repository.GetUser(currentUser);
                if (user != null)
                {
                    return user;
                }
                throw new SecurityAccessDeniedException();
            }
            throw new SecurityAccessDeniedException();
        }
    }
}
