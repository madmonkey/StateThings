using System;
using System.Collections.Generic;
using System.Reflection;
using StateInterface.Designer.Model;
using StateInterface.Properties;
using System.Linq;

namespace StateInterface.Areas.Design.Models
{
    public class TransactionSnippetModel
    {
        private List<TransactionSnippetFieldModel> _transactionSnippetFieldModels;
        public string RecordsCenterName { get; set; }
        public int Id { get; set; }
        public string Created { get; set; }
        public string Updated { get; set; }
        public  string TokenName { get; set; }
        public  string TransactionDefinition { get; set; }
        public string Criteria { get; set; }
        public bool IncludePrefixAndSuffix { get; set; }
        public string Description { get; set; }
        public List<TransactionSnippetFieldModel> TransactionSnippetFields 
        {
            get { return _transactionSnippetFieldModels.OrderBy(x => x.TagName).ToList(); } 
            set {_transactionSnippetFieldModels = value;} 
        }
        public TransactionSnippetFieldModel SelectedField { get; set; }
        public SnippetRequestModel SnippetForEdit { get; set; }
        public IEnumerable<KeyValuePair<string,string>> AvailableTypes { get; set; }
        public IEnumerable<KeyValuePair<string, string>> AvailableOptions { get; set; }
        public IEnumerable<KeyValuePair<string, string>> AvailableNameFormats { get; set; }
        public IEnumerable<KeyValuePair<string, string>> AvailableDateFormats { get; set; }

        public string InitialData { get; set; }
        public string DesignHomeUrl { get; set; }
        public string TransactionsHomeUrl { get; set; }
        public string SnippetDetailsUrl { get; set; }
        public string UpdateSnippetUrl { get; set; }
        public string UpdateSnippetFieldUrl { get; set; }
        public string DeleteSnippetFieldUrl { get; set; }
        public bool CanDesignManage { get; set; }
        
        public TransactionSnippetModel(TransactionSnippet snippet, IEnumerable<KeyValuePair<string,string>> availableTypes)
        {
            RecordsCenterName = snippet.RecordsCenter.Name;
            Id = snippet.Id;
            Created = string.Format(Resources.DateTimeFormat, snippet.Created);
            if (snippet.Updated != null)
            {
                Updated = string.Format(Resources.DateTimeFormat, snippet.Updated);
            }
            TokenName = snippet.TokenName;
            TransactionDefinition = snippet.TransactionDefinition;
            Criteria = snippet.Criteria;
            IncludePrefixAndSuffix = snippet.IncludePrefixAndSuffix;
            Description = snippet.Description;
            TransactionSnippetFields = new List<TransactionSnippetFieldModel>();
            foreach (var field in snippet.TransactionSnippetFields.OrderBy(x => x.TagName))
            {
                _transactionSnippetFieldModels.Add(new TransactionSnippetFieldModel(field));
            }
            SelectedField = new TransactionSnippetFieldModel(new TransactionSnippetField());
            SnippetForEdit = new SnippetRequestModel();
            AvailableTypes = availableTypes;
            AvailableOptions = new List<KeyValuePair<string, string>>();
            AvailableNameFormats = new List<KeyValuePair<string, string>>() 
            { 
                new KeyValuePair<string,string>("Last, First Middle", "Last, First Middle"),
                new KeyValuePair<string,string>("Last,First Middle", "Last,First Middle"),
                new KeyValuePair<string,string>("First Middle Last", "First Middle Last"),
                new KeyValuePair<string,string>("Last, First Middle Suffix", "Last, First Middle Suffix"),
                new KeyValuePair<string,string>("First Middle Last Suffix", "First Middle Last Suffix"),
                new KeyValuePair<string,string>("Last,First", "Last,First"),
                new KeyValuePair<string,string>("First Last", "First Last"),
                new KeyValuePair<string,string>("Last", "Last"),
                new KeyValuePair<string,string>("Middle", "Middle"),
                new KeyValuePair<string,string>("First", "First")
            };
            AvailableDateFormats = new List<KeyValuePair<string, string>>() 
            { 
                new KeyValuePair<string,string>("MMDDYYYY", "MMDDYYYY"),
                new KeyValuePair<string,string>("MMDDYY", "MMDDYY"),
                new KeyValuePair<string,string>("YYYYMMDD", "YYYYMMDD"),
                new KeyValuePair<string,string>("YYMMDD", "YYMMDD"),
                new KeyValuePair<string,string>("MM-DD-YYYY", "MM-DD-YYYY"),
                new KeyValuePair<string,string>("MM-DD-YY", "MM-DD-YY"),
                new KeyValuePair<string,string>("YYYY-MM-DD", "YYYY-MM-DD"),
                new KeyValuePair<string,string>("YY-MM-DD", "YY-MM-DD")
            };
        }
    }
}