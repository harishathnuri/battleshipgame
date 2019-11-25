using Battle.Domain;

namespace Battle.Repository.Interfaces
{
    public interface IAttackRepo
    {
        Attack Get(int boardId, int id);
        Attack Create(Attack attack);
    }
}
