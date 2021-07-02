using Battle.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Battle.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddBattleInfrastructureService(this IServiceCollection services)
        {
            services.AddTransient<IAttackRepository, AttackRepository>();
            services.AddTransient<IBattleShipRepository, BattleShipRepository>();
            services.AddTransient<IBlockRepository, BlockRepository>();
            services.AddTransient<IBoardRepository, BoardRepository>();
            services.AddTransient<IBattleShipService, BattleShipService>();
        }
    }
}
