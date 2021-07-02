using Battle.Domain;
using Battle.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Battle.Application.Queries
{
    public class RetrieveAttackRequest : IRequest
    {
        public int BoardId { get; set; }
        public int AttackId { get; set; }
    }

    public class RetrieveAttackResponse : IResponse
    {
        public Attack Attack { get; set; }
    }


    public class RetrieveAttackQuery : IQuery<RetrieveAttackRequest, RetrieveAttackResponse>
    {
        private readonly IAttackRepository attackRepository;
        private readonly ILogger<RetrieveAttackQuery> logger;

        // add domain validation

        // add domain exceptions - e.g., NotFoundAttackId

        public RetrieveAttackQuery(
            IAttackRepository attackRepository,
            ILogger<RetrieveAttackQuery> logger)
        {
            this.attackRepository = attackRepository;
            this.logger = logger;
        }

        public RetrieveAttackResponse Execute(RetrieveAttackRequest request)
        {
            var attack = attackRepository.Get(request.BoardId, request.AttackId);
            var response = new RetrieveAttackResponse()
            {
                Attack = attack,
            };

            return response;
        }
    }
}
