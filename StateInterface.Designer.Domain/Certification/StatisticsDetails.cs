using System;
using System.Collections.Generic;
using System.Linq;
using StateInterface.Designer.Model;

namespace StateInterface.Designer.Model
{
    public class StatisticsDetails
    {
        public List<QAAction> Activities { get; set; }
        public double AverageCertified { get; set; }
        public double AverageVerified { get; set; }
        public double AverageUnitTested { get; set; }
        public DateTime VerificationCompletionDate { get; set; }
        public DateTime CertificationCompletionDate { get; set; }
        public int TotalTestCases { get; set; }
        public int CountCurrentUnitTestPassed { get; set; }
        public int CountCurrentUnitTestFailed { get; set; }
        public int CountCurrentVerifyPassed { get; set; }
        public int CountCurrentVerifyFailed { get; set; }
        public int CountCurrentCertifyPassed { get; set; }
        public int CountCurrentCertifyFailed { get; set; }
        public int CountUnUnitTestedTestCases { get; set; }
        public int CountUnVerifiedTestCases { get; set; }
        public int CountUnCertifiedTestCases { get; set; }
        public double PercentCertified { get; set; }
        public double PercentVerified { get; set; }
        public double PercentUnitTested { get; set; }
        public const MidpointRounding MathRounding = MidpointRounding.ToEven;
        public StatisticsDetails()
        {
            Activities = new List<QAAction>();
            AverageUnitTested = 0;
            AverageVerified = 0;
            AverageCertified = 0;
            CertificationCompletionDate = DateTime.MinValue;
            VerificationCompletionDate = DateTime.MinValue;
            TotalTestCases = 0;
            CountCurrentUnitTestFailed = 0;
            CountCurrentUnitTestPassed = 0;
            CountCurrentVerifyFailed = 0;
            CountCurrentVerifyPassed = 0;
            CountCurrentCertifyFailed = 0;
            CountCurrentVerifyPassed = 0;
            CountUnUnitTestedTestCases = 0;
            CountUnVerifiedTestCases = 0;
            CountUnCertifiedTestCases = 0;
            PercentUnitTested = 0;
            PercentVerified = 0;
            PercentCertified = 0;
        }
        public static double CalculateEstimatedAverage(string date, double total)
        {
            DateTime value;
            var average = total;
            if (DateTime.TryParse(date, out value) && value > DateTime.Now)
            {
                var days = aetBusinessDays(DateTime.Now, value);
                average = total / days;
            }
            return Math.Round(average, 2);
        }
        public static DateTime CalculateEstimatedDate(double average, double total)
        {
            if (average > 0)
            {
                var days = (int)Math.Ceiling(total/average);
                var completionDate = addBusinessDays(DateTime.Today.Date, days);
                return completionDate;
            }
            return DateTime.MinValue.Date;
        }
        public void CalculateData()
        {
            if (TotalTestCases > 0)
            {
                //TODO: Doesn't truncating it first defeat the purpose of rounding?
                PercentUnitTested = Math.Round(Math.Truncate(
                    ((double) CountCurrentUnitTestPassed/TotalTestCases)*100.0), 2, MathRounding);
                PercentVerified = Math.Round(Math.Truncate(
                    ((double) CountCurrentVerifyPassed/TotalTestCases)*100.0), 2, MathRounding);
                PercentCertified = Math.Round(Math.Truncate(
                    ((double) CountCurrentCertifyPassed/TotalTestCases)*100.0), 2, MathRounding);
            }
            CountUnUnitTestedTestCases = TotalTestCases - CountCurrentUnitTestPassed;
            CountUnVerifiedTestCases = TotalTestCases - CountCurrentVerifyPassed;
            CountUnCertifiedTestCases = TotalTestCases - CountCurrentCertifyPassed;
            AverageUnitTested = CalculateAverage(QAActionType.UnitTest);
            AverageVerified = CalculateAverage(QAActionType.Verify);
            AverageCertified = CalculateAverage(QAActionType.Certify);
            calculateCompletion();
        }
        public double CalculateAverage(string action)
        {
            var result = Activities.Where(x => x.QAActionType.ActionName.Equals(action))
                .GroupBy(x => x.OccurredAt.Date).ToList();
            return result.Any() ? Math.Round(result.Select(item => item.Count()).ToList().Average(), 2) : 0;
        }

        private void calculateCompletion()
        {
            if (AverageVerified > 0)
            {
                var daysToComplete = (int)Math.Ceiling(CountUnVerifiedTestCases / AverageVerified);
                VerificationCompletionDate = addBusinessDays(DateTime.Now.Date, daysToComplete);
            }
            if (AverageCertified > 0)
            {
                var daysToComplete = (int)Math.Ceiling(CountUnCertifiedTestCases / AverageCertified);
                CertificationCompletionDate = addBusinessDays(DateTime.Now.Date, daysToComplete);
            }
        }
        private static DateTime addBusinessDays(DateTime date, int days)
        {
            if (days <= 0)
            {
                return date;
            }
            if (date.DayOfWeek == DayOfWeek.Saturday)
            {
                date = date.AddDays(2);
                days -= 1;
            }
            else if (date.DayOfWeek == DayOfWeek.Sunday)
            {
                date = date.AddDays(1);
                days -= 1;
            }

            date = date.AddDays(days / 5 * 7);
            int extraDays = days % 5;

            if ((int)date.DayOfWeek + extraDays > 5)
            {
                extraDays += 2;
            }

            return date.AddDays(extraDays);
        }
        private static int aetBusinessDays(DateTime start, DateTime end)
        {
            if (start.DayOfWeek == DayOfWeek.Saturday)
            {
                start = start.AddDays(2);
            }
            else if (start.DayOfWeek == DayOfWeek.Sunday)
            {
                start = start.AddDays(1);
            }

            if (end.DayOfWeek == DayOfWeek.Saturday)
            {
                end = end.AddDays(-1);
            }
            else if (end.DayOfWeek == DayOfWeek.Sunday)
            {
                end = end.AddDays(-2);
            }

            var diff = (int)end.Subtract(start).TotalDays + 1;

            int result = diff / 7 * 5 + diff % 7;

            if (end.DayOfWeek < start.DayOfWeek)
            {
                return result - 2;
            }
            return result;
        }
    }
}
