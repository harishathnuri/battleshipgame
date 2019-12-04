using Battle.API.Controllers;
using Battle.API.ViewModel;
using Battle.Domain;
using Battle.Repository.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Battle.API.Tests
{
    public class BattleShipControllerShould
    {
        private IBoardRepo fakeBoardRepo;
        private ILogger<BattleShipController> fakeLogger;

        [SetUp]
        public void Setup()
        {
            var fakeBoard = Helper.FakeBoardFactory();

            var moqBoardRepo = new Mock<IBoardRepo>();
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
            var fakeBattleShipRepo = new Mock<IBattleShipRepo>().Object;
            var fakeBlockRepo = new Mock<IBlockRepo>().Object;
            //sut
            var sut = new BattleShipController(
                fakeBoardRepo, fakeBattleShipRepo,
                fakeBlockRepo, fakeBattleShipService, fakeLogger);

            //act
            var response = sut.ApiBattleShipPost(Helper.FAKE_BOARD_ID, battleShipBlocks);

            //assert
            Assert.AreEqual((int)HttpStatusCode.Created, response.StatusCode);
        }
    }
}