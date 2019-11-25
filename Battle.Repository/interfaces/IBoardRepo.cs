using Battle.Domain;
using System.Collections.Generic;

namespace Battle.Repository.Interfaces
{
    public interface IBoardRepo
    {
        Board Get(int id);
        List<Board> List();
        Board Create(Board board);
    }
}
