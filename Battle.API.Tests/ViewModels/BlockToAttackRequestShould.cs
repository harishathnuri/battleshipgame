using Battle.API.ViewModel;
using NUnit.Framework;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Battle.API.Tests.ViewModels
{
    public class BlockToAttackRequestShould
    {
        [Test]
        public void ReturnInvalidBlockErrorForNumberLessThanZero()
        {
            //arrange
            var request = new BlockToAttackRequest()
            {
                Number = -1
            };

            //act
            var results = request.Validate(new ValidationContext(request)).ToList();

            //assert
            Assert.AreEqual(results.Count, 1);
            Assert.AreEqual(results[0].ErrorMessage, "Block number should be between 1 and 100");
        }

        [Test]
        public void ReturnInvalidBlockErrorForNumberGreaterThanHundred()
        {
            //arrange
            var request = new BlockToAttackRequest()
            {
                Number = -1
            };

            //act
            var results = request.Validate(new ValidationContext(request)).ToList();

            //assert
            Assert.AreEqual(results.Count, 1);
            Assert.AreEqual(results[0].ErrorMessage, "Block number should be between 1 and 100");
        }
    }
}

