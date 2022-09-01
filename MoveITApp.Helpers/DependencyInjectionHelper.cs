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
    /// <summary>
    /// Class that contains methods for DI of the different dependencies in the application
    /// </summary>
    public static class DependencyInjectionHelper
    {
        /// <summary>
        /// Injects db context
        /// </summary>
        /// <param name="services">Collection of app services</param>
        /// <param name="connectionString">Database connection string</param>
        public static void InjectDbContext(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<MoveITDbContext>(x =>
            x.UseSqlServer(connectionString));
        }

        /// <summary>
        /// Injects repositories
        /// </summary>
        /// <param name="services">Collection of app services</param>
        public static void InjectRepositories(IServiceCollection services)
        {
            services.AddTransient<IRepository<Proposal>, ProposalRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IDistanceRuleRepository, DistanceRuleRepository>();
            services.AddTransient<IMovingObjectRuleRepository, MovingObjectRuleRepository>();
        }

        /// <summary>
        /// Injects services
        /// </summary>
        /// <param name="services">Collection of app services</param>
        public static void InjectServices(IServiceCollection services)
        {
            services.AddTransient<IProposalService, ProposalService>();
            services.AddTransient<IUserService, UserService>();
        }

    }
}
