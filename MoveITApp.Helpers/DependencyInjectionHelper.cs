using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MoveITApp.DataAccess;
using MoveITApp.DataAccess.Implementations;
using MoveITApp.DataAccess.Interfaces;
using MoveITApp.Domain.Models;
using MoveITApp.Services.Implementations;
using MoveITApp.Services.Interfaces;

namespace MoveITApp.Helpers
{
    public static class DependencyInjectionHelper
    {
        public static void InjectDbContext(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<MoveITDbContext>(x =>
            x.UseSqlServer(connectionString));
        }

        public static void InjectRepositories(IServiceCollection services)
        {
            services.AddTransient<IRepository<Proposal>, ProposalRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IDistanceRuleRepository, DistanceRuleRepository>();
            services.AddTransient<IMovingObjectRuleRepository, MovingObjectRuleRepository>();
        }

        public static void InjectServices(IServiceCollection services)
        {
            services.AddTransient<IProposalService, ProposalService>();
            services.AddTransient<IUserService, UserService>();
        }

    }
}
