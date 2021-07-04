using Battle.API.Controllers;
using Battle.API.ViewModel;
using Battle.Domain;
using Battle.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net;

namespace Battle.API.Tests.Controller
{
    public class BattleShipControllerShould
    {
        private IBoardRepository fakeBoardRepo;
        private ILogger<BattleShipController> fakeLogger;

        [SetUp]
        public void Setup()
        {
            var fakeBoard = Helper.FakeBoardFactory();

            var moqBoardRepo = new Mock<IBoardRepository>();
            moqBoardRepo.Setup(br => br.Get(It.IsAny<int>())).Returns(fakeBoard);
            fakeBoardRepo = moqBoardRepo.Object;

            var moqLogger = new Mock<ILogger<BattleShipController>>();
            fakeLogger = moqLogger.Object;
        }

        [Test]
        public void ReturnCreatedAtResult()
        {
            //arrange
            var blockNumbers = new List<int> { 91, 92, 93, 94, 95 };
            var battleShipBlocks = new BattleShipToBeCreatedRequest
            {
                BlockNumbers = blockNumbers
            };
            var expectedBattleShip = Helper.FakeBattleShipFactory(blockNumbers, 5);

            var moqBattleShipService = new Mock<IBattleShipService>();
            moqBattleShipService
                .Setup(br => br.SaveBattleShip(It.IsAny<int>(), It.IsAny<List<BattleShipBlock>>()))
                .Returns(expectedBattleShip);

            var fakeBattleShipService = moqBattleShipService.Object;
            var fakeBattleShipRepo = new Mock<IBattleShipRepository>().Object;
            var fakeBlockRepo = new Mock<IBlockRepository>().Object;
            //sut
            var sut = new BattleShipController(
                fakeBoardRepo, fakeBattleShipRepo,
                fakeBlockRepo, fakeBattleShipService, fakeLogger);

            //act
            var response = sut.ApiBattleShipPost(Helper.FAKE_BOARD_ID, battleShipBlocks);

            //assert
            Assert.AreEqual((int)HttpStatusCode.Created, response.StatusCode);
        }

        [Test]
        public void ReturnBadRequestResult()
        {
            //arrange
            var blockNumbers = new List<int> { 1 };
            var battleShipBlocks = new BattleShipToBeCreatedRequest
            {
                BlockNumbers = blockNumbers
            };

            var moqBattleShipService = new Mock<IBattleShipService>();
            var fakeBattleShipService = moqBattleShipService.Object;
            var fakeBattleShipRepo = new Mock<IBattleShipRepository>().Object;
            var fakeBlockRepo = new Mock<IBlockRepository>().Object;
            //sut
            var sut = new BattleShipController(
                fakeBoardRepo, fakeBattleShipRepo,
                fakeBlockRepo, fakeBattleShipService, fakeLogger);

            //act
            var response = sut.ApiBattleShipPost(Helper.FAKE_BOARD_ID, battleShipBlocks);

            //assert
            Assert.AreEqual((int)HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        public void ReturnBattleShipResult()
        {
            //arrange
            var blockNumbers = new List<int> { 91, 92, 93, 94, 95 };
            var fakeBattleShip = Helper.FakeBattleShipFactory(blockNumbers, 5);

            var moqBattleShipService = new Mock<IBattleShipService>();
            var fakeBattleShipService = moqBattleShipService.Object;
            var moqBattleShipRepo = new Mock<IBattleShipRepository>();
            moqBattleShipRepo.Setup(repo => repo.Get(It.IsAny<int>(), It.IsAny<int>())).Returns(fakeBattleShip);
            var fakeBattleShipRepo = moqBattleShipRepo.Object;
            var fakeBlockRepo = new Mock<IBlockRepository>().Object;
            //sut
            var sut = new BattleShipController(
                fakeBoardRepo, fakeBattleShipRepo,
                fakeBlockRepo, fakeBattleShipService, fakeLogger);

            //act
            var response = sut.ApiBattleShipGet(Helper.FAKE_BOARD_ID, 1) as ObjectResult;

            //assert
            Assert.AreEqual((int)HttpStatusCode.OK, response.StatusCode);
        }
    }
}