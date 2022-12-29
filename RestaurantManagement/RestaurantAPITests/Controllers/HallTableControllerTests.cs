using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestaurantAPI.Controllers;
using RestaurantBLL.Services;
using RestaurantDAL.Repost;
using RestaurantEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAPI.Controllers.Tests
{
    [TestClass()]
    public class HallTableControllerTests
    {
        HallTableController hallTableController;
        Fixture _fixture;
        Mock<IHallTableRepost> moq;
        public HallTableControllerTests()
        {
            _fixture = new Fixture();
            moq = new Mock<IHallTableRepost>();
        }
        
        [TestMethod()]
        public void GetHallTablesTest()
        {
            var hallTableList = _fixture.CreateMany<HallTable>(3).ToList();
            moq.Setup(obj => obj.GetHallTables()).Returns(hallTableList);
            hallTableController = new HallTableController(new HallTableService(moq.Object));
            var result = hallTableController.GetHallTables();

            Assert.AreEqual(result.Count(), 3);
        }
        [TestMethod()]
        public async Task GetHallTablesNegativeTest()
        {
            List<HallTable> hallTableList = null;
            moq.Setup(obj => obj.GetHallTables()).Returns(hallTableList);
            hallTableController = new HallTableController(new HallTableService(moq.Object));
            var result=hallTableController.GetHallTables();
            Assert.IsNull(result);            
        }
        /*[TestMethod()]
        public void GetHallTable_ThrowException()
        {
            moq.Setup(obj => obj.GetHallTables()).Throws(new Exception());
            hallTableController = new HallTableController(new HallTableService(moq.Object));
            var result = hallTableController.GetHallTables();

            Assert.AreEqual(400, null);
        }*/

        [TestMethod()]
        public async Task DeleteHallTableTest()
        {
            var hallTable = _fixture.Create<HallTable>();
            moq.Setup(obj => obj.DeleteHallTable(1));
            hallTableController = new HallTableController(new HallTableService(moq.Object));
            var result = hallTableController.DeleteHallTable(1);
            var Obj = result as ObjectResult;

            Assert.AreEqual(Obj.StatusCode, 200);
        }

        [TestMethod()]
        public async Task DeleteHallTableNegativeTest()
        {
            var hallTable=_fixture.Create<HallTable>();
            moq.Setup(obj => obj.DeleteHallTable(It.IsAny<int>())).Throws<Exception>();
            hallTableController = new HallTableController(new HallTableService(moq.Object));
            var result = hallTableController.DeleteHallTable(1);
            var Obj = result as ObjectResult;

            Assert.AreEqual(Obj.StatusCode, 400);
        }
        
        [TestMethod()]
        public void UpdateHallTableTest()
        {
            var hallTable = _fixture.Create<HallTable>();
            moq.Setup(obj => obj.DeleteHallTable(It.IsAny<int>()));

            hallTableController = new HallTableController(new HallTableService(moq.Object));
            var result = hallTableController.DeleteHallTable(1);
            var Obj = result as ObjectResult;

            Assert.AreEqual(Obj.StatusCode, 200);
        }

        [TestMethod]
        public async Task UpdateHallTableNegativeTest()
        {
            var hallTable = _fixture.Create<HallTable>();
            moq.Setup(obj => obj.UpdateHallTable(It.IsAny<HallTable>())).Throws<Exception>();

            hallTableController = new HallTableController(new HallTableService(moq.Object));
            var result = hallTableController.UpdateHallTable(hallTable);
            var Obj = result as ObjectResult;

            Assert.AreEqual(Obj.StatusCode, 400);
        }

        [TestMethod()]
        public void GetHallTableByIdTest()
        {
            var hallTable = _fixture.Create<HallTable>();
            moq.Setup(obj => obj.GetHallTableById(1)).Returns(hallTable);
            hallTableController = new HallTableController(new HallTableService(moq.Object));                        

            Assert.AreEqual(hallTableController.GetHallTableById(1), hallTable);
        }

        [TestMethod]
        public async Task GetHallTableById_NegativeTest()
        {
            var hallTable = _fixture.Create<HallTable>();
            moq.Setup(obj => obj.GetHallTableById(10)).Returns(hallTable);
            hallTableController = new HallTableController(new HallTableService(moq.Object));                        

            Assert.AreNotEqual(hallTableController.GetHallTableById(1), hallTable);
        }
        [TestMethod()]
        public void AddHallTableTest()
        {
            var hallTable = _fixture.Create<HallTable>();
            moq.Setup(obj => obj.AddHallTable(hallTable));
            hallTableController = new HallTableController(new HallTableService(moq.Object));
            var result = hallTableController.AddHallTable(hallTable);
            var obj = result as ObjectResult;

            Assert.AreEqual(obj.StatusCode,200);
        }

        [TestMethod()]
        public async Task AddHallTableNegativeTest()
        {
            var hallTable = _fixture.Create<HallTable>();
            moq.Setup(obj => obj.AddHallTable(It.IsAny<HallTable>())).Throws(new Exception());
            hallTableController = new HallTableController(new HallTableService(moq.Object));
            var result = hallTableController.AddHallTable(hallTable);
            var obj = result as ObjectResult;

            Assert.AreEqual(400, obj.StatusCode);
        }
    }
}