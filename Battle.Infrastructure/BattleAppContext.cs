using Battle.Domain;
using Microsoft.EntityFrameworkCore;

namespace Battle.Infrastructure
{
    public class BattleAppContext : DbContext
    {
        public BattleAppContext(DbContextOptions<BattleAppContext> options)
            : base(options)
        {
        }

        public DbSet<Board> Boards { get; set; }
        public DbSet<BattleShip> BattleShips { get; set; }
        public DbSet<Block> Blocks { get; set; }
        public DbSet<Attack> Attacks { get; set; }
        public DbSet<BattleShipBlock> BattleShipBlocks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BattleShipBlock>()
            .HasOne(bb => bb.Block)
            .WithOne(b => b.BattleShipBlock)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
