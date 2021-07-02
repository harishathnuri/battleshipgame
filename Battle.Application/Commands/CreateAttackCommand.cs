using Battle.Domain;
using Battle.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace Battle.Application.Commands
{
    public class CreateAttackRequest : IRequest
    {
        public int BoardId { get; set; }
        public int BlockNumber { get; set; }
    }

    public class CreateAttackResponse : IResponse
    {
        public int BoardId { get; set; }
        public int AttackId { get; set; }
        public AttackResult AttackResult { get; set; }
    }

    public class CreateAttackCommand : ICommand<CreateAttackRequest, CreateAttackResponse>
    {
        private readonly IBoardRepository boardRepository;
        private readonly IAttackRepository attackRepository;
        private readonly ILogger<CreateAttackCommand> logger;

        // add domain validation

        // add domain exceptions - e.g., Block number should between 1 and 100

        public CreateAttackCommand(
            IBoardRepository boardRepository,
            IAttackRepository attackRepository,
            ILogger<CreateAttackCommand> logger)
        {
            this.boardRepository = boardRepository;
            this.attackRepository = attackRepository;
            this.logger = logger;
        }

        public CreateAttackResponse Execute(CreateAttackRequest request)
        {


            var block = new Block
            {
                BoardId = request.BoardId,
                Number = request.BlockNumber
            };

            // prep board to ask question
            var board = boardRepository.Get(request.BoardId);
            // ask board whether given block can be attacked
            var attackResult = board.CanAttackBlock(block);

            var response = new CreateAttackResponse
            {
                AttackResult = attackResult
            };

            // if attack is successful save the entity in database
            if (attackResult.Status == true)
            {
                var blockUnderAttack =
                    board.Blocks.First(b => b.Number == block.Number);
                var attack = new Attack
                {
                    BlockId = blockUnderAttack.Id
                };
                attackRepository.Create(attack);

                response.BoardId = block.BoardId;
                response.AttackId = attack.Id;
            }

            return response;
        }
    }
}
