using StateInterface.Designer.Model;
using StateInterface.Models;
using System.Collections.Generic;

namespace StateInterface.Areas.Design.Models
{
    public class TransactionSnippetCatalogModel
    {
        public RecordsCenterSelectorModel RecordsCenterSelector { get; set; }
        public List<CatalogItemModel> CatalogItems { get; set; }

        public SnippetsRequestModel SnippetsRequest { get; set; }
        public SnippetRequestModel SnippetRequest { get; set; }
        public string GetSnippetsUrl { get; set; }
        public string SnippetDetailsUrl { get; set; }
        public string CreateSnippetUrl { get; set; }
        public string DeleteSnippetUrl { get; set; }
        public bool CanDesignManage { get; set; }
        public string InitialData { get; set; }
        public string DesignHomeUrl { get; set; }

        public TransactionSnippetCatalogModel()
        {
            CatalogItems = new List<CatalogItemModel>();
            SnippetsRequest = new SnippetsRequestModel();
            SnippetRequest = new SnippetRequestModel();
        }
        public TransactionSnippetCatalogModel(User user, IEnumerable<RecordsCenter> recordsCenters)
            : this()
        {
            RecordsCenterSelector = new RecordsCenterSelectorModel(user, recordsCenters);
        }
    }
}