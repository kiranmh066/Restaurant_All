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
    public class FoodControllerTests
    {
        FoodController foodController;

        Fixture _fixture;

        Mock<IFoodRepost> moq;

        public FoodControllerTests()
        {
            _fixture = new Fixture();
            moq = new Mock<IFoodRepost>();
        }


        [TestInitialize]
        public void setup()
        {
            var moq = new Mock<FoodService>();
        }
        [TestMethod()]
        public async Task GetFoodsTest()
        {
            var foodlist = _fixture.CreateMany<Food>(3).ToList();

            moq.Setup(x => x.GetFoods()).Returns(foodlist);
            foodController = new FoodController(new FoodService(moq.Object));

            var result = foodController.GetFoods();

            Assert.AreEqual(result.Count(), 3);
        }

        [TestMethod()]
        public async Task GetFoodsNegativeTest()
        {
            var foodlist = _fixture.CreateMany<Food>().ToList();

            moq.Setup(x => x.GetFoods()).Returns(foodlist);
            foodController = new FoodController(new FoodService(moq.Object));
            foodController.GetFoods();
        }
        [TestMethod()]
        public void DeleteFoodTest()
        {
            var food = _fixture.Create<Food>();

            moq.Setup(x => x.DeleteFood(1));

            foodController = new FoodController(new FoodService(moq.Object));
            var result = foodController.DeleteFood(1);
            var Obj = result as ObjectResult;

            Assert.AreEqual(Obj.StatusCode, 200);
        }
        [TestMethod()]
        public void DeleteFood_ThrowsException_IfIdNotFound()
        {
            var food = _fixture.Create<Food>();

            moq.Setup(x => x.DeleteFood(It.IsAny<int>())).
                 Throws(new Exception());
            foodController = new FoodController(new FoodService(moq.Object));
            var result = foodController.DeleteFood(1);
            var Obj = result as ObjectResult;

            Assert.AreEqual(Obj.StatusCode, 400);
        }
        [TestMethod()]
        public void UpdateFoodTest()
        {
            var food = _fixture.Create<Food>();

            moq.Setup(x => x.UpdateFood(food));
            foodController = new FoodController(new FoodService(moq.Object));
            var result = foodController.UpdateFood(food);
            var Obj = result as ObjectResult;

            Assert.AreEqual(200, Obj.StatusCode);
        }
        [TestMethod()]
        public void UpdateFood_ThrowsException_IfIdNotFound()
        {
            var food = _fixture.Create<Food>();

            moq.Setup(x => x.UpdateFood(It.IsAny<Food>())).
                 Throws(new Exception());
            foodController = new FoodController(new FoodService(moq.Object));
            var result = foodController.UpdateFood(food);
            var Obj = result as ObjectResult;

            Assert.AreEqual(Obj.StatusCode, 400);
        }
        [TestMethod()]
        public void GetFoodByIdTest()
        {
            var food = _fixture.Create<Food>();


            moq.Setup(x => x.GetFoodById(1)).Returns(food);
            foodController = new FoodController(new FoodService(moq.Object));

            Assert.AreEqual(foodController.GetFoodById(1), food);
        }
        [TestMethod()]
        public void GetFoodById_ExistingIdPassed_ReturnsRightItem()
        {
            // Arrange
            var food = _fixture.Create<Food>();


            moq.Setup(x => x.GetFoodById(10)).Returns(food);
            foodController = new FoodController(new FoodService(moq.Object));
            // Act
            var okResult = foodController.GetFoodById(1);
            // Assert
            Assert.AreEqual(okResult, null);
        }
        [TestMethod()]
        public async Task AddFoodTest()
        {
            var food = _fixture.Create<Food>();


            moq.Setup(x => x.AddFood(food));

            foodController = new FoodController(new FoodService(moq.Object));

            var result = foodController.AddFood(food);
            var Obj = result as ObjectResult;

            Assert.AreEqual(200, Obj.StatusCode);
        }
        [TestMethod()]
        public async Task AddFoodNegativeTest()
        {
            var food = _fixture.Create<Food>();

            moq.Setup(x => x.AddFood(It.IsAny<Food>())).
                 Throws(new Exception());
            foodController = new FoodController(new FoodService(moq.Object));
            var result = foodController.AddFood(food);
            var Obj = result as ObjectResult;

            Assert.AreEqual(Obj.StatusCode, 400);
        }
        
        
       


    }
}