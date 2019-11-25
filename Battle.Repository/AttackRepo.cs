using Battle.Domain;
using Battle.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace Battle.Repository
{
    public class AttackRepo : IAttackRepo
    {
        private readonly BattleAppContext context;
        private readonly ILogger<AttackRepo> logger;
        public AttackRepo(BattleAppContext context, ILogger<AttackRepo> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public Attack Create(Attack attack)
        {
            context.Attacks.Add(attack);
            context.SaveChanges();
            return attack;
        }

        public Attack Get(int boardId, int id)
        {
            var attack = context.Attacks
                .Include(a => a.Block)
                .Join(context.Blocks, a => a.BlockId, b => b.Id, (a, b) => new { Attack = a, Block = b })
                .Where(ab => ab.Block.BoardId == boardId)
                .FirstOrDefault(ab => ab.Attack.Id == id)
                ?.Attack;
            return attack;
        }
    }
}
