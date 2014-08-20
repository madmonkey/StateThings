using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using StateInterface.Model;
using StateInterface.Model.Interface;

namespace StateInterface.Repository
{
    public class StateInterfaceRepository : IStateInterfaceRepository
    {
        private StateInterfaceContext _dbContext;
        public StateInterfaceRepository()
        {
            // Set to turn off auto creation of DB
            //Database.SetInitializer<StateInterfaceContext>(null);

            _dbContext = new StateInterfaceContext();
            _dbContext.Database.Initialize(true);
        }
        public IQueryable<RecordsCenter> GetRecordsCenters()
        {
            IQueryable<RecordsCenter> result = null;

            result = _dbContext.RecordsCenters;

            return result;
        }
        public RecordsCenter GetRecordsCenter(int id)
        {
            return _dbContext.RecordsCenters
                .Include(b => b.Categories)
                .Where(x => x.Id == id).FirstOrDefault();
        }
        public RecordsCenter GetRecordsCenter(string code)
        {
            return _dbContext.RecordsCenters
                .Include(b => b.Categories)
                .Where(x => x.Code == code).FirstOrDefault();
        }
        public IQueryable<Category> GetCategoriesForRecordsCenters(int recordsCenterId)
        {
            IQueryable<Category> result = null;

            result = _dbContext.Categories.Where(x => x.Id == 1);

            return result;
        }
        public void AddRecordsCenter(RecordsCenter recordsCenter)
        {
            _dbContext.RecordsCenters.Add(recordsCenter);
        }
        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
        public void DeleteRecordsCenter(RecordsCenter recordsCenter)
        {
            _dbContext.RecordsCenters.Remove(recordsCenter);
        }
        public IQueryable<RecordsCenter> GetRecordsCentersWithCategories()
        {
            return _dbContext.RecordsCenters
                            .Include(b => b.Categories);
        }
    }
}
