using Battle.API.Controllers;
using Battle.API.ViewModel;
using Battle.Domain;
using Battle.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Net;

namespace Battle.API.Tests
{
    public class AttackControllerShould
    {
        private IBoardRepository fakeBoardRepo;
        private ILogger<AttackController> fakeLogger;

        [SetUp]
        public void Setup()
        {
            var fakeBoard = Helper.FakeBoardFactory();

            var moqBoardRepo = new Mock<IBoardRepository>();
            moqBoardRepo.Setup(br => br.Get(It.IsAny<int>())).Returns(fakeBoard);
            fakeBoardRepo = moqBoardRepo.Object;

            var moqLogger = new Mock<ILogger<AttackController>>();
            fakeLogger = moqLogger.Object;
        }

        [Test]
        public void ReturnCreatedAtResult()
        {
            //arrange
            var blockToAttack = new BlockToAttackRequest
            {
                Number = 1
            };
            var moqAttackRepo = new Mock<IAttackRepository>();
            moqAttackRepo.Setup(repo => repo.Create(It.IsAny<Attack>()));
            var fakeAttackRepo = moqAttackRepo.Object;

            var controller = new AttackController(
                fakeBoardRepo, fakeAttackRepo, fakeLogger);

            //act
            var response = controller.ApiAttackPost(Helper.FAKE_BOARD_ID, blockToAttack);

            //assert
            Assert.AreEqual((int)HttpStatusCode.Created, response.StatusCode);
        }

        [Test]
        public void ReturnBadRequestResult()
        {
            //arrange
            var blockToAttack = new BlockToAttackRequest
            {
                Number = 81
            };
            var moqAttackRepo = new Mock<IAttackRepository>();
            moqAttackRepo.Setup(repo => repo.Create(It.IsAny<Attack>()));
            var fakeAttackRepo = moqAttackRepo.Object;

            var controller = new AttackController(
                fakeBoardRepo, fakeAttackRepo, fakeLogger);

            //act
            var response = controller.ApiAttackPost(Helper.FAKE_BOARD_ID, blockToAttack);

            //assert
            Assert.AreEqual((int)HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        public void ReturnAttackResult()
        {
            //arrange
            var blockToAttack = new BlockToAttackRequest
            {
                Number = Helper.FAKE_BLOCK_NUMBER
            };
            var moqAttackRepo = new Mock<IAttackRepository>();
            moqAttackRepo
                .Setup(repo => repo.Get(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new Attack { 
                    Id = 1,
                    BlockId = Helper.FAKE_BLOCK_ID,
                    Block = new Block() 
                    { 
                        Id = Helper.FAKE_BLOCK_ID,
                        BoardId = Helper.FAKE_BOARD_ID,
                        Number = Helper.FAKE_BLOCK_NUMBER
                    }
                });
            var fakeAttackRepo = moqAttackRepo.Object;
            var controller = new AttackController(
                fakeBoardRepo, fakeAttackRepo, fakeLogger);

            //act
            var response = controller.ApiAttackGet(Helper.FAKE_BOARD_ID, Helper.FAKE_BLOCK_ID) as ObjectResult;

            //assert
            moqAttackRepo.Verify(repo => repo.Get(Helper.FAKE_BOARD_ID, Helper.FAKE_BLOCK_ID), Times.Once);
            Assert.AreEqual((int)HttpStatusCode.OK, response.StatusCode);
        }
    }
}
