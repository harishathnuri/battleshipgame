using Battle.Domain;
using Battle.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace Battle.Infrastructure
{
    public class BoardRepository : IBoardRepository
    {
        private readonly BattleAppContext context;
        private readonly ILogger<BoardRepository> logger;
        public BoardRepository(
            BattleAppContext context, ILogger<BoardRepository> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public Board Create(Board board)
        {
            context.Boards.Add(board);
            context.SaveChanges();
            return board;
        }

        public Board Get(int id)
        {
            var board = context.Boards
                .Include(b => b.BattleShips)
                .ThenInclude(bs => bs.BattleShipBlocks)
                .Include(b => b.Blocks)
                .FirstOrDefault(b => b.Id == id);
            return board;
        }

        public List<Board> List()
        {
            var boards = context.Boards
                .Include(b => b.BattleShips)
                .ThenInclude(bs => bs.BattleShipBlocks)
                .Include(b => b.Blocks)
                .ToList();
            return boards;
        }
    }
}
