using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateInterface.Model.Interface
{
    public interface IStateInterfaceRepository
    {
        void SaveChanges();
        void AddRecordsCenter(RecordsCenter recordsCenter);
        void DeleteRecordsCenter(RecordsCenter recordsCenter);
        RecordsCenter GetRecordsCenter(int id);
        RecordsCenter GetRecordsCenter(string code);
        IQueryable<RecordsCenter> GetRecordsCenters();
        IQueryable<RecordsCenter> GetRecordsCentersWithCategories();
        IQueryable<Category> GetCategoriesForRecordsCenters(int recordsCenterId);
    }
}
