using StateInterface.Designer.Model;
using StateInterface.Models;
using System.Collections.Generic;

namespace StateInterface.Areas.Design.Models
{
    public class SnippetModel
    {
        public RecordsCenterSelectorModel RecordsCenterSelector { get; set; }
        public List<TransactionSnippetModel> TransactionSnippets { get; set; }
        public SnippetsParameterModel SnippetsParameter { get; set; }
        public SnippetParameterModel SnippetParameter { get; set; }
        public string GetSnippetsUrl { get; set; }
        public string SnippetDetailsUrl { get; set; }
        public string CreateSnippetUrl { get; set; }
        public string DeleteSnippetUrl { get; set; }
        public bool CanDesignManage { get; set; }
        public string InitialData { get; set; }

        public SnippetModel()
        {
            TransactionSnippets = new List<TransactionSnippetModel>();
            SnippetsParameter = new SnippetsParameterModel();
            SnippetParameter = new SnippetParameterModel();
        }
        public SnippetModel(User user, IEnumerable<RecordsCenter> recordsCenters)
            : this()
        {
            RecordsCenterSelector = new RecordsCenterSelectorModel(user, recordsCenters);
        }
    }
}