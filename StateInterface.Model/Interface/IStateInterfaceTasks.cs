using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateInterface.Model.Interface
{
    public interface IStateInterfaceTasks
    {
        void AddRecordsCenter(RecordsCenter recordsCenter);
        void DeleteRecordsCenter(RecordsCenter recordsCenter);
        IQueryable<RecordsCenter> GetRecordsCenters();
        IQueryable<RecordsCenter> GetRecordsCentersWithCategories();
        RecordsCenter GetRecordsCenter(int id);
        RecordsCenter GetRecordsCenter(string code);
        void UpdateRecordsCenter(RecordsCenter recordsCenter);
    }
}
