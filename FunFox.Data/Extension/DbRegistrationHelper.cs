using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunFox.Data.Extension
{
    public static class DbRegistrationHelper
    {
        public static IServiceCollection AddFunFoxDatabase(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<FunFoxDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            return services;
        }
    }
}
