﻿using StateInterface.Designer.Model;
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
        
        public IEnumerable<RecordsCenter> GetRecordsCenters(TaskParameter taskParameter)
        {
            var currentUser = validateUserContext(taskParameter);
            return _repository.GetAll<RecordsCenter>().OrderBy(x => x.Name);
        }
       
        public RecordsCenter GetRecordsCenterByName(TaskParameter<RecordsCenterName> taskParameter)
        {
            var currentUser = validateUserContext(taskParameter);
            validateModel<RecordsCenterName>(taskParameter);
            return _repository.GetRecordsCenterByName(taskParameter.Parameters.Name);
            
        }

        public IEnumerable<Category> GetCategories(TaskParameter taskParameter)
        {
            var currentUser = validateUserContext(taskParameter);
            return _repository.GetAll<Category>();
        }

        public IEnumerable<RequestForm> GetForms(TaskParameter<RecordsCenterId> taskParameter)
        {
            var currentUser = validateUserContext(taskParameter);
            validateModel<RecordsCenterId>(taskParameter);
            return _repository.GetAll<RequestForm>().Where(x => x.RecordsCenter.Id == taskParameter.Parameters.Id).OrderBy(x => x.FormId);
        }
        
        public IEnumerable<RequestForm> GetForms(TaskParameter<FormsCategoryByRecordsCenterName> taskParameter)
        {
            var currentUser = validateUserContext(taskParameter);
            validateModel<FormsCategoryByRecordsCenterName>(taskParameter);
            return _repository.GetAll<RequestForm>().Where(x => x.RecordsCenter.Name.Equals(taskParameter.Parameters.Name) && x.Categories.Any(y => y.Id == taskParameter.Parameters.CategoryId)).OrderBy(x => x.FormId);
        }

        public TestCase UpdateTestCase(TaskParameter<CriteriaTestCasePassFail> taskParameter)
        {
            var currentUser = validateUserContext(taskParameter);
            validateModel<CriteriaTestCasePassFail>(taskParameter);
            var criteria = _repository.GetById<Criteria>(taskParameter.Parameters.Id);
            var qaActionTypes = _repository.GetAll<QAActionType>();

            var qaAction = new QAAction()
            {
                Criteria = criteria,
                HasPassed = taskParameter.Parameters.HasPassed,
                Note = taskParameter.Parameters.Note,
                OccurredAt = taskParameter.Parameters.Occurred,
                TestCaseId = taskParameter.Parameters.TestCaseId,
                ByUser = taskParameter.CurrentUser,
            };

            var qaState = criteria.GetTestCaseQAState(taskParameter.Parameters.TestCaseId);

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

            return criteria.Transaction.RequestForm.TestCases.LastOrDefault(x => x.TestCaseId.Equals(taskParameter.Parameters.TestCaseId));
        }

        public TestCase ResetTestCase(TaskParameter<CriteriaTestCase> taskParameter)
        {
            var currentUser = validateUserContext(taskParameter);
            validateModel<CriteriaTestCase>(taskParameter);
            var criteria = _repository.GetById<Criteria>(taskParameter.Parameters.Id);
            var qaActionTypes = _repository.GetAll<QAActionType>();

            var qaAction = new QAAction()
            {
                Criteria = criteria,
                Note = taskParameter.Parameters.Note,
                OccurredAt = taskParameter.Parameters.Occurred,
                ByUser = taskParameter.CurrentUser,
                HasPassed = null,
                TestCaseId = taskParameter.Parameters.TestCaseId,
                QAActionType = qaActionTypes.FirstOrDefault(x => x.ActionName.Equals(QAActionType.Reset))
            };

            criteria.QAActions.Add(qaAction);
            _repository.Save(criteria);

            criteria.Transaction.RequestForm.GenerateTestCases();
            return criteria.Transaction.RequestForm.TestCases.LastOrDefault(x => x.TestCaseId.Equals(taskParameter.Parameters.TestCaseId));
        }

        public RequestForm GetForm(TaskParameter<FormById> taskParameter)
        {
            var currentUser = validateUserContext(taskParameter);
            validateModel<FormById>(taskParameter);
            return _repository.GetForm(taskParameter.Parameters.FormId, _repository.GetById<RecordsCenter>(taskParameter.Parameters.Id));
        }

        public IEnumerable<Field> GetFieldCatalogItems(TaskParameter<RecordsCenterName> taskParameter)
        {
            var currentUser = validateUserContext(taskParameter);
            validateModel<RecordsCenterName>(taskParameter);
            return _repository.GetAll<Field>()
                .Where(x => x.RecordsCenter.Name == taskParameter.Parameters.Name)
                .OrderBy(x => x.TagName);
        }

        public Field GetField(TaskParameter<FieldByTag> taskParameter)
        {
            var currentUser = validateUserContext(taskParameter);
            validateModel<FieldByTag>(taskParameter);
            return _repository.GetAll<Field>()
                .SingleOrDefault(x => x.RecordsCenter.Name == taskParameter.Parameters.Name && x.TagName == taskParameter.Parameters.TagName);
        }

        public IEnumerable<RequestFormProjection> GetFormProjectionsUsingField(TaskParameter<Field> taskParameter)
        {
            var currentUser = validateUserContext(taskParameter);
            validateModel<Field>(taskParameter);
            return _repository.GetFormProjectionsUsingField(taskParameter.Parameters);
        }

        public OptionList GetList(TaskParameter<ListByName> taskParameter)
        {
            var currentUser = validateUserContext(taskParameter);
            validateModel<ListByName>(taskParameter);
            return _repository.GetList(taskParameter.Parameters.ListName, _repository.GetById<RecordsCenter>(taskParameter.Parameters.Id));
        }

        public IEnumerable<FormFieldProjection> GetFormFieldProjectionsUsingOptionList(TaskParameter<OptionList> taskParameters)
        {
            var currentUser = validateUserContext(taskParameters);
            return _repository.GetFormFieldProjectionsUsingOptionList(taskParameters.Parameters);
        }

        public IEnumerable<RequestFormDetailProjection> GetRecordsCenterAcceptanceStatus(TaskParameter<RecordsCenterId> taskParameter)
        {
            var currentUser = validateUserContext(taskParameter);
            return _repository.GetRecordsCenterAcceptanceStatus(_repository.GetById<RecordsCenter>(taskParameter.Parameters.Id));
        }

        public IEnumerable<RequestFormProjection> GetFormProjections(TaskParameter<RecordsCenterId> taskParameter)
        {
            var currentUser = validateUserContext(taskParameter);
            return _repository.GetFormProjectionsForRecordsCenter(GetRecordsCenters(taskParameter).FirstOrDefault(x => x.Id == taskParameter.Parameters.Id));
        }
        
        public IEnumerable<Application> GetApplications(TaskParameter taskParameter)
        {
            var currentUser = validateUserContext(taskParameter);
            return _repository.GetAll<Application>();
        }
        
        public RequestForm UpdateRequestForm(TaskParameter<RequestForm> taskParameter)
        {
            var currentUser = validateUserContext(taskParameter);
            _repository.Save(taskParameter.Parameters);
            return _repository.GetById<RequestForm>(taskParameter.Parameters.Id);

        }

        public StatisticsRecordsCenter GetStatisticsForRecordsCenter(TaskParameter<RecordsCenterName> taskParameter)
        {
            var currentUser = validateUserContext(taskParameter);
            RecordsCenter recordsCenter = GetRecordsCenterByName(taskParameter);
            StatisticsRecordsCenter statisticsRecordsCenter = new StatisticsRecordsCenter(recordsCenter);

            var applications = GetApplications(taskParameter).OrderBy(x => x.Name);
            var forms = GetForms(new TaskParameter<RecordsCenterId>(taskParameter.CurrentUser, new RecordsCenterId(recordsCenter.Id))).OrderBy(x => x.FormId);

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

        public IEnumerable<TestCase> GetOpenIssues(TaskParameter<RecordsCenterName> taskParameter)
        {
            var currentUser = validateUserContext(taskParameter);
            List<TestCase> failedTestCases = new List<TestCase>();
            var recordsCenter = GetRecordsCenterByName(taskParameter);

            //TODO: Determine how to use GetFailedTestCases(application) method
            failedTestCases.AddRange(recordsCenter.GetFailedTestCases());

            return failedTestCases;
        }

        public IEnumerable<ListProjection> GetListProjections(TaskParameter<RecordsCenterId> taskParameter)
        {
            var currentUser = validateUserContext(taskParameter);
            return _repository.GetListProjectionsForRecordsCenter(GetRecordsCenters(taskParameter).FirstOrDefault(x => x.Id == taskParameter.Parameters.Id));
        }

        public IEnumerable<TransactionSnippet> GetTransactionSnippets(TaskParameter<RecordsCenterId> taskParameter)
        {
            var currentUser = validateUserContext(taskParameter);
            return _repository.GetAll<TransactionSnippet>().Where(x => x.RecordsCenter.Id == taskParameter.Parameters.Id).OrderBy(x => x.TokenName);
        }

        public TransactionSnippet GetTransactionSnippet(TaskParameter<SnippetFieldByToken> taskParameter)
        {
            var currentUser = validateUserContext(taskParameter);
            return _repository.GetAll<TransactionSnippet>().FirstOrDefault(x => x.RecordsCenter.Id == taskParameter.Parameters.Id && x.TokenName == taskParameter.Parameters.TokenName);
        }
        
        public TransactionSnippet UpdateTransactionSnippet(TaskParameter<TransactionSnippet> taskParameter)
        {
            var currentUser = validateUserContext(taskParameter);
            taskParameter.Parameters.Updated = DateTime.UtcNow;
            _repository.Save<TransactionSnippet>(taskParameter.Parameters);
            return taskParameter.Parameters;
        }

        public TransactionSnippet UpdateTransactionSnippetField(TaskParameter<SnippetFieldDetail> taskParameter)
        {
            var currentUser = validateUserContext(taskParameter);
            var snippet = _repository.GetById<TransactionSnippet>(taskParameter.Parameters.Id);
            if (snippet != null)
            {
                taskParameter.Parameters.SnippetField.IsValid();
                if (taskParameter.Parameters.SnippetField.Id == 0 && snippet.TransactionSnippetFields.Any(x => string.Compare(x.TagName, taskParameter.Parameters.SnippetField.TagName, StringComparison.InvariantCultureIgnoreCase) == 0))
                {
                    throw new ArgumentException("Key already exists"); //Key already exists
                }
                return _repository.UpdateTransactionSnippetField(snippet.Id, taskParameter.Parameters.SnippetField);
            }
            throw new KeyNotFoundException();
        }

        public TransactionSnippet DeleteTransactionSnippet(TaskParameter<Snippet> taskParameter)
        {
            var currentUser = validateUserContext(taskParameter);
            var snippet = GetTransactionSnippet(taskParameter);
            _repository.Remove<TransactionSnippet>(snippet);
            return snippet;
        }
        
        public TransactionSnippet GetTransactionSnippet(TaskParameter<Snippet> taskParameter)
        {
            var currentUser = validateUserContext(taskParameter);
            return _repository.GetById<TransactionSnippet>(taskParameter.Parameters.Id);
        }

        public TransactionSnippet DeleteTransactionSnippetField(TaskParameter<SnippetField> taskParameter)
        {
            var currentUser = validateUserContext(taskParameter);
            var transactionSnippetField = _repository.GetById<TransactionSnippetField>(taskParameter.Parameters.FieldId);
            _repository.Remove(transactionSnippetField);
            var snippet = _repository.GetById<TransactionSnippet>(taskParameter.Parameters.Id);
            snippet.Updated = DateTime.UtcNow;
            _repository.Save(snippet);
            return snippet;
        }

        public void SetRecordsCenterForUser(TaskParameter<RecordsCenterName> taskParameter)
        {
            var currentUser = validateUserContext(taskParameter);
            var recordsCenter = _repository.GetRecordsCenterByName(taskParameter.Parameters.Name);
            currentUser.CurrentRecordsCenter = recordsCenter;
            _repository.Save(currentUser);
        }
        
        public User GetUser(TaskParameter taskParameter)
        {
            var user = _repository.GetAll<User>().FirstOrDefault(x => x.LoginName == taskParameter.CurrentUser);
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

        private User validateUserContext(TaskParameter taskParameter)
        {
            if (!string.IsNullOrWhiteSpace(taskParameter.CurrentUser))
            {
                var user = _repository.GetUser(taskParameter.CurrentUser);
                if(user != null)
                {
                    return user;
                }
                throw new SecurityAccessDeniedException();
            }
            throw new SecurityAccessDeniedException();
        }

        private void validateModel<T>(TaskParameter<T> taskParameter) where T : class
        {
            IValidate canTestModel = taskParameter as IValidate;
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
    }
}
