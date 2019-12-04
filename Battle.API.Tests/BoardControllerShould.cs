using Battle.API.Controllers;
using Battle.Domain;
using Battle.Repository.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net;

namespace Battle.API.Tests
{
    public class BoardControllerShould
    {
        private ILogger<BoardController> fakeLogger;

        [SetUp]
        public void Setup()
        {
            var moqLogger = new Mock<ILogger<BoardController>>();
            fakeLogger = moqLogger.Object;
        }

        [Test]
        public void ReturnCreatedAtResult()
        {
            //arrange
            var fakeBoard = Helper.FakeBoardFactory();
            var moqBoardRepo = new Mock<IBoardRepo>();
            moqBoardRepo.Setup(br => br.Get(It.IsAny<int>())).Returns(fakeBoard);
            moqBoardRepo.Setup(br => br.Create(It.IsAny<Board>())).Returns(fakeBoard);
            var fakeBoardRepo = moqBoardRepo.Object;

            var fakeBlocks = Helper.FakeBlocksFactory();
            var moqBlockRepo = new Mock<IBlockRepo>();
            moqBlockRepo.Setup(
                br => br.CreateBlocksForBoard(It.IsAny<List<Block>>()))
                .Returns(fakeBlocks);
            var fakeBlockRepo = moqBlockRepo.Object;

            var controller = new BoardController(fakeBoardRepo, fakeBlockRepo, fakeLogger);

            //act
            var response = controller.ApiBoardPost();

            //assert
            Assert.AreEqual((int)HttpStatusCode.Created, response.StatusCode);
        }
    }
}
