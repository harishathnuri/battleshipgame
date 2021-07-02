using Battle.API.Controllers;
using Battle.API.ViewModel;
using Battle.Domain;
using Battle.Domain.Interfaces;
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
            ////arrange
            //var blockToAttack = new BlockToAttackRequest
            //{
            //    Number = 1
            //};
            //var moqAttackRepo = new Mock<IAttackRepository>();
            //moqAttackRepo.Setup(repo => repo.Create(It.IsAny<Attack>()));
            //var fakeAttackRepo = moqAttackRepo.Object;
            //var controller = new AttackController(
            //    fakeBoardRepo, fakeAttackRepo, fakeLogger);

            ////act
            //var response = controller.ApiAttackPost(Helper.FAKE_BOARD_ID, blockToAttack);

            ////assert
            //Assert.AreEqual((int)HttpStatusCode.Created, response.StatusCode);
        }
    }
}
