using Battle.Repository.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Battle.Repository.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddBattleAppService(this IServiceCollection services)
        {
            services.AddTransient<IAttackRepo, AttackRepo>();
            services.AddTransient<IBattleShipRepo, BattleShipRepo>();
            services.AddTransient<IBlockRepo, BlockRepo>();
            services.AddTransient<IBoardRepo, BoardRepo>();
        }
    }
}
