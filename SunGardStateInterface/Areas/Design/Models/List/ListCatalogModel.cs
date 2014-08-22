using StateInterface.Areas.Certify.Models;
using StateInterface.Designer.Model;
using StateInterface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StateInterface.Areas.Design.Models
{
    public class ListCatalogModel
    {
        public RecordsCenterSelectorModel RecordsCenterSelector { get; set; }
        public List<CatalogItemModel> CatalogItems { get; set; }

        public string InitialData { get; set; }
        public ListsRequestModel ListsRequest { get; set; }
        public string GetListsUrl { get; set; }
        public string ListDetailsUrl { get; set; }
        public string DesignHomeUrl { get; set; }

        public ListCatalogModel()
        {
            CatalogItems = new List<CatalogItemModel>();
            ListsRequest = new ListsRequestModel();
        }
        public ListCatalogModel(User user, IEnumerable<RecordsCenter> recordsCenters)
            : this()
        {
            RecordsCenterSelector = new RecordsCenterSelectorModel(user, recordsCenters);
        }
    }
}