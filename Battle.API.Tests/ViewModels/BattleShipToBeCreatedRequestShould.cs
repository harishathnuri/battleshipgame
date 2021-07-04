using Battle.API.ViewModel;
using NUnit.Framework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Battle.API.Tests.ViewModels
{
    public class BattleShipToBeCreatedRequestShould
    {
        [Test]
        public void ReturnAtleastOneBlockErrorForNullBlocks()
        {
            //arrange
            var request = new BattleShipToBeCreatedRequest()
            {
                BlockNumbers = null
            };

            //act
            var results = request.Validate(new ValidationContext(request)).ToList();

            //assert
            Assert.AreEqual(results.Count, 1);
            Assert.AreEqual(results[0].ErrorMessage, "Battle ship should be atleast of one block size");
        }

        [Test]
        public void ReturnAtleastOneBlockErrorForNoBlocks()
        {
            //arrange
            var request = new BattleShipToBeCreatedRequest()
            {
                BlockNumbers = new List<int>()
            };

            //act
            var results = request.Validate(new ValidationContext(request)).ToList();

            //assert
            Assert.AreEqual(results.Count, 1);
            Assert.AreEqual(results[0].ErrorMessage, "Battle ship should be atleast of one block size");
        }

        [Test]
        public void ReturnMaxBlocksErrorForBlocksExceeding10()
        {
            //arrange
            var request = new BattleShipToBeCreatedRequest()
            {
                BlockNumbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 }
            };

            //act
            var results = request.Validate(new ValidationContext(request)).ToList();

            //assert
            Assert.AreEqual(results.Count, 1);
            Assert.AreEqual(results[0].ErrorMessage, "Battle ship should be maximum ten block size");
        }
    }
}
