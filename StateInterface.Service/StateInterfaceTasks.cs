using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StateInterface.Model.Interface;
using StateInterface.Model;

namespace StateInterface.Service
{
    public class StateInterfaceTasks : IStateInterfaceTasks
    {
        IStateInterfaceRepository _repository;

        public StateInterfaceTasks(IStateInterfaceRepository repository)
        {
            _repository = repository;
        }

        public void AddRecordsCenter(RecordsCenter recordsCenter)
        {
            _repository.AddRecordsCenter(recordsCenter);
            _repository.SaveChanges();
        }

        public IQueryable<RecordsCenter> GetRecordsCenters()
        {
            return _repository.GetRecordsCenters();
        }


        public RecordsCenter GetRecordsCenter(int id)
        {
            return _repository.GetRecordsCenter(id);
        }


        public RecordsCenter GetRecordsCenter(string code)
        {
            return _repository.GetRecordsCenter(code);
        }

        public void UpdateRecordsCenter(RecordsCenter recordsCenterUpdate)
        {
            var recordsCenter = _repository.GetRecordsCenter(recordsCenterUpdate.Id);
            recordsCenter.Code = recordsCenterUpdate.Code;
            recordsCenter.Name = recordsCenterUpdate.Name;
            recordsCenter.Description = recordsCenterUpdate.Description;
            _repository.SaveChanges();
        }

        public void DeleteRecordsCenter(RecordsCenter recordsCenterToDelete)
        {
            var recordsCenter = _repository.GetRecordsCenter(recordsCenterToDelete.Id);
            _repository.DeleteRecordsCenter(recordsCenter);
            _repository.SaveChanges();
        }


        public IQueryable<RecordsCenter> GetRecordsCentersWithCategories()
        {
            return _repository.GetRecordsCentersWithCategories();
        }
    }
}
