namespace Battle.Domain
{
    public class Attack
    {
        public int Id { get; set; }
        public int BlockId { get; set; }
        public virtual Block Block { get; set; }
    }
}
