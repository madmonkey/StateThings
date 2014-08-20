using StateInterface.Model.Interface;
using SunGardStateInterface.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SunGardStateInterface.API.Controllers
{
    public class RecordsCentersController : ApiController
    {
        IStateInterfaceTasks _stateInterfaceTasks;

        public RecordsCentersController(IStateInterfaceTasks stateInterfaceTasks)
        {
            _stateInterfaceTasks = stateInterfaceTasks;
        }
        // GET api/values
        public IEnumerable<RecordsCenterModel> Get()
        {
            var recordsCenters = _stateInterfaceTasks.GetRecordsCenters().OrderBy(x => x.Code);

            List<RecordsCenterModel> recordsCenterModels = new List<RecordsCenterModel>();

            foreach (var recordsCenter in recordsCenters)
            {
                recordsCenterModels.Add(new RecordsCenterModel(recordsCenter, false));
            }
            return recordsCenterModels;
        }

        // GET api/values/5
        public RecordsCenterModel Get(int id)
        {
            var recordsCenter = _stateInterfaceTasks.GetRecordsCenter(id);
            var recordsCenterModel = new RecordsCenterModel(recordsCenter, true);
            return recordsCenterModel;
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            var recordsCenter = _stateInterfaceTasks.GetRecordsCenter(id);

            _stateInterfaceTasks.DeleteRecordsCenter(recordsCenter);
        }
    }
}
