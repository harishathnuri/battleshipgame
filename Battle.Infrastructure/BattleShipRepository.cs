using Battle.Domain;
using Battle.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace Battle.Infrastructure
{
    public class BattleShipRepository : IBattleShipRepository
    {
        private readonly BattleAppContext context;
        private readonly ILogger<BattleShipRepository> logger;
        public BattleShipRepository(
            BattleAppContext context, ILogger<BattleShipRepository> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public BattleShip Add(BattleShip battleShip)
        {
            context.BattleShips.Add(battleShip);
            context.SaveChanges();
            return battleShip;
        }

        public BattleShip Get(int boardId, int id)
        {
            var battleShip = context.BattleShips
                .Include(b => b.BattleShipBlocks)
                .ThenInclude(bsb => bsb.Block)
                .Where(b => b.BoardId == boardId)
                .FirstOrDefault(b => b.Id == id);
            return battleShip;
        }

        public List<BattleShip> List(int boardId)
        {
            var battleShips = context.BattleShips
                .Include(b => b.BattleShipBlocks)
                .ThenInclude(bsb => bsb.Block)
                .Where(b => b.BoardId == boardId)
                .ToList();
            return battleShips;
        }
    }
}
