using System.Collections.Generic;

namespace Battle.Domain.Interfaces
{
    public interface IBoardRepository
    {
        Board Get(int id);
        List<Board> List();
        Board Create(Board board);
    }
}
