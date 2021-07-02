namespace Battle.Domain.Interfaces
{
    public interface IAttackRepository
    {
        Attack Get(int boardId, int id);
        Attack Create(Attack attack);
    }
}
