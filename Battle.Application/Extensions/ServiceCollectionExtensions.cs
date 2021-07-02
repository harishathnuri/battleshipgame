using Battle.Application.Commands;
using Battle.Application.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Battle.Application
{
    public static class ServiceCollectionExtensions
    {
        public static void AddBattleApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<ICommand<CreateAttackRequest, CreateAttackResponse>, CreateAttackCommand>();
            services.AddTransient<IQuery<RetrieveAttackRequest, RetrieveAttackResponse>, RetrieveAttackQuery>();
        }
    }
}
