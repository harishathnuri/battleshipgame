namespace Battle.Domain
{
    public class BattleShipBlock
    {
        public int Id { get; set; }
        public int BattleShipId { get; set; }
        public int BlockId { get; set; }
        public virtual Block Block { get; set; }
    }
}
