namespace Battle.Domain
{
    public class Block
    {
        public int Id { get; set; }
        public int Number { get; set; }

        public int BoardId { get; set; }
        public virtual Board Board { get; set; }
        public virtual BattleShipBlock BattleShipBlock { get; set; }
    }
}
