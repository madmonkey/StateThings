
//using StateInterface.Designer.Model.Projections;
//using System.Collections.Generic;
//using System.Linq;

//namespace StateInterface.Designer.Model
//{
//    public class StatisticsRequestForm
//    {
//        public StatisticsDetails Statistics { get; set; }
//        public int Id { get; set; }
//        public string FormId { get; set; }
//        public string Title { get; set; }
//        public string Description { get; set; }
//        public RecordsCenter RecordsCenter { get; set; }
//        public List<TransactionProjection> Transactions { get; set; }
//        public List<RequestFormCategoryProjection> RequestFormCategories { get; set; }

//        public StatisticsRequestForm(RequestFormDetailProjection form)
//        {
//            Id = form.Id;
//            FormId = form.FormId;
//            Title = form.Title;
//            Description = form.Description;
//            RecordsCenter = form.RecordsCenter;
//            Transactions = new List<TransactionProjection>(form.Transactions);
//            RequestFormCategories = new List<RequestFormCategoryProjection>();
//            foreach (var item in form.RequestFormCategories)
//            {
//                RequestFormCategories.Add(item);
//            }
//            Statistics = GetQaStatisticsDetails();
//            Statistics.CalculateData();
//        }
//        private StatisticsDetails GetQaStatisticsDetails()
//        {
//            var statisticsDetails = new StatisticsDetails();


//            foreach (var transaction in Transactions)
//            {
//                foreach (var criteria in transaction.Criterion)
//                {
//                    var testCases = criteria.GetTestCases().ToList();

//                    statisticsDetails.TotalTestCases += testCases.Count();

//                    foreach (var testCase in testCases)
//                    {
//                        var result = criteria.QAActions.Where(x => x.Criteria.Id == criteria.Id
//                                && x.TestCaseId.Equals(testCase.TestCaseId)
//                                && x.HasPassed == true).ToList();
//                        foreach (var activity in result)
//                        {
//                            statisticsDetails.Activities.Add(activity);
//                        }
                        
//                        var qaAction = criteria.QAActions.OrderBy(x => x.OccurredAt)
//                            .LastOrDefault(x => x.Criteria.Id == criteria.Id && x.TestCaseId.Equals(testCase.TestCaseId));

//                        if (qaAction != null)
//                        {
//                            if (qaAction.QAActionType.ActionName.Equals(QAActionType.Certify))
//                            {
//                                if (qaAction.HasPassed == true)
//                                {
//                                    statisticsDetails.CountCurrentCertifyPassed += 1;
//                                }
//                                else
//                                {
//                                    statisticsDetails.CountCurrentCertifyFailed += 1;
//                                }
//                                statisticsDetails.CountCurrentVerifyPassed += 1;
//                                statisticsDetails.CountCurrentUnitTestPassed += 1;
//                            }
//                            else if (qaAction.QAActionType.ActionName.Equals(QAActionType.Verify))
//                            {
//                                if (qaAction.HasPassed == true)
//                                {
//                                    statisticsDetails.CountCurrentVerifyPassed += 1;
//                                }
//                                else
//                                {
//                                    statisticsDetails.CountCurrentVerifyFailed += 1;
//                                }
//                                statisticsDetails.CountCurrentUnitTestPassed += 1;
//                            }
//                            else if (qaAction.QAActionType.ActionName.Equals(QAActionType.UnitTest))
//                            {
//                                if (qaAction.HasPassed == true)
//                                {
//                                    statisticsDetails.CountCurrentUnitTestPassed += 1;
//                                }
//                                else
//                                {
//                                    statisticsDetails.CountCurrentUnitTestFailed += 1;
//                                }
//                            }
//                        }
//                    }
//                }
//            }
//            return statisticsDetails;
//        }
//    }
//}
