using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StateInterface.Model;
using StateInterface.Model.Interface;
using StateInterface.Repository;

namespace StateInterface.Test
{
    [TestClass]
    public class StateInterfaceTest
    {
        [TestMethod]
        public void AddRecordsCenterTest()
        {
            IStateInterfaceRepository stateInterfaceRepository = new StateInterfaceRepository();
            stateInterfaceRepository.AddRecordsCenter(new RecordsCenter() { Code = "Test", Name = "Test", Description = "Description" });
            stateInterfaceRepository.SaveChanges();
        }

        [TestMethod]
        public void GetRecordsCentersTest()
        {
            IStateInterfaceRepository stateInterfaceRepository = new StateInterfaceRepository();
            IEnumerable<RecordsCenter> result =  stateInterfaceRepository.GetRecordsCenters();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetCategoriesForRecordsCentersTest()
        {
            IStateInterfaceRepository stateInterfaceRepository = new StateInterfaceRepository();
            IEnumerable<Category> result = stateInterfaceRepository.GetCategoriesForRecordsCenters(1);
            Assert.IsNotNull(result);
        }
    }
}
