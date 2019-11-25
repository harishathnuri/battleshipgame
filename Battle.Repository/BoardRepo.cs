using Battle.Domain;
using Battle.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace Battle.Repository
{
    public class BoardRepo : IBoardRepo
    {
        private readonly BattleAppContext context;
        private readonly ILogger<BoardRepo> logger;
        public BoardRepo(
            BattleAppContext context, ILogger<BoardRepo> logger)
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
